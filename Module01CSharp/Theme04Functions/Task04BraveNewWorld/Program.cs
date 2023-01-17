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

            string bag = "Сумка:";

            bool isPlaying = true;

            DrawMap(map, barrySymbol, ref allBerriesCount);
            ShowSymbol(userSymbol, userHorizontalPosition, userVerticalPosition);

            while (isPlaying)
            {
                ShowBag(bag, collectBerriesCount, allBerriesCount);

                isPlaying = collectBerriesCount != allBerriesCount;

                if (Console.KeyAvailable)
                {
                    ChangeDirection(ref userHorizontalStep, ref userVerticalStep);

                    if (map[userVerticalPosition + userVerticalStep, userHorizontalPosition + userHorizontalStep] != wallSymbol)
                    {
                        Move(userSymbol, emptySpaceSymbol, ref userHorizontalPosition, ref userVerticalPosition, userHorizontalStep, userVerticalStep);

                        CollectBerries(map, userHorizontalPosition, userVerticalPosition, barrySymbol, emptySpaceSymbol, ref bag, ref collectBerriesCount);
                    }
                }
            }

            int verticalPositionCursor = 19;
            Console.SetCursorPosition(0, verticalPositionCursor);
            Console.Write("Уровень пройден!");
            Console.ReadKey();
        }

        static void ShowBag(string bagView, int collectBerriesCount, int allBerriesCount)
        {
            int verticalPositionCursor;

            verticalPositionCursor = 16;
            Console.SetCursorPosition(0, verticalPositionCursor);
            Console.Write($"Собрано: {collectBerriesCount}/{allBerriesCount}");

            verticalPositionCursor = 17;
            Console.SetCursorPosition(0, 17);
            Console.Write(bagView);
        }

        static void ShowSymbol(char userSymbol, int userHorizontalPosition, int userVerticalPosition)
        {
            Console.SetCursorPosition(userHorizontalPosition, userVerticalPosition);
            Console.Write(userSymbol);
        }

        static void ChangeDirection(ref int horizontalStep, ref int verticalStep)
        {
            const ConsoleKey UpKey = ConsoleKey.UpArrow;
            const ConsoleKey DownKey = ConsoleKey.DownArrow;
            const ConsoleKey LeftKey = ConsoleKey.LeftArrow;
            const ConsoleKey RightKey = ConsoleKey.RightArrow;

            int up = -1;
            int down = 1;
            int left = -1;
            int right = 1;

            horizontalStep = 0;
            verticalStep = 0;

            switch (Console.ReadKey(true).Key)
            {
                case UpKey:
                    verticalStep = up;
                    break;

                case DownKey:
                    verticalStep = down;
                    break;

                case LeftKey:
                    horizontalStep = left;
                    break;

                case RightKey:
                    horizontalStep = right;
                    break;
            }
        }

        static void Move(char userSymbol, char emptySpaceSymbol, ref int userHorizontalPosition, ref int userVerticalPosition, int userHorizontalStep, int userVerticalStep)
        {
            Console.SetCursorPosition(userHorizontalPosition, userVerticalPosition);
            Console.Write(emptySpaceSymbol);

            userHorizontalPosition += userHorizontalStep;
            userVerticalPosition += userVerticalStep;

            Console.SetCursorPosition(userHorizontalPosition, userVerticalPosition);
            Console.Write(userSymbol);

            ShowSymbol(userSymbol, userHorizontalPosition, userVerticalPosition);
        }

        static void CollectBerries(char[,] map, int verticalPosition, int horizontalPosition, char barrySymbol, char emptySpaceSymbol, ref string bag, ref int collectBerries)
        {
            if (map[horizontalPosition, verticalPosition] == barrySymbol)
            {
                collectBerries++;
                map[horizontalPosition, verticalPosition] = emptySpaceSymbol;

                bag += $" {barrySymbol}";
            }
        }

        static void DrawMap(char[,] map, char barrySymbol, ref int allBerries)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);

                    if(map[i, j] == barrySymbol)
                    {
                        allBerries++;
                    }
                }
                Console.WriteLine();
            }
        }
    }
}