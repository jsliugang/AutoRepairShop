using System;


namespace AutoRepairShop.Tools
{
    class MsgDecoratorTool
    {
        public static void PrintCustomMessage(string message, ConsoleColor textColor, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = textColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void PrintServiceMessage(string message)
        {
            PrintCustomMessage(message, ConsoleColor.DarkGray, ConsoleColor.Black);
        }

        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static DateTime PassMeTime()
        {
            return TimeTool.GetGameTime();
        }

        public static void PrintWarningMessage(string message)
        {
            PrintCustomMessage(message, ConsoleColor.Red, ConsoleColor.Black);
        }

        public static void PrintMenuMessage(string message)
        {
            PrintCustomMessage(message, ConsoleColor.DarkYellow, ConsoleColor.Black);
        }
    }
}
