using System;
using System.Linq;
using System.Threading;
using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Data.Models.CarTypes;
using AutoRepairShop.Services;
using AutoRepairShop.Tools;
using AutoRepairShop.WorkFlow;
using Timer = System.Timers.Timer;

namespace AutoRepairShop.Data.Models.Humans
{
    internal abstract class RepairMan : Human
    {
        public double Salary { get; set; }
        public bool IsBusy { get; set; }
        protected static Random Rand = new Random();

        protected RepairMan()
        {
            Salary = 0;
        }

        public void Say(string message, ConsoleColor clr)
        {
            Console.ForegroundColor = clr;
            Console.WriteLine($"{message}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void SetBusy(bool value)
        {
            IsBusy = value;
        }

        protected void Disassemble(CarPart part, ConsoleColor clr)
        {
            Say($"{Name} is disassembling the {part.Name}", clr);
        }

        protected void Repair(CarPart part, ConsoleColor clr)
        {
            Say($"{Name} is repairing the {part.Name}", clr);
            part.Durability = 70;
        }

        protected void Assemble(CarPart part, ConsoleColor clr)
        {
            Say($"{Name} is assembling the {part.Name}", clr);
        }

        public void MakeRepairs(string partName, Car car, ConsoleColor clr)
        {
            IsBusy = true;
            var carPart = car.CarContent.Find(x => x.Name == partName);
            Disassemble(carPart, clr);
            Thread.Sleep(TimeTool.ConvertToRealTime(2)*TimeTool.Thousand);
            Repair(carPart, clr);
            Thread.Sleep(TimeTool.ConvertToRealTime(1) * TimeTool.Thousand);
            Assemble(carPart, clr);
            IsBusy = false;
        }

        public void DiagnozeCar(Customer customer, ConsoleColor clr)
        {
            foreach (var part in customer.MyCar.CarContent)
            {
                Thread.Sleep(TimeTool.ConvertToRealTime(0.2) * TimeTool.Thousand);
                Say(part.Durability>0
                    ? $"{Name} found that {part.Name} is OK! Durability: {part.Durability}"
                    : $"{Name} found that {part.Name} is broken!", clr);
                if (part.Durability == 0)
                    customer.MyAgreement.PartsToReplace.Add(part);
                if (part.Durability <= 15 && part.Durability > 0)
                    customer.MyAgreement.PartsToRepair.Add(part);
            }
            IsBusy = false;
        }

        public virtual int ReplaceFluid(Car car, ConsoleColor clr)
        {
            IsBusy = true;
            Say($"{Name}: Replacing fluids.", clr);
            for (var i = 0; i < car.CarLiquids.CarLiquids.Count; i++)
                car.CarLiquids.UpdateAmount(car.CarLiquids.CarLiquids.ElementAt(i).Key, 100);
            Thread.Sleep(TimeTool.ConvertToRealTime(3) * TimeTool.Thousand);
            Say($"{Name}: All done!", clr);
            IsBusy = false;
            return 150; //fixed price to replace all liquids
        }

        public CarPart GetNewWorkingPart(string partName, ConsoleColor clr)
        {
            CarPart newPart;
            while (true)
            {
                newPart = CheckPartAvailability(partName, clr);
                if (newPart != null)
                {
                    if (newPart.Durability > 60)
                        break;
                    Say($"{Name}: The new {newPart.Name} has defects, I will look for another one.", clr);
                    continue;
                }
                Say($"{Name}: {partName} is not in garage, we have to request it from Stock.", clr);
                RequestPartFromStock(partName, clr);
            }
            return newPart;
        }

        public virtual void ReplacePart(string partName, Car car, ConsoleColor clr)
        {
            IsBusy = true;
            var newPart = GetNewWorkingPart(partName, clr);
            ShopManager.Lucy.PartsUsedMonthlyList[newPart.GetType()] += 1;
            var oldPart = car.CarContent.Find(x => x.Name == partName);
            Say($"New part durability = {newPart.Durability}", clr);
            Disassemble(oldPart, clr);
            Thread.Sleep(TimeTool.ConvertToRealTime(1) * TimeTool.Thousand);
            Say($"Replacing the broken part with new one!", clr);
            Thread.Sleep(TimeTool.ConvertToRealTime(2) * TimeTool.Thousand);
            Assemble(newPart, clr);
            car.CarContent[car.CarContent.IndexOf(car.CarContent.Find(x => x.Name == partName))] = newPart;
            IsBusy = false;
        }

        public virtual void Modify(string modificationType, Car car, ConsoleColor clr)
        {
            IsBusy = true;
            Say($"Applying modifications to {car.Name}", clr);
            var newPart = GetNewWorkingPart(modificationType, clr);
            car.CarContent.Add(newPart);
            Thread.Sleep(TimeTool.ConvertToRealTime(3) * TimeTool.Thousand);
            Say($"All done!", clr);
            IsBusy = false;
        }

        public CarPart CheckPartAvailability(string name, ConsoleColor clr)
        {
            var newPart = ShopManager.GarStMan.RetrieveNewCarPart(name);
            if (newPart != null)
                return newPart;
            Say($"{name} is out of stock!", clr);
            return null;
        }

        public void RequestPartFromStock(string partName, ConsoleColor clr)
        {
            Say($"{partName} successfully requested and moved to garage!", clr);
            if (StockService.Read(partName) == null)
            {
                Say($"{partName} is out of stock!", clr);
                return;
            }
            StockService.MovepartToGarage(partName);
            Say($"{partName} has been delivered to Garage!", clr);
        }

        public void GetSickLeave(bool sick, RepairMan rm)
        {
            if (!sick)
                return;
            IsBusy = true;
            Say($"Oh noes!, {Name} got sick! Got to drink some vodka to feel better!");
            var sickLeaveTimer = new Timer(TimeTool.ConvertToRealTime(120) * TimeTool.Thousand);
            sickLeaveTimer.Elapsed += (source, e) => OnHealthy(rm);
            sickLeaveTimer.AutoReset = false;
            sickLeaveTimer.Enabled = true;
        }

        private static void OnHealthy(RepairMan rm)
        {
            rm.IsBusy = false;
        }

        public void GetSalary(double money)
        {
            Salary += money;
        }
    }
}