using AutoRepairShop.Classes.Cars.CarParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Enum;

namespace AutoRepairShop.Classes.Managers
{
    abstract class Manager
    {
        Dictionary<CarPart, int> PartsStock = new Dictionary<CarPart, Int32>();
        Dictionary<Modifications, int> ModificationsStock = new Dictionary<Modifications,Int32>();

        public Manager()
        {
            

        }
    }
}
