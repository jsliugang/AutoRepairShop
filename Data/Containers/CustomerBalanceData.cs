using System;

namespace AutoRepairShop.Data.Containers
{
    struct CustomerBalanceData
    {
        public CustomerBalanceData(double costOfParts, double costOfServices)
        {
            CostOfParts = costOfParts;
            CostOfServices = costOfServices;
        }
        public double CostOfParts { get; set; }
        public double CostOfServices { get; set; }

        public double CalculateTotal(double discount)
        {
            double discountAmount = CostOfServices * discount / 100;
            double total = CostOfParts + CostOfServices - discountAmount;
            Console.WriteLine($"TOTAL PAID IS {total}");
            return total;
        }
    }
}
