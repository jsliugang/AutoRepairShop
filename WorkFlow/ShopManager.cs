using System;
using System.Collections.Generic;
using System.Threading;
using AutoRepairShop.Data.Lists;
using AutoRepairShop.Data.Models.CarTypes;
using AutoRepairShop.Data.Models.Humans;
using AutoRepairShop.Data.Repository;
using AutoRepairShop.Services;
using AutoRepairShop.Tools;

namespace AutoRepairShop.WorkFlow
{
    internal sealed class ShopManager : Human
    {
        private bool _isGarageEmpty = true;
        private int _costOfServices = 0;
        public static StockManager StMan = new StockManager();
        public static GarageStockManager GarStMan = new GarageStockManager();
        private readonly LoggerService _lrw = new LoggerService();
        private static readonly Dictionary<string, int> ServicesCatalogue = new Dictionary<string, int>();
        private static int _balance;
        public static List<string> ModificationsOffer = new List<string>();
        public static Customer CurrentCustomer;

        private ShopManager()
        {
            Name = "Lucy";   
            ServicesCatalogue.Add("Diagnoze", 50);
            ServicesCatalogue.Add("Repair", 250);
            ServicesCatalogue.Add("Modify", 500);
            ServicesCatalogue.Add("Replace", 200);
            ServicesCatalogue.Add("ReplaceLiquid", 50);
            _balance = BalanceReadWrite.Read();
            ModificationsOffer.Add("CustomBonnet");
            ModificationsOffer.Add("Decals");
            ModificationsOffer.Add("ExhaustPipe");
            ModificationsOffer.Add("NO2");
            ModificationsOffer.Add("Spinners");
            ModificationsOffer.Add("Spoiler");
            ModificationsOffer.Add("SportSuspension");
            ModificationsOffer.Add("TitaniumWipers");
        }

        public static ShopManager Lucy { get { return Nested.lucy; } }

        private class Nested
        {
            static Nested()
            {
            }
            internal static readonly ShopManager lucy = new ShopManager();
        }

        public static void AcceptNewCustomer(Customer customer)
        {
            CurrentCustomer = customer;
            CustomerQueue<Customer>.Dequeue();
            Lucy._costOfServices = 0;
            Lucy.Say($"Greetings {CurrentCustomer.Name}! My name is {Lucy.Name} and I will be your Repair Shop Manager today.");
            Lucy.Say($"Please leave your {CurrentCustomer.MyCar.Name} at the parking lot.");
            if (Lucy._isGarageEmpty)
            {
                Lucy._isGarageEmpty = false;
            }
            Lucy._lrw.StoreLog($"New customer: {CurrentCustomer.Name}, Car: {CurrentCustomer.MyCar.Name}, Customer registered");
            Lucy.Say($"{CurrentCustomer.Name}, what shall we do with your {CurrentCustomer.MyCar.Name}?");
            RepairAutomationTool.MakeRepairChoice();
        }

        public static void ReleaseCustomer(Customer customer)
        {
            Lucy.Say($"Ok then!");
            Lucy._isGarageEmpty = true;
            Lucy._costOfServices -= Lucy._costOfServices * (customer.MyDiscounts.GetDiscountRate() / 100);
            customer.MyDiscounts.PunchDiscountCard();
            Lucy.Say($"Your total for today is {Lucy._costOfServices}.");
            customer.MakePayment();
            Lucy.Say($"Have a great day, {customer.Name}");
            Lucy._lrw.StoreLog($"{CurrentCustomer.Name}, Car: {CurrentCustomer.MyCar.Name}, Customer released. Amount: {Lucy._costOfServices}");
            BalanceReadWrite.Write(_balance += Lucy._costOfServices);
        }

        public static void CustomerOnHold()
        {
            Lucy.Say($"All repair men are busy at this moment!");//ask to wait?
        }

        public static void ShopIsClosed()
        {
            Lucy.Say($"The Auto Repair Shop will open at 8 am tomorrow! We are not working at night time: {MsgDecoratorTool.PassMeTime()}");
        }

        public static void ProcessOrder(int choice, string part)
        {
            Car customerCar = CurrentCustomer.MyCar;
            Lucy.Say("Will do!");
            switch (choice)
            {
                case 1: //diagnoze
                    ICanDiagnoze<RepairMan> diagnozeMan = CanDiagnozeList.RepairMen.Find(x => x.IsBusy == false);
                    if (diagnozeMan != null)
                    {
                        diagnozeMan.DiagnozeCar(customerCar);
                    }                  
                    else
                    {
                        CustomerOnHold();
                    }
                    AddCostToTotal("Diagnoze", 0);
                    break;

                case 2: //repair
                    ICanRepair<RepairMan> repairMan = CanRepairList.RepairMen.Find(x => x.IsBusy == false);
                    repairMan = CanRepairList.RepairMen.Find(x => x.IsBusy == false);
                    if (repairMan != null)
                    {
                        repairMan.MakeRepairs(part);
                    }
                    else
                    {
                        CustomerOnHold();
                    }
                    AddCostToTotal("Repair", 0);
                    break;

                case 3: // mods
                    int cost = 0;
                    ICanCustomize<RepairMan> customizeMan = CanCustomizeList.RepairMen.Find(x => x.IsBusy == false);
                    if (customizeMan != null)
                    {
                        cost = customizeMan.Modify(customerCar, part);
                    }
                    else
                    {
                        CustomerOnHold();
                    }
                    if (cost != 0)
                    {
                        AddCostToTotal("Modify", cost);
                    }
                    break;
                case 4: //replace
                    cost = 0;
                    ICanReplace<RepairMan> replaceMan = CanReplaceList.RepairMen.Find(x => x.IsBusy == false);
                    if (replaceMan != null)
                    {
                        cost = replaceMan.ReplacePart(part, customerCar);
                    }
                    else
                    {
                        CustomerOnHold();
                    }
                    if (cost != 0)
                    {
                        AddCostToTotal("Replace", cost);
                    }
                    break;

                case 5: //liquids 
                    ICanReplaceFluids<RepairMan> replaceFluidsMan = CanReplaceFluidsList.RepairMen.Find(x => x.IsBusy == false);
                    if (replaceFluidsMan != null)
                    {
                        AddCostToTotal("ReplaceLiquid", replaceFluidsMan.ReplaceFluid(CurrentCustomer.MyCar, part));
                    }
                    else
                    {
                        CustomerOnHold();
                    }
                    break;
            }
        }

        public static void AddCostToTotal(string service, int partCost)
        {
            ServicesCatalogue.TryGetValue(service, out int currentCost);
            currentCost += partCost;
            Lucy.Say($"{service} complete. The cost is {currentCost} USD.");
            Lucy._costOfServices += currentCost;
        }

        public static void LastTimeLog()
        {
            Lucy._lrw.StoreTime();
        }

        public static void Thank()
        {
            Lucy.Say($"Oh thank you, thank you *blashes*");
        }

        public static DateTime WhatTimeIsItNow()
        {
            return MsgDecoratorTool.PassMeTime();
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

        public override void Say(string message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            base.Say(message);
            Console.ResetColor();
        }
    }
}