namespace AutoRepairShop.Data.Repository
{
    public interface IStock<T>
        where T : class
    {
        T ProvideItem();
        void AddMany(int amount);
    }
}
