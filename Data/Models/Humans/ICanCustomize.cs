using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.Humans
{
    interface ICanCustomize<T>: ICanBase where T:class 
    {
        void Modify(Car car, string modificationType);
        void PerformModification(string partName, Car car);
    }
}
