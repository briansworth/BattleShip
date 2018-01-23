using BattleShip.MainComponents.GameBoardLogic.BaseBattleOperations;
using System;

namespace BattleShip.MainComponents.BattleLogic.ShotAction
{
    public abstract class BaseBattleOperations
    {
        public int Rockets { get; set; }

        private bool isShotUsedBefore(string shot)
        {
            if (isShotValid(shot) != true)
            {
                return false;
            }
            int row = GameBoardUtilis.GetRowNumber(shot[0]);
            int col = GameBoardUtilis.GetColumnNumber(shot[1]);
            if (PlayerLayer.Matrix[row, col] == ' ' ||
                PlayerLayer.Matrix[row, col] == 'x')
            {
                return true;
            }
            return false;
        }

        private bool isShotValid(string shot)
        {
            if (isInputValid(shot) != true)
            {
                return false;
            }
            int row = GameBoardUtilis.GetRowNumber(shot[0]);
            int column = GameBoardUtilis.GetColumnNumber(shot[1]);
            if (row > 0 &&
                column > 0 &&
                row < PlayerLayer.Matrix.GetLength(0) &&
                column < PlayerLayer.Matrix.GetLength(1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Shoot()
        {
            string coordinates = "";
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Rockets left: " + Rockets + "\n");
            Console.ForegroundColor = Foreground;
            Console.WriteLine("Enter in the coordinates for your shot:");
            coordinates = Console.ReadLine();
            if (coordinates.ToLower() == "menu"
                || coordinates.ToLower() == "back")
            {
                GameBoardUtilis.BackToMenu = 1;
            }
            while (isShotValid(coordinates) != true
                && GameBoardUtilis.BackToMenu == 0
                || isShotUsedBefore(coordinates) == true
                && GameBoardUtilis.BackToMenu == 0)
            {
                Console.Clear();
                Console.WriteLine("\n\n");
                Grid.WriteGrid(PlayerLayer);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Rockets left: " + Rockets + "\n");
                Console.ForegroundColor = Foreground;
                if (isShotUsedBefore(coordinates))
                {
                    Console.WriteLine(String.Format("{0}: {1}",
                        coordinates,
                        "You already shot there... Try again\n"));
                }
                else
                {
                    Console.WriteLine(String.Format("'{0}' {1}",
                        coordinates,
                        "is not a valid location.\n"));
                }
                coordinates = Console.ReadLine();
            }
            if (GameBoardUtilis.BackToMenu == 0)
            {
                Console.Clear();
                Rockets--;
                WriteShot(coordinates);
            }
        }

        public abstract Grid PlayerLayer { get; set; }
        public abstract ConsoleColor Foreground { get; set; }

        protected abstract bool isInputValid(string shot);
        protected abstract void WriteShot(string shot);
    }
}
