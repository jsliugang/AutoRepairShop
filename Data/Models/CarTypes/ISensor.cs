namespace AutoRepairShop.Data.Models.CarTypes
{
    internal interface ISensor
    {
        bool IsWorking { get; set; }
        void SensorData();
    }
}
