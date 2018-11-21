using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Interfaces
{
    interface ICentralLock
    {
        bool IsWorking { get; set; }
        bool CarIsLocked { get; set; }

        void CarLock();
    }
}
