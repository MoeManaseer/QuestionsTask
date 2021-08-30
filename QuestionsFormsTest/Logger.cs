using System;
using System.IO;
using System.Text;

namespace QuestionsFormsTest
{
    public static class Logger
    {
        public static void WriteExceptionMessage(Exception e)
        {
            StringBuilder errorString = new StringBuilder();
            DateTime dateTime = DateTime.Now;

            errorString.AppendLine(dateTime.ToString("MM/dd HH:mm:ss") + " :-");
            errorString.AppendLine(e.Message);
            errorString.AppendLine(e.StackTrace);
            errorString.AppendLine(e.HelpLink);

            WriteExceptionMessageToFile(errorString.ToString(), dateTime.ToString("MM-dd"));
        }

        private static void WriteExceptionMessageToFile(string errorString, string currentDate)
        {
            string curFile = Environment.CurrentDirectory + "\\" + currentDate + "-log.txt";
            CheckFile(curFile);

            using (StreamWriter sw = File.AppendText(curFile))
            {
                sw.Write(errorString);
            }
        }

        private static void CheckFile(string curFile)
        {
            if (!File.Exists(curFile))
            {
                File.Create(curFile).Close();
            }
        }
    }
}
