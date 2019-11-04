using System;
using System.Globalization;
using System.IO;

namespace ELI.Domain.Helpers
{
    public class LogMessage
    {
        public static void LogMessageNow(string message, string userName = "", string fileInstance = "")
        {
            string loggerDirPath = System.AppDomain.CurrentDomain.BaseDirectory + "Logging";

            if (!System.IO.Directory.Exists(loggerDirPath))
                System.IO.Directory.CreateDirectory(loggerDirPath);

            DateTime dtInstance = DateTime.Now;
            string logFileName = "ELI_" + dtInstance.Year.ToString("0000") + dtInstance.Month.ToString("00") + dtInstance.Day.ToString("00") + ((fileInstance.Length > 0) ? "_" : "") + fileInstance + ".txt";
            string logFilePath = loggerDirPath + "\\" + logFileName;

            TextWriter twLogFile = new StreamWriter(logFilePath, true);
            twLogFile.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture) + "\t\t" + userName + "\t" + message);
            twLogFile.Close();

        }
    }
}
