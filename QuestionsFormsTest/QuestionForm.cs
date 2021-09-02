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
        private QuestionsController QuestionsController;
        private string CurrentQuestionType;
        private DataRow CurrentQuestion;
        private int CurrentQuestionId;
        private bool IsUpdated;
        private List<Control> QuestionsInputFieldList;
        private Dictionary<string, string> QuestionTypesDictionary;
        private string[] QuestionTypes;

        public QuestionForm(QuestionsController pQuestionsController, string[] pQuestionTypes)
        {
            try
            {
                InitializeComponent();
                QuestionsController = pQuestionsController;
                CurrentQuestionId = -1;
                CurrentQuestionType = "SmileyQuestions";
                QuestionTypes = pQuestionTypes;
                QuestionTypesDictionary = new Dictionary<string, string>();
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }

        public QuestionForm(QuestionsController pQuestionsController, string pQuestionType, int pQuestionId, string[] pQuestionTypes)
        {
            try
            {
                InitializeComponent();
                QuestionsController = pQuestionsController;
                CurrentQuestionId = pQuestionId;
                CurrentQuestionType = pQuestionType;
                QuestionTypes = pQuestionTypes;
                QuestionTypesDictionary = new Dictionary<string, string>();
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }

        private void QuestionForm_Load(object sender, EventArgs e)
        {
            int tResponseCode = 0;

            try
            {
                tResponseCode = QuestionsController.GetQuestionRow(CurrentQuestionType, CurrentQuestionId, ref CurrentQuestion);

                if (tResponseCode == 0)
                {
                    FillQuestionTypes();
                    BindComboBox();
                    questionTypeCombo.SelectedValue = CurrentQuestionType;
                    UpdateQuestionsInputFieldList();
                    IsUpdated = false;

                    if (CurrentQuestionId == -1)
                    {
                        controlBtn.Text = "Add";
                        headerLbl.Text = "New Question";
                        ShowExtraQuestionFields();
                    }
                    else
                    {
                        questionTypeCombo.Enabled = false;
                        controlBtn.Text = "Update";
                        headerLbl.Text = "Update Question";
                        UpdateQuestionFields();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);

                string tMessage = ResultCodes.GetCodeMessage(tResponseCode);
                string tMessageCaption = "Error";
                MessageBoxButtons tMessageButtons = MessageBoxButtons.OK;
                MessageBoxIcon tIcon = MessageBoxIcon.Error;

                MessageBox.Show(tMessage, tMessageCaption, tMessageButtons, tIcon);
            }
        }

        /// <summary>
        /// Fills the question types from the string array to the Dictionary and maps them.
        /// </summary>
        private void FillQuestionTypes()
        {
            try
            {
                foreach (string tQuestionType in QuestionTypes)
                {
                    QuestionTypesDictionary.Add(tQuestionType + "Questions", tQuestionType);
                }
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }

        /// <summary>
        /// Updates the question input controls and inserts them into the QuestionsInputFieldList
        /// </summary>
        private void UpdateQuestionsInputFieldList()
        {
            try
            {
                QuestionsInputFieldList = new List<Control>();

                Control tQuestionDataContainer = Controls["containerQuestion"];
                Control tQuestionExtraDataContainer = tQuestionDataContainer.Controls["container" + CurrentQuestionType];

                foreach (Control tQuestionInputField in tQuestionDataContainer.Controls)
                {
                    if (tQuestionInputField.Name.Contains("input"))
                    {
                        QuestionsInputFieldList.Add(tQuestionInputField);
                    }
                }

                foreach (Control tQuestionExtraInputField in tQuestionExtraDataContainer.Controls)
                {
                    if (tQuestionExtraInputField.Name.Contains("input"))
                    {
                        QuestionsInputFieldList.Add(tQuestionExtraInputField);
                    }
                }
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }

        /// <summary>
        /// Binds the ComboBox to the dictionary data
        /// </summary>
        private void BindComboBox()
        {
            try
            {
                questionTypeCombo.DataSource = new BindingSource(QuestionTypesDictionary, null);
                questionTypeCombo.DisplayMember = "Value";
                questionTypeCombo.ValueMember = "Key";
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }

        /// <summary>
        /// Updates the form fields with data from the current question data
        /// </summary>
        private void UpdateQuestionFields()
        {
            try
            {
                ShowExtraQuestionFields();

                foreach (Control tQuestionInputField in QuestionsInputFieldList)
                {
                    string tCurrentFieldName = tQuestionInputField.Name.Split('_')[1];
                    tQuestionInputField.Text = CurrentQuestion[tCurrentFieldName].ToString();
                }
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }

        /// <summary>
        /// Picks and shows the current question extra data
        /// </summary>
        private void ShowExtraQuestionFields()
        {
            try
            {
                Control QuestionDataContainer = Controls["containerQuestion"];

                foreach (var tQuestionExtraDataContainer in QuestionTypesDictionary)
                {
                    QuestionDataContainer.Controls["container" + tQuestionExtraDataContainer.Key].Visible = false;
                }

                QuestionDataContainer.Controls["container" + CurrentQuestionType].Visible = true;
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }

        /// <summary>
        /// Checks if any of the question input fields are empty, if so show a user an error message with what is empty
        /// </summary>
        /// <returns></returns>
        private bool ValidateFields()
        {
            ArrayList tControlNames = new ArrayList();

            try
            {
                foreach (Control tQuestionInputField in QuestionsInputFieldList)
                {
                    if (string.IsNullOrEmpty(tQuestionInputField.Text))
                        tControlNames.Add(tQuestionInputField.Tag);
                }

                if (tControlNames.Count != 0)
                {
                    StringBuilder tMessageString = new StringBuilder();
                    string tMessageCaption = "Error";
                    MessageBoxButtons tMessageButtons = MessageBoxButtons.OK;
                    MessageBoxIcon tIcon = MessageBoxIcon.Error;

                    tMessageString.AppendLine("There are empty values.. please fill them.\n");
                    tMessageString.AppendLine("Empty values:-\n");

                    foreach (string tControlName in tControlNames)
                    {
                        tMessageString.AppendLine("- " + tControlName);
                    }

                    MessageBox.Show(tMessageString.ToString(), tMessageCaption, tMessageButtons, tIcon);
                }
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }

            return tControlNames.Count == 0;
        }

        /// <summary>
        /// Fills the current question datarow with the data from the question input fields
        /// </summary>
        private void FillQuestionRow()
        {
            try
            {
                foreach (Control tQuestionInputField in QuestionsInputFieldList)
                {
                    string curFieldName = tQuestionInputField.Name.Split('_')[1];
                    CurrentQuestion[curFieldName] = tQuestionInputField.Text;
                }
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }

        /// <summary>
        /// Compares the current question row data with the input fields, to see if the question should be updated or they are the same
        /// </summary>
        /// <returns>If the question datarow has the same values in the input fields or not</returns>
        private bool CheckQuestionFields()
        {
            bool tIsUpdatable = false;

            try
            {
                foreach (Control tQuestionInputField in QuestionsInputFieldList)
                {
                    string tCurrentFieldName = tQuestionInputField.Name.Split('_')[1];

                    if (!tQuestionInputField.Text.Equals(CurrentQuestion[tCurrentFieldName].ToString()))
                    {
                        tIsUpdatable = true;
                        break;
                    }
                }

                if (!tIsUpdatable)
                {
                    string message = "Update failed.. nothing to update!";
                    string messageCaption = "Error";
                    MessageBoxButtons messageButtons = MessageBoxButtons.OK;
                    MessageBoxIcon icon = MessageBoxIcon.Error;

                    MessageBox.Show(message, messageCaption, messageButtons, icon);
                }
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }

            return tIsUpdatable;
        }

        /// <summary>
        /// Event listener that fires whenever the questionTypeCombo changes It's values then updates the form to that corresponding question type
        /// </summary>
        /// <param name="sender">The control that fired the event</param>
        /// <param name="e">Extra data about the event</param>
        private void questionTypeCombo_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                CurrentQuestionType = questionTypeCombo.SelectedValue.ToString();
                QuestionsController.GetQuestionRow(CurrentQuestionType, CurrentQuestionId, ref CurrentQuestion);
                UpdateQuestionsInputFieldList();
                ShowExtraQuestionFields();
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }

        /// <summary>
        /// Fires whenever the Add/Update button gets clicked, then calls the corresponding function to Add/Update the question
        /// </summary>
        /// <param name="sender">The control that fired the event</param>
        /// <param name="e">Extra data about the event</param>
        private void controlBtn_Click(object sender, EventArgs e)
        {
            try
            {
                bool tIsNew = CurrentQuestionId == -1;

                if (ValidateFields() && (tIsNew || CheckQuestionFields()))
                {
                    FillQuestionRow();
                    int tResponseCode = tIsNew ? QuestionsController.AddQuestion(CurrentQuestion) : QuestionsController.EditQuestion(CurrentQuestion);
                    this.IsUpdated = tResponseCode == 0;

                    string tMessage = ResultCodes.GetCodeMessage(tResponseCode);
                    string tMessageCaption = "";
                    MessageBoxButtons tMessageButtons = MessageBoxButtons.OK;
                    MessageBoxIcon tIcon;

                    if (tResponseCode == 0)
                    {
                        tIcon = MessageBoxIcon.Information;
                        tMessageCaption = "Success";
                    }
                    else
                    {
                        tIcon = MessageBoxIcon.Error;
                        tMessageCaption = "Error";
                    }

                    MessageBox.Show(tMessage, tMessageCaption, tMessageButtons, tIcon);

                    if (tIsNew)
                    {
                        QuestionsController.GetQuestionRow(CurrentQuestionType, -1, ref CurrentQuestion);
                    }
                }
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }


        /// <summary>
        /// Fires whenever the user hits the exit button, then shows a confirmation message of closing the form
        /// </summary>
        /// <param name="sender">The control that fired the event</param>
        /// <param name="e">Extra data about the event</param>
        private void exitButton_Click(object sender, EventArgs e)
        {
            try
            {
                string tMessage = "Are you sure you want to exit?" + (IsUpdated ? "" : " There are unsaved changes.");
                string tMessageCaption = "Exit";
                MessageBoxButtons tMessageButtons = MessageBoxButtons.YesNo;
                MessageBoxIcon tIcon = MessageBoxIcon.Warning;
                DialogResult tResult;

                tResult = MessageBox.Show(tMessage, tMessageCaption, tMessageButtons, tIcon);

                if (tResult == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Close();
                }
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }

        /// <summary>
        /// Validator function that makes sure that the ending field is always less than the starting field
        /// </summary>
        /// <param name="sender">The control that fired the event</param>
        /// <param name="e">Extra data about the event</param>
        private void input_EndStartValues_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                input_EndValue.Value = Math.Max(input_EndValue.Value, Math.Min(input_StartValue.Value + 1, 100));
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }
    }
}
