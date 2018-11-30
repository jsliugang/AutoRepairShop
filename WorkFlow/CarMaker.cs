﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoRepairShop.Data.Enums;
using AutoRepairShop.Data.Models.CarBuilders;
using AutoRepairShop.Data.Models.CarTypes;
using AutoRepairShop.Data.Models.Factories;
using AutoRepairShop.Tools;

namespace AutoRepairShop.WorkFlow
{
    class CarMaker
    {
        public List<CarBuilder> CarBuilders = new List<CarBuilder>
        {
            new AmbulanceBuilder()
        };
        public CarMaker()
        {
            CarBuilders.Add(new AmbulanceBuilder(true));
        }

        public Car MakeRandomCar()
        {
            Type t = CarBuilders[0].GetType();
            var cb = AbstractFactory.Create<CarBuilders[0].GetType()> ();
            return cb.CreateCarRandomly();
        }

        //ReturnBuilder(rand.Next(1,12), true);

        public Car MakeCar()
        {
            CarBuilder cb = ReturnBuilder(SelectBuilder(), false);
            cb.CreateCar();
            return cb.Car;
        }

        public int SelectBuilder()
        {
            MsgDecoratorTool.PrintServiceMessage("**CAR BUILDER**");
            MsgDecoratorTool.PrintServiceMessage($"Select car type:");
            foreach (CarsEnum carEnum in Enum.GetValues(typeof(CarsEnum)))
            {
                MsgDecoratorTool.PrintServiceMessage($"{carEnum.GetHashCode()}. {carEnum}");
            }
            Int32.TryParse(Console.ReadLine(), out int userInput);
            return userInput;
        }

        public CarBuilder ReturnBuilder(int userInput, bool random)
        {
            CarBuilder cb;
            switch (userInput)
            {
                case 1:
                    cb = new AmbulanceBuilder(random);
                    return cb;
                case 2:
                    cb = new CarHaulerBuilder(random);
                    return cb;
                case 3:
                    cb = new DumpTruckBuilder(random);
                    return cb;
                case 4:
                    cb = new OffroaderBuilder(random);
                    return cb;
                case 5:
                    cb = new PickupBuilder(random);
                    return cb;
                case 6:
                    cb = new PrimeMoverBuilder(random);
                    return cb;
                case 7:
                    cb = new RacecarBuilder(random);
                    return cb;
                case 8:
                    cb = new SnowplugBuilder(random);
                    return cb;
                case 9:
                    cb = new StreetSweeperBuilder(random);
                    return cb;
                case 10:
                    cb = new TractorBuilder(random);
                    return cb;
                case 11:
                    cb = new WagonBuilder(random);
                    return cb;
                default:
                    return ReturnBuilder(SelectBuilder(), random);
            }
        }
    }


}
