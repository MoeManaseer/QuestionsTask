using System;
using System.Windows.Forms;

namespace QuestionsFormsTest
{
    public partial class Form1 : Form
    {
        QuestionsController qc;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            qc = new QuestionsController();
            allQuestionsGrid.DataSource = qc.getData().Tables["allTable"];
            allQuestionsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            allQuestionsGrid.Columns["Text"].Width = 350;
            allQuestionsGrid.Columns["Question type"].Width = 100;
            allQuestionsGrid.Columns["OriginalId"].Visible = false;
            allQuestionsGrid.Columns["Index"].Visible = false;
            allQuestionsGrid.Columns["QuestionTable"].Visible = false;
            initControlsRecursive(this.Controls);
        }

        private void allQuestionsGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            ToggleButtons(true);
        }

        private void ToggleButtons(bool value)
        {
            editBtn.Enabled = value;
            removeBtn.Enabled = value;
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            allQuestionsGrid.DataSource = qc.getData().Tables["allTable"];
            outputLbl.Text = "Table updated";
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            QuestionForm qf = new QuestionForm(qc);
            LaunchQuestionsForm(qf);
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            string curType = allQuestionsGrid.CurrentRow.Cells["QuestionTable"].Value.ToString();
            int originalIndex = (int)allQuestionsGrid.CurrentRow.Cells["OriginalId"].Value;
            int curIndex = allQuestionsGrid.CurrentRow.Index;
            QuestionForm qf = new QuestionForm(qc, false, curType, originalIndex, curIndex);
            LaunchQuestionsForm(qf);
        }

        private void LaunchQuestionsForm(QuestionForm qf)
        {
            qf.ShowDialog();
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            string deleteMessage = "Are you sure you want to delete this question? deleted questions are lost forever...";
            string deleteCaption = "Delete question";
            MessageBoxButtons messageButtons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Warning;
            DialogResult result;

            result = MessageBox.Show(deleteMessage, deleteCaption, messageButtons, icon);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                int curIndex = allQuestionsGrid.CurrentRow.Index;
                int originalIndex = (int)allQuestionsGrid.CurrentRow.Cells["OriginalId"].Value;
                string curType = allQuestionsGrid.CurrentRow.Cells["QuestionTable"].Value.ToString();

                bool didRemove = qc.RemoveQuestion(curIndex, curType, originalIndex);

                if (didRemove)
                {
                    deleteMessage = "The question was removed successfuly.";
                    icon = MessageBoxIcon.Asterisk;
                    CheckList();
                }
                else
                {
                    deleteMessage = "An error happend while deleting the question, please refresh and try again.";
                    icon = MessageBoxIcon.Error;
                }

                messageButtons = MessageBoxButtons.OK;
                MessageBox.Show(deleteMessage, deleteCaption, messageButtons, icon);
            }
        }

        private void CheckList()
        {
            if (allQuestionsGrid.RowCount == 0)
            {
                ToggleButtons(false);
            }
        }

        /// <summary>
        /// Helper function that adds an on click event listener to all controls except the refreshBtn
        /// </summary>
        /// <param name="coll">Controls to loop thro</param>
        private void initControlsRecursive(Control.ControlCollection coll)
        {
            foreach (Control c in coll)
            {
                if (!c.Name.Equals("refreshBtn"))
                {
                    c.MouseClick += (sender, e) => {
                        outputLbl.Text = "";
                    };
                }

                initControlsRecursive(c.Controls);
            }
        }
    }
}
