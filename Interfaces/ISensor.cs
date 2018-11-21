using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Interfaces
{
    interface ISensor
    {
        bool IsWorking { get; set; }

        void SensorData();
    }
}
