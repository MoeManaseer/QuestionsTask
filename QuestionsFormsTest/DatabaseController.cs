using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace QuestionsFormsTest
{
    class DatabaseController
    {
        private readonly string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        private readonly SqlConnection con;

        public DatabaseController()
        {
            con = new SqlConnection(CS);
        }

        public DataSet GetAllQuestions()
        {
            DataSet tempSet = new DataSet();
            try
            {
                string sqlStatment = "select * from SmileyQuestions;select * from SliderQuestions;select * from StarsQuestions";
                SqlDataAdapter dAdapter = new SqlDataAdapter(sqlStatment, con);
                dAdapter.TableMappings.Add("Table", "SmileyQuestions");
                dAdapter.TableMappings.Add("Table1", "SliderQuestions");
                dAdapter.TableMappings.Add("Table2", "StarQuestions");
                dAdapter.Fill(tempSet);
            }
            catch (SqlException e)
            {
                
            }

            return tempSet;
        }

        public bool AddNewQuestion(string tableName)
        {
            using (con)
            {
                string sqlStatment = "";
            }

            return true;
        }

        public bool EditQuestion(string tableName, int id)
        {
            return true;
        }

        public bool DeleteQuestion(string tableName, int id)
        {
            int affectedRows = 0;

            try
            {
                SqlCommand deleteCmd = new SqlCommand("delete from " + tableName + " where id = " + id, con);

                con.Open();
                affectedRows = deleteCmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine("tomato");
                Console.WriteLine(e);
            }
            finally
            {
                con.Close();
            }

            return affectedRows == 1;
        }
    }
}
