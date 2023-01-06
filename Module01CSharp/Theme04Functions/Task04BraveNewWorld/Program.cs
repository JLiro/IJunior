using System;

namespace Task04BraveNewWorld
{
    internal class Program
    {
        static void Main()
        {
            Console.CursorVisible = false;

            char[,] map = {
                            {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#',},
                            {'#',' ',' ',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#',},
                            {'#',' ',' ',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#',},
                            {'#',' ',' ',' ','#',' ',' ',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#',},
                            {'#',' ',' ',' ',' ',' ',' ',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#',},
                            {'#',' ',' ',' ',' ',' ',' ',' ','#',' ',' ','X','X','X',' ',' ',' ',' ',' ','#',},
                            {'#',' ',' ',' ',' ',' ',' ',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#',},
                            {'#',' ',' ',' ','#',' ',' ',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#',},
                            {'#',' ',' ',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#',},
                            {'#',' ',' ',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#',},
                            {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#',}
                          };

            char userSymbol = '@';
            int userVerticalPosition = 5;
            int userHorizontalPosition = 16;

            int userHorizontalStep = 0;
            int userVerticalStep = 0;

            char barrySymbol = 'X';
            int allBerriesCount = 0;
            int collectBerriesCount = 0;

            char emptySpaceSymbol = ' ';
            char wallSymbol = '#';

            char[] bag = new char[9];
            string bagView = "Сумка:";

            bool isPlaying = true;

            DrawMap(map, barrySymbol, ref allBerriesCount);

            Console.SetCursorPosition(userHorizontalPosition, userVerticalPosition);
            Console.Write(userSymbol);

            while (isPlaying)
            {
                Console.SetCursorPosition(0, 16);
                Console.Write($"Собрано: {collectBerriesCount}/{allBerriesCount}");

                Console.SetCursorPosition(0, 17);
                Console.Write(bagView);

                isPlaying = collectBerriesCount != allBerriesCount;

                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;

                    ChangeDirection(key, ref userHorizontalStep, ref userVerticalStep);

                    if (map[userVerticalPosition + userVerticalStep, userHorizontalPosition + userHorizontalStep] != wallSymbol)
                    {
                        Move(userSymbol, emptySpaceSymbol, ref userHorizontalPosition, ref userVerticalPosition, userHorizontalStep, userVerticalStep);

                        CollectBerries(map, userHorizontalPosition, userVerticalPosition, barrySymbol, emptySpaceSymbol, ref bag, ref bagView, ref collectBerriesCount);
                    }
                }
            }

            Console.SetCursorPosition(0, 19);
            Console.Write("Уровень пройден!");
            Console.ReadKey();
        }
        static void ChangeDirection(ConsoleKey сonsoleKey, ref int horizontalStep, ref int verticalStep)
        {
            const ConsoleKey UpKey = ConsoleKey.UpArrow;
            const ConsoleKey DownKey = ConsoleKey.DownArrow;
            const ConsoleKey LeftKey = ConsoleKey.LeftArrow;
            const ConsoleKey RightKey = ConsoleKey.RightArrow;

            const int Immobility = 0;
            const int Step = 1;

            int up = -Step;
            int down = Step;
            int left = -Step;
            int right = Step;

            switch (сonsoleKey)
            {
                case UpKey:
                    horizontalStep = Immobility; verticalStep = up;
                    break;
                case DownKey:
                    horizontalStep = Immobility; verticalStep = down;
                    break;
                case LeftKey:
                    horizontalStep = left; verticalStep = Immobility;
                    break;
                case RightKey:
                    horizontalStep = right; verticalStep = Immobility;
                    break;
            }
        }

        static void Move(char userSymbol, char emptySpaceSymbol, ref int horizontal, ref int vertical, int horizontalDirection, int verticalDirection)
        {
            Console.SetCursorPosition(horizontal, vertical);
            Console.Write(emptySpaceSymbol);

            horizontal += horizontalDirection;
            vertical += verticalDirection;

            Console.SetCursorPosition(horizontal, vertical);
            Console.Write(userSymbol);
        }

        static void CollectBerries(char[,] map, int verticalPosition, int horizontalPosition, char barrySymbol, char emptySpaceSymbol, ref char[] bag, ref string bagView, ref int collectBerries)
        {
            if (map[horizontalPosition, verticalPosition] == barrySymbol)
            {
                collectBerries++;
                map[horizontalPosition, verticalPosition] = emptySpaceSymbol;

                char[] tempBag = new char[bag.Length + 1];

                for (int i = 0; i < bag.Length; i++)
                {
                    tempBag[i] = bag[i];
                }

                tempBag[bag.Length - 1] = barrySymbol;
                bag = tempBag;

                bagView += $" {barrySymbol}";
            }
        }

        static void DrawMap(char[,] map, char barrySymbol, ref int allBerries)
        {
            sbyte barry = 1;
            sbyte emptySpace = 0;

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);

                    allBerries += map[i, j] == barrySymbol ? barry : emptySpace;
                }
                Console.WriteLine();
            }
        }
    }
}