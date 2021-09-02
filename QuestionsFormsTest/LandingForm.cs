using System;
using System.Windows.Forms;

namespace QuestionsFormsTest
{
    public partial class LandingForm : Form
    {
        private QuestionsController QuestionsControllerObject;

        private string[] QuestionTypes { get; set; }
        public LandingForm()
        {
            try
            {
                InitializeComponent();
                QuestionTypes = new string[] { "Smiley", "Star", "Slider" };
                QuestionsControllerObject = new QuestionsController(QuestionTypes);
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }

        private void LandingFrom_Load(object sender, EventArgs e)
        {
            int tResponseCode = 0;

            try
            {
                tResponseCode = QuestionsControllerObject.FillQuestionsDataSet();

                if (tResponseCode == 0)
                {
                    allQuestionsGrid.DataSource = QuestionsControllerObject.QuestionsDataSet.Tables["AllQuestions"];
                    allQuestionsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    allQuestionsGrid.Columns["Text"].Width = 250;
                    allQuestionsGrid.Columns["QOrder"].Width = 30;
                    allQuestionsGrid.Columns["OriginalId"].Visible = false;
                    allQuestionsGrid.Columns["Id"].Visible = false;
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
                string tCaption = "Error";
                MessageBoxButtons tMessageButtons = MessageBoxButtons.OK;
                MessageBoxIcon tIcon = MessageBoxIcon.Error;

                MessageBox.Show(tMessage, tCaption, tMessageButtons, tIcon);

                DisableForm();
            }
        }

        /// <summary>
        /// Helper function that disables all controls in the form
        /// </summary>
        private void DisableForm()
        {
            try
            {
                foreach (Control tFormControl in Controls)
                {
                    tFormControl.Enabled = false;
                }
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }

        private void allQuestionsGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ToggleButtons(true);
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }

        /// <summary>
        /// Helper function that disables/enabels the edit/add buttons
        /// </summary>
        /// <param name="pValue">bool value to enable/disable the form add/edit buttons</param>
        private void ToggleButtons(bool pValue)
        {
            try
            {
                editBtn.Enabled = pValue;
                removeBtn.Enabled = pValue;
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }

        /// <summary>
        /// On click function that launches the question form to add a new question
        /// </summary>
        /// <param name="sender">The control that fired the event</param>
        /// <param name="e">Extra data about the event</param>
        private void addBtn_Click(object sender, EventArgs e)
        {
            try
            {
                QuestionForm tQuestionForm = new QuestionForm(QuestionsControllerObject, QuestionTypes);
                tQuestionForm.ShowDialog();
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }

        /// <summary>
        /// On click function that launches the question form to edit the selected question
        /// </summary>
        /// <param name="sender">The control that fired the event</param>
        /// <param name="e">Extra data about the event</param>
        private void editBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string tCurrentQuestionType = allQuestionsGrid.CurrentRow.Cells["Type"].Value.ToString() + "Questions";
                int tCurrentQuestionOriginalId = (int)allQuestionsGrid.CurrentRow.Cells["OriginalId"].Value;
                QuestionForm tQuestionForm = new QuestionForm(QuestionsControllerObject, tCurrentQuestionType, tCurrentQuestionOriginalId, QuestionTypes);
                tQuestionForm.ShowDialog();
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }

        /// <summary>
        /// On click function that removes the currently selected question after getting confirmation from the user
        /// </summary>
        /// <param name="sender">The control that fired the event</param>
        /// <param name="e">Extra data about the event</param>
        private void removeBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string tMessage = "Are you sure you want to delete this question? deleted questions are lost forever...";
                string tCaption = "Delete question";
                MessageBoxButtons tMessageButtons = MessageBoxButtons.YesNo;
                MessageBoxIcon tIcon = MessageBoxIcon.Warning;
                DialogResult tResult;

                tResult = MessageBox.Show(tMessage, tCaption, tMessageButtons, tIcon);
                if (tResult == System.Windows.Forms.DialogResult.Yes)
                {
                    int tQuestionIndex = Convert.ToInt32(allQuestionsGrid.CurrentRow.Cells["Id"].Value);
                    int tResponseCode = QuestionsControllerObject.RemoveQuestion(tQuestionIndex);

                    tMessage = ResultCodes.GetCodeMessage(tResponseCode);

                    if (tResponseCode == 0)
                    {
                        tIcon = MessageBoxIcon.Asterisk;
                        CheckList();
                    }
                    else
                    {
                        tIcon = MessageBoxIcon.Error;
                    }

                    tMessageButtons = MessageBoxButtons.OK;
                    MessageBox.Show(tMessage, tCaption, tMessageButtons, tIcon);
                }
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }

        /// <summary>
        /// Helper function that checks whether the questiosn data grid is empty or not, if it is disable the edit/delete buttons
        /// </summary>
        private void CheckList()
        {
            try
            {
                if (allQuestionsGrid.RowCount == 0)
                {
                    ToggleButtons(false);
                }
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }
    }
}
