using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Managers;

namespace AutoRepairShop.Classes.Cars.CarParts
{
    abstract class CarPart
    {
        public string Name { get; }
        public bool IsWorking { get; set; }
        public int Cost { get; set; }

        protected CarPart(){}

        protected CarPart(string name)
        {
            Name = name;
            IsWorking = SetPartState();
        }

        protected bool SetPartState()
        {
            while (true)
            {
                Menu.PrintServiceMessage($" ->  Is {Name} ok? 1=Yes, 0=No");
                string userInput = Console.ReadLine();
                if (userInput == "1")
                {
                    return true;
                }
                if (userInput == "0")
                {
                    return false;
                }
                Menu.ThrowWarning();
                SetPartState();
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
