using System;
using System.Collections.Generic;
using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Data.Models.CarTypes;
using AutoRepairShop.Tools;
using AutoRepairShop.WorkFlow;

namespace AutoRepairShop.Data.Models.CarBuilders
{
    public abstract class CarBuilder:ICarBuilder
    {
        public Car Car { get; set; }
        public List<string> CarNames;


        public CarBuilder(bool random)
        {
            CarNames = new List<string>();
            SetCarNamesList();
            if (!random) //manual input
            {
                MsgDecoratorTool.PrintServiceMessage($"Please choose Car subtype:");
                foreach (string carName in CarNames)
                {
                    MsgDecoratorTool.PrintServiceMessage($"{CarNames.IndexOf(carName)}. {carName}");
                }
            }
        }

        public CarBuilder()
        {
            
        }

        public void RandomCarName()
        {
            Random rand = new Random();
            Car.Name = CarNames[rand.Next(0, CarNames.Count)];
        }

        protected abstract void SetCarNamesList();

        protected void ProcessNamingInput()
        {
            Int32.TryParse(Console.ReadLine(), out int userInput);
            Car.Name = CarNames[userInput];
        }

        public virtual Car CreateCar()
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
            SetLiquids();
            SetMods();
            return Car;
        }

        public Car CreateCarRandomly()
        {
            Random rand = new Random();
            Car.CarContent.Add(new BodyPart(rand.NextDouble() > 0.5));
            Car.CarContent.Add(new CarburetorPart(rand.NextDouble() > 0.5));
            Car.CarContent.Add(new EnginePart(rand.NextDouble() > 0.5));
            Car.CarContent.Add(new GearboxPart(rand.NextDouble() > 0.5));
            Car.CarContent.Add(new HeatRegulatorPart(rand.NextDouble() > 0.5));
            Car.CarContent.Add(new HornPart(rand.NextDouble() > 0.5));
            Car.CarContent.Add(new MufflerPart(rand.NextDouble() > 0.5));
            Car.CarContent.Add(new RadiatorPart(rand.NextDouble() > 0.5));
            Car.CarContent.Add(new WheelsPart(rand.NextDouble() > 0.5));
            Car.CarLiquids = new Liquids();
            return Car;
            // TODO: generate random mods
        }

        public void SetBody()
        {
            MsgDecoratorTool.PrintServiceMessage($"Is Body working? 1-Yes, 0-No:");
            Car.CarContent.Add(new BodyPart(SetWorking()));
        }

        public void SetCarburetor()
        {
            MsgDecoratorTool.PrintServiceMessage($"Is Carburetor working? 1-Yes, 0-No:");
            Car.CarContent.Add(new CarburetorPart(SetWorking()));
        }

        public void SetEngine()
        {
            MsgDecoratorTool.PrintServiceMessage($"Is Engine working? 1-Yes, 0-No:");
            Car.CarContent.Add(new EnginePart(SetWorking()));
        }

        public void SetGearbox()
        {
            MsgDecoratorTool.PrintServiceMessage($"Is Gearbox working? 1-Yes, 0-No:");
            Car.CarContent.Add(new GearboxPart(SetWorking()));
        }

        public void SetHeatRegulator()
        {
            MsgDecoratorTool.PrintServiceMessage($"Is Heat Regulator working? 1-Yes, 0-No:");
            Car.CarContent.Add(new HeatRegulatorPart(SetWorking()));
        }

        public void SetHorn()
        {
            MsgDecoratorTool.PrintServiceMessage($"Is Horn working? 1-Yes, 0-No:");
            Car.CarContent.Add(new HornPart(SetWorking()));
        }

        public void SetLiquids()
        {
            Car.CarLiquids = new Liquids();
        }

        public void SetMuffler()
        {
            MsgDecoratorTool.PrintServiceMessage($"Is Muffler working? 1-Yes, 0-No:");
            Car.CarContent.Add(new MufflerPart(SetWorking()));
        }

        public void SetRadiator()
        {
            MsgDecoratorTool.PrintServiceMessage($"Is Radiator working? 1-Yes, 0-No:");
            Car.CarContent.Add(new RadiatorPart(SetWorking()));
        }

        public void SetWheels()
        {
            MsgDecoratorTool.PrintServiceMessage($"Are wheels working? 1-Yes, 0-No:");
            Car.CarContent.Add(new WheelsPart(SetWorking()));
        }

        public void SetMods()
        {
            while (true)
            {
                MsgDecoratorTool.PrintServiceMessage($"Please choose modes (press 0 to quit):");
                MsgDecoratorTool.PrintServiceMessage($"1. Custom bonnet");
                MsgDecoratorTool.PrintServiceMessage($"2. Decals");
                MsgDecoratorTool.PrintServiceMessage($"3. ExhaustPipe");
                MsgDecoratorTool.PrintServiceMessage($"4. NO 2");
                MsgDecoratorTool.PrintServiceMessage($"5. Spinners");
                MsgDecoratorTool.PrintServiceMessage($"6. Spoiler");
                MsgDecoratorTool.PrintServiceMessage($"7. Sport Suspension");
                MsgDecoratorTool.PrintServiceMessage($"8. Titanium Wipers");
                Int32.TryParse(Console.ReadLine(), out int userInput);
                switch (userInput)
                {
                    case 1:
                        MsgDecoratorTool.PrintServiceMessage($"Is Bonnet OK? 1-Yes, 0-No:");
                        Car.CarContent.Add(new CustomBonnetMod(SetWorking()));
                        break;
                    case 2:
                        MsgDecoratorTool.PrintServiceMessage($"Are Decals OK? 1-Yes, 0-No:");
                        Car.CarContent.Add(new DecalsMod(SetWorking()));
                        break;
                    case 3:
                        MsgDecoratorTool.PrintServiceMessage($"Is Exhaust Pipe OK? 1-Yes, 0-No:");
                        Car.CarContent.Add(new ExhaustPipeMod(SetWorking()));
                        break;
                    case 4:
                        MsgDecoratorTool.PrintServiceMessage($"Is NO2 OK? 1-Yes, 0-No:");
                        Car.CarContent.Add(new No2Mod(SetWorking()));
                        break;
                    case 5:
                        MsgDecoratorTool.PrintServiceMessage($"Are Spinners OK? 1-Yes, 0-No:");
                        Car.CarContent.Add(new SpinnersMod(SetWorking()));
                        break;
                    case 6:
                        MsgDecoratorTool.PrintServiceMessage($"Is Spolier OK? 1-Yes, 0-No:");
                        Car.CarContent.Add(new SpoilerMod(SetWorking()));
                        break;
                    case 7:
                        MsgDecoratorTool.PrintServiceMessage($"Is Sport Suspension OK? 1-Yes, 0-No:");
                        Car.CarContent.Add(new SportSuspensionMod(SetWorking()));
                        break;
                    case 8:
                        MsgDecoratorTool.PrintServiceMessage($"Are Titanium Wipers OK? 1-Yes, 0-No:");
                        Car.CarContent.Add(new TitaniumWipersMod(SetWorking()));
                        break;

                    default:
                        MsgDecoratorTool.PrintServiceMessage($"No modifications chosen.");
                        break;
                }
                MsgDecoratorTool.PrintServiceMessage($"Add more? Yes-1, No-0:");
                Int32.TryParse(Console.ReadLine(), out int again);
                if (again == 1)
                {
                    continue;
                }
                break;
            }
        }

        public bool SetWorking()
        {
            Int32.TryParse(Console.ReadLine(), out int i);
            return i == 1;
        }
    }
}