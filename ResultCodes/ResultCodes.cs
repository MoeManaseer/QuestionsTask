using LoggerUtils;
using System;

namespace ResultCodes
{
    public enum ResultCodesEnum
    {
        SUCCESS = 0,
        CODE_FAILUER = 1,
        QUESTION_OUT_OF_DATE = 3,
        DATABASE_CONNECTION_FAILURE = 4060,
        DATABASE_AUTHENTICATION_FAILUER = 229,
        DATABASE_CONNECTION_DENIED = 18456,
        DATABASE_SQL_INCORRECT = 102,
        DATA_FILLING_ERROR = 5,
        SERVER_PAUSED = 17142,
        SERVER_NOT_FOUND_OR_DOWN = 2,
        CURRENT_DATA_INVALID = 10,
        SERVER_CONNECTION_FAILURE = 53,
    };

    public static class ResultCodesUtil
    {
        public static bool IsSqlError(int pCodeNumber)
        {
            bool tIsSqlError = true;

            try
            {
                if ((int) ResultCodesEnum.CODE_FAILUER == pCodeNumber ||
                    (int) ResultCodesEnum.CURRENT_DATA_INVALID == pCodeNumber ||
                    (int) ResultCodesEnum.DATA_FILLING_ERROR == pCodeNumber ||
                    (int) ResultCodesEnum.SUCCESS == pCodeNumber ||
                    (int)ResultCodesEnum.QUESTION_OUT_OF_DATE == pCodeNumber)
                {
                    tIsSqlError = false;
                }
            }
            catch (Exception tException)
            {
                Logger.WriteExceptionMessage(tException);
            }

            return tIsSqlError;
        }

        /// <summary>
        /// Helper function that gets the corrosponding string for the code number that we send it
        /// </summary>
        /// <param name="pCodeNumber">The code number</param>
        /// <returns>A string message corosponding to the code number we sent it</returns>
        public static string GetCodeMessage(int pCodeNumber)
        {
            string tCodeMessage = "Empty Message";
            ResultCodesEnum tCurrentResult = (ResultCodesEnum)pCodeNumber;

            try
            {
                switch (tCurrentResult)
                {
                    case ResultCodesEnum.SUCCESS:
                        tCodeMessage = "Operation successfuly occured.\n";
                        break;
                    case ResultCodesEnum.CODE_FAILUER:
                        tCodeMessage = "Something wrong happend.. please restart the application.\n";
                        break;
                    case ResultCodesEnum.QUESTION_OUT_OF_DATE:
                        tCodeMessage = "Something wrong happend while updating/deleting the question, the question probably got deleted.. please try again or restart the application to get the latest changes.\n";
                        break;
                    case ResultCodesEnum.DATABASE_CONNECTION_FAILURE:
                        tCodeMessage = "Couldn't connect to database, please check your connection settings.\n";
                        break;
                    case ResultCodesEnum.DATABASE_AUTHENTICATION_FAILUER:
                        tCodeMessage = "Connection to database denied, please check your username and password.\n";
                        break;
                    case ResultCodesEnum.DATABASE_CONNECTION_DENIED:
                        tCodeMessage = "Connection to database denied, please check your username and password.\n";
                        break;
                    case ResultCodesEnum.DATABASE_SQL_INCORRECT:
                        tCodeMessage = "Incorrect SQL syntax, please contact admin.\n";
                        break;
                    case ResultCodesEnum.DATA_FILLING_ERROR:
                        tCodeMessage = "Something wrong happend while filling the data, please retry or restart the applciation.\n";
                        break;
                    case ResultCodesEnum.CURRENT_DATA_INVALID:
                        tCodeMessage = "The operation was succesful at the database, but something wrong happend, please restart the application to see the new changes.\n";
                        break;
                    case ResultCodesEnum.SERVER_CONNECTION_FAILURE:
                        tCodeMessage = "Connection to server failure, please check the connection settings.\n";
                        break;
                    case ResultCodesEnum.SERVER_NOT_FOUND_OR_DOWN:
                        tCodeMessage = "Connection to server failure, the server was not found or it is down, please contact admin.\n";
                        break;
                    case ResultCodesEnum.SERVER_PAUSED:
                        tCodeMessage = "The server is currently paused, no new connections allowed, please contact admin.\n";
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
