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
    sealed class ShopManager : Human
    {
        private Customer _customer;
        private bool IsGarageEmpty = true;
        StockManager StMan = new StockManager();
        GarageStockManager GarStMan = new GarageStockManager();
        LogManager LogMan = new LogManager();
        LogManager LM = new LogManager();

        public static readonly ShopManager Lucy = new ShopManager();

        private ShopManager()
        {
            Name = "Lucy";          
        }

        public override void Say(string message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            base.Say(message);
            Console.ResetColor();
        }

        public static void TakeCar(Car customerCar)
        {
            Lucy.Say("Will do! Your order will be ready soon!");
        }

        public static void AcceptNewCustomer(Customer customer)
        {
                Lucy._customer = customer;
                Lucy.Say($"Greetings {Lucy._customer.Name}! My name is {Lucy.Name} and I will be your Repair Shop Manager today.");
                Lucy.Say($"Please leave your {Lucy._customer.MyCar.Name} at the parking lot.");
                ShopManager.CheckWorkerBusy();
                Lucy.IsGarageEmpty = false;
                Lucy.LM.StoreLog($"New customer: {Lucy._customer.Name}, Car: {Lucy._customer.MyCar.Name}, Customer registered");    
            
            Lucy.Say($"{Lucy._customer.Name}, what shall we do with your {Lucy._customer.MyCar.Name}?");
            Menu.RepairMenu();
        }

        

        public static void ReleaseCustomer(Customer customer)
        {
            //calculate amount, talk to customer, take money
            Lucy.IsGarageEmpty = true;
            Lucy.LM.StoreLog($"{Lucy._customer.Name}, Car: {Lucy._customer.MyCar.Name}, Customer released. Work completed: , Amount: ");
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
            if (7 < now.Hour && now.Hour < 24)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
