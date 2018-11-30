using System.Collections.Generic;
using AutoRepairShop.Data.Models.Humans;

namespace AutoRepairShop.Data.Lists
{
    static class CanRepairList
    {
        public static List<ICanRepair<RepairMan>> RepairMen = new List<ICanRepair<RepairMan>>();

        static CanRepairList()
        {
            RepairMen.Add(RmSanSanuch.SanSanuch);
            RepairMen.Add(RmKirill.Kirill);
            RepairMen.Add(RmPetrovich.Petrovich);
            RepairMen.Add(RmVano.Vano);
        }
    }
}
