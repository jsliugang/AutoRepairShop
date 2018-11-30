using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.Humans
{
    interface ICanCustomize<T>: ICanBase where T:class 
    {
        int Modify(Car car, string modificationType);
        int PerformModification(string partName, Car car);
    }
}
