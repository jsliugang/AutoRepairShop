using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes;
using AutoRepairShop.Classes.Cars;
using AutoRepairShop.Classes.Managers;

namespace AutoRepairShop.Classes.Humans
{
    class ShopManager : Human
    {
        private Customer _customer;

        public ShopManager()
        {
            Name = "Lucy";
            //Greet();
            //Say("I am the Repair Shop manager! How can I help you?");
            StockManager StMan = new StockManager();
            GarageStockManager GarStMan = new GarageStockManager();
            LogManager LogMan = new LogManager();
            FileManager FileMan = new FileManager();
        }

        public void AcceptNewCustomer(Customer customer)
        {
            _customer = customer;
            Say($"Greetings {_customer.Name}! My name is {Name} and I will be your Repair Shop Manager today.");
            Say($"Please leave your {_customer.MyCar.Name} at the parking lot.");
            CheckWorkerBusy();
        }

        public void CheckWorkerBusy()
        {
            
        }
    }
}
