using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Classes.Cars.CarTypes
{
    class Snowplug:CleaningVehicle
    {
        public Snowplug()
        {

        }

        public void CleanSnow()
        {
            Console.WriteLine($"{Name} is cleaning the snow!");
        }
    }
}
