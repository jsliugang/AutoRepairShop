using System;
using System.IO;
using System.Linq;

namespace AutoRepairShop.Services
{
    class BalanceReadWrite
    {
        public static int Balance;

        public static int Read()
        {
            if (new FileInfo("Balance.txt").Length == 0)
            {
                return Balance = 1000;
            }
            string lastLine = File.ReadLines("Balance.txt").Last();
            Int32.TryParse(lastLine, out Balance);
            return Balance;
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
