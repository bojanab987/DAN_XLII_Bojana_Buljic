using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zadatak_1.Services
{
    /// <summary>
    /// Singleton pattern class for loging acctions in file
    /// </summary>
    class LogActions
    {
        public readonly string file = @"..\..\Files\Log.txt";
        private object locker = new object();
        private static LogActions log;

        private LogActions()
        {

        }

        public static LogActions GetLog()
        {
            if(log==null)
            {
                log = new LogActions();
            }
            return log;
        }

        public void PrintInFile(string result)
        {
            lock(locker)
            {
                try
                {
                    string date = DateTime.Now.ToShortDateString();
                    string time = DateTime.Now.ToLongTimeString();
                    result = date + " " + time + " " + result;
                    Thread.Sleep(2000);
                    StreamWriter sw = new StreamWriter(file, true);
                    sw.WriteLine(result);
                    sw.Close();
                }
                catch(FileNotFoundException)
                {
                    Console.WriteLine("No file found.");
                }
            }
        }
    }
}
