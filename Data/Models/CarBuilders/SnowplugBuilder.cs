using System;
using AutoRepairShop.Data.Base;
using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.CarBuilders
{
    class SnowplugBuilder:CarBuilder
    {
        public SnowplugBuilder(bool random):base(random)
        {
            Car = new Snowplug();
            if (random)
            {
                RandomCarName();
            }
            else
            {
                ProcessNamingInput();
            }
        }

        public override void CreateCar()
        {
            Random rand = new Random();
            Car.CarContent.Add(new BodyPart(rand.NextDouble() > 0.5));
            Car.CarContent.Add(new CarburetorPart(rand.NextDouble() > 0.5));
            Car.CarContent.Add(new EnginePart(rand.NextDouble() > 0.5));
            Car.CarContent.Add(new GearboxPart(rand.NextDouble() > 0.5));
            Car.CarContent.Add(new HeatRegulatorPart(rand.NextDouble() > 0.5));
            Car.CarContent.Add(new MufflerPart(rand.NextDouble() > 0.5));
            Car.CarContent.Add(new RadiatorPart(rand.NextDouble() > 0.5));
            Car.CarContent.Add(new WheelsPart(rand.NextDouble() > 0.5));
        }

        protected override void SetCarNamesList()
        {
            CarNames.Add("Snowplug ZLST551Q");
            CarNames.Add("Petrol 11 HP snowplug STG1101QE-02");
        }
    }
}
