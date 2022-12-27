/*/ 
    - == === ЗАДАЧА === == -
        
        Вы задаете вопросы пользователю, по типу "как вас зовут", "какой ваш знак зодиака"
    и тд, после чего, по данным, которые он ввел, формируете небольшой текст о пользователе:
    "Вас зовут Алексей, вам 21 год, вы водолей и работаете на заводе."
/*/

using System;

namespace Task03StringManipulation
{
    internal class Program
    {
        static void Main()
        {
            string stroke = "\nИнтервьюер:";

            Console.Write("Интервьюер: Как Вас зовут?\nВы: ");
            stroke += " Вас зовут " + Console.ReadLine();

            Console.Write("Интервьюер: Сколько Вам лет?\nВы: ");
            stroke += ", вам " + Console.ReadLine();

            Console.Write("Интервьюер: Из какого Вы города?\nВы: ");
            stroke += " и вы из города " + Console.ReadLine();

            Console.WriteLine(stroke);
            Console.ReadKey();
        }
    }
}
