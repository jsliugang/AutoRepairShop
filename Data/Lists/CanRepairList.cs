using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using AutoRepairShop.Data.Models.Humans;

namespace AutoRepairShop.Data.Lists
{
    [Synchronization]
    static class CanRepairList
    {
        public static List<ICanRepair<RepairMan>> RepairMen = new List<ICanRepair<RepairMan>>();

        static CanRepairList()
        {
            RepairMen.Add(RmKirill.Kirill);
            RepairMen.Add(RmPetrovich.Petrovich);
            RepairMen.Add(RmVano.Vano);
            RepairMen.Add(RmLeha.Leha);
            RepairMen.Add(RmMihaluch.Mihaluch);
            RepairMen.Add(RmSerega.Serega);
            RepairMen.Add(RmSanSanuch.SanSanuch);
        }
    }
}
