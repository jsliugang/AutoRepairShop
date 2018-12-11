using System.Collections.Generic;
using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.Humans
{
    interface ICanDiagnoze<T>:ICanBase where T : class
    {
        int Priority { get; }
        void DiagnozeCar(Car car);
    }
}
