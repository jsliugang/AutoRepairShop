using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AutoRepairShop.Classes
{
    sealed class Time
    {
        private static readonly DateTime gameStartRealTime;
        private static readonly DateTime gameStartGameTime;

        static Time()
        {
            gameStartRealTime = DateTime.Now;
            if (new FileInfo("LucyLog.txt").Length == 0)
            {
                gameStartGameTime = new DateTime(1990, 1, 1, 9, 0, 0);
            }
            else
            {
                try
                {
                    var lastLine = File.ReadLines("LucyLog.txt").Last();
                    gameStartGameTime = DateTime.ParseExact(lastLine, "MM/dd/yyyy h:mm tt", CultureInfo.CurrentUICulture);
                }
                catch (FormatException)
                {
                    gameStartGameTime = new DateTime(1990, 1, 1, 9, 0, 0);
                }
            }
        }

        public void GetGameTimeToScreen()
        {
         
            Console.WriteLine($"The GameTime Now is {GetGameTime().ToString("MM/dd/yyyy h:mm tt", CultureInfo.CurrentUICulture)}");
        }

        public DateTime GetGameTime()
        {
            DateTime timeNow = DateTime.Now;
            TimeSpan differenceFromAppStart = timeNow - gameStartRealTime;
            double difference = differenceFromAppStart.TotalSeconds * 720;
            DateTime newGameTime = new DateTime();
            newGameTime = gameStartGameTime;
            newGameTime = newGameTime.AddSeconds(difference);
            return newGameTime;
        }

        public void UpdateBalance()
        {
            
        }
    }
}
