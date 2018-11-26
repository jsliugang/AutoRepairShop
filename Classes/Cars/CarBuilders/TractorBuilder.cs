﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarTypes;

namespace AutoRepairShop.Classes.Cars.CarBuilders
{
    class TractorBuilder:CarBuilder
    {
        public TractorBuilder()
        {
            Car = new Tractor();
            ProcessNamingInput();
        }

        public override void CreateCar()
        {
        }
        protected override void SetCarNamesList()
        {
            CarNames.Add("Ursus 11054");
            CarNames.Add("Fendt 820");
            CarNames.Add("MTZ 122");
        }
    }
}
