namespace AutoRepairShop.Data.Models.CarTypes
{
    internal interface ICentralLock
    {
        bool IsWorking { get; set; }
        bool CarIsLocked { get; set; }
        void CarLock();
    }
}
