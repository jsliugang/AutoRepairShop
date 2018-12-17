using System;
using System.Collections.Generic;
using AutoRepairShop.Data.Models.Humans;
using AutoRepairShop.Data.Repository;
using AutoRepairShop.WorkFlow;

namespace AutoRepairShop.Tools
{
    internal sealed class RepairAutomationTool
    {
        public static readonly CarMaker Cm = new CarMaker();
        public static List<Customer> DefaultCustomerList;
        public static Random Rand = new Random();

        public RepairAutomationTool()
        {
            AddNewCustomers(10);
            ShopManager.Lucy.Greet();
        }

        public static void AddNewCustomers(int count)
        {
            DefaultCustomerList = new List<Customer>();
            for (var i = 0; i < count; i++)
            {
                var newCustomer = new Customer(Cm.MakeCar());
                //newCustomer.Say($"{newCustomer.Name}: I heard this is a great place to repair my car. I might consider coming soon...");
                CustomerQueue<Customer>.Enqueue(newCustomer, DefaultCustomerList);
            }
        }

        public static void RemoveDisappointedCustomer(Customer customer)
        {
            CustomerQueue<Customer>.Remove(DefaultCustomerList, customer);
        }
    }
}