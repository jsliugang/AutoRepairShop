namespace AutoRepairShop.Data.Models.CarTypes
{
    interface ISensor
    {
        bool IsWorking { get; set; }

        void SensorData();
    }
}
