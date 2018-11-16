using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AutoRepairShop.Classes
{
    class Time
    {
        private static readonly DateTime gameStartRealTime;
        private static readonly DateTime gameStartGameTime;

        static Time()
        {
            gameStartRealTime = DateTime.Now;
            gameStartGameTime = new DateTime(1990, 1, 1, 9, 0, 0); //Starting time of the application. Need to be read from log of last closed session
            Console.WriteLine($"The GameTime Now is {gameStartGameTime}");
        }

        public void GetGameTime()
        {
            DateTime timeNow = DateTime.Now;
            TimeSpan differenceFromAppStart = timeNow - gameStartRealTime;
            double difference = differenceFromAppStart.TotalSeconds * 720;
            DateTime newGameTime = new DateTime();
            newGameTime = gameStartGameTime;
            newGameTime = newGameTime.AddSeconds(difference);
            Console.WriteLine($"The GameTime Now is {newGameTime}");
        }
    }
}
