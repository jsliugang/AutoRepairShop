using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Classes.Managers
{
    class Menu
    {
        public Menu()
        {
            GreetUser();
            DisplayMenu();
        }

        public void GreetUser()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to the Repair Shop!");
            Console.ResetColor();
        }

        public void DisplayMenu()
        {
            
        }

        public void ProcessInput()
        {
            string userInput;
            userInput = Console.ReadLine();
        }
    }
}
