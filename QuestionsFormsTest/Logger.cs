using System;
using System.IO;
using System.Text;

namespace QuestionsFormsTest
{
    public static class Logger
    {
        /// <summary>
        /// Helper function to write exceptions to a txt file
        /// </summary>
        /// <param name="e">The exception that was thrown</param>
        public static void WriteExceptionMessage(Exception e)
        {
            DateTime dateTime = DateTime.Now;
            string curDate = dateTime.ToString("MM-dd");
            string curFile = Environment.CurrentDirectory + "\\" + curDate + "-log.txt";
            CheckFile(curFile);

            StringBuilder errorString = new StringBuilder();
            errorString.AppendLine(dateTime.ToString("MM/dd HH:mm:ss") + " :-");
            errorString.AppendLine(e.Message);
            errorString.AppendLine(e.StackTrace);
            errorString.AppendLine(e.HelpLink);


            WriteExceptionMessageToFile(errorString.ToString(), curFile);
        }

        /// <summary>
        /// Writes the exception string to the specific file
        /// </summary>
        /// <param name="errorString">The string to be written to the file</param>
        /// <param name="curFile">The file string path</param>
        private static void WriteExceptionMessageToFile(string errorString, string curFile)
        {
            using (StreamWriter sw = File.AppendText(curFile))
            {
                sw.Write(errorString);
            }
        }

        /// <summary>
        /// Helper function to check if the file exists, if it doesn't create it
        /// </summary>
        /// <param name="curFile">The file string path</param>
        private static void CheckFile(string curFile)
        {
            if (!File.Exists(curFile))
            {
                File.Create(curFile).Close();
            }
        }
    }
}
