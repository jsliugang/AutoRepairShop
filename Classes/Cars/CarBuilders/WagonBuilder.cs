﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarTypes;

namespace AutoRepairShop.Classes.Cars.CarBuilders
{
    class WagonBuilder:CarBuilder
    {
        public WagonBuilder(bool random):base(random)
        {
            Car = new Wagon();
            if (random)
            {
                RandomCarName();
            }
            else
            {
                ProcessNamingInput();
            }
        }

        public override void CreateCar()
        {
        }
        protected override void SetCarNamesList()
        {
            CarNames.Add("Lada Granta");
            CarNames.Add("Audi RS4 V Avant");
        }
    }
}
