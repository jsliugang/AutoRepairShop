using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShop.Classes;
using AutoRepairShop.Classes.Humans;
using AutoRepairShop.Classes.Managers;

namespace AutoRepairShop
{


    class Program
    {
        static void Main(string[] args)
        {
            //Process BalanceViewer = new Process();
            //BalanceViewer.StartInfo.UseShellExecute = false;
            //BalanceViewer.StartInfo.RedirectStandardOutput = true;
            //BalanceViewer.StartInfo.FileName = @"C:\Users\Yuri.Pustovoy\Documents\Visual Studio 2017\Projects\AutoRepairShop\BalanceViewer\bin\Debug\BalanceViewer.exe";
            //BalanceViewer.Start();
            //StreamReader sw = BalanceViewer.StandardOutput;
            //BalanceViewer.OutputDataReceived += CaptureOutput;
            //BalanceViewer.BeginOutputReadLine();
            Menu menu = new Menu();
            //BalanceViewer.WaitForExit();

            SetConsoleCtrlHandler(ConsoleCtrlCheck, true);
            //BalanceViewer.WaitForExit();
        }

        static void CaptureOutput(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine($"Received: {e.Data}"); 
        }

        private static bool ConsoleCtrlCheck(CtrlTypes ctrlType)

        {
            switch (ctrlType)
            {
                case CtrlTypes.CTRL_C_EVENT:
                    ShopManager.LastTimeLog();
                    break;

                case CtrlTypes.CTRL_BREAK_EVENT:
                    ShopManager.LastTimeLog();
                    break;

                case CtrlTypes.CTRL_CLOSE_EVENT:
                    ShopManager.LastTimeLog();
                    break;

                case CtrlTypes.CTRL_LOGOFF_EVENT:
                    ShopManager.LastTimeLog();
                    break;

                case CtrlTypes.CTRL_SHUTDOWN_EVENT:
                    ShopManager.LastTimeLog();
                    break;
            }
            return true;
        }

        #region unmanaged
        [DllImport("Kernel32")]

        public static extern bool SetConsoleCtrlHandler(HandlerRoutine Handler, bool Add);
        public delegate bool HandlerRoutine(CtrlTypes CtrlType);
        public enum CtrlTypes
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }
        #endregion
    }
}
