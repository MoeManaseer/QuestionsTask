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
            allQuestionsGrid.Columns["Text"].Width = 500;
            allQuestionsGrid.Columns["Order"].Width = 114;
            allQuestionsGrid.Columns["OriginalId"].Visible = false;
            allQuestionsGrid.Columns["QuestionType"].Visible = false;
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

        }

        private void editBtn_Click(object sender, EventArgs e)
        {

        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            bool didRemove = qc.RemoveQuestion(allQuestionsGrid.CurrentRow.Index, allQuestionsGrid.CurrentRow.Cells["QuestionType"].Value.ToString(), (int) allQuestionsGrid.CurrentRow.Cells["OriginalId"].Value);
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
