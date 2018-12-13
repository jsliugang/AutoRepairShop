using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Timers;
using AutoRepairShop.Tools;
using AutoRepairShop.WorkFlow;

namespace AutoRepairShop.Services
{
    class MonthlyPaymentService
    {
        private static Timer _paymentTimer;
        private static double _lastMonthBalance;
        private static double _bribeAmount;

        public MonthlyPaymentService()
        {
            _lastMonthBalance = BalanceReadWrite.Read();
            SetPaymentTimer();
        }

        private static void SetPaymentTimer()
        {
            _paymentTimer = new Timer(TimeTool.ConvertToRealTime(672) * TimeTool.Thousand);
            _paymentTimer.Elapsed += OnMonthlyPaymentEvent;
            _paymentTimer.AutoReset = true;
            _paymentTimer.Enabled = true;
        }

        private static void OnMonthlyPaymentEvent(Object source, ElapsedEventArgs e)
        {
            _bribeAmount = (ShopManager.Balance - _lastMonthBalance) * 0.05;
            ShopManager.Balance -= _bribeAmount;
            BalanceReadWrite.Write(ShopManager.Balance);
            Console.WriteLine("Bribes have been paid in full!");
            _lastMonthBalance = ShopManager.Balance;
            PaySalary();
        }

        private static void PaySalary()
        {
            foreach (var repairMan in ShopManager.Lucy.Salary.Keys)
            {
                repairMan.GetSalary(ShopManager.Lucy.Salary[repairMan]+100);
                ShopManager.Balance -= ShopManager.Lucy.Salary[repairMan]-100;
                ShopManager.Lucy.Salary[repairMan] = 0;
                BalanceReadWrite.Write(ShopManager.Balance);
            }
            ShopManager.Lucy.Salary.Keys.ToList().ForEach(x => ShopManager.Lucy.Salary[x] = 0);
            ShopManager.Lucy.GetSalary();
        }
    }
}
