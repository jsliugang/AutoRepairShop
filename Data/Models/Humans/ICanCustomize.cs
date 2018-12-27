using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.Humans
{
    internal interface ICanCustomize<T>: ICanBase where T:class 
    {
        int Priority { get; }
        void Modify(string modificationType, Car car);
    }
}
