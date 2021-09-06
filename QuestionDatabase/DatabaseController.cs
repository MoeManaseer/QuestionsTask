using LoggerUtils;
using ResultCodes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace QuestionDatabase
{
    public class DatabaseController
    {
        private SqlConnection SQLConnection;
        public ConnectionString ConnectionString { private set; get; }

        public DatabaseController()
        {
            try
            {
                ConnectionString = new ConnectionString();
            }
            catch (Exception e)
            {
                Logger.WriteExceptionMessage(e);
            }
        }

        /// <summary>
        /// Changes the connectionstring instance with a new one
        /// </summary>
        /// <param name="tNewConnectionString">The new connectionstring instance</param>
        /// <returns>a result code to be used to determine if success or failure</returns>
        public int ChangeConnectionString(ConnectionString tNewConnectionString)
        {
            int tResponseCode = (int)ResultCodesEnum.CODE_FAILUER;

            try
            {
                ConnectionString = new ConnectionString(tNewConnectionString);
                tResponseCode = (int)ResultCodesEnum.SUCCESS;
            }
            catch (Exception e)
            {
                Logger.WriteExceptionMessage(e);
            }

            return tResponseCode;
        }

        /// <summary>
        /// Tests if the connection string given to it successfuly connects to the database
        /// </summary>
        /// <param name="tConnectionString">The connection string to test</param>
        /// <returns>a result code to be used to determine if success or failure</returns>
        public int TestDatabaseConnection(ConnectionString tConnectionString)
        {
            int tResultCode = (int)ResultCodesEnum.SUCCESS;
            SqlCommand tSQLCommand = null;

            try
            {
                string tomato = tConnectionString.ToString();
                SQLConnection = new SqlConnection(tConnectionString.ToString());
                tSQLCommand = new SqlCommand("SELECT 1 FROM AllQuestions;", SQLConnection);

                SQLConnection.Open();
                tSQLCommand.ExecuteNonQuery();
            }
            catch (SqlException tSQLException)
            {
                Logger.WriteExceptionMessage(tSQLException);
                tResultCode = tSQLException.Number;
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
                tResultCode = (int) ResultCodesEnum.CODE_FAILUER;
            }
            finally
            {
                if (SQLConnection.State == ConnectionState.Open)
                {
                    tSQLCommand.Dispose();
                    SQLConnection.Close();
                }
            }

            return tResultCode;
        }

        /// <summary>
        /// Gets data from the database based on the given table names and constructs the DataSet
        /// </summary>
        /// <param name="pQuestionsDataSet">The dataset to be constructed</param>
        /// <param name="pTableNames">The table names to get from the database</param>
        /// <returns>a result code to be used to determine if success or failure</returns>
        public int GetData(DataSet pQuestionsDataSet, string[] pTableNames)
        {
            SqlCommand tSQLCommand = null;
            SqlDataReader tSQLReader = null;
            int tResultCode = (int)ResultCodesEnum.SUCCESS;

            try
            {
                SQLConnection = new SqlConnection(ConnectionString.ToString());
                StringBuilder SqlStatementString = new StringBuilder();

                // Construct the multiple selects from different tables provided in the params
                foreach (string tTableName in pTableNames)
                {
                    SqlStatementString.Append("SELECT * FROM " + tTableName + "Questions;");
                }

                tSQLCommand = new SqlCommand(SqlStatementString.ToString(), SQLConnection);
                SQLConnection.Open();
                tSQLReader = tSQLCommand.ExecuteReader(CommandBehavior.KeyInfo);

                // Creates a new table for each result set that came back from the select statement
                foreach (string tTableName in pTableNames)
                {
                    DataTable tNewTableSchema = tSQLReader.GetSchemaTable();
                    DataTable tNewTable = new DataTable();

                    // Maps the rows of the new table the same as the ones retrieved from the database
                    foreach (DataRow tRow in tNewTableSchema.Rows)
                    {
                        string tNewColName = tRow.Field<string>("ColumnName");
                        Type tNewColType = tRow.Field<Type>("DataType");
                        tNewTable.Columns.Add(tNewColName, tNewColType);
                    }

                    // Get the rows of each table and insert them into the new table
                    while (tSQLReader.Read())
                    {
                        var tNewTableRow = tNewTable.Rows.Add();
                        foreach (DataColumn col in tNewTable.Columns)
                        {
                            tNewTableRow[col.ColumnName] = tSQLReader[col.ColumnName];
                        }
                    }

                    // Add the new table to the data set and get the next result set
                    pQuestionsDataSet.Tables.Add(tNewTable);
                    tSQLReader.NextResult();
                }
            }
            catch (SqlException tSQLException)
            {
                Logger.WriteExceptionMessage(tSQLException);
                tResultCode = tSQLException.Number;
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
                tResultCode = (int)ResultCodesEnum.CODE_FAILUER;
            }
            finally
            {
                // This is to prevent having an error thrown here, if the connection is open then everything else is open
                if (SQLConnection.State == ConnectionState.Open)
                {
                    tSQLReader.Close();
                    tSQLCommand.Dispose();
                    SQLConnection.Close();
                    pQuestionsDataSet.AcceptChanges();
                }
            }

            return tResultCode;
        }

        /// <summary>
        /// Adds a new question to the database
        /// </summary>
        /// <param name="pQuestionRow">The new question to be added to the database</param>
        /// <returns>a result code to be used to determine if success or failure</returns>
        public int AddQuestion(DataRow pQuestionRow, ref int pQuestionId, ref int pQuestionAllTableId)
        {
            SqlTransaction tSQLTransaction = null;
            SqlCommand tSQLCommand = null;
            int tResultCode = (int)ResultCodesEnum.SUCCESS;

            try
            {
                string tTableName = pQuestionRow.Table.TableName;

                SQLConnection = new SqlConnection(ConnectionString.ToString());
                SQLConnection.Open();
                tSQLTransaction = SQLConnection.BeginTransaction();

                // Get the correct procedure based on the tablename
                tSQLCommand = new SqlCommand("Add_" + tTableName, SQLConnection, tSQLTransaction);
                tSQLCommand.CommandType = CommandType.StoredProcedure;

                // Get every value from the datarow except for Id since it gets auto generated in the database
                foreach (DataColumn tCurrentColumn in pQuestionRow.Table.Columns)
                {
                    string tCurrentColumnName = tCurrentColumn.ToString();

                    if (!tCurrentColumnName.Equals("Id"))
                    {
                        tSQLCommand.Parameters.Add(new SqlParameter("@" + tCurrentColumnName, pQuestionRow[tCurrentColumnName]));
                    }
                }

                tSQLCommand.Parameters.Add("@Id", SqlDbType.Int);
                tSQLCommand.Parameters["@Id"].Direction = ParameterDirection.Output;

                tSQLCommand.Parameters.Add("@AllQuestionsId", SqlDbType.Int);
                tSQLCommand.Parameters["@AllQuestionsId"].Direction = ParameterDirection.Output;

                tSQLCommand.ExecuteNonQuery();
                pQuestionId = Convert.ToInt32(tSQLCommand.Parameters["@Id"].Value);
                pQuestionAllTableId = Convert.ToInt32(tSQLCommand.Parameters["@AllQuestionsId"].Value);
                tSQLTransaction.Commit();
            }
            catch (SqlException tSQLException)
            {
                if (tSQLTransaction != null)
                {
                    tSQLTransaction.Rollback();
                }
                Logger.WriteExceptionMessage(tSQLException);
                tResultCode = tSQLException.Number;
            }
            catch (Exception tException)
            {
                tSQLTransaction.Rollback();
                Logger.WriteExceptionMessage(tException);
                tResultCode = (int)ResultCodesEnum.CODE_FAILUER;
            }
            finally
            {
                // This is to prevent having an error thrown here, if the connection is open then everything else is open
                if (SQLConnection.State == ConnectionState.Open)
                {
                    tSQLCommand.Dispose();
                    tSQLTransaction.Dispose();
                    SQLConnection.Close();
                }
            }

            return tResultCode;
        }

        /// <summary>
        /// Edits a question in the database
        /// </summary>
        /// <param name="pQuestionRow">The question that should be edited in the database</param>
        /// <returns>a result code to be used to determine if success or failure</returns>
        public int EditQuestion(DataRow pQuestionRow)
        {
            SqlTransaction tSQLTransaction = null;
            SqlCommand tSQLCommand = null;
            int tResultCode = (int)ResultCodesEnum.SUCCESS;

            try
            {
                string tTableName = pQuestionRow.Table.TableName;

                SQLConnection = new SqlConnection(ConnectionString.ToString());
                SQLConnection.Open();
                tSQLTransaction = SQLConnection.BeginTransaction();

                // Get the correct procedure based on the tablename
                tSQLCommand = new SqlCommand("Update_" + tTableName, SQLConnection, tSQLTransaction);
                tSQLCommand.CommandType = CommandType.StoredProcedure;

                // Insert the new values as params to be used in the procedure
                foreach (DataColumn tCurrentColumn in pQuestionRow.Table.Columns)
                {
                    string tCurrentColumnName = tCurrentColumn.ToString();
                    tSQLCommand.Parameters.Add(new SqlParameter("@" + tCurrentColumnName, pQuestionRow[tCurrentColumnName]));
                }

                tResultCode = tSQLCommand.ExecuteNonQuery() != 0 ? (int)ResultCodesEnum.SUCCESS : (int)ResultCodesEnum.QUESTION_OUT_OF_DATE;

                tSQLTransaction.Commit();
            }
            catch (SqlException tSQLException)
            {
                if (tSQLTransaction != null)
                {
                    tSQLTransaction.Rollback();
                }
                Logger.WriteExceptionMessage(tSQLException);
                tResultCode = tSQLException.Number;
            }
            catch (Exception e)
            {
                Logger.WriteExceptionMessage(e);
                tResultCode = (int)ResultCodesEnum.CODE_FAILUER;
            }
            finally
            {
                // This is to prevent having an error thrown here, if the connection is open then everything else is open
                if (SQLConnection.State == ConnectionState.Open)
                {
                    tSQLCommand.Dispose();
                    tSQLTransaction.Dispose();
                    SQLConnection.Close();
                }
            }

            return tResultCode;
        }

        /// <summary>
        /// Deletes a question from the database
        /// </summary>
        /// <param name="pQuestionRow">The question row that should be removed</param>
        /// <returns>a result code to be used to determine if success or failure</returns>
        public int DeleteQuestion(DataRow pQuestionRow)
        {
            SqlTransaction tSQLTransaction = null;
            SqlCommand tSQLCommand = null;
            int tResultCode = (int)ResultCodesEnum.SUCCESS;

            try
            {
                string tTableName = pQuestionRow.Table.TableName;
                int tQuestionId = Convert.ToInt32(pQuestionRow["Id"]);

                SQLConnection = new SqlConnection(ConnectionString.ToString());
                SQLConnection.Open();
                tSQLTransaction = SQLConnection.BeginTransaction();

                // Get the correct procedure based on the tablename
                tSQLCommand = new SqlCommand("Delete_" + tTableName, SQLConnection, tSQLTransaction);
                tSQLCommand.CommandType = CommandType.StoredProcedure;
                tSQLCommand.Parameters.Add(new SqlParameter("@Id", tQuestionId));

                // If rows are affected, return 0 which is success, if not return 2 which means nothing happend
                tResultCode = tSQLCommand.ExecuteNonQuery() != 0 ? (int)ResultCodesEnum.SUCCESS : (int)ResultCodesEnum.QUESTION_OUT_OF_DATE;

                tSQLTransaction.Commit();
            }
            catch (SqlException tSQLException)
            {
                if (tSQLTransaction != null)
                {
                    tSQLTransaction.Rollback();
                }
                Logger.WriteExceptionMessage(tSQLException);
                tResultCode = tSQLException.Number;
            }
            catch (Exception e)
            {
                tSQLTransaction.Rollback();
                Logger.WriteExceptionMessage(e);
                tResultCode = (int)ResultCodesEnum.CODE_FAILUER;
            }
            finally
            {
                // This is to prevent having an error thrown here, if the connection is open then everything else is open
                if (SQLConnection.State == ConnectionState.Open)
                {
                    tSQLCommand.Dispose();
                    tSQLTransaction.Dispose();
                    SQLConnection.Close();
                }
            }

            return tResultCode;
        }
    }
}
