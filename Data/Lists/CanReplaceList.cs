﻿using System.Collections.Generic;
using AutoRepairShop.Data.Models.Humans;

namespace AutoRepairShop.Data.Lists
{
    static class CanReplaceList
    {
        public static List<ICanReplace<RepairMan>> RepairMen = new List<ICanReplace<RepairMan>>();

        static CanReplaceList()
        {      
            RepairMen.Add(RmSanSanuch.SanSanuch);
            RepairMen.Add(RmVano.Vano);
        }
    }
}
