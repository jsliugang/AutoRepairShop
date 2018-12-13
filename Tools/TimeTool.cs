using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Timers;
using AutoRepairShop.Data.Models.Humans;
using AutoRepairShop.Services;
using AutoRepairShop.WorkFlow;
using Timer = System.Timers.Timer;

namespace AutoRepairShop.Tools
{
    internal sealed class TimeTool
    {
        public static TimeTool TimeInstance => Nested.Time;
        public const int Thousand = 1000;
        private const string DatetimeDormat = "MM/dd/yyyy h:mm tt";
        private static Timer _sickTimer;
        private static Timer _nextDayTimer;
        private readonly DateTime _gameStartRealTime;
        private readonly DateTime _gameStartGameTime;
        public static Random Rand = new Random();

        private TimeTool()
        {
            _gameStartRealTime = DateTime.Now;
            if (new FileInfo("LucyLog.txt").Length == 0)
            {
                _gameStartGameTime = new DateTime(1990, 1, 1, 9, 0, 0);
            }
            else
            {
                try
                {
                    var lastLine = File.ReadLines("LucyLog.txt").Last();
                    _gameStartGameTime = DateTime.ParseExact(lastLine, DatetimeDormat, CultureInfo.CurrentUICulture);
                }
                catch (FormatException)
                {
                    _gameStartGameTime = new DateTime(1990, 1, 1, 9, 0, 0);
                }
            }
        }

        private class Nested
        {
            static Nested()
            {}
            internal static readonly TimeTool Time = new TimeTool();
        }

        public void SetTimers()
        {
            SetSickTimer();
            SetNextDayTimer(CalculateSecondsToNextDay());
            GetGameTimeToScreen();
        }

        private static void SetSickTimer()
        {
            _sickTimer = new Timer(ConvertToRealTime(168)*Thousand);
            _sickTimer.Elapsed += OnSickEvent;
            _sickTimer.AutoReset = true;
            _sickTimer.Enabled = true;
        }

        private static void OnSickEvent(Object source, ElapsedEventArgs e)
        {
            RmKirill.Kirill.GetSickLeave(Rand.NextDouble() > 0.5, RmKirill.Kirill);
            RmVano.Vano.GetSickLeave(Rand.NextDouble() > 0.5, RmVano.Vano);
            RmPetrovich.Petrovich.GetSickLeave(Rand.NextDouble() > 0.5, RmPetrovich.Petrovich);
            RmSanSanuch.SanSanuch.GetSickLeave(Rand.NextDouble() > 0.5, RmSanSanuch.SanSanuch);
        }

        public static double CalculateSecondsToNextDay()
        {
            DateTime closingTime = GetGameTime().AddDays(1).Subtract(GetGameTime().TimeOfDay);
            TimeSpan periodToWait = closingTime - GetGameTime();
            double seconds = periodToWait.TotalSeconds;
            seconds = Math.Abs(seconds / 720);
            return seconds;
        }

        public static void SetNextDayTimer(double seconds)
        {     
            _nextDayTimer = new Timer(seconds * Thousand);
            _nextDayTimer.Elapsed += OnDayEndEvent;
            _nextDayTimer.AutoReset = false;
            _nextDayTimer.Enabled = true;
            RepairAutomationTool.AddNewCustomers(2);
           
        }

        private static void OnDayEndEvent(Object source, ElapsedEventArgs e)
        {
            ShopManager.Dss.Display();
            ShopManager.Dss.Clear();
            SetNextDayTimer(120);
            GetGameTimeToScreen();
            FileFolderManagementService.CreateFolder();
        }

        public static int ConvertToRealTime(double gameHours)
        {
            var secondsRealTime = (int)(gameHours * 3600) / 720;
            return secondsRealTime;
        }

        public static void GetGameTimeToScreen()
        {        
            Console.WriteLine($"The GameTime Now is {GetGameTime().ToString(DatetimeDormat, CultureInfo.CurrentUICulture)}");
        }

        public static DateTime GetGameTime()
        {
            DateTime timeNow = DateTime.Now;
            TimeSpan differenceFromAppStart = timeNow - TimeInstance._gameStartRealTime;
            double difference = differenceFromAppStart.TotalSeconds * 720;
            var newGameTime = TimeInstance._gameStartGameTime;
            newGameTime = newGameTime.AddSeconds(difference);
            return newGameTime;
        }
    }
}