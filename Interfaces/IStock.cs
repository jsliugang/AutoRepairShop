using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarParts;

namespace AutoRepairShop.Interfaces
{
    public interface IStock<T>
        where T : class
    {
        T ProvideItem();
    }
}
