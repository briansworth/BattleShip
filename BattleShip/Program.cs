using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BattleShip
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(80, 38);
            Console.BufferHeight = 38;
            Console.BufferWidth = (80);
            while (true)
            {
                Game game = Menu.WriteMenu();
                if (game.Difficulty == 6)
                {
                    game.GodMode();
                }
                Console.WriteLine("\n\n");
                while (game.IsGameOver() != true)
                {
                    Grid.WriteGrid(game.Board.PlayerLayer);
                    game.Board.Shoot();
                }
                if (game.Board.BackToMenu == 0)
                {
                    game.GameOver();
                    Console.WriteLine("\nPress Enter to exit to menu");
                    string final = Console.ReadLine();
                    if (final.ToLower() == "exit")
                    {
                        Menu.exit();
                    }
                }
                Console.Clear();
            }
        }
    }
}
