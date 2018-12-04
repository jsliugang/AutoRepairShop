using System;
using System.Collections.Generic;
using System.Linq;
using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Data.Models.Humans;
using AutoRepairShop.Data.Repository;
using AutoRepairShop.WorkFlow;

namespace AutoRepairShop.Tools
{
    internal sealed class RepairAutomationTool
    {
        public static readonly CarMaker _cm = new CarMaker();
        public static List<Customer> DefaultCustomerList;
        public static Random rand = new Random();

        public RepairAutomationTool()
        {
            ShopManager.Lucy.Greet();
            AddNewCustomers(10);
            TimeTool.TimeInstance.SetTimers();
            CheckQueue();
        }

        public static void CheckQueue()
        {
            while (true)
            {
                if (ShopManager.CustomersOnHold.Count != 0)
                {
                    ShopManager.ResumeWorkingWithCustomer(CustomerQueue<Customer>.Peek(ShopManager.CustomersOnHold));
                }
                if (ShopManager.Customers.Count == 0)
                {
                    continue;
                }
                ShopManager.AcceptNewCustomer(CustomerQueue<Customer>.Peek(ShopManager.Customers));
            }
        }

        public static void AddNewCustomers(int count)
        {
            DefaultCustomerList = new List<Customer>();
            for (int i = 0; i < count; i++)
            {
                DefaultCustomerList.Add(new Customer(_cm.MakeRandomCar()));
            }
        }

        public static void RemoveDisappointedCustomer(Customer customer)
        {
            DefaultCustomerList.RemoveAt(DefaultCustomerList.IndexOf(customer));
        }

        public static void MakeRepairChoice()
        {
            foreach (CarPart carPart in ShopManager.CurrentCustomer.MyCar.CarContent)
            {
                if (ShopManager.WorkingHours())
                {
                    if (carPart.IsWorking) continue;

                    if (rand.NextDouble() > 0.5)
                    {
                        ShopManager.CurrentCustomer.MakeRepairOrder(carPart.Name);
                    }
                    else
                    {
                        ShopManager.CurrentCustomer.ReplaceBrokenParts(carPart.Name);
                    }
                }
                
            }

            if (ShopManager.WorkingHours())
            {
                if (rand.NextDouble() > 0.5)
                {
                    ShopManager.CurrentCustomer.PimpMyCar(ShopManager.ModificationsOffer[rand.Next(0, ShopManager.ModificationsOffer.Count)]);
                }
                else
                {
                    int randomLiquid = rand.Next(0, ShopManager.CurrentCustomer.MyCar.CarLiquids.CarLiquids.Count);
                    ShopManager.CurrentCustomer
                        .ReplaceLiquids(ShopManager.CurrentCustomer
                            .MyCar.CarLiquids.CarLiquids.Keys.ElementAt(randomLiquid));
                }
            }

           

            ShopManager.CurrentCustomer.LeaveShop();
        }

        public static void AddCustomer()
        {
                Customer newCustomer = DefaultCustomerList[rand.Next(0, DefaultCustomerList.Count)];
                CustomerQueue<Customer>.Enqueue(newCustomer, ShopManager.Customers);
                newCustomer.SetWaitForServicesTimer();
        }   

        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}