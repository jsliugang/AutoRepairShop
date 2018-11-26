using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarParts;
using AutoRepairShop.Classes.Cars.CarTypes;
using AutoRepairShop.Classes.Managers;

namespace AutoRepairShop.Classes.Humans
{
    sealed class ShopManager : Human
    {
        private Customer _customer;
        private bool IsGarageEmpty = true;
        private int costOfServices = 0;
        public static StockManager StMan = new StockManager();
        public static GarageStockManager GarStMan = new GarageStockManager();
        LogReadWrite LRW = new LogReadWrite();
        private static RM_Kirill Kirill = new RM_Kirill();
        private static RM_Petrovich Petrovich = new RM_Petrovich();
        private static RM_Vano Vano = new RM_Vano();
        private static Dictionary<string, int> ServicesCatalogue = new Dictionary<string, int>();

        public static readonly ShopManager Lucy = new ShopManager();

        private ShopManager()
        {
            Name = "Lucy";   
            ServicesCatalogue.Add("Diagnoze", 50);
            ServicesCatalogue.Add("Repair", 250);
            ServicesCatalogue.Add("Modify", 500);
            ServicesCatalogue.Add("Replace", 200);
            ServicesCatalogue.Add("ReplaceLiquid", 50);
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
                    if (!Petrovich.IsBusy)
                    {
                        Petrovich.DiagnozeCar(customerCar);
                    }
                    else if (!Vano.IsBusy)
                    {
                        Vano.DiagnozeCar(customerCar);
                    }
                    else if (!Kirill.IsBusy)
                    {
                        Kirill.DiagnozeCar(customerCar);
                    }
                    else
                    {
                        CustomerOnHold();
                    }
                    AddCostToTotal("Diagnoze", 0);
                    Menu.RepairMenu();
                    break;

                case 2: //repair
                    CarPart part = Lucy._customer.PointAtCarPart();
                    if (!Petrovich.IsBusy)
                    {
                        Petrovich.MakeRepairs(part);
                    }
                    else if (!Vano.IsBusy)
                    {
                        Vano.MakeRepairs(part);
                    }
                    else if (!Kirill.IsBusy)
                    {
                        Kirill.MakeRepairs(part);
                    }
                    else
                    {
                        CustomerOnHold();
                    }
                    AddCostToTotal("Repair", 0);
                    Menu.RepairMenu();
                    break;

                case 3: // mods
                    if (Kirill.IsBusy)
                    {
                        CustomerOnHold();
                    }
                    int cost = Kirill.Modify(customerCar);
                    if (cost != 0)
                    {
                        AddCostToTotal("Modify", cost);
                    }
                    Menu.RepairMenu();
                    break;
                case 4: //replace
                    CarPart brokenPart = Lucy._customer.PointAtCarPart();
                    if (Vano.IsBusy)
                    {
                        CustomerOnHold();
                    }
                    cost = Vano.ReplacePart(brokenPart, customerCar);
                    if (cost != 0)
                    {
                        AddCostToTotal("Replace", cost);
                    }
                    Menu.RepairMenu();
                    break;

                case 5: //liquids
                    if (Petrovich.IsBusy)
                    {
                        CustomerOnHold();
                    }
                    AddCostToTotal("ReplaceLiquid", Petrovich.ReplaceFluid(Lucy._customer.MyCar));
                    Menu.RepairMenu();
                    break;

                default:
                    break;
            }
        }

        public static void AddCostToTotal(string service, int partCost)
        {
            ServicesCatalogue.TryGetValue(service, out int currentCost);
            currentCost += partCost;
            Lucy.Say($"{service} complete. The cost is {currentCost} USD.");
            Lucy.costOfServices += currentCost;
        }

        public static Customer GetCurrentCustomer()
        {
            return Lucy._customer;
        }

        public static void AcceptNewCustomer(Customer customer)
        {
            Lucy._customer = customer;
            Lucy.costOfServices = 0;
            Lucy.Say($"Greetings {Lucy._customer.Name}! My name is {Lucy.Name} and I will be your Repair Shop Manager today.");
            Lucy.Say($"Please leave your {Lucy._customer.MyCar.Name} at the parking lot.");
            ShopManager.CheckWorkerBusy();
            Lucy.IsGarageEmpty = false;
            Lucy.LRW.StoreLog($"New customer: {Lucy._customer.Name}, Car: {Lucy._customer.MyCar.Name}, Customer registered");    
            
            Lucy.Say($"{Lucy._customer.Name}, what shall we do with your {Lucy._customer.MyCar.Name}?");
            Menu.RepairMenu();
        }

        

        public static void ReleaseCustomer(Customer customer)
        {
            // Calculate amount, talk to customer, take money
            Lucy.IsGarageEmpty = true;
            Lucy.Say($"Your total for today is {Lucy.costOfServices}.");
            customer.MakePayment();
            Lucy.Say($"Have a great day, {customer.Name}");
            Lucy.LRW.StoreLog($"{Lucy._customer.Name}, Car: {Lucy._customer.MyCar.Name}, Customer released. Amount: {Lucy.costOfServices}");
        }

        public static void LastTimeLog()
        {
            Lucy.LRW.StoreTime();
        }

        public static int CustomerReplyHandler(int reply)
        {
            if (reply == -1)
            {
                Lucy.Say($"Ok then!");
                ReleaseCustomer(Lucy._customer);
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
    }
}
