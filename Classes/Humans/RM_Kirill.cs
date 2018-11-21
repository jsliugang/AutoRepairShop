using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars;
using AutoRepairShop.Classes.Cars.Modifications;

namespace AutoRepairShop.Classes.Humans
{
    class RM_Kirill:RepairMan
    {
        public RM_Kirill()
        {
            Name = "Kirill Artemovich";
        }

        public void Modify(Car car)
        {
            Console.WriteLine($"Kirill Artemovich: What kind of modification would you like?");
            Console.WriteLine($"1. Custom bonnet");
            Console.WriteLine($"2. Decals");
            Console.WriteLine($"3. ExhaustPipe");
            Console.WriteLine($"4. NO 2");
            Console.WriteLine($"5. Spinners");
            Console.WriteLine($"6. Spoiler");
            Console.WriteLine($"7. Sport Suspension");
            Console.WriteLine($"8. Titanium Wipers");
            string i = Console.ReadLine();
            int choice = Int32.Parse(i);
            Console.WriteLine($"Applying modifications to {car.Name}");
            Thread.Sleep(15000);
            switch (choice)
            {
                case 1:
                    car.AddModification(new CustomBonnet());
                    break;
                case 2:
                    car.AddModification(new Decals());
                    break;
                case 3:
                    car.AddModification(new ExhaustPipe());
                    break;
                case 4:
                    car.AddModification(new NO2());
                    break;
                case 5:
                    car.AddModification(new Spinners());
                    break;
                case 6:
                    car.AddModification(new Spoiler());
                    break;
                case 7:
                    car.AddModification(new SportSuspension());
                    break;
                case 8:
                    car.AddModification(new TitaniumWipers());
                    break;
                default:
                    Console.WriteLine($"Please enter a valid modification!");
                    break;
            }
            Console.WriteLine($"All done!");
            
        }
    }
}
