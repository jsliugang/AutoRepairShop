using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Data.Models.Humans;

namespace AutoRepairShop.Services
{
    class DailyStatService
    {
        private static List<Customer> stats;

        public void PrintDailyStat()
        {
            foreach (Customer customer in stats)
            {
                
            }
        }

        public void SaveStat(Customer customer)
        {
            stats.Add(customer);
        }

        public void CleanUp()
        {
            stats.Clear();
        }
    }
}
