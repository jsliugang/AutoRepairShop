﻿using System.Collections.Generic;
using AutoRepairShop.Data.Models.Humans;

namespace AutoRepairShop.Data.Lists
{
    static class CanCustomizeList
    {
        public static List<ICanCustomize<RepairMan>> RepairMen = new List<ICanCustomize<RepairMan>>();

        static CanCustomizeList()
        {
            RepairMen.Add(RmKirill.Kirill);
            RepairMen.Add(RmSanSanuch.SanSanuch);
            RepairMen.Add(RmPetrovich.Petrovich);
        }
    }
}