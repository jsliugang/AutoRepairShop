using System;

namespace AutoRepairShop.Data.Models.Humans
{
    internal interface ICanDiagnoze<T>:ICanBase where T : class
    {
        int Priority { get; }
        void DiagnozeCar(Customer customer, ConsoleColor clr);
    }
}
