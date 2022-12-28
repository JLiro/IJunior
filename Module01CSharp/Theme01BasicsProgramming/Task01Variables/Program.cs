/*/ 
    - == === ЗАДАЧА === == -
        
        Попрактикуйтесь в создании переменных, объявить 10 переменных разных типов.
    Напоминание: переменные именуются с маленькой буквы, если название состоит
    из нескольких слов, то комбинируем их следующим образом - названиеПеременной.
    Также имя всегда должно отражать суть того, что хранит переменная.
/*/

using System;

namespace Task01_Variables
{
    internal class Program
    {
        static void Main()
        {
            byte lettersInWord;         
            sbyte outsideTemperature;   
            short balanceCreditCard;    
            ushort countBread;         
            int countApple;             
            uint countSeconds;        
            decimal balanceBankAccount; 
            float revenueToday = 5.7f;  
            double earningsToday = 5.7; 
            char question = '?';       
            string welcomeText = "Привет, мир!";
            bool isRainingNow = false;
        }
    }
}
