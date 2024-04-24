using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips
{
    class Game
    {
        public static void Start()
        {
            Player player1 = CreatePlayer(1);
            Player player2 = CreatePlayer(2);

            Console.Clear();
            Console.WriteLine($"{player1.Name}, rozpocznij ustawianie statków.");
            SetupBoard(player1);
            Console.Clear();
            Console.WriteLine($"{player2.Name}, rozpocznij ustawianie statków.");
            SetupBoard(player2);

            Play(player1, player2);
        }

        private static Player CreatePlayer(int playerNumber)
        {
            Console.Write($"Gracz {playerNumber}, podaj swoje imię: ");
            string name = Console.ReadLine();
            return new Player(name);
        }

        private static void SetupBoard(Player player)
        {
            for (int size = 4; size >= 1; size--)
            {
                for (int i = 0; i < 5 - size; i++)
                {
                    Console.WriteLine($"Ustaw {5 - size} statki o długości {size}.");
                    ShipPlacement(player.OwnBoard, size);
                    Console.Clear();
                    Console.WriteLine($"{player.Name}, oto Twoja plansza:");
                    player.OwnBoard.Display();
                }
            }
        }

        private static void ShipPlacement(Board board, int size)
        {
            while (true)
            {
                Console.Write($"Podaj współrzędne statku (od A-J i 1-10, np. D5) o długości {size}: ");
                string input = Console.ReadLine().ToUpper();

                if (input.Length < 2 || input.Length > 3 || !char.IsLetter(input[0]) || !char.IsDigit(input[input.Length - 1]))
                {
                    Console.WriteLine("Nieprawidłowe współrzędne. Podaj literę i cyfrę.");
                    continue;
                }

                int x = input[0] - 'A';
                int y = int.Parse(input.Substring(1)) - 1;
                char direction = 'H'; 

                if (size > 1)
                {
                    Console.Write("Podaj kierunek statku (V - pionowy, H - poziomy): ");
                    direction = char.ToUpper(Console.ReadKey().KeyChar);
                    Console.WriteLine();

                    if (direction != 'H' && direction != 'V')
                    {
                        Console.WriteLine("Nieprawidłowy kierunek. Wprowadź H lub V.");
                        continue;
                    }
                }

                if (direction != 'H' && direction != 'V')
                {
                    Console.WriteLine("Nieprawidłowy kierunek. Wprowadź H lub V.");
                    continue;
                }

                if (direction == 'H' && (x + size > 10 || y < 0 || y > 9))
                {
                    Console.WriteLine("Statek nie mieści się na planszy w tym miejscu. Wybierz inne współrzędne.");
                    continue;
                }

                if (direction == 'V' && (y + size > 10 || x < 0 || x > 9))
                {
                    Console.WriteLine("Statek nie mieści się na planszy w tym miejscu. Wybierz inne współrzędne.");
                    continue;
                }

                bool isCollision = IsCollision(board, size, x, y, direction);

                if (isCollision)
                {
                    Console.WriteLine("Statek nie może się stykać z innymi statkami. Wybierz inne współrzędne.");
                    continue;
                }

                board.ShipPlacement(input, direction, size);
                break;
            }
        }

        private static bool IsCollision(Board board, int size, int x, int y, char direction)
        {
            bool isCollision = false;
            if (direction == 'H')
            {
                for (int i = -1; i <= size; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        int newX = x + i;
                        int newY = y + j;
                        if (newX >= 0 && newX < 10 && newY >= 0 && newY < 10)
                        {
                            if (board.GetCell(newX, newY) != '.')
                            {
                                isCollision = true;
                                break;
                            }
                        }
                    }
                    if (isCollision) break;
                }
            }
            else if (direction == 'V')
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= size; j++)
                    {
                        int newX = x + i;
                        int newY = y + j;
                        if (newX >= 0 && newX < 10 && newY >= 0 && newY < 10)
                        {
                            if (board.GetCell(newX, newY) != '.')
                            {
                                isCollision = true;
                                break;
                            }
                        }
                    }
                    if (isCollision) break;
                }
            }
            return isCollision;
        }

        private static void Play(Player player1, Player player2)
        {
            Console.Clear();
            Console.WriteLine("Rozpoczynamy grę!");

            bool gameEnded = false;
            Player currentPlayer = player1;
            Player otherPlayer = player2;
            Player winner = null;

            while (!gameEnded)
            {
                Console.WriteLine($"Wciśnij dowolny klawisz, aby zacząć strzelać {currentPlayer.Name}");
                Console.ReadKey(true);
                Console.Clear();

                Console.WriteLine($"{currentPlayer.Name}, teraz Twoja kolej. Strzelaj!");

                while (true)
                {
                    Console.WriteLine("# oznacza statek, X oznacza trafienie w statek, O oznacza pudło");
                    Console.WriteLine($"Twoja plansza, {currentPlayer.Name}:");
                    currentPlayer.OwnBoard.DisplayHits();

                    Console.WriteLine($"Twoja plansza trafień, {currentPlayer.Name}:");
                    otherPlayer.OwnBoard.DisplayShots();

                    Console.Write("Podaj współrzędne strzału: ");
                    string input = Console.ReadLine().ToUpper();

                    if (input.Length < 2 || input.Length > 3 || !char.IsLetter(input[0]) || !char.IsDigit(input[input.Length - 1]))
                    {
                        Console.WriteLine("Nieprawidłowe współrzędne. Podaj literę i cyfrę.");
                        Console.Clear();
                        continue;
                    }

                    int x = "ABCDEFGHIJ".IndexOf(input[0]);
                    int y = int.Parse(input.Substring(1)) - 1;

                    if (x < 0 || x >= 10 || y < 0 || y >= 10)
                    {
                        Console.WriteLine("Współrzędne poza zakresem planszy.");
                        Console.Clear();
                        continue;
                    }

                    char cellValue = otherPlayer.OwnBoard.GetCell(x, y);

                    if (cellValue == '#')
                    {
                        otherPlayer.OwnBoard.SetCell(x, y, 'X');
                        if (otherPlayer.OwnBoard.AreShipsSunk())
                        {
                            gameEnded = true;
                            winner = currentPlayer;
                            break;
                        }
                        else if (otherPlayer.OwnBoard.IsShipSunk(x, y))
                        {
                            Console.WriteLine("Trafiony, zatopiony!");
                        }
                        else
                        {
                            Console.WriteLine("Trafiony!");
                        }
                        Thread.Sleep(1000);
                        Console.Clear();
                    }
                    else
                    {
                        otherPlayer.OwnBoard.SetCell(x, y, 'O');
                        Console.WriteLine("Pudło!");
                        Thread.Sleep(1000);
                        Console.Clear();
                        break;
                    }
                }

                Player temp = currentPlayer;
                currentPlayer = otherPlayer;
                otherPlayer = temp;
            }

            Console.Clear();
            Console.WriteLine($"BRAWO, WYGRAŁEŚ {winner.Name}!");
            Console.WriteLine("Naciśnij dowolny klawisz aby powrócić do menu...");
            Console.ReadKey(true);
        }
    }
}