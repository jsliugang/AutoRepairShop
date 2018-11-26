using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Interfaces;

namespace AutoRepairShop.Classes.Cars.CarTypes
{
    abstract class TruckCar:Car, IRadio
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

        protected TruckCar()
        {
            IsWorking = true;
            RadioState = false;
        }

        
        
    }
}
