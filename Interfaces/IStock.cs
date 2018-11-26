using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarParts;

namespace AutoRepairShop.Interfaces
{
    interface IStock<T> where T: CarPart
    {
        T ProvideItem();
    }
}
