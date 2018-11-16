using AutoRepairShop.Classes.Cars.CarParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Classes.Cars
{
    abstract class Car
    {
        public string Name { get; set; }
        protected List<string> CarNames = new List<string>();

        protected Car()
        {
                
        }

        protected Car(string name)
        {
            Name = name;
            Console.WriteLine("Please specify what parts are broken:");

            BodyPart body = new BodyPart();
            CarburetorPart carburetor = new CarburetorPart();
            EnginePart engine = new EnginePart();
            GearboxPart gearbox = new GearboxPart();
            HeatRegulatorPart heatregularor = new HeatRegulatorPart();
            MufflerPart muffler = new MufflerPart();
            RadiatorPart radiator = new RadiatorPart();
            WheelsPart wheels = new WheelsPart();

            Liquids carLiquids = new Liquids();
        }

        public void Drive()
        {
            //Check if all parts are working before driving
            Console.WriteLine("Wroom-wroom,what is the destination?");
        }

        public void Stop()
        {
            Console.WriteLine("Stopping the car!");
        }

        public void Park(string place)
        {
            Console.WriteLine($"Let's park at {place}");
        }
    }
}
