using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Humans;
using AutoRepairShop.Classes.Managers;

namespace AutoRepairShop.Classes.Managers
{
    class LogManager
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

        public void DumpLog()
        {
            string currentTime = ShopManager.WhatTimeIsItNow().ToString();
            using (StreamReader r = File.OpenText("LucyLog.txt"))
            {
                DumpLog(r);
            }
        }

        public static void Log(string logMessage, TextWriter w)
        {
            w.Write($"\r\n{Menu.PassMeTime()} : ");
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
