using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarParts;

namespace AutoRepairShop.Classes.Humans
{
    class RM_Petrovich:RepairMan
    {
        public RM_Petrovich()
        {
            Name = "Petrovich";
        }

        public void ReplaceFluid(Liquids fluids)
        {
            byte value;
            Console.WriteLine($"Petrovich: What liquid shall I replace?");
            fluids.CarLiquids.TryGetValue("Fuel", out value);
            Console.WriteLine($"Fuel = {value}");
            fluids.CarLiquids.TryGetValue("EngineOil", out value);
            Console.WriteLine($"EngineOil = {value}");
            fluids.CarLiquids.TryGetValue("BrakeFluid", out value);
            Console.WriteLine($"BrakeFluid = {value}");
            fluids.CarLiquids.TryGetValue("CoolingLiquid", out value);
            Console.WriteLine($"CoolingLiquid = {value}");
            fluids.CarLiquids.TryGetValue("WindshieldWasherLiquid", out value);
            Console.WriteLine($"WindshieldWasherLiquid = {value}");
            string key = Console.ReadLine();

            if (key != null && fluids.CarLiquids.ContainsKey(key))
            {
                Console.WriteLine($"Petrovich: Getting access to tank.");
                Thread.Sleep(15000);
                fluids.CarLiquids[key] = 100;
                Console.WriteLine($"Petrovich: All done!");
            }
            else
            {
                Console.WriteLine($"No such liquid found! Please, try again:");
                ReplaceFluid(fluids);
            }         
        }
    }
}
