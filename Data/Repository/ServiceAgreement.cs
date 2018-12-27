using System.Collections.Generic;
using AutoRepairShop.Data.Models.CarParts;

namespace AutoRepairShop.Data.Repository
{
    internal class ServiceAgreement
    {
        public string CustomerName { get; set; }
        public List<CarPart> PartsToRepair = new List<CarPart>();
        public List<CarPart> PartsToReplace = new List<CarPart>();
        public double TotalPartCost { get; set; }
        public double TotalServicesCost { get; set; }
        public double Total { get; set; }
        public string DocPath = null;

        public ServiceAgreement(string customerName)
        {
            CustomerName = customerName;
        }

        public double GetTotal()
        {
            Total = TotalPartCost + TotalServicesCost;
            return Total;
        }
    }
}
