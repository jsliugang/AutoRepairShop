using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Classes.Cars.CarParts
{
    class BodyPart:CarPart
    {
        public BodyPart():base("Body")
        {
            Cost = 1000;
        }


    }
}
