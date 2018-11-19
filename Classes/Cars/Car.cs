using AutoRepairShop.Classes.Cars.CarParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Managers;

namespace AutoRepairShop.Classes.Cars
{
    abstract class Car
    {
        protected List<string> CarNames;
        protected BodyPart body;
        protected CarburetorPart carburetor;
        protected EnginePart engine;
        protected GearboxPart gearbox;
        protected HeatRegulatorPart heatregularor;
        protected MufflerPart muffler;
        protected RadiatorPart radiator;
        protected WheelsPart wheels;
        protected HornPart horn;
        Liquids carLiquids;


        protected Car()
        {
            Console.WriteLine($"ctor car");
        }

        protected Car(string name)
        {
            Name = name;
            Menu.PrintMenuMessage("**Please specify what parts are broken: **");
            CarNames = new List<string>();
            body = new BodyPart();
            carburetor = new CarburetorPart();
            engine = new EnginePart();
            gearbox = new GearboxPart();
            heatregularor = new HeatRegulatorPart();
            muffler = new MufflerPart();
            radiator = new RadiatorPart();
            wheels = new WheelsPart();
            horn = new HornPart();
            carLiquids = new Liquids();
        }

        public string Name { get; set; }

        public void Drive()
        {
            if (ComputerCheck())
            {
                if (engine.CheckFuel(carLiquids))
                {
                    Console.WriteLine("Wroom-wroom,what is the destination?");
                }
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

        public bool ComputerCheck()
        {
            if (body.IsWorking &&
                carburetor.IsWorking &&
                engine.IsWorking &&
                gearbox.IsWorking &&
                heatregularor.IsWorking &&
                muffler.IsWorking &&
                radiator.IsWorking &&
                wheels.IsWorking &&
                horn.IsWorking)
            {
                return true;
            }
            return false;
        }
    }
}
