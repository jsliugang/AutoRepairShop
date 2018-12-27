using System;

namespace AutoRepairShop.Data.Models.CarTypes
{
    internal abstract class PassengerCar:Car, IRadio, ISensor
    {
        public bool RadioState { get; set; }
        bool IRadio.IsWorking { get; set; }
        bool ISensor.IsWorking { get; set; }

        public void SwitchRadio()
        {
            RadioState = !RadioState;
            Console.WriteLine(RadioState ? $"Radio switched on!" : $"Radio switched off!");
        }

        public void SensorData()
        {
            Console.WriteLine($"The sensors are sensing!");
        }

        protected PassengerCar()
        {
            RadioState = false;
        }
    }
}
