using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip
{
    public class Menu
    {
        public static Game WriteMenu()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            WriteTitle();
            LoopOptions();
            int difficulty = Difficulty();
            Game game = new Game(difficulty);
            return game;
        }

        private static int Difficulty()
        {
            Console.Clear();
            bool proceed = false;
            while (proceed == false)
            {
                Console.WriteLine("Select your difficulty:");
                Console.WriteLine("1 = Easy");
                Console.WriteLine("2 = Medium");
                Console.WriteLine("3 = Hard");
                Console.WriteLine("4 = Insane");
                string input = Console.ReadLine();
                switch (input.ToLower())
                {
                    case "1":
                        proceed = true;
                        return 1;
                    case "2":
                        proceed = true;
                        return 2;
                    case "3":
                        proceed = true;
                        return 3;
                    case "4":
                        proceed = true;
                        return 4;
                    case "harder":
                        proceed = true;
                        return 5;
                    case "god":
                        proceed = true;
                        return 6;
                    case "exit":
                        exit();
                        break;
                    case "back":
                    case "menu":
                        Console.Clear();
                        WriteTitle();
                        LoopOptions();
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Let's try that again shall we..\n");
                        proceed = false;
                        break;
                }
            }
            return 0;
        }

        private static void LoopOptions()
        {
            bool notContinue = true;
            while (notContinue)
            {
                Console.WriteLine("1 - New Game (or press Enter)");
                Console.WriteLine("2 - Exit");
                string input = Console.ReadLine();

                switch (input.ToLower())
                {
                    case "":
                        notContinue = false;
                        break;
                    case "1":
                        Console.Clear();
                        notContinue = false;
                        break;
                    case "2":
                        exit();
                        break;
                    case "exit":
                        exit();
                        break;
                    default:
                        Console.WriteLine("Please enter in 1 for a new game, or 2 to exit.");
                        Console.WriteLine("You can also type exit\n");
                        break;
                }
            }
        }

        public static void exit()
        {
            Environment.Exit(0);
        }

        private static void WriteTitle()
        {
            string nameOfTheGame = @"
        _           _   _   _           _     _       
       | |         | | | | | |         | |   (_)      
       | |__   __ _| |_| |_| | ___  ___| |__  _ _ __  
       | '_ \ / _` | __| __| |/ _ \/ __| '_ \| | '_ \ 
       | |_) | (_| | |_| |_| |  __/\__ \ | | | | |_) |
       |_.__/ \__,_|\__|\__|_|\___||___/_| |_|_| .__/ 
                                               | |    
                                               |_|  ";

            string ship = @"                                        ___
                                       |___|
                                  ______|_|
                           _   __|_________|  _
            _        =====| | |            | | |==== _
      =====| |        .---------------------------. | |====
<-----------------'   .  .  .  .  .  .  .  .   '------------/
  \                                                        /
   \                                                      /
    \____________________________________________________/
";
            Console.WriteLine(nameOfTheGame);
            Console.WriteLine(ship);
        }
    }
}
