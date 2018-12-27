using System;
using System.Timers;
using AutoRepairShop.Tools;
using AutoRepairShop.WorkFlow;

namespace AutoRepairShop.Services
{
    internal class WeeklyPaymentService
    {
        private static Timer _utilitiesTimer;
        private static double _lastWeekBalance;
        private static double _utilitiesAmount;

        public WeeklyPaymentService()
        {
            _lastWeekBalance = BalanceReadWrite.Read();
            SetUtilitiesPaymentTimer();
        }

        private static void SetUtilitiesPaymentTimer()
        {
            _utilitiesTimer = new Timer(TimeTool.ConvertToRealTime(168) * TimeTool.Thousand);
            _utilitiesTimer.Elapsed += OnWeeklyPaymentEvent;
            _utilitiesTimer.AutoReset = true;
            _utilitiesTimer.Enabled = true;
        }

        private static void OnWeeklyPaymentEvent(Object source, ElapsedEventArgs e)
        {
            _utilitiesAmount = (ShopManager.Balance - _lastWeekBalance) * 0.15;
            ShopManager.Balance -= _utilitiesAmount;
            BalanceReadWrite.Write(ShopManager.Balance);
            Console.WriteLine("Utilities have been paid in full!");
            _lastWeekBalance = ShopManager.Balance;
        }
    }
}
