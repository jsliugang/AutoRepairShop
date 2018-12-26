using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AutoRepairShop.Data.Models;
using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Data.Models.Humans;
using AutoRepairShop.Data.Repository;
using AutoRepairShop.Services;
using AutoRepairShop.Tools;

namespace AutoRepairShop.WorkFlow
{
    internal sealed class ShopManager : Human
    {
        private static double _balance;
        public static double Balance { get => _balance; set => _balance = value; }
        public static GarageStockManager GarStMan = new GarageStockManager();
        public static DailyStatService Dss = new DailyStatService();
        public Dictionary<RepairMan, double> Salary = new Dictionary<RepairMan, double>();
        public readonly ContractSignatureService _css = new ContractSignatureService();
        private readonly FileLoggerService _lrw = new FileLoggerService();
        public static List<List<Customer>> CustomerQueuesList = new List<List<Customer>>();
        public static List<Garage> GarageList = new List<Garage>();
        private readonly object _threadlock;
        public static readonly CarMaker Cm = new CarMaker();
        public static ObservableCollection<Customer> DefaultCustomerList;

        public Dictionary<Type, int> PartsUsedMonthlyList = new Dictionary<Type, int>()
        {
            {typeof(BodyPart), 0},
            {typeof(CarburetorPart), 0},
            {typeof(CustomBonnetMod), 0},
            {typeof(DecalsMod), 0},
            {typeof(EnginePart), 0},
            {typeof(ExhaustPipeMod), 0},
            {typeof(GearboxPart), 0},
            {typeof(HeatRegulatorPart), 0},
            {typeof(HornPart), 0},
            {typeof(MufflerPart), 0},
            {typeof(No2Mod), 0},
            {typeof(RadiatorPart), 0},
            {typeof(SpinnersMod), 0},
            {typeof(SpoilerMod), 0},
            {typeof(SportSuspensionMod), 0},
            {typeof(TitaniumWipersMod), 0},
            {typeof(WheelsPart), 0},
        };

        private ShopManager()
        { 
            Name = "Lucy";
            Balance = BalanceReadWrite.Read();
            Salary.Add(RmKirill.Kirill, 0);
            Salary.Add(RmPetrovich.Petrovich, 0);
            Salary.Add(RmVano.Vano, 0);
            Salary.Add(RmSanSanuch.SanSanuch, 0);
            Salary.Add(RmLeha.Leha, 0);
            Salary.Add(RmMihaluch.Mihaluch, 0);
            Salary.Add(RmSerega.Serega, 0);
            TimeTool.TimeInstance.SetTimers();
            FileFolderManagementService.CreateFolder();
            _threadlock = new object();
            AddNewCustomers(10);         
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
            for (var i = 0; i < 3; i++)
            {
                GarageList.Add(new Garage(i.ToString()));
            }
        }

        public static async Task<bool> AcceptNewCustomerAsync(Customer customer)
        {
            if (!WorkingHours()()) return false;
            await Task.Run(() =>
            {
                Dss.AddCustomer(customer); //daily logging service
                Lucy.Say($"Greetings {customer.Name}! My name is {Lucy.Name} and I will be your Repair Shop Manager today.\nPlease leave your {customer.MyCar.Name} at the parking lot.\nLucy thread {Thread.CurrentThread.ManagedThreadId}");
                //Console.WriteLine($"Lucy thread {Thread.CurrentThread.ManagedThreadId}");
                
                customer.SetWaitForServicesTimer();
                lock (GarageList)
                {
                    var min = GarageList.Min(i => i.CustomersQueue.Count);
                    Garage selectedGarage = GarageList.First(x => x.CustomersQueue.Count == min);
                    CustomerQueue<Customer>.Enqueue(customer,
                        selectedGarage.CustomersQueue);
                    Console.WriteLine($"{customer.Name} with {customer.MyCar.Name} has been added to garage: {selectedGarage.Name}"); //debug
                }
                Lucy._lrw.StoreLog($"New customer: {customer.Name}, Car: {customer.MyCar.Name}, Customer registered");
                Lucy.Say($"{customer.Name}, what shall we do with your {customer.MyCar.Name}?");
            });
            return true;
        }

        public double ApproximateCost(Customer customer)
        {
            lock (_threadlock)
            {
                double partsToRepair = customer.MyAgreement.PartsToRepair.Count;
                double cost = partsToRepair * Garage.ServicesCatalogue["Repair"];
                foreach (var part in customer.MyAgreement.PartsToReplace)
                {
                    cost += part.Cost;
                    cost += Garage.ServicesCatalogue["Replace"];
                }
                return cost;
            }
        }

        public static async Task<bool> ReleaseCustomerAsync(Customer customer)
        {
            await Task.Run(() =>
            {
                if (customer.MyCar.IsOnWarranty == false)
                {
                    Lucy.Say($"Ok then!");
                    customer.SetWarrantyTimer();
                    double discount = customer.MyAgreement.TotalServicesCost *
                    customer.MyDiscounts.GetDiscountRate();
                    double total = customer.MyAgreement.GetTotal() - discount;
                    Lucy.Say($"Your total for today is {total}.");
                    customer.MakePayment();
                    customer.MyDiscounts.PunchDiscountCard();
                    Dss.FinalizeCustomer(customer, total);
                    total -= customer.MyAgreement.TotalPartCost; // pay to part suppliers
                    AcceptPayment(total);                  
                    Console.WriteLine($"List of parts:");
                    foreach (var carPart in customer.MyAgreement.PartsToRepair)
                    {
                        Console.WriteLine($"{carPart.Name}: {carPart.Durability}");
                    }
                    foreach (var carPart in customer.MyAgreement.PartsToReplace)
                    {
                        Console.WriteLine($"{carPart.Name}: {carPart.Durability}");
                    }
                    return true;
                }
                Lucy.Say($"Have a great day, {customer.Name}");
                Lucy._lrw.StoreLog($"{customer.Name}, Car: {customer.MyCar.Name}, Customer released. Amount: 0");
                Dss.FinalizeCustomer(customer, 0);
                return true;
            });
            return true;
        }

        public static void PayWarrantyCompensation(double amount)
        {
            amount *= 4;
            Balance -= amount;
            BalanceReadWrite.Write(Balance);
        }

        public static void AcceptPayment(double amount)
        {
            lock (Lucy._threadlock)
            {
                Balance += amount;
                BalanceReadWrite.Write(Balance);
            }
        }

        public static void UpdateAgreement(RepairMan rm, Customer customer, string service, double partCost)
        {
            lock (Lucy._threadlock)
            {
                Dss.AddWorkOrder(rm, service, Garage.ServicesCatalogue[service], partCost);
                customer.MyAgreement.TotalServicesCost += Garage.ServicesCatalogue[service];
                customer.MyAgreement.TotalPartCost += partCost;
                AddSalary(CalculateSalary(Garage.ServicesCatalogue[service], customer), rm);
            }
        }

        public static void ShopIsClosed()
        {
            Lucy.Say($"The Auto Repair Shop will open at 8 am tomorrow! We are not working at night time: {MsgDecoratorTool.PassMeTime()}");
        }

        public static double CalculateSalary(double income, Customer customer)
        {
            lock (Lucy._threadlock)
            {
                double discount = income * customer.MyDiscounts.GetDiscountRate() / 100;
                double salary = income - discount;
                salary = salary * 0.5;
                return salary;
            }
        }

        public static void AddSalary(double salary, RepairMan rm)
        {
            lock (Lucy._threadlock)
            {
                if (rm.Name == "SanSanuch")
                {
                    salary *= 2;
                }
                var salaryReceiver = Lucy.Salary.First(x => x.Key.Name == rm.Name).Key;
                Lucy.Salary[salaryReceiver] += salary;
            }
        }
        
        public static void LastTimeLog()
        {
            Lucy._lrw.StoreTime();
        }

        public static DateTime WhatTimeIsItNow()
        {
            return MsgDecoratorTool.PassMeTime();
        }

        public static Func<bool> WorkingHours()
        {
            lock (Lucy._threadlock)
            {
                DateTime now = WhatTimeIsItNow();
                if (7 < now.Hour && now.Hour < 24 && now.DayOfWeek != DayOfWeek.Sunday)
                {
                    return () => true;
                }
                DateTime nextWorkingDateStart = TimeTool.GetGameTime()
                    .AddDays(1)
                    .Subtract(TimeTool.GetGameTime().TimeOfDay);
                nextWorkingDateStart = nextWorkingDateStart.AddHours(8).Subtract(new TimeSpan(1, 0, 0, 0, 0));
                TimeSpan night = nextWorkingDateStart - TimeTool.GetGameTime();
                double nightSeconds = night.TotalSeconds;
                nightSeconds = Math.Abs(nightSeconds / 720);
                if (now.DayOfWeek == DayOfWeek.Sunday)
                {
                    nightSeconds += 120;
                }
                int sleepTime = (int) nightSeconds * TimeTool.Thousand + TimeTool.Thousand; //change it back
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} has been put to sleep for {sleepTime}");
                return () => Lucy.SomeFunc(sleepTime);
            }
        }

        public bool SomeFunc(double time)
        {
            Thread.Sleep((int)time);
            return true;
        }

        public static void RequestParts()
        {
            foreach (var pair in Lucy.PartsUsedMonthlyList)
            {
                int amount = IntRounding(pair.Value);
                SupplierService.ProcessOrder(pair.Key, amount);
                StockService.StMan.Create(pair.Key, 1);
            }
        }

        public static int IntRounding(int numberToRound)
        {
            int temp = numberToRound % 10;
            return temp >= 5 ? (numberToRound - temp + 10) : (numberToRound - temp);
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

        public static void AddNewCustomers(int count)
        {
            DefaultCustomerList = new ObservableCollection<Customer>();
            for (var i = 0; i < count; i++)
            {
                var newCustomer = new Customer(Cm.MakeCar());
                CustomerQueue<Customer>.Enqueue(newCustomer, DefaultCustomerList);
            }
        }

        public static void RemoveDisappointedCustomer(Customer customer)
        {
            CustomerQueue<Customer>.Remove(DefaultCustomerList, customer);
        }
    }

}