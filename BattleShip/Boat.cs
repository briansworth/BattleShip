using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip
{
    public class Boat
    {
        public int Length { get; private set; }
        public string Name { get; set; }
        public char[,] Position { get; set; }
        public int[,] Coordinates { get; set; }
        public int Orientation { get; set; }
        public int[] Seed { get; set; }
        public char Symbol { get; set; }
        public int Hits { get; set; }
        public bool Sunk { get; set; }
        private static Random rand = new Random();

        public Boat(int length, string name)
        {
            this.Length = length;
            this.Hits = 0;
            this.Sunk = false;
            this.Name = name;
            this.Orientation = rand.Next(0, 2);
            this.Symbol = 'b';
        }

        public Boat(int length, string name, char symbol)
        {
            this.Length = length;
            this.Hits = 0;
            this.Sunk = false;
            this.Name = name;
            this.Orientation = rand.Next(0, 2);
            this.Symbol = symbol;
        }

        public static List<Boat> GenerateBoats()
        {
            List<Boat> boatList = new List<Boat>();
            boatList.Add(new Boat(3, "Submarine", 'S'));
            boatList.Add(new Boat(2, "Patrol Boat", 'P'));
            boatList.Add(new Boat(5, "Carrier", 'C'));
            boatList.Add(new Boat(4, "Battle Ship", 'B'));
            boatList.Add(new Boat(3, "Destroyer", 'D'));
            return boatList;
        }
        public static void PlaceBoats(List<Boat> boats, Grid boatGrid)
        {
            foreach (Boat boat in boats)
            {
                boat.PlaceBoat(boatGrid);
            }
        }
        private static int[] GetBoatSeed(char[,] Grid)
        {
            int height = Grid.GetLength(0);
            int width = Grid.GetLength(1);
            int w = rand.Next(1, width);
            int h = rand.Next(1, height);
            int[] seedCoord = { h, w };
            return seedCoord;
        }

        private static void VerticalBoat(Boat boat, Grid grid)
        {
            boat.Seed = GetBoatSeed(grid.Matrix);

            if (boat.Seed[0] + boat.Length > grid.Matrix.GetLength(0))
            {
                if (!(boat.Seed[0] - boat.Length < 1))
                {
                    VerticalBoatUp(boat, grid);
                }
                else
                {
                    throw new ArgumentException(
                       "Bad Seed: " + boat.Seed[0] + "x" + boat.Seed[1]);
                }
            }
            else
            {
                VerticalBoatDown(boat, grid);
            }
        }
        private static void VerticalBoatUp(Boat boat, Grid grid)
        {
            int[,] coords = new int[boat.Length, 2];
            char[,] position =
                new Char[grid.Matrix.GetLength(0), grid.Matrix.GetLength(1)];
            for (int i = boat.Seed[0]; i > boat.Seed[0] - boat.Length; i--)
            {
                position[i, boat.Seed[1]] = boat.Symbol;
            }
            for (int ii = 0; ii < coords.GetLength(0); ii++)
            {
                for (int iii = 0; iii < 2; iii++)
                {
                    if (iii == 0)
                    {
                        coords[ii, iii] = boat.Seed[0] - ii;
                    }
                    else
                    {
                        coords[ii, iii] = boat.Seed[1];
                    }
                }
            }
            boat.Position = position;
            boat.Coordinates = coords;
        }

        private static void VerticalBoatDown(Boat boat, Grid grid)
        {
            int[,] coords = new int[boat.Length, 2];
            char[,] position =
                new Char[grid.Matrix.GetLength(0), grid.Matrix.GetLength(1)];
            for (int i = boat.Seed[0]; i < boat.Seed[0] + boat.Length; i++)
            {
                position[i, boat.Seed[1]] = boat.Symbol;
            }
            for (int ii = 0; ii < coords.GetLength(0); ii++)
            {
                for (int iii = 0; iii < 2; iii++)
                {
                    if (iii == 0)
                    {
                        coords[ii, iii] = boat.Seed[0] + ii;
                    }
                    else
                    {
                        coords[ii, iii] = boat.Seed[1];
                    }
                }
            }
            boat.Position = position;
            boat.Coordinates = coords;
        }

        private static void HorizontalBoat(Boat boat, Grid grid)
        {
            boat.Seed = GetBoatSeed(grid.Matrix);
            if (boat.Seed[1] + boat.Length > grid.Matrix.GetLength(1))
            {
                if (!(boat.Seed[1] - boat.Length < 1))
                {
                    HorizontalBoatLeft(boat, grid);
                }
                else
                {
                    throw new ArgumentException(
                        "Bad Seed: " + boat.Seed[0] + "x" + boat.Seed[1]);
                }
            }
            else
            {
                HorizontalBoatRight(boat, grid);
            }
        }

        private static void HorizontalBoatRight(Boat boat, Grid grid)
        {
            char[,] position =
                new Char[grid.Matrix.GetLength(0), grid.Matrix.GetLength(1)];
            int[,] coords = new int[boat.Length, 2];
            for (int i = boat.Seed[1]; i < boat.Seed[1] + boat.Length; i++)
            {
                for (int ii = 0; ii < coords.GetLength(0); ii++)
                {
                    position[boat.Seed[0], i] = boat.Symbol;
                }
                for (int ii = 0; ii < coords.GetLength(0); ii++)
                {
                    for (int iii = 0; iii < 2; iii++)
                    {
                        if (iii == 0)
                        {
                            coords[ii, iii] = boat.Seed[0];
                        }
                        else
                        {
                            coords[ii, iii] = boat.Seed[1] + ii;
                        }
                    }
                }
            }
            boat.Position = position;
            boat.Coordinates = coords;
        }

        private static void HorizontalBoatLeft(Boat boat, Grid grid)
        {
            char[,] position =
                new Char[grid.Matrix.GetLength(0), grid.Matrix.GetLength(1)];
            int[,] coords = new int[boat.Length, 2];
            for (int i = boat.Seed[1]; i > boat.Seed[1] - boat.Length; i--)
            {
                position[boat.Seed[0], i] = boat.Symbol;
            }
            for (int ii = 0; ii < coords.GetLength(0); ii++)
            {
                for (int iii = 0; iii < 2; iii++)
                {
                    if (iii == 0)
                    {
                        coords[ii, iii] = boat.Seed[0];
                    }
                    else
                    {
                        coords[ii, iii] = boat.Seed[1] - ii;
                    }
                }
            }
            boat.Position = position;
            boat.Coordinates = coords;
        }

        public Grid PlaceBoat(Boat boat, Grid grid)
        {
            if (boat.Length > grid.Matrix.GetLength(0) ||
                boat.Length > grid.Matrix.GetLength(1))
            {
                throw new ArgumentException(
                    "The 'Boat' length must be smaller than the 'Grid' dimensions");
            }
            if (boat.Orientation == 1)
            {
                Boat.HorizontalBoat(boat, grid);
            }
            else
            {
                Boat.VerticalBoat(boat, grid);
            }
            while (!IsBoatPositionValid(boat, grid))
            {
                boat.Seed = GetBoatSeed(grid.Matrix);
                if (boat.Orientation == 1)
                {
                    Boat.HorizontalBoat(boat, grid);
                }
                else
                {
                    Boat.VerticalBoat(boat, grid);
                }
                IsBoatPositionValid(boat, grid);
            }
            for (int i = 0; i < boat.Position.GetLength(0); i++)
            {
                for (int ii = 0; ii < boat.Position.GetLength(1); ii++)
                {
                    if (boat.Position[i, ii] == boat.Symbol)
                    {
                        grid.Matrix[i, ii] = boat.Symbol;
                    }
                }
            }
            grid.Boats.Add(boat);
            return grid;
        }

        public void ShowBoat(Grid grid)
        {
            for (int i = 0; i < this.Position.GetLength(0); i++)
            {
                for (int ii = 0; ii < this.Position.GetLength(1); ii++)
                {
                    if (this.Position[i, ii] == Symbol)
                    {
                        if (grid.Matrix[i, ii] != 'x')
                        {
                            grid.Matrix[i, ii] = this.Symbol;
                        }
                    }
                }
            }
        }

        public Grid PlaceBoat(Grid grid)
        {
            if (this.Length > grid.Matrix.GetLength(0) ||
                this.Length > grid.Matrix.GetLength(1))
            {
                throw new ArgumentException(
                    "The 'Boat' length must be smaller than the 'Grid' dimensions");
            }
            if (this.Orientation == 1)
            {
                Boat.HorizontalBoat(this, grid);
            }
            else
            {
                Boat.VerticalBoat(this, grid);
            }
            while (!IsBoatPositionValid(this, grid))
            {
                Seed = GetBoatSeed(grid.Matrix);
                if (Orientation == 1)
                {
                    Boat.HorizontalBoat(this, grid);
                }
                else
                {
                    Boat.VerticalBoat(this, grid);
                }
                IsBoatPositionValid(this, grid);
            }
            for (int i = 0; i < this.Position.GetLength(0); i++)
            {
                for (int ii = 0; ii < this.Position.GetLength(1); ii++)
                {
                    if (this.Position[i, ii] == Symbol)
                    {
                        grid.Matrix[i, ii] = this.Symbol;
                    }
                }
            }
            grid.Boats.Add(this);
            return grid;
        }

        private static bool IsBoatPositionValid(Boat boat, Grid grid)
        {
            for (int i = 0; i < grid.Matrix.GetLength(0); i++)
            {
                for (int ii = 0; ii < grid.Matrix.GetLength(1); ii++)
                {
                    if (grid.Matrix[i, ii] != grid.Symbol &&
                        boat.Position[i, ii] == boat.Symbol)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void WriteCoordinates()
        {
            for (int i = 0; i < Coordinates.GetLength(0); i++)
            {
                for (int ii = 0; ii < Coordinates.GetLength(1); ii++)
                {
                    if (ii == 0)
                    {
                        Console.Write("Coords: " + Coordinates[i, ii] + "x");
                    }
                    else
                    {
                        Console.WriteLine(Coordinates[i, ii]);
                    }
                }
            }
        }
    }
}
