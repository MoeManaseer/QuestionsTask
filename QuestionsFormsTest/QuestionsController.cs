using System.Data;

namespace QuestionsFormsTest
{
    class QuestionsController
    {
        private DataSet questionsDataSet = new DataSet();
        private DatabaseController dbController = new DatabaseController();

        public DataSet getData()
        {
            questionsDataSet = dbController.GetAllQuestions();
            foreach (DataTable dt in questionsDataSet.Tables)
            {
                dt.PrimaryKey = new DataColumn[] { dt.Columns["Id"] };
            }
            ConstructAllQuestionsTable();

            return questionsDataSet;
        }

        private void ConstructAllQuestionsTable()
        {
            DataTable allTable = new DataTable();
            allTable.TableName = "allTable";

            DataColumn text = new DataColumn("Text");
            DataColumn originalId = new DataColumn("OriginalId");
            originalId.DataType = typeof(int);
            DataColumn questionType = new DataColumn("QuestionType");
            DataColumn qOrder = new DataColumn("Order");

            allTable.Columns.Add(text);
            allTable.Columns.Add(originalId);
            allTable.Columns.Add(questionType);
            allTable.Columns.Add(qOrder);

            FillAllQuestionsTable(allTable);
            questionsDataSet.Tables.Add(allTable);
        }

        private void FillAllQuestionsTable(DataTable allTable)
        {
            foreach (DataTable dt in questionsDataSet.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr.RowState != DataRowState.Deleted)
                    {
                        DataRow formattedQuestionDR = allTable.NewRow();
                        formattedQuestionDR["Text"] = dr["Text"];
                        formattedQuestionDR["OriginalId"] = dr["Id"];
                        formattedQuestionDR["QuestionType"] = dt.TableName;
                        formattedQuestionDR["Order"] = dr["QOrder"];
                        allTable.Rows.Add(formattedQuestionDR);
                    }
                }
            }

            allTable.AcceptChanges();
        }

        public bool RemoveQuestion(int index, string type, int originalId)
        {
            bool sqlExecuted = dbController.DeleteQuestion(type, originalId);

            if (sqlExecuted)
            {
                questionsDataSet.Tables["allTable"].Rows[index].Delete();
                questionsDataSet.Tables[type].Rows.Find(originalId).Delete();
                questionsDataSet.AcceptChanges();
            }

            return sqlExecuted;
        }

        public bool EditQuestion()
        {

            return true;
        }

        public bool AddQuestion()
        {

            return true;
        }
        
        public bool UpdateQuestion()
        {

            return true;
        }

        public DataRow GetQuestion(int id, string tableName)
        {
            return questionsDataSet.Tables[tableName].Rows.Find(id);
        }
    }
}
