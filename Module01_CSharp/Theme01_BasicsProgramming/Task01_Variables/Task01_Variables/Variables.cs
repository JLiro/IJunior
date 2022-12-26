using System;

/*/ 
    - == === ЗАДАЧА === == -
        
        Попрактикуйтесь в создании переменных, объявить 10 переменных разных типов.
    Напоминание: переменные именуются с маленькой буквы, если название состоит
    из нескольких слов, то комбинируем их следующим образом - названиеПеременной.
    Также имя всегда должно отражать суть того, что хранит переменная.
/*/

namespace Task01_Variables
{
    internal class Variables
    {
        static void Main()
        {
            /*/ Основные типы: int, uint | float | char | string | bool /*/

            /*/ Целочисленные /*/
            byte lettersInWord;         // 0 до 255
            sbyte outsideTemperature;   // -128 до 127
            short balanceCreditCard;    // -32768 до 32767
            ushort countBread;          // 0 до 65535
            int countApple;             // -2147483648 до 2147483647
            uint countSeconds;          // 0 до 4294967295
            decimal balanceBankAccount; // Используют для валюты

            /*/ Числа с плавающей точкой /*/
            float revenueToday = 5.7f;  // 7 знаков
            double earningsToday = 5.7; // 15 знаков
            char question = '?';        // Символьный тип

            /*/ Cтроковые /*/
            string welcomeText = "Привет, мир!";
            Console.WriteLine(welcomeText);

            /*/ Логические /*/
            bool IsRainingNow = false;

            bool IsFallingNow;      //Объявление
            IsFallingNow = false;   //Инициализация
        }
    }
}
