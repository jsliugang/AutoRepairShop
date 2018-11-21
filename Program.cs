using System;
using System.Collections.Generic;
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
            SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true);
            Menu menu = new Menu();
            Console.ReadLine();
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
