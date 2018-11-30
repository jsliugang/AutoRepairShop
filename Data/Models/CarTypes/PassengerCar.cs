using System;

namespace AutoRepairShop.Data.Models.CarTypes
{
    abstract class PassengerCar:Car, IRadio, ISensor
    {
        bool IRadio.IsWorking { get; set; }
        public bool RadioState { get; set; }
        bool ISensor.IsWorking { get; set; }

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
