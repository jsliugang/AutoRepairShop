using System;
using System.Runtime.InteropServices;
using AutoRepairShop.Services;
using AutoRepairShop.Tools;
using AutoRepairShop.WorkFlow;
using Microsoft.Office.Interop.Word;
using Task = System.Threading.Tasks.Task;

namespace AutoRepairShop
{
    class Program
    {
        static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
            Console.ReadLine();
        }

        static async Task MainAsync()
        {
            await Task.Run(() =>
            {
                MsgDecoratorTool.PrintCustomMessage("Welcome to the Repair Shop!", ConsoleColor.Green,
                    ConsoleColor.Black);
                SetConsoleCtrlHandler(ConsoleCtrlCheck, true);
                WeeklyPaymentService wps = new WeeklyPaymentService();
                MonthlyPaymentService mps = new MonthlyPaymentService();
                RepairAutomationTool repairAutomationTool = new RepairAutomationTool();
            });
        }

        private static bool ConsoleCtrlCheck(CtrlTypes ctrlType)

        {
            switch (ctrlType)
            {
                case CtrlTypes.CtrlCEvent:
                    ShopManager.LastTimeLog();
                    break;

                case CtrlTypes.CtrlBreakEvent:
                    ShopManager.LastTimeLog();
                    break;

                case CtrlTypes.CtrlCloseEvent:
                    ShopManager.LastTimeLog();
                    break;

                case CtrlTypes.CtrlLogoffEvent:
                    ShopManager.LastTimeLog();
                    break;

                case CtrlTypes.CtrlShutdownEvent:
                    ShopManager.LastTimeLog();
                    break;
            }
            return true;
        }

        #region unmanaged
        [DllImport("Kernel32")]

        public static extern bool SetConsoleCtrlHandler(HandlerRoutine handler, bool add);
        public delegate bool HandlerRoutine(CtrlTypes ctrlType);
        public enum CtrlTypes
        {
            CtrlCEvent = 0,
            CtrlBreakEvent,
            CtrlCloseEvent,
            CtrlLogoffEvent = 5,
            CtrlShutdownEvent
        }
        #endregion
    }
}
