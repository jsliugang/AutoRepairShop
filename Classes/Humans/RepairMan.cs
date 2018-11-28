using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoRepairShop.Classes;
using AutoRepairShop.Classes.Cars.CarTypes;
using AutoRepairShop.Classes.Cars.CarParts;

namespace AutoRepairShop.Classes.Humans
{
    abstract class RepairMan : Human
    {
        public bool IsBusy { get; set; }

        protected RepairMan()
        {

        }
        protected void Disassemble(CarPart part)
        {
            Console.WriteLine($"{Name} is disassembling the {part.Name}");
        }

        protected void Repair(CarPart part)
        {
            Console.WriteLine($"{Name} is repairing the {part.Name}");
            part.IsWorking = true;
        }

        protected void Assemble(CarPart part)
        {
            Console.WriteLine($"{Name} is assembling the {part.Name}");
        }

        public void MakeRepairs(CarPart carPart)
        {
            Disassemble(carPart);
            Thread.Sleep(10000);
            Repair(carPart);
            Thread.Sleep(5000);
            Assemble(carPart);
        }
        
        public void DiagnozeCar(Car car)
        {   
            foreach (CarPart part in car.CarContent)
            {
                Thread.Sleep(1000);
                Console.WriteLine(part.IsWorking
                    ? $"{Name} found that {part.Name} is OK!"
                    : $"{Name} found that {part.Name} is broken!");
            }
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

        public void GetSickLeave()
        {
            IsBusy = true;
            //specify amount of days sick
        }
    }
}
