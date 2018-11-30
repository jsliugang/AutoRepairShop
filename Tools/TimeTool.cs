using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Timers;
using AutoRepairShop.Data.Models.Humans;
using AutoRepairShop.Services;
using Timer = System.Timers.Timer;

namespace AutoRepairShop.Tools
{
    internal sealed class TimeTool
    {
        private const int Thousand = 1000;
        private const string DatetimeDormat = "MM/dd/yyyy h:mm tt";
        private Timer _sickTimer;
        private Timer _newCustomerTimer;
        private readonly DateTime _gameStartRealTime;
        private readonly DateTime _gameStartGameTime;
        public static TimeTool TimeInstance { get { return Nested.Time; } }
        private ErrorLogService errLog = new ErrorLogService();

        private TimeTool()
        {
            SetSickTimer();
            SetNewCustomerTimer();
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
            {

            }

            internal static readonly TimeTool Time = new TimeTool();
        }

        private void SetNewCustomerTimer()
        {
            _newCustomerTimer = new Timer(ConvertToGameTime(new Random().NextDouble() * new Random().Next(1,10)) * Thousand);
            _newCustomerTimer.Elapsed += OnNewCustomerEvent;
            _newCustomerTimer.AutoReset = true;
            _newCustomerTimer.Enabled = true;
        }

        private static void OnNewCustomerEvent(Object source, ElapsedEventArgs e)
        {
            int hour = TimeInstance.GetGameTime().Hour;
            if (7 < hour && hour < 24)
            {
                RepairAutomationTool.AddCustomer();
            }

        }

        private void SetSickTimer()
        {
            _sickTimer = new Timer(ConvertToGameTime(168)*Thousand);
            _sickTimer.Elapsed += OnSickEvent;
            _sickTimer.AutoReset = true;
            _sickTimer.Enabled = true;
        }

        private static void OnSickEvent(Object source, ElapsedEventArgs e)
        {
            Random rand = new Random();
            RmKirill.Kirill.GetSickLeave(rand.NextDouble() > 0.5);
            RmVano.Vano.GetSickLeave(rand.NextDouble() > 0.5);
            RmPetrovich.Petrovich.GetSickLeave(rand.NextDouble() > 0.5);
            RmSanSanuch.SanSanuch.GetSickLeave(rand.NextDouble() > 0.5);
        }

        public void SetNextDayTimer()
        {
            GetGameTimeToScreen();
            DateTime nextWorkingDateStart = GetGameTime().AddDays(1).Subtract(GetGameTime().TimeOfDay);
            nextWorkingDateStart = nextWorkingDateStart.AddHours(8).Subtract(new TimeSpan(1, 0, 0, 0, 0));
            TimeSpan periodToWait = nextWorkingDateStart - GetGameTime();
            double seconds = periodToWait.TotalSeconds;
            seconds = Math.Abs(seconds/720);
            Console.WriteLine($"Seconds to wait = {seconds}");
            errLog.StoreLog($"{GetGameTime()}: Wait for {seconds} seconds.");
            Thread.Sleep((int) seconds * Thousand);
            errLog.StoreLog($"{GetGameTime()}: Resume game.");

        }

        public int ConvertToGameTime(double gameHours)
        {
            int secondsRealTime = (int)(gameHours * 3600) / 720;
            return secondsRealTime;
        }

        public void GetGameTimeToScreen()
        {        
            Console.WriteLine($"The GameTime Now is {GetGameTime().ToString(DatetimeDormat, CultureInfo.CurrentUICulture)}");
        }

        public DateTime GetGameTime()
        {
            DateTime timeNow = DateTime.Now;
            TimeSpan differenceFromAppStart = timeNow - _gameStartRealTime;
            double difference = differenceFromAppStart.TotalSeconds * 720;
            var newGameTime = _gameStartGameTime;
            newGameTime = newGameTime.AddSeconds(difference);
            return newGameTime;
        }
    }
}
