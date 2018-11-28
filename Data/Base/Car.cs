using System;
using System.Collections.Generic;
using System.Linq;
using AutoRepairShop.Data.Models.CarParts;

namespace AutoRepairShop.Data.Base
{
    abstract class Car
    {
        public Liquids CarLiquids;
        public List<CarPart> CarContent;
        public string Name { get; set; }

        protected Car()
        {
            CarContent = new List<CarPart>();
            CarLiquids = new Liquids();
        }

        public void Drive()
        {
            EnginePart engineLink = CarContent.First(m => m.Name == "Engine") as EnginePart;
            if (ComputerCheck() && engineLink.CheckFuel(CarLiquids))
            {
                Console.WriteLine("Wroom-wroom,what is the destination?");
            }
        }

        public void Stop()
        {
            Console.WriteLine("Stopping the car!");
        }

        public void Park(string place)
        {
            Console.WriteLine($"Let's park at {place}");
        }

        public virtual void Honk()
        {
            Console.WriteLine("Honk honk! What is taking so long?!");
        }

        public bool ComputerCheck()
        {
            return CarContent.All(x => x.IsWorking);
        }
    }
}
