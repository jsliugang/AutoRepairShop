using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Classes.Cars.CarTypes
{
    class Wagon:PassengerCar
    {
        public Wagon()
        {
            CarNames.Add("Lada Granta");
            CarNames.Add("Audi RS4 V Avant");
        }
    }
}
