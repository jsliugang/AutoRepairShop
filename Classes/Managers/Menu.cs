using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Humans;
using AutoRepairShop;

namespace AutoRepairShop.Classes.Managers
{
    class Menu
    {
        private List<Customer> customers = new List<Customer>();
        private List<RepairMan> repairMen = new List<RepairMan>();
        private ShopManager sm = new ShopManager();
        private Time time;

        public Menu()
        {
            // Initialize all the repairMen
            time = new Time();
            GreetUser();
            DisplayMenu();
        }

        public void GreetUser()
        {
            PrintCustomMessage("Welcome to the Repair Shop!", ConsoleColor.Green, ConsoleColor.Black);
        }

        public void DisplayMenu()
        {
            Console.WriteLine("*****MENU*****");
            Console.WriteLine("1. Create new customer.");
            Console.WriteLine("2. Check current orders.");
            Console.WriteLine("3. Check game time.");
            ProcessInput();
        }

        public void ProcessInput()
        {
            string userInput = Console.ReadLine();
            if (userInput == "1")
            {
                Customer newCustomer = new Customer();
                customers.Add(newCustomer);
                sm.AcceptNewCustomer(newCustomer);
                //foreach (object o in customers)
                //{
                //    foreach (PropertyInfo prop in o.GetType().GetProperties())
                //    {
                //        Console.WriteLine(prop.GetValue(o));
                //    }                     
                //}
            }
            else if (userInput == "3")
            {
                time.GetGameTime();
                DisplayMenu();
            }
        }

        public void PrintCustomMessage(string message, ConsoleColor textColor, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = textColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
