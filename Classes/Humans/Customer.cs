using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars;
using AutoRepairShop.Classes.Cars.CarParts;
using AutoRepairShop.Classes.Cars.CarTypes;
using AutoRepairShop.Classes.Managers;

namespace AutoRepairShop.Classes.Humans
{
    class Customer:Human
    {
        public Car MyCar { get; set; }

        public Customer()
        {
            Menu.PrintServiceMessage("Please set new customer's name:");
            Name = Console.ReadLine();
            Menu.PrintMenuMessage($"New Customer has arrived! Name - {Name}");
            //var type = typeof(Car);
            Console.WriteLine("Provide Car Type:");
            Console.WriteLine($"1. Passenger");
            Console.WriteLine($"2. Truck");
            Console.WriteLine($"3. Special");
            int userInput;
            Int32.TryParse(Console.ReadLine(), out userInput);

            switch (userInput)
            {
                case 1:
                    MyCar = new PassengerCar();
                    break;

                case 2:
                    MyCar = new TruckCar();
                    break;

                case 3:
                    MyCar = new SpecialCar();
                    break;

                default:
                    break;
            }

        }

        public override void Say(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            base.Say(message);
            Console.ResetColor();
        }

        public int GetReply(int menuLength)
        {
            string reply = Console.ReadLine();
            if (reply == "quit")
            {
                Say("Sorry, I have to go, see you later!");
                return -1;
            }
            else if (reply == "looking good")
            {
                Say("You look great today, Lucy!");
                ShopManager.Thank();
                return GetReply(menuLength);
            }
            else
            {
                int.TryParse(reply, out int i);
                if (i > 0 && i <= menuLength)
                {
                    return i;
                }
                else
                {
                    Menu.ThrowWarning();
                    return GetReply(menuLength);
                }
            }
        }

        public void MakePayment()
        {
            Say($"Here it is, sweetheart.");
        }

        public void MakeDiagnosticsOrder()
        {
            Say($"Please diagnoze my {MyCar.Name}, I need to know what is broken");
            ShopManager.TakeCar(MyCar, 1);
        }

        public void MakeRepairOrder()
        {
            Say($"Please repair all the broken parts of my {MyCar.Name}.");
            ShopManager.TakeCar(MyCar, 2);
        }

        public void PimpMyCar()
        {
            Say($"Xzibit, pimp my {MyCar.Name}!!");
            ShopManager.TakeCar(MyCar, 3);
        }

        public void ReplaceBrokenParts()
        {
            Say($"Replace all broken parts in {MyCar.Name}, please...");
            ShopManager.TakeCar(MyCar, 4);
        }

        public void ReplaceLiquids()
        {
            Say($"Replace the liquids in {MyCar.Name}, please...");
            ShopManager.TakeCar(MyCar, 5);
        }

        public CarPart PointAtCarPart()
        {
            Console.WriteLine($"{MyCar.Name} contains the following parts");
            for (int i=0; i<MyCar.CarContent.Count; i++)
            {
                if (!MyCar.CarContent[i].IsWorking)
                {
                    Console.WriteLine($"{i}. Repair {MyCar.CarContent[i].Name}!");
                }
            }
            int userInput;
            int.TryParse(Console.ReadLine(), out userInput);
            return MyCar.CarContent[userInput];
        }

    }
}
