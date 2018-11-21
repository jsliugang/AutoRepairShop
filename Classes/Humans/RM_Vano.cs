using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars;
using AutoRepairShop.Classes.Cars.CarParts;
using AutoRepairShop.Classes.Cars.Modifications;
using AutoRepairShop.Classes.Humans;

namespace AutoRepairShop.Classes.Humans
{
    class RM_Vano:RepairMan
    {
        public RM_Vano()
        {
            Name = "Vano";
        }

        public void ReplacePart(CarPart part)
        {
            _carPart = part;
            Disassemble();
            Thread.Sleep(5000);
            Replace();
            Thread.Sleep(10000);
            Assemble();
        }

        private void Replace()
        {
            Console.WriteLine($"Replacing the broken part with new one!");
            _carPart.IsWorking = true;
        }

        
    }
}
