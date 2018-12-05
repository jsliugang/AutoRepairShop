using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Data.Models.CarTypes;
using AutoRepairShop.Tools;
using AutoRepairShop.WorkFlow;
using Timer = System.Timers.Timer;

namespace AutoRepairShop.Data.Models.Humans
{
    abstract class RepairMan : Human
    {
        public double Salary { get; set; }
        public bool IsBusy { get; set; }
        public int Priority { get; set; }
        protected static Random rand = new Random();

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
            CarPart carPart = ShopManager.CurrentCustomer.MyCar.CarContent.Find(x => x.Name == partName);
            Disassemble(carPart);
            Thread.Sleep(10000);
            Repair(carPart);
            Thread.Sleep(5000);
            Assemble(carPart);
        }
        
        public List<CarPart> DiagnozeCar(Car car)
        {
            List<CarPart> diagnosticsResults = new List<CarPart>();
            foreach (CarPart part in car.CarContent)
            {
                Thread.Sleep(1000);
                Console.WriteLine(part.IsWorking
                    ? $"{Name} found that {part.Name} is OK! Durability: {part.Durability}"
                    : $"{Name} found that {part.Name} is broken!");
                if (part.Durability<=15)
                {
                    diagnosticsResults.Add(part);
                }
            }
            return diagnosticsResults;
        }

        public CarPart CheckPartAvailability(string name)
        {
            CarPart newPart = ShopManager.GarStMan.RetrieveNewCarPart(name);
            if (newPart != null)
            {
                return newPart;
            }
            Console.WriteLine($"{name} is out of stock!");
            return null;
        }

        public bool RequestPartFromStock(string partName)
        {
            if (ShopManager.MovePartToGarage(partName))
            {
                Thread.Sleep(10000);
                Console.WriteLine($"{partName} successfully requested and moved to garage!");
                return true;
            }
            Console.WriteLine($"{partName} is out of stock!");
            return false;
        }

        public void GetSickLeave(bool sick, RepairMan rm)
        {
            if (!sick)
                return;
            IsBusy = true;
            Console.WriteLine($"Oh noes!, {Name} got sick! Got to drink some vodka to feel better!");
            Timer sickLeaveTimer = new Timer(TimeTool.ConvertToGameTime(120) * TimeTool.Thousand);
            sickLeaveTimer.Elapsed += (source, e) => OnHealthy(source, e, rm);
            sickLeaveTimer.AutoReset = false;
            sickLeaveTimer.Enabled = true;
        }

        private static void OnHealthy(Object source, ElapsedEventArgs e, RepairMan rm)
        {
            rm.IsBusy = false;
        }

        public void GetSalary(double money)
        {
            Salary += money;
        }
    }
}
