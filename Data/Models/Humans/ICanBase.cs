namespace AutoRepairShop.Data.Models.Humans
{
    internal interface ICanBase
    {
        string Name { get; set; }
        bool IsBusy { get; set; }
    }
}
