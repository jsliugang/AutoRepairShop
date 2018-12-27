using System;
using AutoRepairShop.Data.Models.CarBuilders;
using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.WorkFlow
{
    internal class CarMaker
    {
        public static Random Rand = new Random();

        public Car MakeCar()
        {
            CarBuilder cb = ReturnBuilder(Rand.Next(1, 11), true);
            cb.CreateCar();
            return cb.Car;
        }

        public CarBuilder ReturnBuilder(int carBuilder, bool random)
        {
            CarBuilder cb;
            switch (carBuilder)
            {
                case 1:
                    cb = new AmbulanceBuilder();
                    return cb;
                case 2:
                    cb = new CarHaulerBuilder();
                    return cb;
                case 3:
                    cb = new DumpTruckBuilder();
                    return cb;
                case 4:
                    cb = new OffroaderBuilder();
                    return cb;
                case 5:
                    cb = new PickupBuilder();
                    return cb;
                case 6:
                    cb = new PrimeMoverBuilder();
                    return cb;
                case 7:
                    cb = new RacecarBuilder();
                    return cb;
                case 8:
                    cb = new SnowplugBuilder();
                    return cb;
                case 9:
                    cb = new StreetSweeperBuilder();
                    return cb;
                case 10:
                    cb = new TractorBuilder();
                    return cb;
                case 11:
                    cb = new WagonBuilder();
                    return cb;
                default:
                    return ReturnBuilder(Rand.Next(1, 11), random);
            }
        }
    }
}
