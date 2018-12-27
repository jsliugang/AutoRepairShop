using System.Collections.Generic;
using AutoRepairShop.Data.Models.Humans;

namespace AutoRepairShop.Data.Lists
{
    static class CanDiagnozeList
    {
        public static List<ICanDiagnoze<RepairMan>> RepairMen = new List<ICanDiagnoze<RepairMan>>();

        static CanDiagnozeList()
        {
            RepairMen.Add(RmKirill.Kirill);
            RepairMen.Add(RmPetrovich.Petrovich);
            RepairMen.Add(RmVano.Vano);
            RepairMen.Add(RmSanSanuch.SanSanuch);
        }
    }
}
