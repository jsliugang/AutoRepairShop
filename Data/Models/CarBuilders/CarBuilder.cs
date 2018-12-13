using System;
using System.Collections.Generic;
using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.CarBuilders
{
    abstract class CarBuilder
    {
        internal static Random Rand = new Random();
        public Car Car { get; set; }
        public List<string> CarNames;

        protected CarBuilder()
        {
            CarNames = new List<string>();
            SetCarNamesList();
        }

        public void RandomCarName()
        {         
            Car.Name = CarNames[Rand.Next(0, CarNames.Count)];
        }

        protected abstract void SetCarNamesList();

        public virtual void CreateCar()
        {
            SetBody();
            SetCarburetor();
            SetEngine();
            SetGearbox();
            SetHeatRegulator();
            SetHorn();
            SetLiquids();
            SetMuffler();
            SetRadiator();
            SetWheels();
        }

        public void SetBody()
        {
            Car.CarContent.Add(new BodyPart(SetRandomDurability()));
        }

        public void SetCarburetor()
        {
            Car.CarContent.Add(new CarburetorPart(SetRandomDurability()));
        }

        public void SetEngine()
        {
            Car.CarContent.Add(new EnginePart(SetRandomDurability()));
        }

        public void SetGearbox()
        {
            Car.CarContent.Add(new GearboxPart(SetRandomDurability()));
        }

        public void SetHeatRegulator()
        {
            Car.CarContent.Add(new HeatRegulatorPart(SetRandomDurability()));
        }

        public void SetHorn()
        {
            Car.CarContent.Add(new HornPart(SetRandomDurability()));
        }

        public void SetLiquids()
        {
            Car.CarLiquids = new Liquids();
        }

        public void SetMuffler()
        {
            Car.CarContent.Add(new MufflerPart(SetRandomDurability()));
        }

        public void SetRadiator()
        {
            Car.CarContent.Add(new RadiatorPart(SetRandomDurability()));
        }

        public void SetWheels()
        {
            Car.CarContent.Add(new WheelsPart(SetRandomDurability()));
        }

        public byte SetRandomDurability()
        {
            return (byte) Rand.Next(0,101);
        }
    }
}