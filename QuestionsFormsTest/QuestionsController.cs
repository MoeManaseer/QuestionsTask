using System.Data;

namespace QuestionsFormsTest
{
    public class QuestionsController
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
            originalId.DataType = System.Type.GetType("System.Int32");
            DataColumn questionType = new DataColumn("Question type");
            DataColumn questionTable = new DataColumn("QuestionTable");

            DataColumn qOrder = new DataColumn("Order");
            qOrder.DataType = System.Type.GetType("System.Int32");
            DataColumn index = new DataColumn("Index");
            index.DataType = System.Type.GetType("System.Int32");
            index.AutoIncrement = true;
            index.AutoIncrementStep = 1;

            allTable.Columns.Add(text);
            allTable.Columns.Add(originalId);
            allTable.Columns.Add(questionType);
            allTable.Columns.Add(questionTable);
            allTable.Columns.Add(qOrder);
            allTable.Columns.Add(index);
            allTable.PrimaryKey = new DataColumn[] { allTable.Columns["Index"] };

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
                        DataRow formattedQuestionDR = CreateNewFormattedQuestion(allTable, dr, dt.TableName);
                        allTable.Rows.Add(formattedQuestionDR);
                    }
                }
            }

            allTable.AcceptChanges();
        }

        private DataRow CreateNewFormattedQuestion(DataTable curTable, DataRow dataRow, string type)
        {
            DataRow formattedQuestionDR = curTable.NewRow();
            UpdateRowValues(formattedQuestionDR, dataRow, type);

            return formattedQuestionDR;
        }

        private void UpdateRowValues(DataRow newDataRow, DataRow oldDataRow, string type = "")
        {
            try
            {
                newDataRow["Text"] = oldDataRow["Text"];
                newDataRow["OriginalId"] = oldDataRow["Id"];
                newDataRow["Order"] = oldDataRow["QOrder"];

                if (!string.IsNullOrEmpty(type))
                {
                    newDataRow["QuestionTable"] = type;
                    newDataRow["Question type"] = type.Replace("Questions", "");
                }
            }
            catch
            {

            }
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

        public bool EditQuestion(DataRow updatedQuestion, int index, int originalId, string type)
        {
            bool didUpdateQuestion = dbController.EditQuestion(updatedQuestion, type, originalId);

            if (didUpdateQuestion)
            {
                DataTable allTable = questionsDataSet.Tables["allTable"];
                DataRow oldAllTableRow = allTable.Rows.Find(index);
                UpdateRowValues(oldAllTableRow, updatedQuestion);

                questionsDataSet.AcceptChanges();
            }

            return didUpdateQuestion;
        }

        public bool AddQuestion(DataRow newQuestion, string type)
        {
            int newQuestionId = dbController.AddNewQuestion(newQuestion, type);

            if (newQuestionId != -1)
            {
                newQuestion["Id"] = newQuestionId;
                questionsDataSet.Tables[type].Rows.Add(newQuestion);
                DataTable allTable = questionsDataSet.Tables["allTable"];
                DataRow allTableNewRow = CreateNewFormattedQuestion(allTable, newQuestion, type);
                allTable.Rows.Add(allTableNewRow);
                questionsDataSet.AcceptChanges();
            }

            return newQuestionId != -1;
        }
        
        public DataRow GetQuestion(int id, string tableName)
        {
            return questionsDataSet.Tables[tableName].Rows.Find(id);
        }

        public DataRow GetDataRowObject(string tableName)
        {
            return questionsDataSet.Tables[tableName].NewRow();
        }
    }
}
