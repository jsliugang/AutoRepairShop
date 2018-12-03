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

        public RepairAutomationTool()
        {
            TimeTool.TimeInstance.GetGameTimeToScreen();
            ShopManager.Lucy.Greet();
            AddDefaultCustomers();
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
                if (ShopManager.Customers.Count == 0) continue;
                ShopManager.AcceptNewCustomer(CustomerQueue<Customer>.Peek(ShopManager.Customers));
            }
        }

        private void AddDefaultCustomers()
        {
            DefaultCustomerList = new List<Customer>();
            for (int i = 0; i < 10; i++)
            {
                DefaultCustomerList.Add(new Customer(_cm.MakeRandomCar()));
            }
        }

        public static void MakeRepairChoice()
        {
            Random rand = new Random();
            foreach (CarPart carPart in ShopManager.CurrentCustomer.MyCar.CarContent)
            {
                if (!ShopManager.WorkingHours())
                {
                    TimeTool.TimeInstance.SetNextDayTimer();
                }
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

            if (!ShopManager.WorkingHours())
            {
                TimeTool.TimeInstance.SetNextDayTimer();
            }

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

            ShopManager.CurrentCustomer.LeaveShop();
        }

        public static void AddCustomer()
        {
            Random rand = new Random();
            if (ShopManager.WorkingHours())
                //CustomerQueue<Customer>.Enqueue(new Customer(_cm.MakeRandomCar()), ShopManager.Customers);
                CustomerQueue<Customer>.Enqueue(DefaultCustomerList[rand.Next(0, DefaultCustomerList.Count)], ShopManager.Customers);
            else
                ShopManager.ShopIsClosed();
        }   

        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}