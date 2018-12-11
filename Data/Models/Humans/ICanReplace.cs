﻿using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.Humans
{
    interface ICanReplace<T> : ICanBase where T : class
    {
        int Priority { get; }
        void ReplacePart(string partName, Car car);
    }
}