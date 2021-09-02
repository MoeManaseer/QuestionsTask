using System;

namespace QuestionsFormsTest
{
    public static class ResultCodes
    {
        /// <summary>
        /// Helper function that gets the corrosponding string for the code number that we send it
        /// </summary>
        /// <param name="pCodeNumber">The code number</param>
        /// <returns>A string message corosponding to the code number we sent it</returns>
        public static string GetCodeMessage(int pCodeNumber)
        {
            string tCodeMessage = "Empty Message";

            try
            {
                switch (pCodeNumber)
                {
                    case 0:
                        tCodeMessage = "Operation successfuly occured.\n";
                        break;
                    case 1:
                        tCodeMessage = "Something wrong happend.. please restart the application.\n";
                        break;
                    case 2:
                        tCodeMessage = "Something wrong happend while updating/deleting the question, the question probably got deleted.. please try again or restart the application to get the latest changes.\n";
                        break;
                    case 5:
                        tCodeMessage = "Something wrong happend while filling the data, please retry or restart the applciation.\n";
                        break;
                    case 10:
                        tCodeMessage = "The operation was succesful at the database, but seomthing wrong happend, please restart the application to see the new changes.\n";
                        break;
                    case 102:
                        tCodeMessage = "Incorrect SQL syntax, please contact an admin.\n";
                        break;
                    case 4060:
                        tCodeMessage = "Coudln't connect to database, please check your connection settings.\n";
                        break;
                    case 18456:
                        tCodeMessage = "Connection to database denied, please check your username and password.\n";
                        break;
                    case 229:
                        tCodeMessage = "Unauthorized user, can't execute sql.\n";
                        break;
                    case -1:
                        tCodeMessage = "Operation was unsuccessful. please try again.\n";
                        break;
                    default:
                        tCodeMessage = "Unkown error occured, please restart the application.\n";
                        break;
                }
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }

            return tCodeMessage;
        }
    }
}
