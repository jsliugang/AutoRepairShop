using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Humans;
using AutoRepairShop;
using AutoRepairShop.Classes.Enum;
using AutoRepairShop.Classes.Cars.CarParts;

namespace AutoRepairShop.Classes.Managers
{
    sealed class Menu
    {

        private static List<Customer> customers = new List<Customer>();
        private List<RepairMan> repairMen = new List<RepairMan>();
        public static Time Time { get; }
        private GarageStockManager GSM = new GarageStockManager();
     
        static Menu()
        {
            // Initialize all the repairMen
            Time = new Time();
            Menu.GreetUser();
            Menu.DisplayMenu();
        }

        public static void GreetUser()
        {
            Menu.PrintCustomMessage("Welcome to the Repair Shop!", ConsoleColor.Green, ConsoleColor.Black);
        }

        public static void DisplayMenu()
        {
            PrintMenuMessage("*****MENU*****");
            PrintMenuMessage("1. Create new customer.");
            PrintMenuMessage("2. Check current orders.");
            PrintMenuMessage("3. Check game time.");
            PrintMenuMessage("=========================");
            //GSM.RetrieveNewCarPart(typeof(BodyPart));  // testing of Stock and GarageStock
            Menu.ProcessInput();
        }

        public static void RepairMenu()
        {
            Menu.PrintMenuMessage("***REPAIR MENU***");
            Menu.PrintMenuMessage("1. Diagnoze");
            Menu.PrintMenuMessage("2. Repair broken parts");
            Menu.PrintMenuMessage("3. Upgrade my ride!");
            Menu.PrintMenuMessage("4. Replace broken parts");
            Menu.ProcessServiceInput();
        }

        public static void ProcessServiceInput()
        {
            int userInput;
            Int32.TryParse(Console.ReadLine(), out userInput);
            Customer currentCustomer = customers.Last();
            switch (userInput)
            {
                case 1:
                    currentCustomer.MakeDiagnosticsOrder();
                    break;

                case 2:
                    currentCustomer.MakeRepairOrder();
                    break;

                case 3:
                    currentCustomer.PimpMyCar();
                    break;

                case 4:
                    currentCustomer.RepairBrokenParts();
                    break;

                default:
                    ThrowWarning();
                    RepairMenu();
                    break;

            }
        }

        public static void ProcessInput()
        {
            string userInput = Console.ReadLine();
            if (userInput == "1")
            {
                if (ShopManager.WorkingHours())
                {
                    Customer newCustomer = new Customer();
                    Menu.customers.Add(newCustomer);
                    ShopManager.AcceptNewCustomer(newCustomer);
                }
                else
                {
                    Console.WriteLine($"The Auto Repair Show will open at 8 am tomorrow! We are not working at night time: {PassMeTime().ToString()}");
                    Menu.DisplayMenu();
                }
                
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
                Menu.Time.GetGameTimeToScreen();
                DisplayMenu();
            }
        }

        public static void ThrowWarning()
        {
            PrintCustomMessage("Invalid input! Try again:", ConsoleColor.Red, ConsoleColor.Black);
        }

        public static void PrintMenuMessage(string message)
        {
            PrintCustomMessage(message, ConsoleColor.DarkYellow, ConsoleColor.Black);
        }

        public static void PrintCustomMessage(string message, ConsoleColor textColor, ConsoleColor backgroundColor)
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

        public static DateTime PassMeTime()
        {
            return Menu.Time.GetGameTime();
        }
    }
}
