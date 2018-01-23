using BattleShip.MainComponents.GameBoardLogic.BaseBattleOperations;
using System;
using System.Linq;

namespace BattleShip
{
    public class Game
    {
        public Gameboard Board { get; set; }
        public int Difficulty { get; set; }

        public Game()
        {
            Board = new Gameboard();
            Difficulty = 0;
            Console.Clear();
        }

        public Game(int difficulty)
        {
            Board = new Gameboard();
            Difficulty = difficulty;
            SetRocketValue(difficulty);
            Console.Clear();
        }

        public bool IsGameOver()
        {
            return GameBoardUtilis.BackToMenu == 1 || Board.Rockets <= 0 || AllBoatsSunk(Board);
        }

        public void GameOver()
        {
            if (AllBoatsSunk(Board))
            {
                Grid.WriteGrid(Board.PlayerLayer);
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
                ConsoleColor currentColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(String.Format("\n{0}\n{1}\n{2}\n{3}",
                    "  ----------------------------",
                    " | 01011001 01001111 01010101 |",
                    " | 01010111 01001111 01001110 |",
                    "  ----------------------------\n"));
                Console.ForegroundColor = currentColor;
                Grid.WriteGrid(Board.BoatLayer);
                Console.WriteLine("\n");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("CONGRATULATIONS!");
                int usedRockets = GetUsedRockets();
                Console.WriteLine("You won with " + usedRockets + " Rockets");
                Console.ForegroundColor = currentColor;

            }
            else if (Board.Rockets <= 0)
            {
                Console.Clear();
                ConsoleColor currentColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("GAME OVER\n");
                Console.ForegroundColor = currentColor;
                Console.WriteLine("Better luck next time.");
                foreach (Boat boat in Board.BoatLayer.Boats)
                {
                    if (boat.Sunk == false)
                    {
                        boat.ShowBoat(Board.PlayerLayer);
                    }
                }
                Grid.WriteGrid(Board.PlayerLayer);
            }
        }

        public void GodMode()
        {
            Console.Clear();
            Console.WriteLine("GET READY");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("3");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("2");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("1");
            System.Threading.Thread.Sleep(1000);
            Grid.WriteGrid(Board.BoatLayer);
            System.Threading.Thread.Sleep(2500);
            Console.Clear();
        }

        private int GetUsedRockets()
        {
            int currentRockets = Board.Rockets;
            SetRocketValue(Difficulty);
            int diff = Board.Rockets - currentRockets;
            return diff;
        }

        public bool AllBoatsSunk(Gameboard board)
        {
            return board.BoatLayer.Boats.All(b => b.Sunk);
        }

        private int SetRocketValue(int difficulty)
        {
            switch (Difficulty)
            {
                case 1:
                    Board.Rockets = 70;
                    break;
                case 2:
                    Board.Rockets = 60;
                    break;
                case 3:
                    Board.Rockets = 50;
                    break;
                case 4:
                    Board.Rockets = 35;
                    break;
                case 5:
                    Board.Rockets = 43;
                    break;
                case 6:
                    Board.Rockets = 20;
                    break;
                default:
                    Board.Rockets = 60;
                    break;
            }
            return Board.Rockets;
        }
    }
}
