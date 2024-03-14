using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips
{
    class Board
    {
        private char[,] grid;
        private char[,] hits;

        public Board()
        {
            grid = new char[10, 10];
            hits = new char[10, 10];
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    grid[i, j] = '.';
                    hits[i, j] = '.';
                }
            }
        }

        public void Display()
        {
            Console.WriteLine("   A B C D E F G H I J");
            for (int i = 0; i < 10; i++)
            {
                Console.Write((i + 1).ToString().PadLeft(2) + " ");
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public char GetCell(int x, int y)
        {
            return grid[y, x];
        }

        public void SetCell(int x, int y, char value)
        {
            grid[y, x] = value;

            if (value == 'X' || value == '.')
            {
                hits[y, x] = value;
            }
        }

        public void PlaceShip(string coordinates, char direction, int size)
        {
            int x = coordinates[0] - 'A';
            int y = int.Parse(coordinates.Substring(1)) - 1;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newX = x + i;
                    int newY = y + j;

                    if (newX >= 0 && newX < 10 && newY >= 0 && newY < 10 && grid[newY, newX] != '.')
                    {
                        Console.WriteLine("Statek nie może się stykać z innymi statkami. Wybierz inne współrzędne.");
                        return;
                    }
                }
            }

            grid[y, x] = 'O';

            if (direction == 'H')
            {
                for (int i = 1; i < size; i++)
                {
                    grid[y, x + i] = 'O';
                }
            }
            else if (direction == 'V')
            {
                for (int i = 1; i < size; i++)
                {
                    grid[y + i, x] = 'O';
                }
            }
        }

        public bool IsShipSunk(int x, int y)
        {
            for (int i = 1; i <= 3; i++)
            {
                int newY = y - i;
                if (newY >= 0)
                {
                    if (GetCell(x, newY) == 'O')
                    {
                        return false;
                    }
                    else if (GetCell(x, newY) == '.')
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; i <= 3; i++)
            {
                int newX = x + i;
                if (newX < 10)
                {
                    if (GetCell(newX, y) == 'O')
                    {
                        return false;
                    }
                    else if (GetCell(newX, y) == '.')
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; i <= 3; i++)
            {
                int newY = y + i;
                if (newY < 10)
                {
                    if (GetCell(x, newY) == 'O')
                    {
                        return false;
                    }
                    else if (GetCell(x, newY) == '.')
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; i <= 3; i++)
            {
                int newX = x - i;
                if (newX >= 0)
                {
                    if (GetCell(newX, y) == 'O')
                    {
                        return false;
                    }
                    else if (GetCell(newX, y) == '.')
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            return true;
        }

        public bool AllShipsSunk()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (grid[i, j] == 'O')
                        return false;
                }
            }
            return true;
        }

        public void DisplayHits()
        {
            Console.WriteLine("   A B C D E F G H I J");
            for (int i = 0; i < 10; i++)
            {
                Console.Write((i + 1).ToString().PadLeft(2) + " ");
                for (int j = 0; j < 10; j++)
                {
                    char cell = hits[i, j];
                    if (cell == 'X')
                    {
                        Console.Write("X ");
                    }
                    else if (cell == 'O' || grid[i, j] == 'O')
                    {
                        Console.Write("O ");
                    }
                    else
                    {
                        Console.Write(". ");
                    }
                }
                Console.WriteLine();
            }
        }
    }


}

