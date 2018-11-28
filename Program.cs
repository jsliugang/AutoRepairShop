using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using AutoRepairShop.WorkFlow;

namespace AutoRepairShop
{
    class Program
    {
        static void Main()
        {
            Menu menu = new Menu();
            SetConsoleCtrlHandler(ConsoleCtrlCheck, true);
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
