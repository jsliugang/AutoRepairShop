using System;
using System.Runtime.InteropServices;
using AutoRepairShop.CourtServiceReference;
using AutoRepairShop.Services;
using AutoRepairShop.Tools;
using AutoRepairShop.WorkFlow;

namespace AutoRepairShop
{
    class Program
    {
        static void Main()
        {
            //CourtClient client = new CourtClient();
            //string i = client.MakeDecision();
            //Console.WriteLine($"Received the following information: '{i}'");
            //client.Close();
            CourtClient client = new CourtClient("NetTcpBinding_ICourt");
            Console.WriteLine($"{client.MakeDecision()}");
            

            // Use the 'client' variable to call operations on the service.

            // Always close the client.
            client.Close();

            /////////////////////////////////////
            MsgDecoratorTool.PrintCustomMessage("Welcome to the Repair Shop!", ConsoleColor.Green, ConsoleColor.Black);
            SetConsoleCtrlHandler(ConsoleCtrlCheck, true);
            WeeklyPaymentService wps = new WeeklyPaymentService();
            MonthlyPaymentService mps = new MonthlyPaymentService();
            RepairAutomationTool repairAutomationTool = new RepairAutomationTool();
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
