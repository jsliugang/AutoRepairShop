using System;
using System.Collections.Generic;
using System.Linq;
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

        public static double Balance { get; set; }
        public static StockManager StMan = new StockManager();
        public static GarageStockManager GarStMan = new GarageStockManager();
        public static readonly Dictionary<string, double> ServicesCatalogue = new Dictionary<string, double>();
        public static List<string> ModificationsOffer = new List<string>();
        public static Customer CurrentCustomer;
        public static DailyStatService Dss = new DailyStatService();
        public static List<Customer> NewCustomers = new List<Customer>();
        public static List<Customer> CustomersOnHold;
        public Dictionary<RepairMan, double> Salary = new Dictionary<RepairMan, double>();
        private readonly ContractSignatureService _css = new ContractSignatureService();
        private bool _isGarageEmpty = true;
        private readonly FileLoggerService _lrw = new FileLoggerService();

        public static List<Customer> CustomerQueue1;
        public static List<Customer> CustomerQueue2;
        public static List<Customer> CustomerQueue3;

        private ShopManager()
        { 
            Name = "Lucy";
            ServicesCatalogue.Add("Diagnoze", 50);
            ServicesCatalogue.Add("Repair", 250);
            ServicesCatalogue.Add("Modify", 500);
            ServicesCatalogue.Add("Replace", 200);
            ServicesCatalogue.Add("ReplaceLiquid", 50);
            Balance = BalanceReadWrite.Read();
            ModificationsOffer.Add("CustomBonnet");
            ModificationsOffer.Add("Decals");
            ModificationsOffer.Add("ExhaustPipe");
            ModificationsOffer.Add("NO2");
            ModificationsOffer.Add("Spinners");
            ModificationsOffer.Add("Spoiler");
            ModificationsOffer.Add("SportSuspension");
            ModificationsOffer.Add("TitaniumWipers");
            CustomersOnHold = new List<Customer>();
            Salary.Add(RmKirill.Kirill, 0);
            Salary.Add(RmPetrovich.Petrovich, 0);
            Salary.Add(RmVano.Vano, 0);
            Salary.Add(RmSanSanuch.SanSanuch, 0);
            TimeTool.TimeInstance.SetTimers();
            FileFolderManagementService.CreateFolder();
        }

        public static ShopManager Lucy;

        private class Nested
        {
            static Nested()
            {
            }
            internal static readonly ShopManager lucy = new ShopManager();
        }

        static ShopManager()
        {
            RuntimeHelpers.RunClassConstructor(typeof(Nested).TypeHandle);
            Lucy = Nested.lucy;
        }

        public static void CheckQueue()
        {
            Lucy.Greet();
            CustomerQueue1 = new List<Customer>();
            CustomerQueue2 = new List<Customer>();
            CustomerQueue3 = new List<Customer>();

            while (true) // to be removed: customers will be coming to shop by themselves and will be processed into one of the queues by Lucy on Accept new customer
            {
                if (CustomersOnHold.Count != 0)
                {
                    ResumeWorkingWithCustomer(CustomerQueue<Customer>.Peek(CustomersOnHold));
                }
                if (NewCustomers.Count == 0)
                {
                    continue;
                }
                AcceptNewCustomer(CustomerQueue<Customer>.Peek(NewCustomers));
            }
        }

        public static void AcceptNewCustomer(Customer customer)
        {
            if (!WorkingHours()) return;
            CurrentCustomer = customer;
            CustomerQueue<Customer>.Dequeue(NewCustomers);
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
            if (WorkingHours())
            {
                CurrentCustomer.MakeDiagnosticsOrder();
            }
            Lucy._css.AppendContractText();
            RepairAutomationTool.MakeRepairChoice();
        }

        public double ApproximateCost()
        {
            double partsToRepair = CurrentCustomer.MyAgreement.PartsToRepair.Count;
            double cost = partsToRepair * ServicesCatalogue["Repair"];
            foreach (var part in CurrentCustomer.MyAgreement.PartsToReplace)
            {
                cost += part.Cost;
                cost += ServicesCatalogue["Replace"];
            }
            return cost;
        }

        public static void ResumeWorkingWithCustomer(Customer customer)
        {
            if (!WorkingHours()) return;
            CurrentCustomer = customer;
            CustomerQueue<Customer>.Dequeue(CustomersOnHold);
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
            if (CurrentCustomer.MyCar.IsOnWarranty == false)
            {
                Lucy.Say($"Ok then!");
                CurrentCustomer.SetWarrantyTimer();
                Lucy._isGarageEmpty = true;
                double discount = CurrentCustomer.MyAgreement.TotalServicesCost *
                                  CurrentCustomer.MyDiscounts.GetDiscountRate();
                double total = CurrentCustomer.MyAgreement.GetTotal() - discount;
                Lucy.Say($"Your total for today is {total}.");
                CurrentCustomer.MakePayment();
                CurrentCustomer.MyDiscounts.PunchDiscountCard();
                total -= CurrentCustomer.MyAgreement.TotalPartCost; // pay to part suppliers
                AcceptPayment(total);
                Dss.FinalizeCustomer(CurrentCustomer, total);
                Console.WriteLine($"List of parts:");
                foreach (var carPart in CurrentCustomer.MyAgreement.PartsToRepair)
                {
                    Console.WriteLine($"{carPart.Name}: {carPart.Durability}");
                }
                foreach (var carPart in CurrentCustomer.MyAgreement.PartsToReplace)
                {
                    Console.WriteLine($"{carPart.Name}: {carPart.Durability}");
                }
                return;
            }
            
            Lucy.Say($"Have a great day, {customer.Name}");
            Lucy._lrw.StoreLog($"{CurrentCustomer.Name}, Car: {CurrentCustomer.MyCar.Name}, Customer released. Amount: 0");
            Dss.FinalizeCustomer(CurrentCustomer, 0);

        }

        public static void PayWarrantyCompensation(double amount)
        {
            amount *= 4;
            Balance -= amount;
            BalanceReadWrite.Write(Balance);
        }

        public static void AcceptPayment(double amount)
        {
            Balance += amount;
            BalanceReadWrite.Write(Balance);
        }

        public static void CustomerOnHold()
        {
            Lucy.Say($"All repair men are busy at this moment! Please a little...");
            CustomerQueue<Customer>.Enqueue(CurrentCustomer, CustomersOnHold);
            CurrentCustomer.SetWaitForServicesTimer();
        }

        public static void ShopIsClosed()
        {
            Lucy.Say($"The Auto Repair Shop will open at 8 am tomorrow! We are not working at night time: {MsgDecoratorTool.PassMeTime()}");
        }

        public static double CalculateSalary(double income)
        {
            double discount = income * CurrentCustomer.MyDiscounts.GetDiscountRate() / 100;
            double salary = income - discount;
            salary = salary * 0.5;
            return salary;
        }

        public static void AddSalary(double salary, RepairMan rm)
        {
            if (rm.Name == "SanSanuch")
            {
                salary *= 2;
            }
            var salaryReceiver = Lucy.Salary.First(x => x.Key.Name == rm.Name).Key;
            Lucy.Salary[salaryReceiver] += salary;
            Balance -= salary;
            rm.GetSalary(salary);
        }

        public static void ProcessOrder(int choice, string part)
        {
            WorkingHours();
            Car customerCar = CurrentCustomer.MyCar;
            Lucy.Say("Will do!");
            switch (choice)
            {
                case 1: //diagnoze
                    ICanDiagnoze<RepairMan> diagnozeMan = CanDiagnozeList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 1) ??
                                                          CanDiagnozeList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 2) ??
                                                          CanDiagnozeList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 3);
                    if (diagnozeMan != null)
                    {
                        diagnozeMan.DiagnozeCar(customerCar);
                        Dss.AddWorkOrder((RepairMan)diagnozeMan, "Diagnoze", ServicesCatalogue["Diagnoze"], 0);
                        CurrentCustomer.MyAgreement.TotalServicesCost += ServicesCatalogue["Diagnoze"];
                        AddSalary(CalculateSalary(ServicesCatalogue["Diagnoze"]), (RepairMan)diagnozeMan);
                    }                  
                    else
                    {
                        CustomerOnHold();
                    }
                    break;

                case 2: //repair
                    ICanRepair<RepairMan> repairMan = CanRepairList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 1) ??
                                                      CanRepairList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 2);
                    if (repairMan != null)
                    {
                        repairMan.MakeRepairs(part);
                        Dss.AddWorkOrder((RepairMan) repairMan, "Repair", ServicesCatalogue["Repair"], 0);
                        CurrentCustomer.MyAgreement.TotalServicesCost += ServicesCatalogue["Repair"];
                        AddSalary(CalculateSalary(ServicesCatalogue["Repair"]), (RepairMan)repairMan);
                    }
                    else
                    {
                        CustomerOnHold();
                    }
                    break;

                case 3: // mods
                    ICanCustomize<RepairMan> customizeMan = CanCustomizeList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 1) ??
                                                            CanCustomizeList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 2) ??
                                                            CanCustomizeList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 3);
                    if (customizeMan != null)
                    {
                        customizeMan.Modify(part, customerCar);
                        Dss.AddWorkOrder((RepairMan)customizeMan, "Modify", ServicesCatalogue["Modify"], CurrentCustomer.MyCar.CarContent.Find(x => x.Name == part).Cost);
                        CurrentCustomer.MyAgreement.TotalServicesCost += ServicesCatalogue["Modify"];
                        CurrentCustomer.MyAgreement.TotalPartCost += CurrentCustomer.MyCar.CarContent.Find(x => x.Name == part).Cost;
                        AddSalary(CalculateSalary(ServicesCatalogue["Modify"]), (RepairMan)customizeMan);
                        Lucy._css.AddMoreServices("Modification", part, CurrentCustomer.MyCar.CarContent.Find(x => x.Name == part).Cost.ToString(), ServicesCatalogue["Modify"].ToString(), CurrentCustomer.MyAgreement.DocPath);
                    }
                    else
                    {
                        CustomerOnHold();
                    }
                    break;
                case 4: //replace
                    ICanReplace<RepairMan> replaceMan = CanReplaceList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 1) ??
                                                        CanReplaceList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 2) ??
                                                        CanReplaceList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 3);
                    if (replaceMan == null)
                    {
                        CustomerOnHold();
                        break;
                    }
                    replaceMan.ReplacePart(part, customerCar);
                    Dss.AddWorkOrder((RepairMan) replaceMan, "Replace", ServicesCatalogue["Replace"],
                    CurrentCustomer.MyCar.CarContent.Find(x => x.Name == part).Cost);
                    CurrentCustomer.MyAgreement.TotalServicesCost += ServicesCatalogue["Replace"];
                    CurrentCustomer.MyAgreement.TotalPartCost += CurrentCustomer.MyCar.CarContent.Find(x => x.Name == part).Cost;
                    AddSalary(CalculateSalary(ServicesCatalogue["Replace"]), (RepairMan) replaceMan);
                    break;

                case 5: //liquids 
                    ICanReplaceFluids<RepairMan> replaceFluidsMan = CanReplaceFluidsList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 1) ??
                                                                    CanReplaceFluidsList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 2) ??
                                                                    CanReplaceFluidsList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 3);
                    if (replaceFluidsMan != null)
                    {
                        CurrentCustomer.MyAgreement.TotalPartCost += replaceFluidsMan.ReplaceFluid(CurrentCustomer.MyCar);
                        Dss.AddWorkOrder((RepairMan)replaceFluidsMan, "ReplaceLiquid", ServicesCatalogue["ReplaceLiquid"], 0);
                        CurrentCustomer.MyAgreement.TotalServicesCost += ServicesCatalogue["ReplaceLiquid"];
                        AddSalary(CalculateSalary(ServicesCatalogue["ReplaceLiquid"]), (RepairMan)replaceFluidsMan);
                        Lucy._css.AddMoreServices("Liquids", "All liquids", "150", ServicesCatalogue["ReplaceLiquid"].ToString(), CurrentCustomer.MyAgreement.DocPath);
                    }
                    else
                    {
                        CustomerOnHold();
                    }
                    break;
            }
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
            if (7 < now.Hour && now.Hour < 24 && now.DayOfWeek != DayOfWeek.Sunday)
            {
                return true;
            }
            DateTime nextWorkingDateStart = TimeTool.GetGameTime().AddDays(1).Subtract(TimeTool.GetGameTime().TimeOfDay);
            nextWorkingDateStart = nextWorkingDateStart.AddHours(8).Subtract(new TimeSpan(1, 0, 0, 0, 0));
            TimeSpan night = nextWorkingDateStart - TimeTool.GetGameTime();
            double nightSeconds = night.TotalSeconds;
            nightSeconds = Math.Abs(nightSeconds / 720);
            if (now.DayOfWeek == DayOfWeek.Sunday)
            {
                nightSeconds += 120;
            }
            Thread.Sleep((int)nightSeconds * TimeTool.Thousand + 1000);
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

        public static void HandleProblematicCustomer(Customer customer)
        {
            CustomerQueue<Customer>.Remove(NewCustomers, customer);
            CustomerQueue<Customer>.Remove(CustomersOnHold, customer);
            Thread.Sleep(5000);
        }

        public override void Say(string message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            base.Say(message);
            Console.ResetColor();
        }

        public void GetSalary()
        {
            Balance -= 1000;
            BalanceReadWrite.Write(Balance);
        }
    }
}