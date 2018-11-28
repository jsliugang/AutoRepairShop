using System;
using System.Threading;
using AutoRepairShop.Data.Base;

namespace AutoRepairShop.Data.Models.Humans
{
    class RmPetrovich:RepairMan
    {
        public static readonly RmPetrovich Petrovich = new RmPetrovich();

        private RmPetrovich()
        {
            Name = "Petrovich";
        }

        public int ReplaceFluid(Car car)
        {
            while (true)
            {
                byte value;
                Console.WriteLine($"Petrovich: What liquid shall I replace?");
                car.CarLiquids.CarLiquids.TryGetValue("Fuel", out value);
                Console.WriteLine($"Fuel = {value}");
                car.CarLiquids.CarLiquids.TryGetValue("EngineOil", out value);
                Console.WriteLine($"EngineOil = {value}");
                car.CarLiquids.CarLiquids.TryGetValue("BrakeFluid", out value);
                Console.WriteLine($"BrakeFluid = {value}");
                car.CarLiquids.CarLiquids.TryGetValue("CoolingLiquid", out value);
                Console.WriteLine($"CoolingLiquid = {value}");
                car.CarLiquids.CarLiquids.TryGetValue("WindshieldWasherLiquid", out value);
                Console.WriteLine($"WindshieldWasherLiquid = {value}");
                string key = Console.ReadLine();

                if (key != null && car.CarLiquids.CarLiquids.ContainsKey(key))
                {
                    Console.WriteLine($"Petrovich: Getting access to tank.");
                    Thread.Sleep(15000);
                    car.CarLiquids.CarLiquids[key] = 100;
                    Console.WriteLine($"Petrovich: All done!");
                    break;
                }
                Console.WriteLine($"No such liquid found! Please, try again:");
                ReplaceFluid(car);
            }
            return 50; //specify fluid costs
        }
    }
}
