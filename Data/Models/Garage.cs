using System;
using System.Collections.Generic;
using AutoRepairShop.Data.Models.Humans;
using AutoRepairShop.Data.Repository;
using AutoRepairShop.WorkFlow;
using Microsoft.Office.Interop.Word;
using Task = System.Threading.Tasks.Task;

namespace AutoRepairShop.Data.Models
{
    class Garage
    {
        public List<Customer> CustomersQueue = new List<Customer>();
        public string Name { get;}
        public bool IsEmpty { get; set; }
        public Customer CurrentCustomer { get; set; }
        public RepairMan Worker { get; set; }
        public static Random Rand = new Random();

        public Garage(string name)
        {
            Name = name;
            IsEmpty = true;
        }

        public async void SomeWork()
        {
            await Task.Run(() =>
            {
                CurrentCustomer = CustomerQueue<Customer>.Dequeue(CustomersQueue);
                CurrentCustomer.MakeDiagnosticsOrder();
                ShopManager.Lucy._css.AppendContractText();
                CurrentCustomer.RepairBrokenParts();
                CurrentCustomer.ReplaceBrokenParts();

                if (ShopManager.WorkingHours())
                {
                    if (Rand.NextDouble() > 0.5)
                    {
                        CurrentCustomer.PimpMyCar(
                        ShopManager.ModificationsOffer[Rand.Next(0, ShopManager.ModificationsOffer.Count)]);
                    }
                    else
                    {
                        CurrentCustomer.ReplaceLiquids();
                    }
                }
                CurrentCustomer.LeaveShop();
            });
        }
    }
}
