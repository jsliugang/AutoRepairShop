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
            _carPart = part;
            var newPart = CheckPartAvailability(_carPart.Name);
            if (newPart!=null)
            {
                Disassemble();
                Thread.Sleep(5000);
                car.CarContent.Find(x => x.Name == part.Name).IsWorking = newPart.IsWorking;
                Replace();
                Thread.Sleep(10000);
                Assemble();
                return _carPart.Cost;
            }
            return 0;
        }

        private void Replace()
        {        
            Console.WriteLine($"Replacing the broken part with new one!");
        }

        
    }
}
