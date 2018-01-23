using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BattleShip
{
    public class Grid
    {
        public char Symbol { get; set; }
        public char[,] Matrix { get; set; }
        public List<Boat> Boats { get; set; }

        public Grid()
        {
            char[,] grid = GenerateGrid(10, 10);
            this.Boats = new List<Boat>();
            this.Matrix = grid;
            this.Symbol = '#';
            if (!IsGridValidRange(this.Matrix))
            {
                throw new ArgumentException("Dimensions are invalid.");
            }
        }

        public Grid(char symbol)
        {
            char[,] grid = GenerateGrid(10, 10, symbol);
            this.Boats = new List<Boat>();
            this.Matrix = grid;
            this.Symbol = symbol;
            if (!IsGridValidRange(this.Matrix))
            {
                throw new ArgumentException("Dimensions are invalid.");
            }
        }

        public Grid(int height, int width)
        {
            char[,] grid = GenerateGrid(height, width);
            this.Boats = new List<Boat>();
            this.Matrix = grid;
            this.Symbol = '#';
            if (!IsGridValidRange(this.Matrix))
            {
                throw new ArgumentException("Dimensions are invalid.");
            }
        }

        public Grid(int height, int width, char symbol)
        {
            char[,] grid = GenerateGrid(height, width, symbol);
            this.Boats = new List<Boat>();
            this.Matrix = grid;
            this.Symbol = symbol;
            if (!IsGridValidRange(this.Matrix))
            {
                throw new ArgumentException("Dimensions are invalid.");
            }
        }

        private static char[,] GenerateGrid(int x, int y)
        {
            if (x < 0 || y < 0)
            {
                throw new ArgumentException("Must be positive values");
            }
            x = x + 1;
            y = y + 1;
            char[,] grid = new System.Char[x, y];
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int ii = 0; ii < grid.GetLength(1); ii++)
                {
                    if (i == 0 && ii == 0)
                    {
                        grid[i, ii] = ' ';
                    }
                    else if (i == 0 && ii > 0)
                    {
                        string num = (ii - 1).ToString();
                        grid[i, ii] = Convert.ToChar(num);
                    }
                    else if (ii == 0 && i > 0)
                    {
                        grid[i, ii] = GetColumnName(i);
                    }
                    else
                    {
                        grid[i, ii] = '#';
                    }
                }
            }
            return grid;
        }

        private static char[,] GenerateGrid(int x, int y, char symbol)
        {
            if (x < 0 || y < 0)
            {
                throw new ArgumentException("Must be positive values");
            }
            x = x + 1;
            y = y + 1;
            char[,] grid = new System.Char[x, y];
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int ii = 0; ii < grid.GetLength(1); ii++)
                {
                    if (i == 0 && ii == 0)
                    {
                        grid[i, ii] = symbol;
                    }
                    else if (i == 0 && ii > 0)
                    {
                        string num = (ii - 1).ToString();
                        grid[i, ii] = Convert.ToChar(num);
                    }
                    else if (ii == 0 && i > 0)
                    {
                        grid[i, ii] = GetColumnName(i);
                    }
                    else
                    {
                        grid[i, ii] = symbol;
                    }
                }
            }
            return grid;
        }

        public static void WriteGrid(char[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int ii = 0; ii < matrix.GetLength(1); ii++)
                {
                    Console.Write(matrix[i, ii] + "  ");
                }
                Console.WriteLine("\n");
            }
        }

        public static void WriteGrid(Grid grid)
        {
            for (int i = 0; i < grid.Matrix.GetLength(0); i++)
            {
                for (int ii = 0; ii < grid.Matrix.GetLength(1); ii++)
                {
                    if (grid.Matrix[i, ii] == 'x')
                    {
                        ConsoleColor curColor = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(grid.Matrix[i, ii] + "  ");
                        Console.ForegroundColor = curColor;
                    }
                    else if (grid.Matrix[i, ii] != 'x' &&
                        grid.Matrix[i, ii] != ' ' &&
                        grid.Matrix[i, ii] != grid.Symbol &&
                        i != 0 && ii != 0)
                    {
                        ConsoleColor curColor = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(grid.Matrix[i, ii] + "  ");
                        Console.ForegroundColor = curColor;
                    }
                    else
                    {
                        Console.Write(grid.Matrix[i, ii] + "  ");
                    }
                }
                Console.WriteLine("\n");
            }
        }

        public void WriteGrid()
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int ii = 0; ii < Matrix.GetLength(1); ii++)
                {
                    Console.Write(Matrix[i, ii] + "  ");
                }
                Console.WriteLine("\n");
            }
        }

        private static char GetColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            char columnName = ' ';
            int modulo;

            while (dividend > 0 && dividend < 27)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo);
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

        private static bool IsValidDimension(string dimension)
        {
            bool result = false;
            dimension = dimension.Replace(" ", "");
            string pattern = @"^[0-9]{1,2}x[0-9]{1,2}$";
            Regex reg = new Regex(pattern);
            Match match = reg.Match(dimension);
            if (match.Value == dimension)
            {
                result = true;
            }
            return result;
        }

        private static int maxGridSize = 297;
        private static int minGridSize = 30;
        private static int maxHeight = 27;
        private static int maxWidth = 11;

        private static bool IsGridValidRange(char[,] matrix)
        {
            int h = matrix.GetLength(0);
            int w = matrix.GetLength(1);
            if (h <= 1 || w <= 1 || w > maxWidth || h > maxHeight)
            {
                return false;
            }
            if (h < 5 || w < 5)
            {
                return false;
            }
            if (matrix.Length > maxGridSize || matrix.Length < minGridSize)
            {
                return false;
            }

            return true;
        }
    }
}
