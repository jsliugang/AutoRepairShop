namespace AutoRepairShop.Data.Models.CarTypes
{
    internal interface IRadio
    {
        bool IsWorking { get; set; }
        bool RadioState { get; set; }
        void SwitchRadio();
    }
}
