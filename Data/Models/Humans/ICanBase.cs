using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Data.Models.Humans
{
    interface ICanBase
    {
        string Name { get; set; }
        bool IsBusy { get; set; }
    }
}
