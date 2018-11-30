using System;

namespace AutoRepairShop.Data.Models.CarTypes
{
    abstract class TruckCar:Car, IRadio
    {
        public bool IsWorking { get; set; }
        public bool RadioState { get; set; }

        public void SwitchRadio()
        {
            RadioState = !RadioState;
            Console.WriteLine(RadioState ? $"Radio switched on!" : $"Radio switched off!");
        }

        protected TruckCar()
        {
            IsWorking = true;
            RadioState = false;
        }           
    }
}
