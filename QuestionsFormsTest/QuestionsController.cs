using System;
using System.Data;

namespace QuestionsFormsTest
{
    public class QuestionsController
    {
        private DataSet questionsDataSet;
        private DatabaseController dbController = new DatabaseController();

        /// <summary>
        /// Formats and returns the dataset on which to be used in the application
        /// </summary>
        /// <returns>A formatted dataset to be used in the application</returns>
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

        /// <summary>
        /// Constructs the allQuestiosn table
        /// </summary>
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

        /// <summary>
        /// Fills the allTable table with data
        /// </summary>
        /// <param name="allTable">The reference to the allTable table</param>
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

        /// <summary>
        /// Constructs a formattedQuestion to be inserted into the curTable table
        /// </summary>
        /// <param name="curTable">The current table</param>
        /// <param name="dataRow">The datarow of which to take data from</param>
        /// <param name="type">The question type</param>
        /// <returns>A formatted DataRow</returns>
        private DataRow CreateNewFormattedQuestion(DataTable curTable, DataRow dataRow, string type)
        {
            DataRow formattedQuestionDR = curTable.NewRow();
            UpdateRowValues(formattedQuestionDR, dataRow, type);

            return formattedQuestionDR;
        }

        /// <summary>
        /// Updates a allTable row with data from an old DataRow
        /// </summary>
        /// <param name="newDataRow">The new datarow to be updated</param>
        /// <param name="oldDataRow">The old datarow from which to take data from</param>
        /// <param name="type">The old datarow table type</param>
        private void UpdateRowValues(DataRow newDataRow, DataRow oldDataRow, string type = "")
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

        /// <summary>
        /// Removes a question from the database and the dataset
        /// </summary>
        /// <param name="index">The index of the question in the allTable table</param>
        /// <param name="type">The question type</param>
        /// <param name="originalId">The originalId of the question in it's table</param>
        /// <returns>Wether or not the question got removed or not</returns>
        public bool RemoveQuestion(int index, string type, int originalId)
        {
            bool sqlExecuted = dbController.DeleteQuestion(type, originalId);

            if (sqlExecuted)
            {
                try
                {
                    questionsDataSet.Tables["allTable"].Rows[index].Delete();
                    questionsDataSet.Tables[type].Rows.Find(originalId).Delete();
                }
                catch (Exception e)
                {
                    Logger.WriteExceptionMessage(e);
                }
                finally
                {
                    questionsDataSet.AcceptChanges();
                }
            }

            return sqlExecuted;
        }

        /// <summary>
        /// Updates a specific question with new data
        /// </summary>
        /// <param name="updatedQuestion">The updated Datarow</param>
        /// <param name="index">The index of the question in the allTable table</param>
        /// <param name="originalId">The original Id of the question in it's table</param>
        /// <param name="type">The question type</param>
        /// <returns>Wether the question got updated or not</returns>
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

        /// <summary>
        /// Adds a new question to the database and to the dataset
        /// </summary>
        /// <param name="newQuestion">The new datarow to be inserted</param>
        /// <param name="type">The question type</param>
        /// <returns>Wether or not the question got added</returns>
        public bool AddQuestion(DataRow newQuestion, string type)
        {
            int newQuestionId = dbController.AddNewQuestion(newQuestion, type);

            if (newQuestionId != -1)
            {
                try
                {
                    newQuestion["Id"] = newQuestionId;
                    questionsDataSet.Tables[type].Rows.Add(newQuestion);
                    DataTable allTable = questionsDataSet.Tables["allTable"];
                    DataRow allTableNewRow = CreateNewFormattedQuestion(allTable, newQuestion, type);
                    allTable.Rows.Add(allTableNewRow);
                }
                catch (Exception e)
                {
                    Logger.WriteExceptionMessage(e);
                }
                finally
                {
                    questionsDataSet.AcceptChanges();
                }
            }

            return newQuestionId != -1;
        }
        
        /// <summary>
        /// Helper function to return a specific row based on the id
        /// </summary>
        /// <param name="id">The id of the row to find</param>
        /// <param name="tableName">The table name of the question</param>
        /// <returns>A datarow object</returns>
        public DataRow GetQuestion(int id, string tableName)
        {
            return questionsDataSet.Tables[tableName].Rows.Find(id);
        }

        /// <summary>
        /// Returns an empty datarow object
        /// </summary>
        /// <param name="tableName">The table name of the new question</param>
        /// <returns>an empty DataRow</returns>
        public DataRow GetDataRowObject(string tableName)
        {
            return questionsDataSet.Tables[tableName].NewRow();
        }
    }
}
