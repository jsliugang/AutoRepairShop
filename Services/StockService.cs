using AutoRepairShop.Data.Repository;

namespace AutoRepairShop.Services
{
    class StockManager:GarageStockManager
    {
        public bool Read(string partName)
        {
            // Always returns true, as if the part is in Stock
            // Implement database check
            return true;
        }

        public void Delete(string partName)
        {
            // Implement database delete
        }

        public void Create(string partName)
        {
            // Implement database create (new parts in stock)
        }

        public void Update(string partName)
        {
            // Implement database update
        }
    }
}
