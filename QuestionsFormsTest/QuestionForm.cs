using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private void FillDictionaryItems()
        {
            comboBoxDic.Add("SmileyQuestions", "Smiley Question");
            comboBoxDic.Add("SliderQuestions", "Slider Question");
            comboBoxDic.Add("StarQuestions", "Star Question");
        }

        private void QuestionForm_Load(object sender, EventArgs e)
        {
            BindComboBox();
            questionTypeCombo.SelectedValue = curType;

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

        private void BindComboBox()
        {
            questionTypeCombo.DataSource = new BindingSource(comboBoxDic, null);
            questionTypeCombo.DisplayMember = "Value";
            questionTypeCombo.ValueMember = "Key";
        }

        private void UpdateQuestionFields()
        {
            ShowExtraQuestionFields();

            questionText.Text = curQuestion["Text"].ToString();
            questionOrder.Value = (int) curQuestion["QOrder"];

            foreach (Control questionControl in Controls["container" + curType].Controls)
            {
                if (questionControl.Name.Contains("input"))
                {
                    string curFieldName = questionControl.Name.Split('_')[1];
                    questionControl.Text = curQuestion[curFieldName].ToString();
                }
            }
        }

        private void ShowExtraQuestionFields()
        {
            foreach (var value in comboBoxDic)
            {
                Controls["container" + value.Key].Visible = false;
            }

            Controls["container" + curType].Visible = true;
        }

        private void controlBtn_Click(object sender, EventArgs e)
        {
            bool didUpdate = isNew ? AddQuestion() : UpdateQuestion();

            if (didUpdate)
            {
                if (isNew)
                {
                    curQuestion = qc.GetDataRowObject(curType);
                }
            }
        }

        private bool AddQuestion()
        {
            FillQuestionRow();

            return qc.AddQuestion(curQuestion, curType);
        }

        private void FillQuestionRow()
        {
            curQuestion["Text"] = questionText.Text;
            curQuestion["QOrder"] = questionOrder.Value;

            foreach (Control questionControl in Controls["container" + curType].Controls)
            {
                if (questionControl.Name.Contains("input"))
                {
                    string curFieldName = questionControl.Name.Split('_')[1];
                    curQuestion[curFieldName] = questionControl.Text;
                }
            }
        }

        private bool UpdateQuestion()
        {
            bool shouldUpdate = CheckQuestionFields();

            if (shouldUpdate)
            {
                FillQuestionRow();
            }

            return shouldUpdate ? qc.EditQuestion(curQuestion, index, originalId, curType) : false;
        }

        private bool CheckQuestionFields()
        {
            bool isUpdatable = false;

            if (!questionText.Text.Equals(curQuestion["Text"].ToString()) || (int) curQuestion["QOrder"] != questionOrder.Value)
            {
                isUpdatable = true;
            }

            foreach (Control questionControl in Controls["container" + curType].Controls)
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void questionTypeCombo_DropDownClosed(object sender, EventArgs e)
        {
            curType = questionTypeCombo.SelectedValue.ToString();
            isNew = true;
            curQuestion = qc.GetDataRowObject(curType);
            ShowExtraQuestionFields();
        }
    }
}
