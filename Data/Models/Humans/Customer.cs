using System;
using AutoRepairShop.WorkFlow;
using AutoRepairShop.Classes.Data.Models;
using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Data.Models.CarTypes;
using AutoRepairShop.Tools;

namespace AutoRepairShop.Data.Models.Humans
{
    class Customer:Human, IComparable<Customer>
    {
        public Car MyCar { get; set; }
        public int Priority { get; set; }
        public DiscountCard MyDiscounts = new DiscountCard();

        public Customer() //manual ctor
        {
            MsgDecoratorTool.PrintServiceMessage("Please set new customer's name:");
            Name = Console.ReadLine();
            MsgDecoratorTool.PrintServiceMessage($"Please set {Name}'s priority (1 highest to 10 lowest):");
            Int32.TryParse(Console.ReadLine(), out int userInput);
            Priority = userInput;
            MsgDecoratorTool.PrintMenuMessage($"New Customer has arrived! Name - {Name}");
        }

        public Customer(Car car) //automated ctor
        {
            MyCar = car;
            Name = NamesList[new Random().Next(0, NamesList.Count)];
            Priority = new Random().Next(1,10);
        }

        public override void Say(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            base.Say(message);
            Console.ResetColor();
        }

        //TODO Refactor to leave the AutoRepairShop

        public void LeaveShop()
        {
            Say("I think that is it for today, Lucy, I have to go, see you later!");
            ShopManager.ReleaseCustomer(this);
        }

        public void ComplimentLucy()
        {
            Say("You look great today, Lucy!");
            ShopManager.Thank();
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
            ShopManager.ProcessOrder(1, "");
        }

        public void MakeRepairOrder(string part)
        {
            Say($"Please repair all the broken parts of my {MyCar.Name}.");
            ShopManager.ProcessOrder(2, part);
        }

        public void PimpMyCar(string modificationType)
        {
            Say($"Xzibit, pimp my {MyCar.Name}!!");
            ShopManager.ProcessOrder(3, modificationType);
        }

        public void ReplaceBrokenParts(string part)
        {
            Say($"Replace all broken parts in {MyCar.Name}, please...");
            ShopManager.ProcessOrder(4, part);
        }

        public void ReplaceLiquids(string liquid)
        {
            Say($"Replace the liquids in {MyCar.Name}, please...");
            ShopManager.ProcessOrder(5, liquid);
        }

        public CarPart PointAtCarPart()
        {
            Console.WriteLine($"{MyCar.Name} contains the following parts");
            for (int i=0; i<MyCar.CarContent.Count; i++)
            {
                if (!MyCar.CarContent[i].IsWorking)
                {
                    Console.WriteLine($"{i}. Repair {MyCar.CarContent[i].Name}!");
                    return MyCar.CarContent[i];
                }
            }
            return MyCar.CarContent[0]; //replace - if no parts are broken
        }

        public int CompareTo(Customer other)
        {
            if (Priority < other.Priority) return -1;
            if (Priority > other.Priority) return 1;
            return 0;
        }
    }
}
