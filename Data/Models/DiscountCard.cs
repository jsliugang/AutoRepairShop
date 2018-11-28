using System;

namespace AutoRepairShop.Classes.Data.Models
{
    class DiscountCard
    {
        public int NumberOfVisits = 0;
        public string CardName = "Returning customer";

        public void PunchDiscountCard()
        {
            NumberOfVisits++;
            Console.WriteLine($"Your current discount rate is {GetDiscountRate()}% - {CardName}");

        }

        public int GetDiscountRate()
        {
            if (NumberOfVisits >= 2 && NumberOfVisits < 4)
            {
                CardName = "Bronze Card";
                return 5;
            }
            if (NumberOfVisits >= 4 && NumberOfVisits < 6)
            {
                CardName = "Silver Card";
                return 10;
            }
            if (NumberOfVisits >= 6)
            {
                CardName = "Gold Card";
                return 15;
            }
            return 0;
        }
    }
}
