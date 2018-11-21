using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Interfaces;

namespace AutoRepairShop.Classes.Cars.CarTypes
{
    class TruckCar:Car, IRadio
    {
        public bool IsWorking { get; set; }
        public bool RadioState { get; set; }

        public void SwitchRadio()
        {
            RadioState = !RadioState;
            if (RadioState)
            {
                Console.WriteLine($"Radio switched on!");
            }
            else
            {
                Console.WriteLine($"Radio switched off!");
            }
        }

        public TruckCar():base("Truck")
        {
            IsWorking = true;
            RadioState = false;
        }

        
        
    }
}
