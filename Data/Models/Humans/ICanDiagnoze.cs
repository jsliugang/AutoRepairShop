using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.Humans
{
    internal interface ICanDiagnoze<T>:ICanBase where T : class
    {
        int Priority { get; }
        void DiagnozeCar(Car car);
    }
}
