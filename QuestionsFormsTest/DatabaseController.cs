using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace QuestionsFormsTest
{
    public class DatabaseController
    {
        private readonly string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        private SqlConnection con;

        /// <summary>
        /// Gets the tables from the database and returns it as a dataset to be used
        /// </summary>
        /// <returns>DataSet the data from the database</returns>
        public DataSet GetAllQuestions()
        {
            DataSet tempSet = new DataSet();

            try 
            {
                con = new SqlConnection(CS);

                string sqlStatment = "select * from SmileyQuestions;select * from SliderQuestions;select * from StarQuestions";
                SqlDataAdapter dAdapter = new SqlDataAdapter(sqlStatment, con);

                dAdapter.TableMappings.Add("Table", "SmileyQuestions");
                dAdapter.TableMappings.Add("Table1", "SliderQuestions");
                dAdapter.TableMappings.Add("Table2", "StarQuestions");

                dAdapter.Fill(tempSet);
            }
            catch (Exception e)
            {
                Logger.WriteExceptionMessage(e);
                CloseConnection();

                string deleteMessage = "Fatal error in fetching data from database, please contact system admin..";
                string deleteCaption = "Error";
                MessageBoxButtons messageButtons = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Error;
                DialogResult result;

                result = MessageBox.Show(deleteMessage, deleteCaption, messageButtons, icon);

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    Application.Exit();
                }
            }

            return tempSet;
        }

        /// <summary>
        /// Adds a new question to the database
        /// </summary>
        /// <param name="newRow">The datarow to be added to the database</param>
        /// <param name="tableName">The table in which the question should be inserted to</param>
        /// <returns>Int the new question Id from the database</returns>
        public int AddNewQuestion(DataRow newRow, string tableName)
        {
            int newQuestionId = 0;

            try
            {
                con = new SqlConnection(CS);
                SqlCommand addCmd = new SqlCommand("Add_" + tableName, con);
                addCmd.CommandType = CommandType.StoredProcedure;

                foreach (DataColumn curCol in newRow.Table.Columns)
                {
                    string colName = curCol.ToString();
                    if (!colName.Equals("Id"))
                    {
                        addCmd.Parameters.Add(new SqlParameter("@" + colName, newRow[colName]));
                    }
                }

                addCmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                addCmd.Parameters["@Id"].Direction = ParameterDirection.Output;

                con.Open();
                addCmd.ExecuteNonQuery();
                newQuestionId = Convert.ToInt32(addCmd.Parameters["@Id"].Value);
            }
            catch (Exception e)
            {
                Logger.WriteExceptionMessage(e);
            }
            finally
            {
                CloseConnection();
            }

            return newQuestionId;
        }

        /// <summary>
        /// Edits a question in the database
        /// </summary>
        /// <param name="updatedRow">The updated datarow</param>
        /// <param name="tableName">The table name of which the datarow belongs to</param>
        /// <param name="id">The question original id in the database</param>
        /// <returns>Bool wehether the question was updated in the database or not</returns>
        public bool EditQuestion(DataRow updatedRow, string tableName, int id)
        {
            bool didUpdate = false;

            try
            {
                con = new SqlConnection(CS);
                SqlCommand editCmd = new SqlCommand("Update_" + tableName, con);
                editCmd.CommandType = CommandType.StoredProcedure;

                foreach (DataColumn curCol in updatedRow.Table.Columns)
                {
                    string colName = curCol.ToString();
                    editCmd.Parameters.Add(new SqlParameter("@" + colName, updatedRow[colName]));
                }

                con.Open();
                editCmd.ExecuteNonQuery();
                didUpdate = true;
            }
            catch (Exception e)
            {
                Logger.WriteExceptionMessage(e);
            }
            finally
            {
                CloseConnection();
            }

            return didUpdate;
        }

        /// <summary>
        /// Deletes a question from the database
        /// </summary>
        /// <param name="tableName">The tablename in the database</param>
        /// <param name="id">The id of the question to be removed</param>
        /// <returns>bool if the question got deleted or not</returns>
        public bool DeleteQuestion(string tableName, int id)
        {
            int affectedRows = 0;
            
            try
            {
                con = new SqlConnection(CS);
                SqlCommand deleteCmd = new SqlCommand("delete from " + tableName + " where id = " + id, con);

                con.Open();
                affectedRows = deleteCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.WriteExceptionMessage(e);
            }
            finally
            {
                CloseConnection();
            }
            

            return affectedRows == 1;
        }

        /// <summary>
        /// Helper function to close the connection if it was open
        /// </summary>
        private void CloseConnection()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}
