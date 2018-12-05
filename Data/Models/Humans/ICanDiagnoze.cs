using System.Collections.Generic;
using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.Humans
{
    interface ICanDiagnoze<T>:ICanBase where T : class
    {
        List<CarPart> DiagnozeCar(Car car);
    }
}
