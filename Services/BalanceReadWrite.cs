using System.IO;
using System.Linq;

namespace AutoRepairShop.Services
{
    internal class BalanceReadWrite
    {
        public static double Balance;

        public static double Read()
        {
            if (new FileInfo("Balance.txt").Length == 0)
                return Balance = 1000;
            var lastLine = File.ReadLines("Balance.txt").Last();
            double.TryParse(lastLine, out Balance);
            return Balance;
        }

        public static void Write(double balance)
        {
            File.WriteAllText("Balance.txt", string.Empty);
            using (var w = File.AppendText("Balance.txt"))
            {
                w.WriteLine($"\r{balance}");
            }
        }
    }
}
