using System;
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
            Car.CarContent.Add(new BodyPart(SetRandomDurability()));
            Car.CarContent.Add(new CarburetorPart(SetRandomDurability()));
            Car.CarContent.Add(new EnginePart(SetRandomDurability()));
            Car.CarContent.Add(new GearboxPart(SetRandomDurability()));
            Car.CarContent.Add(new HeatRegulatorPart(SetRandomDurability()));
            Car.CarContent.Add(new MufflerPart(SetRandomDurability()));
            Car.CarContent.Add(new RadiatorPart(SetRandomDurability()));
            Car.CarContent.Add(new WheelsPart(SetRandomDurability()));
        }

        protected override void SetCarNamesList()
        {
            CarNames.Add("Snowplug ZLST551Q");
            CarNames.Add("Petrol 11 HP snowplug STG1101QE-02");
        }
    }
}
