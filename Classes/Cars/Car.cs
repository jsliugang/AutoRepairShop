using AutoRepairShop.Classes.Cars.CarParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.Modifications;
using AutoRepairShop.Classes.Managers;

namespace AutoRepairShop.Classes.Cars
{
    abstract class Car
    {
        public Liquids carLiquids;
        protected List<string> CarNames;
        public List<CarPart> CarContent;
        protected BodyPart body;
        protected CarburetorPart carburetor;
        protected EnginePart engine;
        protected GearboxPart gearbox;
        protected HeatRegulatorPart heatregularor;
        protected MufflerPart muffler;
        protected RadiatorPart radiator;
        protected WheelsPart wheels;
        protected HornPart horn;



        protected Car(){}

        protected Car(string name)
        {
            Name = name;
            carLiquids = new Liquids();
            Menu.PrintServiceMessage("**Please specify what parts are broken: **");
            CarNames = new List<string>();
            CarContent = new List<CarPart>();
            CarContent.Add(body = new BodyPart());
            CarContent.Add(carburetor = new CarburetorPart());
            CarContent.Add(engine = new EnginePart());
            CarContent.Add(gearbox = new GearboxPart());
            CarContent.Add(heatregularor = new HeatRegulatorPart());
            CarContent.Add(muffler = new MufflerPart());
            CarContent.Add(radiator = new RadiatorPart());
            CarContent.Add(wheels = new WheelsPart());
            CarContent.Add(horn = new HornPart());
        }

        public string Name { get; set; }

        public void AddModification(Modification modification)
        {
            
        }

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
