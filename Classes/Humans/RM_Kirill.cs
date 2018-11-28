using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarParts;
using AutoRepairShop.Classes.Cars.CarTypes;
using AutoRepairShop.Classes.Managers;

namespace AutoRepairShop.Classes.Humans
{
    class RM_Kirill:RepairMan
    {
        private List<string> _modificationsOffer = new List<string>();

        public RM_Kirill()
        {
            Name = "Kirill Artemovich";
            _modificationsOffer.Add("CustomBonnet");
            _modificationsOffer.Add("Decals");
            _modificationsOffer.Add("ExhaustPipe");
            _modificationsOffer.Add("NO2");
            _modificationsOffer.Add("Spinners");
            _modificationsOffer.Add("Spoiler");
            _modificationsOffer.Add("SportSuspension");
            _modificationsOffer.Add("TitaniumWipers");
        }

        public int Modify(Car car)
        {          
            int userInput=-1;
            while (userInput < 0 && userInput >= _modificationsOffer.Count)
            {
                Console.WriteLine($"Kirill Artemovich: What kind of modification would you like?");
                for (int i = 0; i < _modificationsOffer.Count; i++)
                {
                    Console.WriteLine($"{i}. {_modificationsOffer[i]}");
                }
                Int32.TryParse(Console.ReadLine(), out userInput);               
            }
            Console.WriteLine($"Applying modifications to {car.Name}");
            return PerformModification(_modificationsOffer[userInput], car);
        }

        public int PerformModification(string partName, Car car)
        {
            CarPart newPart = CheckPartAvailability(partName);
            if (newPart != null)
            {
                car.CarContent.Add(newPart);
                Thread.Sleep(15000);
                Console.WriteLine($"All done!");
                return car.CarContent.Last().Cost;
            }
            Console.WriteLine($"{Name}: {partName} is not in garage, we have to request it from Stock.");
            if (RequestPartFromStock(partName))
            {
                return PerformModification(partName, car);
            }
            return 0;
        }
    }
}
