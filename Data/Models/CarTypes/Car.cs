﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Tools;

namespace AutoRepairShop.Data.Models.CarTypes
{
    abstract class Car
    {
        public Liquids CarLiquids;
        public List<CarPart> CarContent;
        public string Name { get; set; }
        private Timer _partLifeTimer;
        public bool CarIsWorking { get; set; }

        protected Car()
        {
            CarContent = new List<CarPart>();
            CarLiquids = new Liquids();
            SetPartLifeTimer();
            CarIsWorking = true;
        }     

        private void SetPartLifeTimer()
        {
            _partLifeTimer = new Timer(TimeTool.ConvertToRealTime(24)*TimeTool.Thousand);
            _partLifeTimer.Elapsed += OnDecreasePartLifeEvent;
            _partLifeTimer.AutoReset = true;
            _partLifeTimer.Enabled = true;
        }

        private void OnDecreasePartLifeEvent(Object source, ElapsedEventArgs e)
        {
            if (DecreaseCarLifetime() || WasteSomeLiquids())
            {
                CarIsWorking = false;
            }
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
            return CarContent.All(x => x.IsWorking) && CarLiquids.CarLiquids.All(y => y.Value > 0);
        }

        public bool WasteSomeLiquids()
        {
            bool timeToRepair = false;
            for (int i = 0; i < CarLiquids.CarLiquids.Count; i++)
            {
                if (CarLiquids.CarLiquids.ElementAt(i).Value > 5)
                {
                    CarLiquids.UpdateAmount(CarLiquids.CarLiquids.ElementAt(i).Key, CarLiquids.CarLiquids.ElementAt(i).Value - 5);
                }
                else
                {
                    CarLiquids.UpdateAmount(CarLiquids.CarLiquids.ElementAt(i).Key, 0);
                    timeToRepair = true;
                }
            }
            return timeToRepair;
        }

        public bool DecreaseCarLifetime()
        {
            bool timeToRepair = false;
            foreach (var carPart in CarContent)
            {
                if (carPart.Durability > 5)
                {
                    carPart.Durability -= 5;
                }
                else
                {
                    carPart.Durability = 0;
                    timeToRepair = true;
                }
            }
            return timeToRepair;
        }
    }
}