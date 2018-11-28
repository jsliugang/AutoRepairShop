using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Classes.Managers
{
    class BalanceReadWrite
    {
        public static int balance;

        public static int Read()
        {
            if (new System.IO.FileInfo("Balance.txt").Length == 0)
            {
                return balance = 1000;
            }
            else
            {
                string lastLine = File.ReadLines("Balance.txt").Last();
                Int32.TryParse(lastLine, out balance);
                return balance;
            }
        }

        public static void Write(int balance)
        {
            File.WriteAllText("Balance.txt", String.Empty);
            using (StreamWriter w = File.AppendText("Balance.txt"))
            {
                w.WriteLine($"\r{balance}");
            }
        }
    }
}
