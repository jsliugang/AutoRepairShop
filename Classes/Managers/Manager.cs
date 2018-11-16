using AutoRepairShop.Classes.Cars.CarParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Enum;

namespace AutoRepairShop.Classes.Managers
{
    abstract class Manager
    {
        Dictionary<Type, Int32> PartsStock = new Dictionary<Type, Int32>();
        Dictionary<Modifications, int> ModificationsStock = new Dictionary<Modifications, Int32>();

        protected Manager()
        {
            SetPartsStock();
            SetModificationsStock();
        }

        private void SetPartsStock()
        {
            PartsStock.Add(typeof(BodyPart), 5);
            //PartsStock.Add(CarParts.BrokenBodyPart, 0);
            //PartsStock.Add(CarParts.Carburetor, 5);
            //PartsStock.Add(CarParts.BrokenCarburetor, 0);
            //PartsStock.Add(CarParts.Engine, 5);
            //PartsStock.Add(CarParts.BrokenEngine, 0);
            //PartsStock.Add(CarParts.GearBox, 5);
            //PartsStock.Add(CarParts.BrokenGearBox, 0);
            //PartsStock.Add(CarParts.HeatRegulator, 5);
            //PartsStock.Add(CarParts.BrokenHeatRegulator, 0);
            //PartsStock.Add(CarParts.Horn, 5);
            //PartsStock.Add(CarParts.BrokenHorn, 0);
            //PartsStock.Add(CarParts.Muffler, 5);
            //PartsStock.Add(CarParts.BrokenMuffler, 0);
            //PartsStock.Add(CarParts.Radiator, 5);
            //PartsStock.Add(CarParts.BrokenRadiator, 0);
            //PartsStock.Add(CarParts.Wheels, 5);
            //PartsStock.Add(CarParts.BrokenWheels, 0);
        }

        private void SetModificationsStock()
        {
            ModificationsStock.Add(Modifications.CustomBonnet, 2);
            ModificationsStock.Add(Modifications.Decals, 2);
            ModificationsStock.Add(Modifications.ExhaustPipe, 2);
            ModificationsStock.Add(Modifications.NO2, 2);
            ModificationsStock.Add(Modifications.Spinners, 2);
            ModificationsStock.Add(Modifications.Spoiler, 2);
            ModificationsStock.Add(Modifications.SportSuspension, 2);
            ModificationsStock.Add(Modifications.TitaniumWipers, 2);
        }

        public virtual CarPart RetrieveNewCarPart(Type type)
        {
            int value;
            PartsStock.TryGetValue(type, out value);
            Console.WriteLine($"The type is set to {type.FullName}.");
            if (value > 0)
            {
                var pp = (BodyPart)Activator.CreateInstance(PartsStock.First().Key);
                return pp;
            }
            else
            {
                return null;
            }


            
        }

    }
}
