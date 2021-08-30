using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace QuestionsFormsTest
{
    public partial class QuestionForm : Form
    {
        private QuestionsController qc;
        private string curType;
        private DataRow curQuestion;
        private bool isNew = true;
        private int originalId;
        private int index;
        private bool isUpdated = false;
        private Control questionsController;
        Dictionary<string, string> comboBoxDic = new Dictionary<string, string>();

        public QuestionForm(QuestionsController qc, bool isNew = true, string questionType = "SmileyQuestions", int originalId = 0, int index = 0)
        {
            InitializeComponent();
            this.qc = qc;
            this.isNew = isNew;
            this.originalId = originalId;
            this.index = index;
            curType = questionType;
            FillDictionaryItems();
        }

        private void QuestionForm_Load(object sender, EventArgs e)
        {
            BindComboBox();
            questionTypeCombo.SelectedValue = curType;
            questionsController = Controls["containerQuestion"];

            if (isNew)
            {
                curQuestion = qc.GetDataRowObject(curType);
                controlBtn.Text = "Add";
                headerLbl.Text = "New Question";
                ShowExtraQuestionFields();
            }
            else
            {
                questionTypeCombo.Enabled = false;
                curQuestion = qc.GetQuestion(originalId, curType);
                controlBtn.Text = "Update";
                headerLbl.Text = "Update Question";
                UpdateQuestionFields();
            }
        }

        /// <summary>
        /// Fills the dicitonary with the table names
        /// </summary>
        private void FillDictionaryItems()
        {
            comboBoxDic.Add("SmileyQuestions", "Smiley Question");
            comboBoxDic.Add("SliderQuestions", "Slider Question");
            comboBoxDic.Add("StarQuestions", "Star Question");
        }

        /// <summary>
        /// Binds the ComboBox to the dictionary data
        /// </summary>
        private void BindComboBox()
        {
            questionTypeCombo.DataSource = new BindingSource(comboBoxDic, null);
            questionTypeCombo.DisplayMember = "Value";
            questionTypeCombo.ValueMember = "Key";
        }

        /// <summary>
        /// Adds a new question
        /// </summary>
        /// <returns>wehether or not the question got added</returns>
        private bool AddQuestion()
        {
            FillQuestionRow();
            bool didAdd = qc.AddQuestion(curQuestion, curType);

            if (didAdd)
            {
                curQuestion = qc.GetDataRowObject(curType);
            }

            return didAdd;
        }

        /// <summary>
        /// Updates a question
        /// </summary>
        /// <returns>Wehether or not the question got updated</returns>
        private bool UpdateQuestion()
        {
            bool shouldUpdate = CheckQuestionFields();

            if (shouldUpdate)
            {
                FillQuestionRow();
            }
            else
            {
                string message = "There is nothing to update.";
                string messageCaption = "Info";
                MessageBoxButtons messageButtons = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Information;

                MessageBox.Show(message, messageCaption, messageButtons, icon);
            }

            return shouldUpdate ? qc.EditQuestion(curQuestion, index, originalId, curType) : false;
        }

        /// <summary>
        /// Updates the form fields with data from the dataRow
        /// </summary>
        private void UpdateQuestionFields()
        {
            ShowExtraQuestionFields();

            foreach (Control questionControl in questionsController.Controls)
            {
                if (questionControl.Name.Contains("input"))
                {
                    string curFieldName = questionControl.Name.Split('_')[1];
                    questionControl.Text = curQuestion[curFieldName].ToString();
                }
            }

            foreach (Control questionControl in questionsController.Controls["container" + curType].Controls)
            {
                if (questionControl.Name.Contains("input"))
                {
                    string curFieldName = questionControl.Name.Split('_')[1];
                    questionControl.Text = curQuestion[curFieldName].ToString();
                    curQuestion[curFieldName] = questionControl.Text;
                }
            }
        }

        /// <summary>
        /// Picks and shows the current question extra data
        /// </summary>
        private void ShowExtraQuestionFields()
        {
            foreach (var value in comboBoxDic)
            {
                questionsController.Controls["container" + value.Key].Visible = false;
            }

            questionsController.Controls["container" + curType].Visible = true;
        }

        /// <summary>
        /// Checks the form fields for empty values, if they exist show the user
        /// </summary>
        /// <returns></returns>
        private bool ValidateFields()
        {
            bool fieldsValid = true;
            ArrayList controlNames = new ArrayList();

            foreach (Control questionControl in questionsController.Controls)
            {
                if (questionControl.Name.Contains("input") && string.IsNullOrEmpty(questionControl.Text))
                {
                    fieldsValid = false;
                    controlNames.Add(questionControl.Tag);
                }
            }

            foreach (Control questionControl in questionsController.Controls["container" + curType].Controls)
            {
                if (questionControl.Name.Contains("input") && string.IsNullOrEmpty(questionControl.Text))
                {
                    fieldsValid = false;
                    controlNames.Add(questionControl.Tag);
                }
            }

            if (!fieldsValid)
            {
                StringBuilder message = new StringBuilder();
                string messageCaption = "Error";
                MessageBoxButtons messageButtons = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Error;

                message.AppendLine("There are empty values.. please fill them.\n");
                message.AppendLine("Empty values:-\n");

                foreach (string controlName in controlNames)
                {
                    message.AppendLine("- " + controlName);
                }

                MessageBox.Show(message.ToString(), messageCaption, messageButtons, icon);
            }

            return fieldsValid;
        }

        /// <summary>
        /// Updates the datarow object with the current data in the form
        /// </summary>
        private void FillQuestionRow()
        {
            foreach (Control questionControl in questionsController.Controls)
            {
                if (questionControl.Name.Contains("input"))
                {
                    string curFieldName = questionControl.Name.Split('_')[1];
                    curQuestion[curFieldName] = questionControl.Text;
                }
            }

            foreach(Control questionControl in questionsController.Controls["container" + curType].Controls)
            {
                if (questionControl.Name.Contains("input"))
                {
                    string curFieldName = questionControl.Name.Split('_')[1];
                    curQuestion[curFieldName] = questionControl.Text;
                }
            }
        }

        /// <summary>
        /// Checks wehether the data from the dataRow is different than the data in the form inputs
        /// </summary>
        /// <returns>wehether or not the data is the same</returns>
        private bool CheckQuestionFields()
        {
            bool isUpdatable = false;

            foreach (Control questionControl in questionsController.Controls)
            {
                if (questionControl.Name.Contains("input"))
                {
                    string curFieldName = questionControl.Name.Split('_')[1];

                    if (!questionControl.Text.Equals(curQuestion[curFieldName].ToString()))
                        isUpdatable = true;
                }
            }

            foreach (Control questionControl in questionsController.Controls["container" + curType].Controls)
            {
                if (questionControl.Name.Contains("input"))
                {
                    string curFieldName = questionControl.Name.Split('_')[1];

                    if (!questionControl.Text.Equals(curQuestion[curFieldName].ToString()))
                        isUpdatable = true;
                }
            }

            return isUpdatable;
        }

        private void questionTypeCombo_DropDownClosed(object sender, EventArgs e)
        {
            curType = questionTypeCombo.SelectedValue.ToString();
            isNew = true;
            curQuestion = qc.GetDataRowObject(curType);
            ShowExtraQuestionFields();
        }

        private void controlBtn_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                bool didUpdate = isNew ? AddQuestion() : UpdateQuestion();
                this.isUpdated = true;
                StringBuilder message = new StringBuilder();
                string messageCaption = "";
                MessageBoxButtons messageButtons = MessageBoxButtons.OK;
                MessageBoxIcon icon;
                
                if (didUpdate)
                {
                    icon = MessageBoxIcon.Information;
                    messageCaption = "Success";
                    message.AppendLine("The question was " + (isNew ? "added" : "updated") + " successfully.");
                }
                else
                {
                    icon = MessageBoxIcon.Error;
                    messageCaption = "Error";
                    message.AppendLine("Error " + (isNew ? "adding" : "updating") + " question.");
                    message.AppendLine("Please retry again...");
                }

                MessageBox.Show(message.ToString(), messageCaption, messageButtons, icon);
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            string message = "Are you sure you want to exit?" + (isUpdated ? "" : " There are unsaved changes.");
            string messageCaption = "Exit";
            MessageBoxButtons messageButtons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Warning;
            DialogResult result;

            result = MessageBox.Show(message, messageCaption, messageButtons, icon);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void input_EndStartValues_ValueChanged(object sender, EventArgs e)
        {
            input_EndValue.Value = Math.Max(input_EndValue.Value, input_StartValue.Value + 1);

            if (input_StartValue.Value == 100)
            {
                input_EndValue.Value = 100;
            }
        }
    }
}
