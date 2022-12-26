using System;

/*/ 
    - == === ЗАДАЧА === == -
        
        Вы задаете вопросы пользователю, по типу "как вас зовут", "какой ваш знак зодиака"
    и тд, после чего, по данным, которые он ввел, формируете небольшой текст о пользователе:
    "Вас зовут Алексей, вам 21 год, вы водолей и работаете на заводе."
/*/

namespace Task01_Variables
{
    internal class Program
    {
        static void Main()
        {
            string stroke = "\n Интервьюер:";

            Console.Write(" Интервьюер: Как Вас зовут?\n Вы: ");
            stroke += " Вас зовут " + Console.ReadLine();

            Console.Write(" Интервьюер: Сколько Вам лет?\n Вы: ");
            stroke += ", вам " + Console.ReadLine();

            Console.Write(" Интервьюер: Из какого Вы города?\n Вы: ");
            stroke += " и вы из города " + Console.ReadLine();

            Console.WriteLine(stroke);
            Console.ReadKey();
        }
    }
}
