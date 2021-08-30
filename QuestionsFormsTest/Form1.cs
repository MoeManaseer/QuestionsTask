using System;
using System.Drawing;
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
            allQuestionsGrid.Columns["Text"].Width = 403;
            allQuestionsGrid.Columns["Order"].Width = 114;
            allQuestionsGrid.Columns["QuestionType"].Width = 114;
            allQuestionsGrid.Columns["OriginalId"].Visible = false;
            allQuestionsGrid.Columns["Index"].Visible = false;
            allQuestionsGrid.Columns["QuestionTable"].Visible = false;
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
            int curIndex = allQuestionsGrid.CurrentRow.Index;
            int originalIndex = (int)allQuestionsGrid.CurrentRow.Cells["OriginalId"].Value;
            string curType = allQuestionsGrid.CurrentRow.Cells["QuestionTable"].Value.ToString();

            bool didRemove = qc.RemoveQuestion(curIndex, curType, originalIndex);
            if (didRemove)
            {
                outputLbl.Text = "Removed row successfuly";
                outputLbl.ForeColor = Color.Green;
            }
            else
            {
                outputLbl.Text = "An error happened while removing the row, please refresh and try again.";
                outputLbl.ForeColor = Color.Red;
            }
        }
    }
}
