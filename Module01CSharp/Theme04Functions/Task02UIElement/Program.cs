﻿using System;

namespace Task02UIElement
{
    internal class Program
    {
        static void Main()
        {
            SpawnBars();
            Console.ReadKey();
        }

        static void SpawnBars()
        {
            string healthBarName = "Жизни";
            int maxHealthValue = 10;
            float fulnessHealthBar;
            int horizontalPositionHealthBar;
            int verticalPositionHealthBar;

            string manaBarName = "Мана";
            int maxManaValue = 10;
            float fulnessManaBar;
            int horizontalPositionManaBar;
            int verticalPositionManaBar;

            SetSettingsDrawBar(healthBarName, out fulnessHealthBar, out horizontalPositionHealthBar, out verticalPositionHealthBar);
            SetSettingsDrawBar(manaBarName, out fulnessManaBar, out horizontalPositionManaBar, out verticalPositionManaBar);

            DrawBar(healthBarName, maxHealthValue, fulnessHealthBar, horizontalPositionHealthBar, verticalPositionHealthBar);
            DrawBar(manaBarName, maxManaValue, fulnessManaBar, horizontalPositionManaBar, verticalPositionManaBar);
        }

        static void SetSettingsDrawBar(string barName, out float fullnessBar, out int horizontalPosition, out int verticalPosition)
        {
            Console.Write($"Введите заполненность бара {barName} в процентах от 0 до 100: ");
            fullnessBar = Convert.ToInt32(Console.ReadLine());

            Console.Write($"Введите позицию бара [{barName}] по X: ");
            horizontalPosition = Convert.ToInt32(Console.ReadLine());
            Console.Write($"Введите позицию бара [{barName}] по Y: ");
            verticalPosition = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
        }

        static void DrawBar(string barName, int maxValue, float fullnessBar, int horizontalPosition, int verticalPosition)
        {
            string barView = String.Empty;

            Console.SetCursorPosition(horizontalPosition, verticalPosition);

            for (int i = 0; i < maxValue; i++)
            {
                barView += i < Convert.ToInt32(fullnessBar / maxValue) ? "#" : "_";
            }

            Console.WriteLine($"[{barView}] {barName}");
        }
    }
}
