using System;
using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.CarBuilders
{
    class StreetSweeperBuilder:CarBuilder
    {
        public StreetSweeperBuilder(bool random):base(random)
        {
            Car = new StreetSweeper();
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
            CarNames.Add("MX - 450");
            CarNames.Add("Dulevo sweeper");
        }
    }
}
