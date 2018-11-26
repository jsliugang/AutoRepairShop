using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Classes.Cars.CarParts
{
    class BodyPart:CarPart
    {
        public BodyPart(bool state):base("Body", state)
        {
            Cost = 1000;
        }

        public void Honk()
        {
            Console.WriteLine($"Honk honk");
        }
    }
}
