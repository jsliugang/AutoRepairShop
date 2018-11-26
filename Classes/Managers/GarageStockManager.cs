using AutoRepairShop.Classes.Cars.CarParts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Data;
using AutoRepairShop.Interfaces;


namespace AutoRepairShop.Classes.Managers
{
    class GarageStockManager
    {
        protected Dictionary<string, IStock<CarPart>> StockManagers = new Dictionary<string, IStock<CarPart>>();

        public GarageStockManager()
        {
            StockManagers.Add("Body", new BodyPartStock());
            StockManagers.Add("Carburetor", new CarburetorPartStock());
            StockManagers.Add("CustomBonnet", new CustomBonnetModStock());
            StockManagers.Add("Decals", new DecalsModStock());
            StockManagers.Add("Engine", new EnginePartStock());
            StockManagers.Add("ExhaustPipe", new ExhaustPipeModStock());
            StockManagers.Add("Gearbox", new GearboxPartStock());
            StockManagers.Add("HeatRegularor", new HeatRegulatorPartStock());
            StockManagers.Add("Horn", new HornPartStock());
            StockManagers.Add("Muffler", new MufflerPartStock());
            StockManagers.Add("NO2", new NO2ModStock());
            StockManagers.Add("Radiator", new RadiatorPartStock());
            StockManagers.Add("Spinners", new SpinnersModStock());
            StockManagers.Add("Spoiler", new SpoilerModStock());
            StockManagers.Add("SportSuspension", new SportSuspensionModStock());
            StockManagers.Add("TitaniumWipers", new TitaniumWipersModStock());
            StockManagers.Add("Wheels", new WheelsPartStock());
        }
        public virtual CarPart RetrieveNewCarPart(string type)
        {
            return StockManagers[type].ProvideItem();
        }

    }
}
