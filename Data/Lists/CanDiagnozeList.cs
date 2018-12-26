using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using AutoRepairShop.Data.Models.Humans;

namespace AutoRepairShop.Data.Lists
{
    [Synchronization]
    static class CanDiagnozeList
    {
        public static List<ICanDiagnoze<RepairMan>> RepairMen = new List<ICanDiagnoze<RepairMan>>();

        static CanDiagnozeList()
        {
            RepairMen.Add(RmKirill.Kirill);
            RepairMen.Add(RmPetrovich.Petrovich);
            RepairMen.Add(RmVano.Vano);
            RepairMen.Add(RmSanSanuch.SanSanuch);
            RepairMen.Add(RmLeha.Leha);
            RepairMen.Add(RmMihaluch.Mihaluch);
            RepairMen.Add(RmSerega.Serega);
        }
    }
}
