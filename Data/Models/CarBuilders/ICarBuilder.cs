using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Data.Models.CarTypes;

namespace AutoRepairShop.Data.Models.CarBuilders
{
    interface ICarBuilder
    {
        Car Car { get; set; }
        Car CreateCarRandomly();

    }
}
