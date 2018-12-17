using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using AutoRepairShop.CourtServiceReference;
using AutoRepairShop.WorkFlow;
using AutoRepairShop.Data.Models.CarTypes;
using AutoRepairShop.Data.Repository;
using AutoRepairShop.Tools;
using Timer = System.Timers.Timer;

namespace AutoRepairShop.Data.Models.Humans
{
    internal class Customer:Human, IComparable<Customer> 
    {
        public Car MyCar { get; set; }
        public DiscountCard MyDiscounts = new DiscountCard();
        public Timer WaitForService;
        public static Random Rand = new Random();
        public ServiceAgreement MyAgreement;
        private Timer _checkCar;
        private Timer _warrantyTimer;

        public Customer(Car car)
        {
            MyCar = car;
            Name = NamesList[Rand.Next(0, NamesList.Count)];
            LastName = LastNamesList[Rand.Next(0, NamesList.Count)];
            SetCheckCarTimer();
            MyAgreement = new ServiceAgreement(Name);
        }

        public void SetWarrantyTimer()
        {
            _warrantyTimer = new Timer(TimeTool.ConvertToRealTime(240) * TimeTool.Thousand);
            _warrantyTimer.Elapsed += OnWarrantyCaseEvent;
            _warrantyTimer.AutoReset = false;
            _warrantyTimer.Enabled = true;
            MyCar.IsOnWarranty = true;
        }

        private void OnWarrantyCaseEvent(Object source, ElapsedEventArgs e)
        {
            MyCar.IsOnWarranty = false;
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
                GoToRepairShop();
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

        private async void GoToRepairShop()
        {
            while (true)
            {
                bool result = await ShopManager.AcceptNewCustomerAsync(this);
                if (result)
                {
                    SetWaitForServicesTimer();
                }
                else
                {
                    Thread.Sleep(TimeTool.ConvertToRealTime(2) * TimeTool.Thousand); //postpone for 2 game hours
                    continue;
                }
                break;
            }
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
            ShopManager.ProcessOrder(1, "", this);
        }

        public void RepairBrokenParts()
        {
            Say($"Please repair all the broken parts of my {MyCar.Name}.");
            foreach (var carPart in MyAgreement.PartsToRepair)
            {
                ShopManager.ProcessOrder(2, carPart.Name, this);
            }
        }

        public void PimpMyCar(string modificationType)
        {
            Say($"Xzibit, pimp my {MyCar.Name}!!");
            ShopManager.ProcessOrder(3, modificationType, this);
        }

        public void ReplaceBrokenParts()
        {
            Say($"Replace all broken parts in {MyCar.Name}, please...");
            foreach (var carPart in MyAgreement.PartsToReplace)
            {
                ShopManager.ProcessOrder(4, carPart.Name, this);
            }
        }

        public void ReplaceLiquids()
        {
            Say($"Replace all the liquids in {MyCar.Name}, please...");
            ShopManager.ProcessOrder(5, "", this);
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
            //ShopManager.HandleProblematicCustomer(this);
            RepairAutomationTool.RemoveDisappointedCustomer(this);
        }

        public void StopWaitForServicesTimer()
        {
            WaitForService.Enabled = false;
        }
    }
}