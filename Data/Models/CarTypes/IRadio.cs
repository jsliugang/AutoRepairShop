namespace AutoRepairShop.Data.Models.CarTypes
{
    interface IRadio
    {
        bool IsWorking { get; set; }
        bool RadioState { get; set; }

        void SwitchRadio();
    }
}
