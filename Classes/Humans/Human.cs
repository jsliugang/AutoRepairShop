using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoRepairShop.Classes.Managers;


namespace AutoRepairShop.Classes.Humans
{
    abstract class Human
    {
        public List<string> NamesList = new List<string>();

        public string Name { get; set; }

        public Human()
        {
            DumpNames();
        }

        public void Greet()
        {
            Say($"Hello! My name is {Name}");
        }

        public virtual void Say(string message)
        {
            Console.WriteLine(message);
        }

        public void DumpNames()
        {
            using (StreamReader r = File.OpenText(@"C:\Users\Yuri.Pustovoy\Documents\Visual Studio 2017\Projects\AutoRepairShop\AutoRepairShop\bin\Debug\HumanNames.txt"))
            {
                DumpNames(r);
            }
        }

        public void DumpNames(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                NamesList.Add(line);
            }
        }

    }
}
