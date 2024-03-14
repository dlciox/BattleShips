using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BattleShips
{
    class Menu
    {
        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Witaj w grze w statki!");
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Zacznij grę");
            Console.WriteLine("2. Zamknij");

            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    Thread.Sleep(2000)
                    Console.Clear();
                    Game.Start();
                    break;
                case 2:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Nieprawidłowa opcja.");
                    Thread.Sleep(3000)
                    Console.Clear();
                    break;
            }
        }
    }

}