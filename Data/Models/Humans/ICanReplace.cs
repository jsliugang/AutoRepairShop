using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.Humans
{
    internal interface ICanReplace<T> : ICanBase where T : class
    {
        int Priority { get; }
        void ReplacePart(string partName, Car car);
    }
}