using System.Collections.Generic;
using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Data.Models.CarPartsStock;

namespace AutoRepairShop.Data.Repository
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
            StockManagers.Add("HeatRegulator", new HeatRegulatorPartStock());
            StockManagers.Add("Horn", new HornPartStock());
            StockManagers.Add("Muffler", new MufflerPartStock());
            StockManagers.Add("NO2", new No2ModStock());
            StockManagers.Add("Radiator", new RadiatorPartStock());
            StockManagers.Add("Spinners", new SpinnersModStock());
            StockManagers.Add("Spoiler", new SpoilerModStock());
            StockManagers.Add("SportSuspension", new SportSuspensionModStock());
            StockManagers.Add("TitaniumWipers", new TitaniumWipersModStock());
            StockManagers.Add("Wheels", new WheelsPartStock());
        }
        public CarPart RetrieveNewCarPart(string type)
        {
            return StockManagers[type].ProvideItem();
        }

        public void AddPartFromStock(string type)
        {
            StockManagers[type].Add(1);
        }
    }
}
