using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.Humans
{
    interface ICanReplace<T> : ICanBase where T : class
    {
        void ReplacePart(string partName, Car car);
    }
}