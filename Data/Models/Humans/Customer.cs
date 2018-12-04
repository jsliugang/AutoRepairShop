using System;
using System.Text;
using System.Timers;
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
        public DiscountCard MyDiscounts = new DiscountCard();
        private Timer _waitForService;
        public static Random rand = new Random();

        public Customer() //manual ctor
        {
            MsgDecoratorTool.PrintServiceMessage("Please set new customer's name:");
            Name = Console.ReadLine();
            MsgDecoratorTool.PrintServiceMessage($"Please set {Name}'s priority (1 highest to 10 lowest):");
            Int32.TryParse(Console.ReadLine(), out int userInput);
            MsgDecoratorTool.PrintMenuMessage($"New Customer has arrived! Name - {Name}");
        }

        public Customer(Car car) //automated ctor
        {
            MyCar = car;
            StringBuilder sb = new StringBuilder();
            sb.Append($"{NamesList[rand.Next(0, NamesList.Count)]} {LastNamesList[rand.Next(0, NamesList.Count)]}");
            Name = sb.ToString();
        }

        public override void Say(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            base.Say(message);
            Console.ResetColor();
        }

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
            if (MyDiscounts.Priority < other.MyDiscounts.Priority) return -1;
            if (MyDiscounts.Priority > other.MyDiscounts.Priority) return 1;
            return 0;
        }

        public void SetWaitForServicesTimer()
        {
            _waitForService = new Timer(TimeTool.ConvertToGameTime(72*TimeTool.Thousand));
            _waitForService.Elapsed += OnScandalEvent;
            _waitForService.AutoReset = false;
            _waitForService.Enabled = true;
        }

        public void OnScandalEvent(Object source, EventArgs e)
        {
            Say($"{Name}: THIS IS NOT GOING ANYWHERE!!!! I have been waiting for 3 days already!");
            Say($"{Name} slams the door and leaves the Auto Repair Shop");
            ShopManager.HandleProblematicCustomer(this);
            RepairAutomationTool.RemoveDisappointedCustomer(this);
        }

        public void StopWaitForServicesTimer()
        {
            _waitForService.Enabled = false;
        }
    }
}
