using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarParts;
using AutoRepairShop.Classes.Cars.CarTypes;
using AutoRepairShop.Classes.Managers;
using AutoRepairShop.Classes.Data;

namespace AutoRepairShop.Classes.Humans
{
    sealed class ShopManager : Human
    {
        //private Customer _customer;
        private bool _isGarageEmpty = true;
        private int _costOfServices = 0;
        public static StockManager StMan = new StockManager();
        public static GarageStockManager GarStMan = new GarageStockManager();
        LogReadWrite _lrw = new LogReadWrite();
        private static RM_Kirill _kirill = new RM_Kirill();
        private static RM_Petrovich _petrovich = new RM_Petrovich();
        private static RM_Vano _vano = new RM_Vano();
        private static RM_SanSanuch _sanSanuch = new RM_SanSanuch();
        private static Dictionary<string, int> _servicesCatalogue = new Dictionary<string, int>();
        private static int _balance;
        public static readonly ShopManager Lucy = new ShopManager();
        public static CustomerQueue<Customer> _customerQueue = new CustomerQueue<Customer>();

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
                    if (!_petrovich.IsBusy)
                    {
                        _petrovich.DiagnozeCar(customerCar);
                    }
                    else if (!_vano.IsBusy)
                    {
                        _vano.DiagnozeCar(customerCar);
                    }
                    else if (!_kirill.IsBusy)
                    {
                        _kirill.DiagnozeCar(customerCar);
                    }
                    else
                    {
                        CustomerOnHold();
                    }
                    AddCostToTotal("Diagnoze", 0);
                    Menu.RepairMenu();
                    break;

                case 2: //repair
                    CarPart part = _customerQueue.Pop().PointAtCarPart();
                    if (!_petrovich.IsBusy)
                    {
                        _petrovich.MakeRepairs(part);
                    }
                    else if (!_vano.IsBusy)
                    {
                        _vano.MakeRepairs(part);
                    }
                    else if (!_kirill.IsBusy)
                    {
                        _kirill.MakeRepairs(part);
                    }
                    else
                    {
                        CustomerOnHold();
                    }
                    AddCostToTotal("Repair", 0);
                    Menu.RepairMenu();
                    break;

                case 3: // mods
                    if (_kirill.IsBusy)
                    {
                        CustomerOnHold();
                    }
                    int cost = _kirill.Modify(customerCar);
                    if (cost != 0)
                    {
                        AddCostToTotal("Modify", cost);
                    }
                    Menu.RepairMenu();
                    break;
                case 4: //replace
                    CarPart brokenPart = _customerQueue.Pop().PointAtCarPart();
                    if (_vano.IsBusy)
                    {
                        CustomerOnHold();
                    }
                    cost = _vano.ReplacePart(brokenPart, customerCar);
                    if (cost != 0)
                    {
                        AddCostToTotal("Replace", cost);
                    }
                    Menu.RepairMenu();
                    break;

                case 5: //liquids
                    if (_petrovich.IsBusy)
                    {
                        CustomerOnHold();
                    }
                    AddCostToTotal("ReplaceLiquid", _petrovich.ReplaceFluid(_customerQueue.Pop().MyCar));
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
            return _customerQueue.Pop();
        }

        public static void AcceptNewCustomer(Customer customer)
        {
            //Lucy._customer = customer; //redundant - shall be removed later
            Lucy._costOfServices = 0;
            Lucy.Say($"Greetings {_customerQueue.Pop().Name}! My name is {Lucy.Name} and I will be your Repair Shop Manager today.");
            Lucy.Say($"Please leave your {_customerQueue.Pop().MyCar.Name} at the parking lot.");
            ShopManager.CheckWorkerBusy();
            Lucy._isGarageEmpty = false;
            Lucy._lrw.StoreLog($"New customer: {_customerQueue.Pop().Name}, Car: {_customerQueue.Pop().MyCar.Name}, Customer registered");    
            Lucy.Say($"{_customerQueue.Pop().Name}, what shall we do with your {_customerQueue.Pop().MyCar.Name}?");
            Menu.RepairMenu();
        }      

        public static void ReleaseCustomer(Customer customer)
        {
            // Calculate amount, talk to customer, take money
            Lucy._isGarageEmpty = true;
            Lucy.Say($"Your total for today is {Lucy._costOfServices}.");
            customer.MakePayment();
            Lucy.Say($"Have a great day, {customer.Name}");
            Lucy._lrw.StoreLog($"{_customerQueue.Pop().Name}, Car: {_customerQueue.Pop().MyCar.Name}, Customer released. Amount: {Lucy._costOfServices}");
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
                ReleaseCustomer(_customerQueue.Pop());
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
