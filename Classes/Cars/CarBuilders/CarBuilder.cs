using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarParts;
using AutoRepairShop.Classes.Cars.CarTypes;
using AutoRepairShop.Classes.Managers;

namespace AutoRepairShop.Classes.Cars.CarBuilders
{
    abstract class CarBuilder
    {
        public Car Car { get; set; }
        public List<string> CarNames;

        public CarBuilder(bool random)
        {
            CarNames = new List<string>();
            SetCarNamesList();
            if (!random) //manual input
            {
                Menu.PrintServiceMessage($"Please choose Car subtype:");
                foreach (string carName in CarNames)
                {
                    Menu.PrintServiceMessage($"{CarNames.IndexOf(carName)}. {carName}");
                }
            }

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

        public virtual void CreateCar()
        {
            Menu.PrintServiceMessage("**Please specify what parts are broken: **");
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
        }

        public void CreateCarRandomly()
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
            // TODO: generate random mods
        }

        public void SetBody()
        {
            Menu.PrintServiceMessage($"Is Body working? 1-Yes, 0-No:");
            Car.CarContent.Add(new BodyPart(SetWorking()));
        }

        public void SetCarburetor()
        {
            Menu.PrintServiceMessage($"Is Carburetor working? 1-Yes, 0-No:");
            Car.CarContent.Add(new CarburetorPart(SetWorking()));
        }

        public void SetEngine()
        {
            Menu.PrintServiceMessage($"Is Engine working? 1-Yes, 0-No:");
            Car.CarContent.Add(new EnginePart(SetWorking()));
        }

        public void SetGearbox()
        {
            Menu.PrintServiceMessage($"Is Gearbox working? 1-Yes, 0-No:");
            Car.CarContent.Add(new GearboxPart(SetWorking()));
        }

        public void SetHeatRegulator()
        {
            Menu.PrintServiceMessage($"Is Heat Regulator working? 1-Yes, 0-No:");
            Car.CarContent.Add(new HeatRegulatorPart(SetWorking()));
        }

        public void SetHorn()
        {
            Menu.PrintServiceMessage($"Is Horn working? 1-Yes, 0-No:");
            Car.CarContent.Add(new HornPart(SetWorking()));
        }

        public void SetLiquids()
        {
            Car.CarLiquids = new Liquids();
        }

        public void SetMuffler()
        {
            Menu.PrintServiceMessage($"Is Muffler working? 1-Yes, 0-No:");
            Car.CarContent.Add(new MufflerPart(SetWorking()));
        }

        public void SetRadiator()
        {
            Menu.PrintServiceMessage($"Is Radiator working? 1-Yes, 0-No:");
            Car.CarContent.Add(new RadiatorPart(SetWorking()));
        }

        public void SetWheels()
        {
            Menu.PrintServiceMessage($"Are wheels working? 1-Yes, 0-No:");
            Car.CarContent.Add(new WheelsPart(SetWorking()));
        }

        public void SetMods()
        {
            while (true)
            {
                Menu.PrintServiceMessage($"Please choose modes (press 0 to quit):");
                Menu.PrintServiceMessage($"1. Custom bonnet");
                Menu.PrintServiceMessage($"2. Decals");
                Menu.PrintServiceMessage($"3. ExhaustPipe");
                Menu.PrintServiceMessage($"4. NO 2");
                Menu.PrintServiceMessage($"5. Spinners");
                Menu.PrintServiceMessage($"6. Spoiler");
                Menu.PrintServiceMessage($"7. Sport Suspension");
                Menu.PrintServiceMessage($"8. Titanium Wipers");
                Int32.TryParse(Console.ReadLine(), out int userInput);
                switch (userInput)
                {
                    case 1:
                        Menu.PrintServiceMessage($"Is Bonnet OK? 1-Yes, 0-No:");
                        Car.CarContent.Add(new CustomBonnetMod(SetWorking()));
                        break;
                    case 2:
                        Menu.PrintServiceMessage($"Are Decals OK? 1-Yes, 0-No:");
                        Car.CarContent.Add(new DecalsMod(SetWorking()));
                        break;
                    case 3:
                        Menu.PrintServiceMessage($"Is Exhaust Pipe OK? 1-Yes, 0-No:");
                        Car.CarContent.Add(new ExhaustPipeMod(SetWorking()));
                        break;
                    case 4:
                        Menu.PrintServiceMessage($"Is NO2 OK? 1-Yes, 0-No:");
                        Car.CarContent.Add(new NO2Mod(SetWorking()));
                        break;
                    case 5:
                        Menu.PrintServiceMessage($"Are Spinners OK? 1-Yes, 0-No:");
                        Car.CarContent.Add(new SpinnersMod(SetWorking()));
                        break;
                    case 6:
                        Menu.PrintServiceMessage($"Is Spolier OK? 1-Yes, 0-No:");
                        Car.CarContent.Add(new SpoilerMod(SetWorking()));
                        break;
                    case 7:
                        Menu.PrintServiceMessage($"Is Sport Suspension OK? 1-Yes, 0-No:");
                        Car.CarContent.Add(new SportSuspensionMod(SetWorking()));
                        break;
                    case 8:
                        Menu.PrintServiceMessage($"Are Titanium Wipers OK? 1-Yes, 0-No:");
                        Car.CarContent.Add(new TitaniumWipersMod(SetWorking()));
                        break;

                    default:
                        Menu.PrintServiceMessage($"No modifications chosen.");
                        break;
                }
                Menu.PrintServiceMessage($"Add more? Yes-1, No-0:");
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