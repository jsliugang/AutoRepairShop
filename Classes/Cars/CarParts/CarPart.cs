using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Classes.Cars.CarParts
{
    abstract class CarPart
    {
        public string Name { get; }
        public bool IsWorking { get; set; }

        protected CarPart()
        {

        }

        protected CarPart(string name)
        {
            Name = name;
            IsWorking = SetPartState();
            Console.WriteLine($"The {Name} is working? {IsWorking}");
        }

        protected bool SetPartState()
        {
            while (true)
            {
                Console.WriteLine($"Is {Name} ok? 1=Yes, 2=No");
                string userInput = Console.ReadLine();
                if (userInput == "1")
                {
                    return true;
                }
                else if (userInput == "0")
                {
                    return false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Input! Please try again:");
                    Console.ResetColor();
                }
            }

        }

        protected void BreakPart()
        {
            if (IsWorking)
            {
                IsWorking = !IsWorking;
            }
            else
            {
                Console.WriteLine($"{Name} is already broken!");
            }
        }
    }
}
