using System;
using System.Collections.Generic;
using System.Linq;
using AutoRepairShop.Data.Models.CarBuilders;
using AutoRepairShop.Data.Models.CarTypes;
using AutoRepairShop.WorkFlow;

namespace AutoRepairShop.Data.Models.Factories
{
    public interface IProduct<TFactory>
    {
        void Work();
    }
    public interface IFactory<TFactory>
    {
        TProduct Create<TProduct>() where TProduct : IProduct<TFactory>;
    }

    public class SpecialCarFactory : IFactory<SpecialCar>
    {
        public TProduct Create<TProduct>() where TProduct : IProduct<SpecialCar>
        {
            return new TProduct();
        }
    }

    public class Product1 : IProduct<SpecialCarFactory>
    {
        public void Work()
        {
            throw new NotImplementedException();
        }
    }


    static class AbstractFactory
    {
        public static T Create<T>() where T:CarBuilder, new ()
        {
            var t = new T();
            
            return t;
        }
    }
}
