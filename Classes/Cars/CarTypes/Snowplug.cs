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
        protected Snowplug()
        {
            CarNames.Add("Snowplug ZLST551Q");
            CarNames.Add("Petrol 11 HP snowplug STG1101QE-02");
        }    
    }
}
