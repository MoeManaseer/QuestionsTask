using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Collections.Generic;

namespace QuestionsFormsTest
{
    public class DatabaseController
    {
        private string ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString.ToString();
        private SqlConnection SQLConnection;
        public DatabaseController()
        {
            try
            {
                ConstructConnectionString();
            }
            catch (Exception e)
            {
                Logger.WriteExceptionMessage(e);
            }
        }

        /// <summary>
        /// Changes the values for the connection string
        /// </summary>
        /// <param name="pConnectionData">List of key value pairs of the new connection string data</param>
        public void ChangeConnectionString(List<KeyValuePair<string, string>> pConnectionData)
        {
            try
            {
                foreach (KeyValuePair<string, string> tKeyValuePair in pConnectionData)
                {
                    ConfigurationManager.AppSettings[tKeyValuePair.Key] = tKeyValuePair.Value;
                }

                ConstructConnectionString();
            }
            catch (Exception e)
            {
                Logger.WriteExceptionMessage(e);
            }
        }

        /// <summary>
        /// Constructs the connection string then assigns it to the connection string variable
        /// </summary>
        private void ConstructConnectionString()
        {
            try
            {
                string DataSource = ConfigurationManager.AppSettings["DataSource"];
                string DatabaseName = ConfigurationManager.AppSettings["Database"];
                string Username = ConfigurationManager.AppSettings["UserId"];
                string Password = ConfigurationManager.AppSettings["Password"];

                ConnectionString = "data source = " + DataSource + "; database = " + DatabaseName + "; uid = " + Username + "; password = " + Password;
            }
            catch (Exception e)
            {
                Logger.WriteExceptionMessage(e);
            }
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
            int tResultCode = 0;

            try 
            {
                SQLConnection = new SqlConnection(ConnectionString);
                StringBuilder SqlStatementString = new StringBuilder();

                foreach (string tTableName in pTableNames)
                {
                    SqlStatementString.Append("SELECT * FROM " + tTableName + "Questions;");
                }

                tSQLCommand = new SqlCommand(SqlStatementString.ToString(), SQLConnection);
                SQLConnection.Open();
                tSQLReader = tSQLCommand.ExecuteReader(CommandBehavior.KeyInfo);

                foreach (string tTableName in pTableNames)
                {
                    DataTable tNewTableSchema = tSQLReader.GetSchemaTable();
                    DataTable tNewTable = new DataTable();
                    foreach (DataRow tRow in tNewTableSchema.Rows)
                    {
                        string tNewColName = tRow.Field<string>("ColumnName");
                        Type tNewColType = tRow.Field<Type>("DataType");
                        tNewTable.Columns.Add(tNewColName, tNewColType);
                    }

                    while (tSQLReader.Read())
                    {
                        var tNewTableRow = tNewTable.Rows.Add();
                        foreach (DataColumn col in tNewTable.Columns)
                        {
                            tNewTableRow[col.ColumnName] = tSQLReader[col.ColumnName];
                        }
                    }

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
                tResultCode = 1;
            }
            finally
            {
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
        public int AddQuestion(DataRow pQuestionRow)
        {
            SqlTransaction tSQLTransaction = null;
            SqlCommand tSQLCommand = null;
            int tResultCode = 0;

            try
            {
                string tTableName = pQuestionRow.Table.TableName;

                SQLConnection = new SqlConnection(ConnectionString);
                SQLConnection.Open();
                tSQLTransaction = SQLConnection.BeginTransaction();

                tSQLCommand = new SqlCommand("Add_" + tTableName, SQLConnection, tSQLTransaction);
                tSQLCommand.CommandType = CommandType.StoredProcedure;

                foreach (DataColumn tCurrentColumn in pQuestionRow.Table.Columns)
                {
                    string tCurrentColumnName = tCurrentColumn.ToString();

                    if (!tCurrentColumnName.Equals("Id"))
                    {
                        tSQLCommand.Parameters.Add(new SqlParameter("@" + tCurrentColumnName, pQuestionRow[tCurrentColumnName]));
                    }
                }

                tSQLCommand.ExecuteNonQuery();
                tSQLTransaction.Commit();

            }
            catch (SqlException tSQLException)
            {
                tSQLTransaction.Rollback();
                Logger.WriteExceptionMessage(tSQLException);
                tResultCode = tSQLException.Number;
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
                tResultCode = 1;
            }
            finally
            {
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
            int tResultCode = 0;

            try
            {
                string tTableName = pQuestionRow.Table.TableName;

                SQLConnection = new SqlConnection(ConnectionString);
                SQLConnection.Open();
                tSQLTransaction = SQLConnection.BeginTransaction();

                tSQLCommand = new SqlCommand("Update_" + tTableName, SQLConnection, tSQLTransaction);
                tSQLCommand.CommandType = CommandType.StoredProcedure;

                foreach (DataColumn tCurrentColumn in pQuestionRow.Table.Columns)
                {
                    string tCurrentColumnName = tCurrentColumn.ToString();
                    tSQLCommand.Parameters.Add(new SqlParameter("@" + tCurrentColumnName, pQuestionRow[tCurrentColumnName]));
                }

                tResultCode = tSQLCommand.ExecuteNonQuery() != 0 ? 0 : 2;

                tSQLTransaction.Commit();
            }
            catch (SqlException tSQLException)
            {
                tSQLTransaction.Rollback();
                Logger.WriteExceptionMessage(tSQLException);
                tResultCode = tSQLException.Number;
            }
            catch (Exception e)
            {
                Logger.WriteExceptionMessage(e);
                tResultCode = 1;
            }
            finally
            {
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
            int tResultCode = 0;

            try
            {
                string tTableName = pQuestionRow.Table.TableName;
                int tQuestionId = Convert.ToInt32(pQuestionRow["Id"]);

                SQLConnection = new SqlConnection(ConnectionString);
                SQLConnection.Open();
                tSQLTransaction = SQLConnection.BeginTransaction();

                tSQLCommand = new SqlCommand("Delete_" + tTableName, SQLConnection, tSQLTransaction);
                tSQLCommand.CommandType = CommandType.StoredProcedure;
                tSQLCommand.Parameters.Add(new SqlParameter("@Id", pQuestionRow));
                tResultCode = tSQLCommand.ExecuteNonQuery() != 0 ? 0 : 2;

                tSQLTransaction.Commit();
            }
            catch (SqlException tSQLException)
            {
                tSQLTransaction.Rollback();
                Logger.WriteExceptionMessage(tSQLException);
                tResultCode = tSQLException.Number;
            }
            catch (Exception e)
            {
                tSQLTransaction.Rollback();
                Logger.WriteExceptionMessage(e);
                tResultCode = 1;
            }
            finally
            {
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
