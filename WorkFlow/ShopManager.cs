using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        private static double _balance;
        public static StockManager StMan = new StockManager();
        public static GarageStockManager GarStMan = new GarageStockManager();
        private readonly FileLoggerService _lrw = new FileLoggerService();
        private static readonly Dictionary<string, int> ServicesCatalogue = new Dictionary<string, int>();
        public static List<string> ModificationsOffer = new List<string>();
        public static Customer CurrentCustomer;
        public static CustomerBalanceData CurrentCustomerBalance;
        public static DailyStatService Dss = new DailyStatService();
        private static Dictionary<Customer, CustomerBalanceData> OnHoldCustomerBalance = new Dictionary<Customer, CustomerBalanceData>();
        public static List<Customer> Customers;
        public static List<Customer> CustomersOnHold;

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
            Customers = new List<Customer>();
            CustomersOnHold = new List<Customer>();
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
            CurrentCustomerBalance = new CustomerBalanceData(0, 0);
            CustomerQueue<Customer>.Dequeue(Customers);
            CurrentCustomer.StopWaitForServicesTimer();
            Dss.AddCustomer(CurrentCustomer); //daily logging service
            Lucy.Say($"Greetings {CurrentCustomer.Name}! My name is {Lucy.Name} and I will be your Repair Shop Manager today.");
            Lucy.Say($"Please leave your {CurrentCustomer.MyCar.Name} at the parking lot.");
            if (Lucy._isGarageEmpty)
            {
                Lucy._isGarageEmpty = false;
            }
            Lucy._lrw.StoreLog($"New customer: {CurrentCustomer.Name}, Car: {CurrentCustomer.MyCar.Name}, Customer registered");
            Lucy.Say($"{CurrentCustomer.Name}, what shall we do with your {CurrentCustomer.MyCar.Name}?");
            CurrentCustomer.MakeDiagnosticsOrder();
            RepairAutomationTool.MakeRepairChoice();
        }

        public static void ResumeWorkingWithCustomer(Customer customer)
        {
            CurrentCustomer = customer;
            CustomerQueue<Customer>.Dequeue(CustomersOnHold);
            CurrentCustomerBalance = OnHoldCustomerBalance[CurrentCustomer];
            OnHoldCustomerBalance.Remove(CurrentCustomer);
            CurrentCustomer.StopWaitForServicesTimer();
            Lucy.Say($"Thank you for waiting. We are ready to complete your work orders, {CurrentCustomer.Name}!");
            if (Lucy._isGarageEmpty)
            {
                Lucy._isGarageEmpty = false;
            }
            RepairAutomationTool.MakeRepairChoice();
        }

        public static void ReleaseCustomer(Customer customer)
        {
            Lucy.Say($"Ok then!");
            Lucy._isGarageEmpty = true;
            Console.WriteLine($"Cost of services: {CurrentCustomerBalance.CostOfServices}");
            Console.WriteLine($"Cost of parts: {CurrentCustomerBalance.CostOfParts}");
            Lucy.Say($"Your total for today is {CurrentCustomerBalance.CalculateTotal(CurrentCustomer.MyDiscounts.GetDiscountRate())}.");
            CurrentCustomer.MakePayment();
            CurrentCustomer.MyDiscounts.PunchDiscountCard();
            Lucy.Say($"Have a great day, {customer.Name}");
            Lucy._lrw.StoreLog($"{CurrentCustomer.Name}, Car: {CurrentCustomer.MyCar.Name}, Customer released. Amount: {CurrentCustomerBalance.CostOfServices}");
            BalanceReadWrite.Write(_balance+AllocateMoney(CurrentCustomerBalance.CostOfServices));
            Dss.FinalizeCustomer(CurrentCustomer, CurrentCustomerBalance.CalculateTotal(CurrentCustomer.MyDiscounts.GetDiscountRate()));
        }

        public static void CustomerOnHold()
        {
            Lucy.Say($"All repair men are busy at this moment! Please a little...");
            OnHoldCustomerBalance.Add(CurrentCustomer, CurrentCustomerBalance);
            CustomerQueue<Customer>.Enqueue(CurrentCustomer, CustomersOnHold);
            CurrentCustomer.SetWaitForServicesTimer();
        }

        public static void ShopIsClosed()
        {
            Lucy.Say($"The Auto Repair Shop will open at 8 am tomorrow! We are not working at night time: {MsgDecoratorTool.PassMeTime()}");
        }

        public static double AllocateMoney(double shopIncome)
        {
            double discount = shopIncome * CurrentCustomer.MyDiscounts.GetDiscountRate() / 100;
            double salary = shopIncome - discount;
            salary = salary * 40 / 100;
            return salary;
        }

        public static double CalculateSalary(double income)
        {
            double discount = income * CurrentCustomer.MyDiscounts.GetDiscountRate() / 100;
            double salary = income - discount;
            salary = salary * 60 / 100;
            return salary;
        }

        public static void PayRepairMan(double salary, RepairMan rm)
        {
            if (rm.Name == "SanSanuch")
            {
                _balance -= salary;
                salary *= 2;
            }
            rm.GetSalary(salary);
        }

        public static void ProcessOrder(int choice, string part)
        {
            Car customerCar = CurrentCustomer.MyCar;
            Lucy.Say("Will do!");
            switch (choice)
            {
                case 1: //diagnoze
                    ICanDiagnoze<RepairMan> diagnozeMan = CanDiagnozeList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 1);
                    if (diagnozeMan != null)
                    {
                        diagnozeMan.DiagnozeCar(customerCar);
                        Dss.AddWorkOrder((RepairMan)diagnozeMan, "Diagnoze", ServicesCatalogue["Diagnoze"], 0);
                        CurrentCustomerBalance.CostOfServices += ServicesCatalogue["Diagnoze"];
                        PayRepairMan(CalculateSalary(ServicesCatalogue["Diagnoze"]), (RepairMan)diagnozeMan);
                        Console.WriteLine($"Cost of services: {CurrentCustomerBalance.CostOfServices}");
                        Console.WriteLine($"Cost of parts: {CurrentCustomerBalance.CostOfParts}");
                    }                  
                    else
                    {
                        CustomerOnHold();
                    }
                    //AddCostToTotal("Diagnoze", 0);//remove?
                    break;

                case 2: //repair
                    ICanRepair<RepairMan> repairMan = CanRepairList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 1);
                    repairMan = CanRepairList.RepairMen.Find(x => x.IsBusy == false);
                    if (repairMan != null)
                    {
                        repairMan.MakeRepairs(part);
                        Dss.AddWorkOrder((RepairMan) repairMan, "Repair", ServicesCatalogue["Repair"], 0);
                        CurrentCustomerBalance.CostOfServices += ServicesCatalogue["Repair"];
                        PayRepairMan(CalculateSalary(ServicesCatalogue["Repair"]), (RepairMan)repairMan);
                        Console.WriteLine($"Cost of services: {CurrentCustomerBalance.CostOfServices}");
                        Console.WriteLine($"Cost of parts: {CurrentCustomerBalance.CostOfParts}");
                    }
                    else
                    {
                        CustomerOnHold();
                    }
                    //AddCostToTotal("Repair", 0);//remove?
                    break;

                case 3: // mods
                    int cost = 0;
                    ICanCustomize<RepairMan> customizeMan = CanCustomizeList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 1);
                    if (customizeMan != null)
                    {
                        cost = customizeMan.Modify(customerCar, part);
                        Dss.AddWorkOrder((RepairMan)customizeMan, "Modify", ServicesCatalogue["Modify"], CurrentCustomer.MyCar.CarContent.Find(x => x.Name == part).Cost);
                        CurrentCustomerBalance.CostOfServices += ServicesCatalogue["Modify"];
                        CurrentCustomerBalance.CostOfParts += CurrentCustomer.MyCar.CarContent.Find(x => x.Name == part).Cost;
                        PayRepairMan(CalculateSalary(ServicesCatalogue["Modify"]), (RepairMan)customizeMan);
                        Console.WriteLine($"Cost of services: {CurrentCustomerBalance.CostOfServices}");
                        Console.WriteLine($"Cost of parts: {CurrentCustomerBalance.CostOfParts}");
                    }
                    else
                    {
                        CustomerOnHold();
                    }
                    if (cost != 0)
                    {
                        //AddCostToTotal("Modify", cost);//remove?
                    }
                    break;
                case 4: //replace
                    cost = 0;
                    ICanReplace<RepairMan> replaceMan = CanReplaceList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 1);
                    if (replaceMan != null)
                    {
                        cost = replaceMan.ReplacePart(part, customerCar);
                        Dss.AddWorkOrder((RepairMan)replaceMan, "Replace", ServicesCatalogue["Replace"], CurrentCustomer.MyCar.CarContent.Find(x => x.Name == part).Cost);
                        CurrentCustomerBalance.CostOfServices += ServicesCatalogue["Replace"];
                        CurrentCustomerBalance.CostOfParts += CurrentCustomer.MyCar.CarContent.Find(x => x.Name == part).Cost;
                        PayRepairMan(CalculateSalary(ServicesCatalogue["Replace"]), (RepairMan)replaceMan);
                        Console.WriteLine($"Cost of services: {CurrentCustomerBalance.CostOfServices}");
                        Console.WriteLine($"Cost of parts: {CurrentCustomerBalance.CostOfParts}");
                    }
                    else
                    {
                        CustomerOnHold();
                    }
                    if (cost != 0)
                    {
                        //AddCostToTotal("Replace", cost);//remove?
                    }
                    break;

                case 5: //liquids 
                    ICanReplaceFluids<RepairMan> replaceFluidsMan = CanReplaceFluidsList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 1);
                    if (replaceFluidsMan != null)
                    {
                        //AddCostToTotal("ReplaceLiquid", replaceFluidsMan.ReplaceFluid(CurrentCustomer.MyCar, part));
                        Dss.AddWorkOrder((RepairMan)replaceFluidsMan, "ReplaceLiquid", ServicesCatalogue["ReplaceLiquid"], 0);
                        CurrentCustomerBalance.CostOfServices += ServicesCatalogue["ReplaceLiquid"];
                        CurrentCustomerBalance.CostOfParts += 50; //fixed liquid price
                        PayRepairMan(CalculateSalary(ServicesCatalogue["ReplaceLiquid"]), (RepairMan)replaceFluidsMan);
                        Console.WriteLine($"Cost of services: {CurrentCustomerBalance.CostOfServices}");
                        Console.WriteLine($"Cost of parts: {CurrentCustomerBalance.CostOfParts}");
                    }
                    else
                    {
                        CustomerOnHold();
                    }
                    break;
            }
        }

        //public static void AddCostToTotal(string service, int partCost)
        //{
        //    ServicesCatalogue.TryGetValue(service, out int currentCost);
        //    currentCost += partCost;
        //    Lucy.Say($"{service} complete. The cost is {currentCost} USD.");
        //    _costOfServices += currentCost;
        //}

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