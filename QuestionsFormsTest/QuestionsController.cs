using LoggerUtils;
using QuestionDatabase;
using ResultCodes;
using System;
using System.Data;

namespace QuestionsFormsTest
{
    public class QuestionsController
    {
        public DatabaseController DatabaseController { get; private set; }
        public DataSet QuestionsDataSet { get; private set; }
        public string[] TableNames { get; private set; }

        public QuestionsController(string[] pQuestionTypes)
        {
            try
            {
                DatabaseController = new DatabaseController();
                // Get all the question types
                TableNames = new string[pQuestionTypes.Length + 1];
                Array.Copy(pQuestionTypes, TableNames, pQuestionTypes.Length);
                // Append the All type to the array so that we can get the AllQuestions table
                TableNames[pQuestionTypes.Length] = "All";
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }
        }

        /// <summary>
        /// Maps the tables with their correct table name and sets the Id as a primary key for each of them
        /// </summary>
        /// <returns>a result code to be used to determine if success or failure</returns>
        private int MapTables()
        {
            int tResponseCode = (int) ResultCodesEnum.SUCCESS;

            try
            {
                for (int i = 0; i < QuestionsDataSet.Tables.Count; i++)
                {
                    DataTable tCurrentTable = QuestionsDataSet.Tables[i];
                    tCurrentTable.TableName = TableNames[i] + "Questions";
                    DataColumn tIdColumn = tCurrentTable.Columns["Id"];
                    tIdColumn.AutoIncrement = true;
                    tIdColumn.AutoIncrementSeed = 1;
                    tIdColumn.AutoIncrementStep = 1;
                    tCurrentTable.PrimaryKey = new DataColumn[] { tCurrentTable.Columns["Id"] };
                }
            }
            catch (Exception tException)
            {
                tResponseCode = (int) ResultCodesEnum.DATA_FILLING_ERROR;
                Logger.WriteExceptionMessage(tException);
            }

            return tResponseCode;
        }

        /// <summary>
        /// Fills the dataset with the data from the database and then maps the tables
        /// </summary>
        /// <returns>a result code to be used to determine if success or failure</returns>
        public int FillQuestionsDataSet()
        {
            int tResponseCode = (int) ResultCodesEnum.SUCCESS;

            try
            {
                QuestionsDataSet = new DataSet();
                tResponseCode = DatabaseController.GetData(QuestionsDataSet, TableNames);

                if ((int) ResultCodesEnum.SUCCESS == tResponseCode)
                {
                    tResponseCode = MapTables();
                }
            }
            catch (Exception tException)
            {
                tResponseCode = (int)ResultCodesEnum.DATA_FILLING_ERROR;
                Logger.WriteExceptionMessage(tException);
            }

            return tResponseCode;
        }

        /// <summary>
        /// Sets a questionRow to a new row based on the type that was providee
        /// </summary>
        /// <param name="pQuestionType">The type of the question</param>
        /// <param name="pQuestionId">The Id of the question</param>
        /// <param name="pQuestionRow">The datarow to fill the data in</param>
        /// <returns>a result code to be used to determine if success or failure</returns>
        public int GetQuestionRow(string pQuestionType, int pQuestionId, ref DataRow pQuestionRow)
        {
            int tResponseCode = (int) ResultCodesEnum.SUCCESS;

            try
            {
                DataTable tCurrentTable = QuestionsDataSet.Tables[pQuestionType];
                pQuestionRow = pQuestionId == -1 ? tCurrentTable.NewRow() : tCurrentTable.Rows.Find(pQuestionId);
            }
            catch (Exception tException)
            {
                tResponseCode = (int) ResultCodesEnum.DATA_FILLING_ERROR;
                Logger.WriteExceptionMessage(tException);
            }

            return tResponseCode;
        }

        /// <summary>
        /// Adds a new question to the database
        /// </summary>
        /// <param name="pQuestionRow">The question to be added</param>
        /// <returns>a result code to be used to determine if success or failure</returns>
        public int AddQuestion(DataRow pQuestionRow)
        {
            int tDidAdd = (int) ResultCodesEnum.SUCCESS;

            try
            {
                int tNewQuestionId = 0;
                int tNewQuestionAllTableId = 0;
                tDidAdd = DatabaseController.AddQuestion(pQuestionRow, ref tNewQuestionId, ref tNewQuestionAllTableId);

                if (tDidAdd == (int) ResultCodesEnum.SUCCESS)
                {
                    DataRow tFormattedQuestionRow = QuestionsDataSet.Tables["AllQuestions"].NewRow();
                    pQuestionRow["Id"] = tNewQuestionId;
                    tFormattedQuestionRow["Id"] = tNewQuestionAllTableId;

                    foreach (DataColumn tQuestionColumn in pQuestionRow.Table.Columns)
                    {
                        string tCurrentColumnName = tQuestionColumn.ColumnName;

                        if (tCurrentColumnName == "Id")
                        {
                            tFormattedQuestionRow["OriginalId"] = pQuestionRow[tCurrentColumnName];
                        }
                        else if (tCurrentColumnName == "Text" || tCurrentColumnName == "Order")
                        {
                            tFormattedQuestionRow[tCurrentColumnName] = pQuestionRow[tCurrentColumnName];
                        }
                    }

                    tFormattedQuestionRow["Type"] = pQuestionRow.Table.TableName.Replace("Questions", "");
                    QuestionsDataSet.Tables["AllQuestions"].Rows.Add(tFormattedQuestionRow);
                    tFormattedQuestionRow.AcceptChanges();


                    QuestionsDataSet.Tables[pQuestionRow.Table.TableName].Rows.Add(pQuestionRow);
                    pQuestionRow.AcceptChanges();
                }
            }
            catch (Exception tException)
            {
                tDidAdd = (int) ResultCodesEnum.CURRENT_DATA_INVALID;
                Logger.WriteExceptionMessage(tException);
            }

            return tDidAdd;
        }

        /// <summary>
        /// Edits a new question in the database
        /// </summary>
        /// <param name="pQuestionRow">The question to be edited</param>
        /// <returns>a result code to be used to determine if success or failure</returns>
        public int EditQuestion(DataRow pQuestionRow)
        {
            int tDidEdit = (int) ResultCodesEnum.SUCCESS;

            try
            {
                tDidEdit = DatabaseController.EditQuestion(pQuestionRow);

                if (tDidEdit == (int) ResultCodesEnum.SUCCESS)
                {
                    pQuestionRow.AcceptChanges();
                    string tFindExpression = "OriginalId = " + pQuestionRow["Id"] + " AND Type = '" + pQuestionRow.Table.TableName.Replace("Questions", "") + "'";
                    DataRow tQuestionAllTableRow = QuestionsDataSet.Tables["AllQuestions"].Select(tFindExpression)[0];

                    tQuestionAllTableRow["Text"] = "Tomato";

                    foreach (DataColumn tQuestionColumn in pQuestionRow.Table.Columns)
                    {
                        string tCurrentColumnName = tQuestionColumn.ColumnName;

                        if (tCurrentColumnName == "Text" || tCurrentColumnName == "Order")
                        {
                            tQuestionAllTableRow[tCurrentColumnName] = pQuestionRow[tCurrentColumnName];
                        }
                    }

                    tQuestionAllTableRow.AcceptChanges();
                }
            }
            catch (Exception tException)
            {
                tDidEdit = (int) ResultCodesEnum.DATA_FILLING_ERROR;
                Logger.WriteExceptionMessage(tException);
            }

            return tDidEdit;
        }

        /// <summary>
        /// Removes a question from the database
        /// </summary>
        /// <param name="pQuestionId">The id of the question that should be removed</param>
        /// <returns>a result code to be used to determine if success or failure</returns>
        public int RemoveQuestion(int pQuestionId)
        {
            int tDidDelete = (int) ResultCodesEnum.SUCCESS;

            try
            {
                DataRow tQuestionDataRow = QuestionsDataSet.Tables["AllQuestions"].Rows.Find(pQuestionId);
                int tQuestionOriginalId = Convert.ToInt32(tQuestionDataRow["OriginalId"]);
                string tTableName = tQuestionDataRow["Type"].ToString() + "Questions";
                DataRow tQuestionInOriginalTable = QuestionsDataSet.Tables[tTableName].Rows.Find(tQuestionOriginalId);

                tDidDelete = DatabaseController.DeleteQuestion(tQuestionInOriginalTable);

                if (tDidDelete == (int) ResultCodesEnum.SUCCESS)
                {
                    tQuestionInOriginalTable.Delete();
                    tQuestionDataRow.Delete();

                    QuestionsDataSet.Tables[tTableName].AcceptChanges();
                    QuestionsDataSet.Tables["AllQuestions"].AcceptChanges();
                }
            }
            catch (Exception tException)
            {
                tDidDelete = (int) ResultCodesEnum.DATA_FILLING_ERROR;
                Logger.WriteExceptionMessage(tException);
            }

            return tDidDelete;
        }
    }
}
