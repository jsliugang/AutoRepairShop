using AutoRepairShop.Data.Models.CarParts;

namespace AutoRepairShop.Data.Models.Humans
{
    interface ICanRepair<T> : ICanBase where T : class
    {
        void MakeRepairs(string partName);
    }
}