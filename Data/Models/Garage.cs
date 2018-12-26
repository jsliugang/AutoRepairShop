using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using AutoRepairShop.Data.Lists;
using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Data.Models.Humans;
using AutoRepairShop.Data.Repository;
using AutoRepairShop.WorkFlow;
using Task = System.Threading.Tasks.Task;

namespace AutoRepairShop.Data.Models
{
    class Garage
    {
        public ObservableCollection<Customer> CustomersQueue = new ObservableCollection<Customer>();
        public string Name { get; }
        public bool IsEmpty { get; set; }
        private Customer CurrentCustomer { get; set; }
        public RepairMan Worker { get; set; }
        public static Random Rand = new Random();
        public static readonly Dictionary<string, double> ServicesCatalogue = new Dictionary<string, double>();
        public static List<string> ModificationsOffer = new List<string>();
        public ConsoleColor ConsoleClr { get; set; }

        public Garage(string name)
        {
            Name = name;
            IsEmpty = true;
            CustomersQueue.CollectionChanged += OnNewCustomerHandler;
            switch (Name)
            {
                case "0":
                    ConsoleClr = ConsoleColor.Blue;
                    break;
                case "1":
                    ConsoleClr = ConsoleColor.DarkGreen;
                    break;
                case "2":
                    ConsoleClr = ConsoleColor.Yellow;
                    break;
                default:
                    break;
            }
        }

        static Garage()
        {
            ServicesCatalogue.Add("Diagnoze", 50);
            ServicesCatalogue.Add("Repair", 250);
            ServicesCatalogue.Add("Modify", 500);
            ServicesCatalogue.Add("Replace", 200);
            ServicesCatalogue.Add("ReplaceLiquid", 50);
            ModificationsOffer.Add("CustomBonnet");
            ModificationsOffer.Add("Decals");
            ModificationsOffer.Add("ExhaustPipe");
            ModificationsOffer.Add("NO2");
            ModificationsOffer.Add("Spinners");
            ModificationsOffer.Add("Spoiler");
            ModificationsOffer.Add("SportSuspension");
            ModificationsOffer.Add("TitaniumWipers");
        }

        public void OnNewCustomerHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Add) return;
            if (!IsEmpty) return;
            Console.WriteLine($"Event triggered on thread: {Thread.CurrentThread.ManagedThreadId}");
            GarageSystem();
        }

        public async void GarageSystem()
        {
            if (!IsEmpty) return;
            IsEmpty = false;
            await Task.Run(() =>
            {
                Console.WriteLine($"GS {Name} thread {Thread.CurrentThread.ManagedThreadId}");
                while (true)
                {                   
                    CurrentCustomer = CustomerQueue<Customer>.Dequeue(CustomersQueue);
                    CurrentCustomer.StopWaitForServicesTimer();
                    if (ShopManager.WorkingHours()())
                    {
                        Diagnoze();
                    }
                    ShopManager.Lucy._css.AppendContractText(CurrentCustomer);
                    foreach (CarPart carPart in CurrentCustomer.MyAgreement.PartsToRepair)
                    {
                        if (ShopManager.WorkingHours()())
                        {
                            RepairPart(carPart.Name);
                        }
                    }
                    for (var i = 0; i < CurrentCustomer.MyAgreement.PartsToReplace.Count; i++)
                    {
                        if (ShopManager.WorkingHours()())
                        {
                            string partName = CurrentCustomer.MyAgreement.PartsToReplace[i].Name;
                            ReplacePart(partName);
                            CurrentCustomer.MyAgreement.PartsToReplace[i] =
                                CurrentCustomer.MyCar.CarContent.Find(x => x.Name == partName);
                        }
                    }
                    if (ShopManager.WorkingHours()())
                    {
                        if (Rand.NextDouble() > 0.5)
                        {
                            AddModification(ModificationsOffer[Rand.Next(0, ModificationsOffer.Count)]);
                        }
                        else
                        {
                            ReplaceFluids();
                        }
                    }
                    CurrentCustomer.LeaveShopAsync(); //fire and forget
                    if (CustomersQueue.Count != 0)
                        continue;
                    break;
                }
            });
            IsEmpty = true;
        }

        private void Diagnoze()
        {
            RepairMan diagnozeMan = GetDiagnozeMan();
            if (diagnozeMan == null) return;
            diagnozeMan.DiagnozeCar(CurrentCustomer, ConsoleClr);
            ShopManager.UpdateAgreement(diagnozeMan, CurrentCustomer, "Diagnoze", 0);
        }

        private RepairMan GetDiagnozeMan()
        {
            lock (CanDiagnozeList.RepairMen)
            {
                ICanDiagnoze<RepairMan> diagnozeMan =
                    CanDiagnozeList.RepairMen.FirstOrDefault(x => x.IsBusy == false && x.Priority == 1) ??
                    CanDiagnozeList.RepairMen.FirstOrDefault(x => x.IsBusy == false && x.Priority == 2) ??
                    CanDiagnozeList.RepairMen.FirstOrDefault(x => x.IsBusy == false && x.Priority == 3);
                if (diagnozeMan != null)
                {
                    diagnozeMan.IsBusy = true;
                }
                return (RepairMan) diagnozeMan;
            }
        }

        private void RepairPart(string part)
        {
            RepairMan repairMan = GetRepairMan();
            if (repairMan == null) return;
            repairMan.MakeRepairs(part, CurrentCustomer.MyCar, ConsoleClr);
            ShopManager.UpdateAgreement(repairMan, CurrentCustomer, "Repair", 0);
        }

        private RepairMan GetRepairMan()
        {
            lock (CanRepairList.RepairMen)
            {
                ICanRepair<RepairMan> repairMan = CanRepairList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 1) ??
                                                  CanRepairList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 2);
                if (repairMan != null)
                {
                    repairMan.IsBusy = true;
                }
                return (RepairMan)repairMan;
            }
        }

        private void ReplacePart(string part)
        {
            RepairMan replaceMan = GetReplaceMan();
            if (replaceMan == null) return;
            replaceMan.ReplacePart(part, CurrentCustomer.MyCar, ConsoleClr);
            ShopManager.UpdateAgreement(replaceMan, CurrentCustomer, "Replace", CurrentCustomer.MyCar.CarContent.Find(x => x.Name == part).Cost);
        }

        private RepairMan GetReplaceMan()
        {
            lock (CanReplaceList.RepairMen)
            {
                ICanReplace<RepairMan> replaceMan = CanReplaceList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 1) ??
                                                    CanReplaceList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 2) ??
                                                    CanReplaceList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 3);
                if (replaceMan != null)
                {
                    replaceMan.IsBusy = true;
                }
                return (RepairMan)replaceMan;
            }
        }

        private void AddModification(string part)
        { 
            lock (CurrentCustomer)
            {
                RepairMan customizeMan = GetCustomizeMan();
                if (customizeMan == null) return;
                customizeMan.Modify(part, CurrentCustomer.MyCar, ConsoleClr);
                ShopManager.UpdateAgreement(customizeMan, CurrentCustomer, "Modify", CurrentCustomer.MyCar.CarContent.Find(x => x.Name == part).Cost);
                ShopManager.Lucy._css.AddMoreServices("Modification", part, CurrentCustomer.MyCar.CarContent.Find(x => x.Name == part).Cost.ToString(), ServicesCatalogue["Modify"].ToString(), CurrentCustomer.MyAgreement.DocPath, CurrentCustomer);
            }
        }

        private RepairMan GetCustomizeMan()
        {
            lock (CanCustomizeList.RepairMen)
            {
                ICanCustomize<RepairMan> customizeMan = CanCustomizeList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 1) ??
                                                        CanCustomizeList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 2) ??
                                                        CanCustomizeList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 3);
                if (customizeMan != null)
                {
                    customizeMan.IsBusy = true;
                }
                return (RepairMan)customizeMan;
            }
        }

        private void ReplaceFluids()
        {
            RepairMan replaceFluidsMan = GetReplaceFluidsMan();
            if (replaceFluidsMan == null) return;
            CurrentCustomer.MyAgreement.TotalPartCost += replaceFluidsMan.ReplaceFluid(CurrentCustomer.MyCar, ConsoleClr);
            ShopManager.UpdateAgreement(replaceFluidsMan, CurrentCustomer, "ReplaceLiquid", 0);
            ShopManager.Lucy._css.AddMoreServices("Liquids", "All liquids", "150", ServicesCatalogue["ReplaceLiquid"].ToString(), CurrentCustomer.MyAgreement.DocPath, CurrentCustomer);
        }

        private RepairMan GetReplaceFluidsMan()
        {
            lock (CanCustomizeList.RepairMen)
            {
                ICanReplaceFluids<RepairMan> replaceFluidsMan = CanReplaceFluidsList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 1) ??
                                                                CanReplaceFluidsList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 2) ??
                                                                CanReplaceFluidsList.RepairMen.Find(x => x.IsBusy == false && x.Priority == 3);
                if (replaceFluidsMan != null)
                {
                    replaceFluidsMan.IsBusy = true;
                }
                return (RepairMan)replaceFluidsMan;
            }
        }
    }
}