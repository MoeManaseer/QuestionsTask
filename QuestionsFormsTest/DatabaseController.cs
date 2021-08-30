using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace QuestionsFormsTest
{
    public class DatabaseController
    {
        private readonly string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        private SqlConnection con;

        public DataSet GetAllQuestions()
        {
            DataSet tempSet = new DataSet();
            using (con = new SqlConnection(CS))
            {
                string sqlStatment = "select * from SmileyQuestions;select * from SliderQuestions;select * from StarQuestions";
                SqlDataAdapter dAdapter = new SqlDataAdapter(sqlStatment, con);
                dAdapter.TableMappings.Add("Table", "SmileyQuestions");
                dAdapter.TableMappings.Add("Table1", "SliderQuestions");
                dAdapter.TableMappings.Add("Table2", "StarQuestions");
                dAdapter.Fill(tempSet);
            }

            return tempSet;
        }

        public int AddNewQuestion(DataRow newRow, string tableName)
        {
            int newQuestionId = 0;

            using (con = new SqlConnection(CS))
            {
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
            

            return newQuestionId;
        }

        public bool EditQuestion(DataRow updatedRow, string tableName, int id)
        {
            bool didUpdate = false;

            using (con = new SqlConnection(CS))
            {
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
            
            using (con = new SqlConnection(CS))
            {
                SqlCommand deleteCmd = new SqlCommand("delete from " + tableName + " where id = " + id, con);

                con.Open();
                affectedRows = deleteCmd.ExecuteNonQuery();
            }

            return affectedRows == 1;
        }
    }
}
