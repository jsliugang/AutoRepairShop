using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoRepairShop.Data.Models.Humans
{
    internal abstract class Human
    {
        public List<string> NamesList = new List<string>();
        public List<string> LastNamesList = new List<string>();
        public string Name { get; set; }
        public string LastName { get; set; }


        protected Human()
        {
            DumpNames();
        }

        public string GetName()
        {
            var sb = new StringBuilder();
            sb.Append(Name);
            sb.Append(" ");
            sb.Append(LastName);
            return sb.ToString();
        }

        public string Get_Name()
        {
            var sb = new StringBuilder();
            sb.Append(Name);
            sb.Append("_");
            sb.Append(LastName);
            return sb.ToString();
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
                DumpNames(r, NamesList);
            }
            using (StreamReader r = File.OpenText(@"C:\Users\Yuri.Pustovoy\Documents\Visual Studio 2017\Projects\AutoRepairShop\AutoRepairShop\bin\Debug\HumanLastNames.txt"))
            {
                DumpNames(r, LastNamesList);
            }
        }

        public void DumpNames(StreamReader r, List<string> list)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                list.Add(line);
            }
        }
    }
}
