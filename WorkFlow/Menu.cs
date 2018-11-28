using System;
using AutoRepairShop.Classes.Cars;
using AutoRepairShop.Data.Models.Humans;
using AutoRepairShop.Data.Repository;
using AutoRepairShop.Tools;

namespace AutoRepairShop.WorkFlow
{
    internal sealed class Menu
    {
        private static readonly CarMaker _cm = new CarMaker();

        //public static Time Time { get; }
        private GarageStockManager _gsm = new GarageStockManager();

        static Menu()
        {
            // Initialize all the repairMen
            //Time = new Time();
        }

        public Menu()
        {
            GreetUser();
            DisplayMenu();
        }

        public static void GreetUser()
        {
            PrintCustomMessage("Welcome to the Repair Shop!", ConsoleColor.Green, ConsoleColor.Black);
        }

        public static void DisplayMenu()
        {
            PrintMenuMessage("*****MENU*****");
            PrintMenuMessage("1. Create new customer.");
            PrintMenuMessage("2. Check last order.");
            PrintMenuMessage("3. Check game time.");
            PrintMenuMessage("=========================");
            //GSM.RetrieveNewCarPart(typeof(BodyPart));  // testing of Stock and GarageStock
            ProcessMenuInput();
        }

        public static void RepairMenu()
        {
            PrintMenuMessage("***REPAIR MENU***");
            PrintMenuMessage("1. Diagnoze");
            PrintMenuMessage("2. Repair broken parts");
            PrintMenuMessage("3. Upgrade my ride!");
            PrintMenuMessage("4. Replace broken parts");
            PrintMenuMessage("5. Check and fix the liquids");
            var reply = ShopManager.CustomerReplyHandler(ShopManager.CustomerQueue.Pop().GetReply(5));
            if (reply == 0)
                new Menu();
            else
                ProcessServiceInput(reply);
        }

        public static void ProcessServiceInput(int userInput)
        {
            var currentCustomer = ShopManager.CustomerQueue.Pop();
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
                        ShopManager.CustomerQueue.Enqueue(new Customer(_cm.MakeRandomCar()));
                        //ShopManager._customerQueue.Pop().AssignCar(cm.MakeRandomCar());
                        ShopManager.AcceptNewCustomer(ShopManager.CustomerQueue.Pop());
                    }
                    else
                    {
                        Console.WriteLine(
                            $"The Auto Repair Shop will open at 8 am tomorrow! We are not working at night time: {PassMeTime()}");
                        DisplayMenu();
                    }
                    break;

                case 2:
                    var checkCustomer = ShopManager.GetCurrentCustomer();
                    if (checkCustomer != null)
                    {
                        PrintMenuMessage(
                            $"The last order was from {checkCustomer.Name}, car - {checkCustomer.MyCar.Name}.");
                        Console.WriteLine();
                        DisplayMenu();
                    }
                    else
                    {
                        PrintMenuMessage($"No orders were placed!");
                        Console.WriteLine();
                        DisplayMenu();
                    }
                    break;

                case 3:
                    TimeTool.GetGameTimeToScreen();
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
            return TimeTool.GetGameTime();
        }
    }
}