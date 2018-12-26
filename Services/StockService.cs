using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Tools;
using AutoRepairShop.WorkFlow;

namespace AutoRepairShop.Services
{
    internal class StockService
    {
        public static List<CarPart> CarPartStock = new List<CarPart>();
        public static List<Type> CarPartType = new List<Type>
        {
            typeof(BodyPart),
            typeof(CarburetorPart),
            typeof(CustomBonnetMod),
            typeof(DecalsMod),
            typeof(EnginePart),
            typeof(ExhaustPipeMod),
            typeof(GearboxPart),
            typeof(HeatRegulatorPart),
            typeof(HornPart),
            typeof(MufflerPart),
            typeof(No2Mod),
            typeof(RadiatorPart),
            typeof(SpinnersMod),
            typeof(SpoilerMod),
            typeof(SportSuspensionMod),
            typeof(TitaniumWipersMod),
            typeof(WheelsPart),
        };
        static Random Rand = new Random();

        public static StockService StMan;

        private class Nested
        {
            static Nested()
            {
                stock = new StockService();
            }
            internal static readonly StockService stock;
        }

        static StockService()
        {
            RuntimeHelpers.RunClassConstructor(typeof(Nested).TypeHandle);
            StMan = Nested.stock;
            CarPartType.ForEach(x => { StMan.Create(x, 30); });
        }

        public static byte SetRandomDurability()
        {
            return Rand.NextDouble() > 0.2 ? (byte)Rand.Next(60, 101) : (byte)0;
        }

        // ------------------------------- DATA ABOVE -------------------------------------

        public static CarPart Read(string partType)
        {
            return CarPartStock.FirstOrDefault(x => x.Name == partType);
        }

        public static void Delete(string partName)
        {
            CarPartStock.Remove(CarPartStock.First(x => x.Name == partName));
        }

        public void Create(Type partType, int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                CarPartStock.Add((CarPart) Activator.CreateInstance(partType, SetRandomDurability()));
            }
        }

        public static void Update(string partName)
        {
            // Implement database update
        }

        public static void MovepartToGarage(string partName)
        {
            Delete(partName);
            Thread.Sleep(TimeTool.ConvertToRealTime(2) * TimeTool.Thousand);
            ShopManager.GarStMan.AddPartFromStock(partName);
        }
    }
}
