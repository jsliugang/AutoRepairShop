using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars;
using AutoRepairShop.Classes.Cars.CarParts;
using AutoRepairShop.Classes.Cars.CarTypes;
using AutoRepairShop.Classes.Humans;

namespace AutoRepairShop.Classes.Humans
{
    class RM_Vano:RepairMan
    {
        public RM_Vano()
        {
            Name = "Vano";
        }

        public int ReplacePart(CarPart part, Car car)
        {
            CarPart newPart = CheckPartAvailability(part.Name);
            if (newPart != null)
            {
                Disassemble(part);
                Thread.Sleep(5000);
                car.CarContent.Find(x => x.Name == part.Name).IsWorking = newPart.IsWorking;
                Console.WriteLine($"Replacing the broken part with new one!");
                Thread.Sleep(10000);
                Assemble(part);
                return part.Cost;
            }
            Console.WriteLine($"{Name}: {part.Name} is not in garage, we have to request it from Stock.");
            if (RequestPartFromStock(part.Name))
            {
                return ReplacePart(part, car);
            }
            return 0;
        }       
    }
}
