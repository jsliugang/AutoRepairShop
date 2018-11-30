using System;
using System.Globalization;
using System.IO;
using AutoRepairShop.Tools;
using AutoRepairShop.WorkFlow;

namespace AutoRepairShop.Services
{
    class ErrorLogService
    {
        public void StoreLog(string logMessage)
        {
            using (StreamWriter w = File.AppendText(@"C:\Users\Yuri.Pustovoy\Documents\Visual Studio 2017\Projects\AutoRepairShop\AutoRepairShop\bin\Debug\ErrorLog.txt"))
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

        public static void Log(string logMessage, TextWriter w)
        {
            w.Write($"\r{MsgDecoratorTool.PassMeTime().ToString("MM/dd/yyyy h:mm tt", CultureInfo.CurrentUICulture)} : ");
            w.WriteLine($"{logMessage}");
        }

    }
}
