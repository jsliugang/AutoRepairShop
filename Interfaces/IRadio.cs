using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Interfaces
{
    interface IRadio
    {
        bool IsWorking { get; set; }
        bool RadioState { get; set; }

        void SwitchRadio();
    }
}
