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
        public static Time Time { get; }
        private GarageStockManager GSM = new GarageStockManager();
     
        static Menu()
        {
            // Initialize all the repairMen
            Time = new Time();
        }

        public Menu()
        {
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
            PrintMenuMessage("2. Check last order.");
            PrintMenuMessage("3. Check game time.");
            PrintMenuMessage("=========================");
            //GSM.RetrieveNewCarPart(typeof(BodyPart));  // testing of Stock and GarageStock
            Menu.ProcessMenuInput();
        }

        public static void RepairMenu()
        {
            Menu.PrintMenuMessage("***REPAIR MENU***");
            Menu.PrintMenuMessage("1. Diagnoze");
            Menu.PrintMenuMessage("2. Repair broken parts");
            Menu.PrintMenuMessage("3. Upgrade my ride!");
            Menu.PrintMenuMessage("4. Replace broken parts");
            Menu.PrintMenuMessage("5. Check and fix the liquids");
            int _reply = ShopManager.CustomerReplyHandler(customers.Last().GetReply(5));
            if (_reply == 0)
            {
                new Menu();
            }
            else
            {
                ProcessServiceInput(_reply);
            }
        }

        public static void ProcessServiceInput(int userInput)
        {
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
                    currentCustomer.ReplaceBrokenParts();
                    break;

                case 5:
                    currentCustomer.ReplaceLiquids();
                    break;

                default:
                    ThrowWarning();
                    RepairMenu();
                    break;
            }
        }

        public static void ProcessMenuInput()
        {
            int userInput; 
            int.TryParse(Console.ReadLine(), out userInput);
            switch (userInput)
            {
                case 1:
                    if (ShopManager.WorkingHours())
                    {
                        Customer newCustomer = new Customer();
                        Menu.customers.Add(newCustomer);
                        ShopManager.AcceptNewCustomer(newCustomer);
                    }
                    else
                    {
                        Console.WriteLine($"The Auto Repair Shop will open at 8 am tomorrow! We are not working at night time: {PassMeTime().ToString()}");
                        Menu.DisplayMenu();
                    }               
                    //foreach (object o in customers)
                    //{
                    //    foreach (PropertyInfo prop in o.GetType().GetProperties())
                    //    {
                    //        Console.WriteLine(prop.GetValue(o));
                    //    }                     
                    //}          
                break;
                case 2:
                    Customer checkCustomer = ShopManager.GetCurrentCustomer();
                    if (checkCustomer != null)
                    {
                        Menu.PrintMenuMessage($"The last order was from {checkCustomer.Name}, car - {checkCustomer.MyCar.Name}.");
                        Console.WriteLine();
                        DisplayMenu();
                    }
                    else
                    {
                        Menu.PrintMenuMessage($"No orders were placed!");
                        Console.WriteLine();
                        DisplayMenu();
                    }
                    break;
                case 3:
                    Time.GetGameTimeToScreen();
                    DisplayMenu();
                    break;
                default:
                    break;
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

        private static void PrintCustomMessage(string message, ConsoleColor textColor, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = textColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void PrintServiceMessage(string message)
        {
            PrintCustomMessage(message, ConsoleColor.DarkGray, ConsoleColor.Black);
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
