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
    class Customer:Human, IComparable<Customer>
    {
        public Car MyCar { get; set; }
        public int Priority { get; set; }

        public Customer() //manual ctor
        {
            Menu.PrintServiceMessage("Please set new customer's name:");
            Name = Console.ReadLine();
            Menu.PrintServiceMessage($"Please set {Name}'s priority (1 highest to 10 lowest):");
            Int32.TryParse(Console.ReadLine(), out int userInput);
            Priority = userInput;
            Menu.PrintMenuMessage($"New Customer has arrived! Name - {Name}");
        }

        public Customer(Car car) //automated ctor
        {
            Random rand = new Random();
            MyCar = car;
            Name = NamesList[rand.Next(0, NamesList.Count)];
            Priority = rand.Next(1,10);
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
            if (reply == "looking good")
            {
                Say("You look great today, Lucy!");
                ShopManager.Thank();
                return GetReply(menuLength);
            }
            int.TryParse(reply, out int i);
            if (i > 0 && i <= menuLength)
            {
                return i;
            }
            Menu.ThrowWarning();
            return GetReply(menuLength);
        }

        public void MakePayment()
        {
            Say($"Here it is, sweetheart.");
            MyCar.Drive();
        }

        public void AssignCar(Car car)
        {
            MyCar = car;
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

        public int CompareTo(Customer other)
        {
            if (this.Priority < other.Priority) return -1;
            if (this.Priority > other.Priority) return 1;
            return 0;
        }
    }
}
