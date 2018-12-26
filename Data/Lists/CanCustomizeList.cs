using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using AutoRepairShop.Data.Models.Humans;

namespace AutoRepairShop.Data.Lists
{
    [Synchronization]
    static class CanCustomizeList
    {
        public static List<ICanCustomize<RepairMan>> RepairMen = new List<ICanCustomize<RepairMan>>();

        static CanCustomizeList()
        {
            RepairMen.Add(RmKirill.Kirill);
            RepairMen.Add(RmSanSanuch.SanSanuch);
            RepairMen.Add(RmPetrovich.Petrovich);
            RepairMen.Add(RmLeha.Leha);
            RepairMen.Add(RmMihaluch.Mihaluch);
        }
    }
}