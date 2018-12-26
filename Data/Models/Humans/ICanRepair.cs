using System;
using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.Humans
{
    internal interface ICanRepair<T> : ICanBase where T : class
    {
        int Priority { get; }
        void MakeRepairs(string partName, Car car, ConsoleColor clr);
    }
}