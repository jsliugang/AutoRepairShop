using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using AutoRepairShop.CourtServiceReference;
using AutoRepairShop.WorkFlow;
using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Data.Models.CarTypes;
using AutoRepairShop.Data.Repository;
using AutoRepairShop.Tools;

namespace AutoRepairShop.Data.Models.Humans
{
    class Customer:Human, IComparable<Customer> 
    {
        public Car MyCar { get; set; }
        public DiscountCard MyDiscounts = new DiscountCard();
        public Timer WaitForService;
        private Timer _checkCar;
        private Timer _warrantyTimer;
        public static Random rand = new Random();
        public ServiceAgreement MyAgreement;

        public Customer(Car car)
        {
            MyCar = car;
            Name = NamesList[rand.Next(0, NamesList.Count)];
            LastName = LastNamesList[rand.Next(0, NamesList.Count)];
            SetCheckCarTimer();
            MyAgreement = new ServiceAgreement(Name);
        }

        public void SetWarrantyTimer()
        {
            _warrantyTimer = new Timer(TimeTool.ConvertToRealTime(240) * TimeTool.Thousand);
            _warrantyTimer.Elapsed += OnWarrantyCaseEvent;
            _warrantyTimer.AutoReset = false;
            _warrantyTimer.Enabled = true;
        }

        private void OnWarrantyCaseEvent(Object source, ElapsedEventArgs e)
        {
            _warrantyTimer.Dispose();
        }

        private void SetCheckCarTimer()
        {
            _checkCar = new Timer(TimeTool.ConvertToRealTime(24) * TimeTool.Thousand);
            _checkCar.Elapsed += OnCarCheckEvent;
            _checkCar.AutoReset = true;
            _checkCar.Enabled = true;
        }

        private void OnCarCheckEvent(Object source, ElapsedEventArgs e)
        {
            if (MyCar.CarIsWorking) return;
            if (_warrantyTimer != null)
            {
                foreach (var carPart in MyAgreement.PartsToRepair)
                {
                    if (carPart.Durability != 0) continue;
                    GoToCourt();
                    _checkCar.Enabled = false;
                }
                foreach (var carPart in MyAgreement.PartsToReplace)
                {
                    if (carPart.Durability != 0) continue;
                    GoToCourt();
                    _checkCar.Enabled = false;
                }
            }
            else
            {
                GoToRepairShop();
                _checkCar.Enabled = false;
            }
        }

        private void GoToCourt()
        {
            CourtClient client = new CourtClient("BasicHttpBinding_ICourt");
            ServiceContract contract = new ServiceContract();
            contract.CustomerName = GetName();
            contract.TotalPartCost = MyAgreement.TotalPartCost;
            contract.TotalServicesCost = MyAgreement.TotalServicesCost;
            contract.Total = MyAgreement.GetTotal();
            var decision = client.MakeDecision(contract);
            client.Close();

            if (decision == -1)
            {
                GoToRepairShop(true);
            }
            if (decision == 0)
            {
                ShopManager.PayWarrantyCompensation(MyAgreement.TotalServicesCost - (MyDiscounts.GetDiscountRate()*MyAgreement.TotalServicesCost/100));
            }
            if (decision == 1)
            {
                ShopManager.AcceptPayment(1000);
            }
            RepairAutomationTool.RemoveDisappointedCustomer(this);
        }

        private void GoToRepairShop()
        {
            CustomerQueue<Customer>.Enqueue(this, ShopManager.Customers);
            SetWaitForServicesTimer();
        }

        private void GoToRepairShop(bool isOnWarranty)
        {
            MyCar.IsOnWarranty = isOnWarranty;
            CustomerQueue<Customer>.Enqueue(this, ShopManager.Customers);
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
            SetCheckCarTimer();
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

        public void RepairBrokenParts()
        {
            Say($"Please repair all the broken parts of my {MyCar.Name}.");
            foreach (CarPart carPart in ShopManager.CurrentCustomer.MyAgreement.PartsToRepair)
            {
                ShopManager.ProcessOrder(2, carPart.Name);
            }
        }

        public void PimpMyCar(string modificationType)
        {
            Say($"Xzibit, pimp my {MyCar.Name}!!");
            ShopManager.ProcessOrder(3, modificationType);
        }

        public void ReplaceBrokenParts()
        {
            Say($"Replace all broken parts in {MyCar.Name}, please...");
            foreach (CarPart carPart in ShopManager.CurrentCustomer.MyAgreement.PartsToReplace)
            {
                ShopManager.ProcessOrder(4, carPart.Name);
            }
        }

        public void ReplaceLiquids()
        {
            Say($"Replace all the liquids in {MyCar.Name}, please...");
            ShopManager.ProcessOrder(5, "");
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
            WaitForService = new Timer(TimeTool.ConvertToRealTime(72*TimeTool.Thousand));
            WaitForService.Elapsed += OnScandalEvent;
            WaitForService.AutoReset = false;
            WaitForService.Enabled = true;
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
            WaitForService.Enabled = false;
        }
    }
}
