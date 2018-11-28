using System;
using System.Collections.Generic;
using System.Threading;
using AutoRepairShop.Data.Base;
using AutoRepairShop.Data.Models.Humans;
using AutoRepairShop.Data.Queues;
using AutoRepairShop.Data.Repository;
using AutoRepairShop.Services;

namespace AutoRepairShop.WorkFlow
{
    sealed class ShopManager : Human
    {
        private bool _isGarageEmpty = true;
        private int _costOfServices = 0;
        public static StockManager StMan = new StockManager();
        public static GarageStockManager GarStMan = new GarageStockManager();
        LoggerService _lrw = new LoggerService();
        //private static RM_Kirill _kirill = new RM_Kirill();
        //private static RM_Petrovich _petrovich = new RM_Petrovich();
        //private static RM_Vano _vano = new RM_Vano();
        //private static RM_SanSanuch _sanSanuch = new RM_SanSanuch();
        private static Dictionary<string, int> _servicesCatalogue = new Dictionary<string, int>();
        private static int _balance;
        public static readonly ShopManager Lucy = new ShopManager();
        public static CustomerQueue<Customer> CustomerQueue = new CustomerQueue<Customer>();

        private ShopManager()
        {
            Name = "Lucy";   
            _servicesCatalogue.Add("Diagnoze", 50);
            _servicesCatalogue.Add("Repair", 250);
            _servicesCatalogue.Add("Modify", 500);
            _servicesCatalogue.Add("Replace", 200);
            _servicesCatalogue.Add("ReplaceLiquid", 50);
            _balance = BalanceReadWrite.Read();
        }

        public override void Say(string message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            base.Say(message);
            Console.ResetColor();
        }

        public static void CustomerOnHold()
        {
            Lucy.Say($"All repair men are busy at this moment!");//ask to wait and menu?
        }

        public static void TakeCar(Car customerCar, int choice)
        {
            Lucy.Say("Will do!");
            switch (choice)
            {

                case 1: //diagnoze
                    if (!RmPetrovich.Petrovich.IsBusy)
                    {
                        RmPetrovich.Petrovich.DiagnozeCar(customerCar);
                    }
                    else if (!RmVano.Vano.IsBusy)
                    {
                        RmVano.Vano.DiagnozeCar(customerCar);
                    }
                    else if (!RmKirill.Kirill.IsBusy)
                    {
                        RmKirill.Kirill.DiagnozeCar(customerCar);
                    }
                    else
                    {
                        CustomerOnHold();
                    }
                    AddCostToTotal("Diagnoze", 0);
                    Menu.RepairMenu();
                    break;

                case 2: //repair
                    CarPart part = CustomerQueue.Pop().PointAtCarPart();
                    if (!RmPetrovich.Petrovich.IsBusy)
                    {
                        RmPetrovich.Petrovich.MakeRepairs(part);
                    }
                    else if (!RmVano.Vano.IsBusy)
                    {
                        RmVano.Vano.MakeRepairs(part);
                    }
                    else if (!RmKirill.Kirill.IsBusy)
                    {
                        RmKirill.Kirill.MakeRepairs(part);
                    }
                    else
                    {
                        CustomerOnHold();
                    }
                    AddCostToTotal("Repair", 0);
                    Menu.RepairMenu();
                    break;

                case 3: // mods
                    if (RmKirill.Kirill.IsBusy)
                    {
                        CustomerOnHold();
                    }
                    int cost = RmKirill.Kirill.Modify(customerCar);
                    if (cost != 0)
                    {
                        AddCostToTotal("Modify", cost);
                    }
                    Menu.RepairMenu();
                    break;
                case 4: //replace
                    CarPart brokenPart = CustomerQueue.Pop().PointAtCarPart();
                    if (RmVano.Vano.IsBusy)
                    {
                        CustomerOnHold();
                    }
                    cost = RmVano.Vano.ReplacePart(brokenPart, customerCar);
                    if (cost != 0)
                    {
                        AddCostToTotal("Replace", cost);
                    }
                    Menu.RepairMenu();
                    break;

                case 5: //liquids
                    if (RmPetrovich.Petrovich.IsBusy)
                    {
                        CustomerOnHold();
                    }
                    AddCostToTotal("ReplaceLiquid", RmPetrovich.Petrovich.ReplaceFluid(CustomerQueue.Pop().MyCar));
                    Menu.RepairMenu();
                    break;

                default:
                    break;
            }
        }

        public static void AddCostToTotal(string service, int partCost)
        {
            _servicesCatalogue.TryGetValue(service, out int currentCost);
            currentCost += partCost;
            Lucy.Say($"{service} complete. The cost is {currentCost} USD.");
            Lucy._costOfServices += currentCost;
        }

        public static Customer GetCurrentCustomer()
        {
            return CustomerQueue.Pop();
        }

        public static void AcceptNewCustomer(Customer customer)
        {
            Lucy._costOfServices = 0;
            Lucy.Say($"Greetings {CustomerQueue.Pop().Name}! My name is {Lucy.Name} and I will be your Repair Shop Manager today.");
            Lucy.Say($"Please leave your {CustomerQueue.Pop().MyCar.Name} at the parking lot.");
            ShopManager.CheckWorkerBusy();
            Lucy._isGarageEmpty = false;
            Lucy._lrw.StoreLog($"New customer: {CustomerQueue.Pop().Name}, Car: {CustomerQueue.Pop().MyCar.Name}, Customer registered");    
            Lucy.Say($"{CustomerQueue.Pop().Name}, what shall we do with your {CustomerQueue.Pop().MyCar.Name}?");
            Menu.RepairMenu();
        }      

        public static void ReleaseCustomer(Customer customer)
        {
            // Calculate amount, talk to customer, take money
            Lucy._isGarageEmpty = true;
            Lucy._costOfServices -= Lucy._costOfServices * (customer.MyDiscounts.GetDiscountRate() / 100);
            customer.MyDiscounts.PunchDiscountCard();
            Lucy.Say($"Your total for today is {Lucy._costOfServices}.");
            customer.MakePayment();
            Lucy.Say($"Have a great day, {customer.Name}");
            Lucy._lrw.StoreLog($"{CustomerQueue.Pop().Name}, Car: {CustomerQueue.Pop().MyCar.Name}, Customer released. Amount: {Lucy._costOfServices}");
            BalanceReadWrite.Write(_balance += Lucy._costOfServices);
        }

        public static void LastTimeLog()
        {
            Lucy._lrw.StoreTime();
        }

        public static int CustomerReplyHandler(int reply)
        {
            if (reply == -1)
            {
                Lucy.Say($"Ok then!");
                ReleaseCustomer(CustomerQueue.Pop());
                return 0;
            }
            return reply;
        }

        public static void Thank()
        {
            Lucy.Say($"Oh thank you, thank you *blashes*");
        }

        public static void CheckWorkerBusy()
        {
            
        }

        public static DateTime WhatTimeIsItNow()
        {
            return Menu.PassMeTime();
        }

        public static bool WorkingHours()
        {
            DateTime now = WhatTimeIsItNow();
            return 7 < now.Hour && now.Hour < 24 && now.DayOfWeek != DayOfWeek.Sunday;
        }

        public static bool MovePartToGarage(string partName)
        {
            if (StMan.Read(partName))
            {
                GarStMan.AddPartFromStock(partName);
                //invoke delete from StMan and add part to GarMan
                return true;
            }
            else
            {
                //no part in stock 
                return false;
            }
        }

        public static void HandleProblematicCustomer()
        {
            Thread.Sleep(5000);
        }
    }
}
