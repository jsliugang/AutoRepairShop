using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarBuilders;
using AutoRepairShop.Classes.Cars.CarTypes;

namespace AutoRepairShop.Classes.Cars
{
    class CarMaker
    {
        public Car MakeCar(CarBuilder builder)
        {
            builder.CreateCar();
            return builder.Car;
        }
    }
}
