using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.Humans
{
    interface ICanReplaceFluids<T> : ICanBase where T : class
    {
        int Priority { get; }
        int ReplaceFluid(Car car);
    }
}
