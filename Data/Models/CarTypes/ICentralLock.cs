namespace AutoRepairShop.Data.Models.CarTypes
{
    interface ICentralLock
    {
        bool IsWorking { get; set; }
        bool CarIsLocked { get; set; }

        void CarLock();
    }
}
