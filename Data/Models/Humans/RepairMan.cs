using System;
using System.Linq;
using System.Threading;
using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Data.Models.CarTypes;
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

        protected void Disassemble(CarPart part)
        {
            Console.WriteLine($"{Name} is disassembling the {part.Name}");
        }

        protected void Repair(CarPart part)
        {
            Console.WriteLine($"{Name} is repairing the {part.Name}");
            part.Durability = 70;
        }

        protected void Assemble(CarPart part)
        {
            Console.WriteLine($"{Name} is assembling the {part.Name}");
        }

        public void MakeRepairs(string partName)
        {
            var carPart = ShopManager.CurrentCustomer.MyCar.CarContent.Find(x => x.Name == partName);
            Disassemble(carPart);
            Thread.Sleep(10000);
            Repair(carPart);
            Thread.Sleep(5000);
            Assemble(carPart);
        }

        public void DiagnozeCar(Car car)
        {
            foreach (var part in car.CarContent)
            {
                Thread.Sleep(1000);
                Console.WriteLine(part.IsWorking
                    ? $"{Name} found that {part.Name} is OK! Durability: {part.Durability}"
                    : $"{Name} found that {part.Name} is broken!");
                if (part.Durability == 0)
                    ShopManager.CurrentCustomer.MyAgreement.PartsToReplace.Add(part);
                if (part.Durability <= 15 && part.Durability > 0)
                    ShopManager.CurrentCustomer.MyAgreement.PartsToRepair.Add(part);
            }
        }

        public virtual int ReplaceFluid(Car car)
        {
            Console.WriteLine($"{Name}: Replacing fluids.");
            for (var i = 0; i < car.CarLiquids.CarLiquids.Count; i++)
                car.CarLiquids.UpdateAmount(car.CarLiquids.CarLiquids.ElementAt(i).Key, 100);
            Thread.Sleep(15000);
            Console.WriteLine($"{Name}: All done!");
            return 150; //fixed price to replace all liquids
        }

        public CarPart GetNewWorkingPart(string partName)
        {
            CarPart newPart;
            while (true)
            {
                newPart = CheckPartAvailability(partName);
                if (newPart != null)
                {
                    if (newPart.Durability > 60)
                        break;
                    Console.WriteLine($"{Name}: The new {newPart.Name} has defects, I will look for another one.");
                    continue;
                }
                Console.WriteLine($"{Name}: {partName} is not in garage, we have to request it from Stock.");
                RequestPartFromStock(partName);
            }
            return newPart;
        }

        public virtual void ReplacePart(string partName, Car car)
        {
            var newPart = GetNewWorkingPart(partName);
            var oldPart = car.CarContent.Find(x => x.Name == partName);
            Console.WriteLine($"New part durability = {newPart.Durability}");
            Disassemble(oldPart);
            Thread.Sleep(5000);
            oldPart = newPart;
            Console.WriteLine($"Replacing the broken part with new one!");
            Thread.Sleep(10000);
            Assemble(oldPart);
            car.CarContent[car.CarContent.IndexOf(car.CarContent.Find(x => x.Name == partName))] = oldPart;
        }

        public virtual void Modify(string modificationType, Car car)
        {
            Console.WriteLine($"Applying modifications to {car.Name}");
            var newPart = GetNewWorkingPart(modificationType);
            car.CarContent.Add(newPart);
            Thread.Sleep(15000);
            Console.WriteLine($"All done!");
        }

        public CarPart CheckPartAvailability(string name)
        {
            var newPart = ShopManager.GarStMan.RetrieveNewCarPart(name);
            if (newPart != null)
                return newPart;
            Console.WriteLine($"{name} is out of stock!");
            return null;
        }

        public void RequestPartFromStock(string partName)
        {
            if (ShopManager.MovePartToGarage(partName))
            {
                Thread.Sleep(10000);
                Console.WriteLine($"{partName} successfully requested and moved to garage!");
            }
            Console.WriteLine($"{partName} is out of stock!");
        }

        public void GetSickLeave(bool sick, RepairMan rm)
        {
            if (!sick)
                return;
            IsBusy = true;
            Console.WriteLine($"Oh noes!, {Name} got sick! Got to drink some vodka to feel better!");
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