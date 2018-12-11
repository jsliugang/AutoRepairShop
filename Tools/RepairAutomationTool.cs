using System;
using System.Collections.Generic;
using AutoRepairShop.Data.Models.Humans;
using AutoRepairShop.Data.Repository;
using AutoRepairShop.Services;
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
            FileFolderManagementService.CreateFolder();
            AddNewCustomers(100);
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
                var _newCustomer = new Customer(_cm.MakeRandomCar());
                _newCustomer.Say($"{_newCustomer.Name}: I heard this is a great place to repair my car. I might consider coming soon...");
                CustomerQueue<Customer>.Enqueue(_newCustomer, DefaultCustomerList);
            }
        }

        public static void RemoveDisappointedCustomer(Customer customer)
        {
            CustomerQueue<Customer>.Remove(DefaultCustomerList, customer);
        }

        public static void MakeRepairChoice()
        {
            ShopManager.CurrentCustomer.RepairBrokenParts();
            ShopManager.CurrentCustomer.ReplaceBrokenParts();

           if (ShopManager.WorkingHours())
           {
                if (rand.NextDouble() > 0.5)
                {
                    ShopManager.CurrentCustomer.PimpMyCar(ShopManager.ModificationsOffer[rand.Next(0, ShopManager.ModificationsOffer.Count)]);
                }
                else
                {
                    ShopManager.CurrentCustomer.ReplaceLiquids();
                }
           }
           //ShopManager.CurrentCustomer.MakeDiagnosticsOrder();
           ShopManager.CurrentCustomer.LeaveShop();
        }

        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}