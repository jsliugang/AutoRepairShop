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
        protected CarPart _carPart;

        protected RepairMan()
        {

        }
        protected void Disassemble()
        {
            Console.WriteLine($"{Name} is disassembling the {_carPart.Name}");
        }

        protected void Repair()
        {
            Console.WriteLine($"{Name} is repairing the {_carPart.Name}");
            _carPart.IsWorking = true;
        }

        protected void Assemble()
        {
            Console.WriteLine($"{Name} is assembling the {_carPart.Name}");
        }

        public void MakeRepairs(CarPart carPart)
        {
            _carPart = carPart; 
            Disassemble();
            Thread.Sleep(10000);
            Repair();
            Thread.Sleep(5000);
            Assemble();
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
            Console.WriteLine($"{Name} is out of stock!");
            return null;
        }
    }
}
