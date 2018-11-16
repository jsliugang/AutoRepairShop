using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes;

namespace AutoRepairShop.Classes.Humans
{
    abstract class RepairMan : Human
    {
        protected RepairMan()
        {
                
        }

        private class GeneralRepair
        {
            private string _partName;
            private string _workerName;
            
            private void Disassemble()
            {
                Console.WriteLine($"{_workerName} is disassembling the {_partName}");
            }

            private void Repair()
            {
                Console.WriteLine($"{_workerName} is repairing the {_partName}");
            }

            private void Assemble()
            {
                Console.WriteLine($"{_workerName} is assembling the {_partName}");
            }

            public void MakeRepairs(string workerName, string partName)
            {
                _workerName = workerName;
                _partName = partName;
                Disassemble();
                Repair();
                Assemble();
            }
        }

        private class Diagnostics
        {
            public void Diagnoze(string workerName, string partName)
            {
                Console.WriteLine($"{workerName} is assembling the {partName}");
                Console.WriteLine($"{workerName} found that {partName} is ok");
            }
        }


        public void DoSomething()
        {
            
        }

    }
}
