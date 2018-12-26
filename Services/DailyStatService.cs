using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using System.Text;
using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Data.Models.Humans;
using AutoRepairShop.Data.Repository;
using AutoRepairShop.Tools;
using AutoRepairShop.WorkFlow;

namespace AutoRepairShop.Services
{
    [Synchronization]
    internal class DailyStatService
    {
        public List<string> DailyStats = new List<string>();
        private readonly FileLoggerService _fls = new FileLoggerService();

        public void AddCustomer(Customer customer)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"New Customer: {customer.Name}, priority: {customer.MyDiscounts.Priority}");
            sb.AppendLine($"-- Car: {customer.MyCar.Name}, Accepted on: {TimeTool.GetGameTime()}");
            sb.AppendLine($"Broken parts:");
            foreach (var carPart in customer.MyCar.CarContent)
            {
                if (carPart.Durability<1)
                {
                    sb.Append($"{carPart.Name}, ");
                }
            }
            sb.AppendLine();
            DailyStats.Add(sb.ToString());
        }

        public void AddWorkOrder(RepairMan rm, string order, double workCost, double partCost)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{TimeTool.GetGameTime()} -- RM: {rm.Name}, Work Order: {order}, work cost: {workCost}, part cost: {partCost}");
            _fls.StoreLog(sb.ToString());
            DailyStats.Add(sb.ToString());
        }

        public void FinalizeCustomer(Customer customer, double totalCost)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Finilized Customer: {customer.Name}, priority: {customer.MyDiscounts.Priority}");
            sb.AppendLine($"-- Car: {customer.MyCar.Name}, Released on: {TimeTool.GetGameTime()}");
            sb.AppendLine($"Car parts:");
            foreach (CarPart carPart in customer.MyCar.CarContent)
            {
                var works = carPart.Durability>0 ? "is working" : "is not working";
                sb.AppendLine($"{carPart.Name} - {works}");
            }
            sb.AppendLine($"The total is {totalCost}");
            sb.AppendLine($"Discount applied: {customer.MyDiscounts.CardName}");
            sb.AppendLine();
            DailyStats.Add(sb.ToString());
        }

        public void Clear()
        {
            DailyStats.Clear();
        }

    //    public void Display()
    //    {
    //        lock (DailyStats) lock (ShopManager.GarageList)
    //        {
    //            Console.ForegroundColor = ConsoleColor.Yellow;
    //            Console.WriteLine("***DAILY STATISTICS***");
    //            foreach (var line in DailyStats)
    //            {
    //                Console.WriteLine($"{line}");
    //            }
    //            Console.WriteLine($"-- Customers in Lines --");
    //            foreach (var garage in ShopManager.GarageList)
    //            {
    //                CustomerQueue<Customer>.Display(garage.CustomersQueue);
    //                Console.WriteLine($"---------------------------------------");
    //            }
    //            Console.WriteLine($"---------------------------------------");
    //            foreach (var keyValue in ShopManager.Lucy.Salary)
    //            {
    //                Console.WriteLine($"{keyValue.Key.Name} earned {keyValue.Value}");
    //            }
    //            Console.WriteLine("***END OF DAILY STATISTICS***");
    //            Console.ForegroundColor = ConsoleColor.White;
    //        }
    //    }
    }
}
