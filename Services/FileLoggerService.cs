using System;
using System.Globalization;
using System.IO;
using AutoRepairShop.Tools;

namespace AutoRepairShop.Services
{
    internal class FileLoggerService
    {
        public void StoreLog(string logMessage)
        {
            using (StreamWriter w = File.AppendText("LucyLog.txt"))
            {
                if (logMessage != null)
                {
                    Log(logMessage, w);
                }
                else
                {
                    Console.WriteLine($"LOG ERROR: No log message provided");
                }
            }
        }

        public void StoreTime()
        {
            using (StreamWriter w = File.AppendText("LucyLog.txt"))
            {
                w.WriteLine($"Application closed");
                w.WriteLine($"\r{MsgDecoratorTool.PassMeTime().ToString("MM/dd/yyyy h:mm tt", CultureInfo.CurrentUICulture)}");
            }
        }

        public void DumpLog()
        {
            using (StreamReader r = File.OpenText("LucyLog.txt"))
            {
                DumpLog(r);
            }
        }

        public static void Log(string logMessage, TextWriter w)
        {
            w.Write($"\r{MsgDecoratorTool.PassMeTime().ToString("MM/dd/yyyy h:mm tt", CultureInfo.CurrentUICulture)} : ");
            w.WriteLine($"{logMessage}");
        }

        public static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
    }
}
