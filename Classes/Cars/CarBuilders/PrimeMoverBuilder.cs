using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Cars.CarTypes;

namespace AutoRepairShop.Classes.Cars.CarBuilders
{
    class PrimeMoverBuilder:CarBuilder
    {
        public PrimeMoverBuilder(bool random):base(random)
        {
            Car = new PrimeMover();
            if (random)
            {
                RandomCarName();
            }
            else
            {
                ProcessNamingInput();
            }
        }

        public override void CreateCar()
        {
        }
        protected override void SetCarNamesList()
        {
            CarNames.Add("MAN TGS 6x6");
            CarNames.Add("Foton AumanH5 4x2");
            CarNames.Add("DAF LF 55");
            CarNames.Add("Volvo FL 7");
            CarNames.Add("DAF XF 95 FAR");
            CarNames.Add("MAN TGX");
        }
    }
}
