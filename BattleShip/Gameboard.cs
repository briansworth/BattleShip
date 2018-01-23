using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BattleShip
{
    public class Gameboard
    {
        public Grid BoatLayer { get; set; }
        public Grid PlayerLayer { get; set; }
        private ConsoleColor Background { get; set; }
        private ConsoleColor Foreground { get; set; }
        public int Rockets { get; set; }
        public int BackToMenu { get; set; }

        public Gameboard(Grid boatGrid, Grid playerGrid)
        {
            BoatLayer = boatGrid;
            PlayerLayer = playerGrid;
            Rockets = 10;
            BackToMenu = 0;
            Foreground = ConsoleColor.White;
            Background = ConsoleColor.Black;
            Console.ForegroundColor = Foreground;
            Console.BackgroundColor = Background;
        }

        public Gameboard()
        {
            Grid playerGrid = new Grid();
            PlayerLayer = playerGrid;
            Rockets = 40;
            BackToMenu = 0;
            Grid boatGrid = new Grid(10, 10, ' ');
            List<Boat> boatList = Boat.GenerateBoats();
            Boat.PlaceBoats(boatList, boatGrid);
            BoatLayer = boatGrid;
            Foreground = ConsoleColor.Gray;
            Background = ConsoleColor.Black;
            Console.ForegroundColor = Foreground;
            Console.BackgroundColor = Background;
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
                BackToMenu = 1;
            }
            while (isShotValid(coordinates) != true
                && BackToMenu == 0
                || isShotUsedBefore(coordinates) == true
                && BackToMenu == 0)
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
            if (BackToMenu == 0)
            {
                Console.Clear();
                Rockets--;
                WriteShot(coordinates);
            }
        }

        private void WriteShot(string shot)
        {
            int col = GetColumnNumber(shot[1]);
            int row = GetRowNumber(shot[0]);
            bool hit = isShotHit(shot);
            if (hit)
            {
                this.PlayerLayer.Matrix[row, col] = 'x';
                Boat hitBoat = HitBoat(shot);
            }
            else
            {
                this.PlayerLayer.Matrix[row, col] = ' ';
            }
        }

        private Boat HitBoat(string shot)
        {
            int col = GetColumnNumber(shot[1]);
            int row = GetRowNumber(shot[0]);
            Boat hitBoat;
            foreach (Boat boat in BoatLayer.Boats)
            {
                hitBoat = boat;
                bool rowMatch = false;
                for (int i = 0; i < boat.Length; i++)
                {
                    for (int ii = 0; ii < 2; ii++)
                    {
                        if (ii == 0 && boat.Coordinates[i, ii] == row)
                        {
                            rowMatch = true;
                        }
                        if (ii == 1 &&
                            rowMatch &&
                            boat.Coordinates[i, ii] == col)
                        {
                            hitBoat.Hits++;
                            IsBoatSunk(hitBoat);
                            return hitBoat;
                        }
                    }
                }
            }
            throw new ArgumentException("No hit boat");
        }

        private bool IsBoatSunk(Boat hitBoat)
        {
            if (hitBoat.Hits >= hitBoat.Length)
            {
                hitBoat.Sunk = true;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine("You sunk the " + hitBoat.Name + "!");
                Console.ForegroundColor = Foreground;
                return true;
            }
            return false;
        }

        private bool isShotHit(string shot)
        {
            if (BoatLayer.Matrix[GetRowNumber(shot[0]),
                GetColumnNumber(shot[1])] != BoatLayer.Symbol)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(shot + " HIT!" + "\n\n");
                Console.ForegroundColor = Foreground;
                return true;
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(shot + " MISSED!" + "\n\n");
            Console.ForegroundColor = Foreground;
            return false;
        }

        private bool isShotUsedBefore(string shot)
        {
            if (isShotValid(shot) != true)
            {
                return false;
            }
            int row = GetRowNumber(shot[0]);
            int col = GetColumnNumber(shot[1]);
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
            int row = GetRowNumber(shot[0]);
            int column = GetColumnNumber(shot[1]);
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

        private bool isInputValid(string shot)
        {
            shot = shot.Replace(" ", "");
            if (shot == "" || shot == " " || shot == null)
            {
                return false;
            }
            if (shot.ToLower() == "exit")
            {
                Menu.exit();
            }
            string pattern = @"^[A-ja-j]{1}[0-9]{1}$";
            Regex reg = new Regex(pattern);
            Match match = reg.Match(shot);
            if (match.Value != shot)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private int GetColumnNumber(char column)
        {
            string strColumn = (column).ToString();
            return int.Parse(strColumn) + 1;
        }

        private static int GetRowNumber(char letter)
        {
            int index = char.ToUpper(letter) - 64;
            return index;
        }
    }
}
