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
        protected BodyPart body = new BodyPart();
        protected CarburetorPart carburetor = new CarburetorPart();
        protected EnginePart engine = new EnginePart();
        protected GearboxPart gearbox = new GearboxPart();
        protected HeatRegulatorPart heatregularor = new HeatRegulatorPart();
        protected MufflerPart muffler = new MufflerPart();
        protected RadiatorPart radiator = new RadiatorPart();
        protected WheelsPart wheels = new WheelsPart();
        protected HornPart horn = new HornPart();

        Liquids carLiquids = new Liquids();


        protected Car()
        {
                
        }

        protected Car(string name)
        {
            Name = name;
            Console.WriteLine("Please specify what parts are broken:");

            
        }

        public void Drive()
        {
            //Check if all parts are working before driving
            if (engine.CheckFuel(carLiquids))
            {
                Console.WriteLine("Wroom-wroom,what is the destination?");
            }
        }

        public void Stop()
        {
            Console.WriteLine("Stopping the car!");
        }

        public void Park(string place)
        {
            Console.WriteLine($"Let's park at {place}");
        }

        public virtual void Honk()
        {
            Console.WriteLine("Honk honk! What is taking so long?!");
        }

    }
}
