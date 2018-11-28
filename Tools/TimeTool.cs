using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Timers;
using AutoRepairShop.Data.Models.Humans;

namespace AutoRepairShop.Tools
{
    sealed class TimeTool
    {
        private const int Thousand = 1000;
        private const string DatetimeDormat = "MM/dd/yyyy h:mm tt";
        private static Timer _sickTimer;

        private static readonly DateTime GameStartRealTime;
        private static readonly DateTime GameStartGameTime;

        static TimeTool()
        {
            SetSickTimer();
            GameStartRealTime = DateTime.Now;
            if (new FileInfo("LucyLog.txt").Length == 0)
            {
                GameStartGameTime = new DateTime(1990, 1, 1, 9, 0, 0);
            }
            else
            {
                try
                {
                    var lastLine = File.ReadLines("LucyLog.txt").Last();
                    GameStartGameTime = DateTime.ParseExact(lastLine, DatetimeDormat, CultureInfo.CurrentUICulture);
                }
                catch (FormatException)
                {
                    GameStartGameTime = new DateTime(1990, 1, 1, 9, 0, 0);
                }
            }
        }

        private static void SetSickTimer()
        {
            _sickTimer = new Timer(ConvertToGameTime(168)*Thousand);
            _sickTimer.Elapsed += OnTimedEvent;
            _sickTimer.AutoReset = true;
            _sickTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Random rand = new Random();
            RmKirill.Kirill.GetSickLeave(rand.NextDouble() > 0.5);
            RmVano.Vano.GetSickLeave(rand.NextDouble() > 0.5);
            RmPetrovich.Petrovich.GetSickLeave(rand.NextDouble() > 0.5);
            RmSanSanuch.SanSanuch.GetSickLeave(rand.NextDouble() > 0.5);
        }

        public static int ConvertToGameTime(double gameHours)
        {
            int secondsRealTime = (int)(gameHours * 3600) / 720;
            return secondsRealTime;
        }

        public static void GetGameTimeToScreen()
        {
         
            Console.WriteLine($"The GameTime Now is {GetGameTime().ToString(DatetimeDormat, CultureInfo.CurrentUICulture)}");
        }

        public static DateTime GetGameTime()
        {
            DateTime timeNow = DateTime.Now;
            TimeSpan differenceFromAppStart = timeNow - GameStartRealTime;
            double difference = differenceFromAppStart.TotalSeconds * 720;
            var newGameTime = GameStartGameTime;
            newGameTime = newGameTime.AddSeconds(difference);
            return newGameTime;
        }
    }
}
