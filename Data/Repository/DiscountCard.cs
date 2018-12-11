using System;

namespace AutoRepairShop.Data.Repository
{
    class DiscountCard
    {
        public int NumberOfVisits { get; set; }
        public int Priority { get; set; }
        public string CardName { get; set; }

        public DiscountCard()
        {
            GetDiscountRate();
        }
        public void PunchDiscountCard()
        {
            NumberOfVisits++;
            Console.WriteLine($"Your current discount rate is {GetDiscountRate()}% - {CardName}");

        }

        public double GetDiscountRate()
        {
            if (NumberOfVisits >= 2 && NumberOfVisits < 4)
            {
                CardName = "Bronze Card";
                Priority = 6;
                return 0.05;
            }
            if (NumberOfVisits >= 4 && NumberOfVisits < 6)
            {
                Priority = 3;
                CardName = "Silver Card";
                return 0.10;
            }
            if (NumberOfVisits >= 6)
            {
                Priority = 1;
                CardName = "Gold Card";
                return 0.15;
            }
            Priority = 9;
            CardName = "Loyal Customer";
            return 0;
        }
    }
}
