using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.Humans
{
    interface ICanDiagnoze<T>:ICanBase where T : class
    {
        void DiagnozeCar(Car car);
    }
}
