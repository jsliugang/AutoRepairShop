using System;
using System.Linq;
using AutoRepairShop.Data.Models.CarParts;
using AutoRepairShop.Data.Models.Humans;
using AutoRepairShop.Data.Repository;
using AutoRepairShop.WorkFlow;

namespace AutoRepairShop.Tools
{
    internal sealed class RepairAutomationTool
    {
        public static readonly CarMaker _cm = new CarMaker();

        public RepairAutomationTool()
        {
            TimeTool.TimeInstance.GetGameTimeToScreen();
            ShopManager.Lucy.Greet();
            CheckQueue();
        }

        public static void CheckQueue()
        {
            while (true)
            {
                int i = CustomerQueue<Customer>.Length;
                if (i==0) continue;
                ShopManager.AcceptNewCustomer(CustomerQueue<Customer>.Pop());
            }
        }

        public static void MakeRepairChoice()
        {
            Random rand = new Random();
            ShopManager.CurrentCustomer.MakeDiagnosticsOrder();
            foreach (CarPart carPart in ShopManager.CurrentCustomer.MyCar.CarContent)
            {
                if (!ShopManager.WorkingHours())
                {
                    TimeTool.TimeInstance.SetNextDayTimer();
                }
                if (carPart.IsWorking) continue;

                if (rand.NextDouble() > 0.5)
                {
                    ShopManager.CurrentCustomer.MakeRepairOrder(carPart.Name);
                }
                else
                {
                    ShopManager.CurrentCustomer.ReplaceBrokenParts(carPart.Name);
                }
            }

            if (!ShopManager.WorkingHours())
            {
                TimeTool.TimeInstance.SetNextDayTimer();
            }

            if (rand.NextDouble() > 0.5)
            {
                ShopManager.CurrentCustomer.PimpMyCar(ShopManager.ModificationsOffer[rand.Next(0, ShopManager.ModificationsOffer.Count)]);
            }
            else
            {
                int randomLiquid = rand.Next(0, ShopManager.CurrentCustomer.MyCar.CarLiquids.CarLiquids.Count);
                ShopManager.CurrentCustomer
                    .ReplaceLiquids(ShopManager.CurrentCustomer
                        .MyCar.CarLiquids.CarLiquids.Keys.ElementAt(randomLiquid));
            }

            ShopManager.CurrentCustomer.LeaveShop();
        }

        public static void AddCustomer()
        {
            if (ShopManager.WorkingHours())
                CustomerQueue<Customer>.Enqueue(new Customer(_cm.MakeRandomCar()));
            else
                ShopManager.ShopIsClosed();
        }   

        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}