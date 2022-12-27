/*/ 
    - == === ЗАДАЧА === == -
        
    ==0 Легенда:
        Вы заходите в поликлинику и видите огромную очередь из старушек,
    вам нужно рассчитать время ожидания в очереди.

    ==0 Формально:
        Пользователь вводит кол-во людей в очереди.
    Фиксированное время приема одного человека всегда равно 10 минутам.
    Пример ввода: Введите кол-во старушек: 14
    Пример вывода: "Вы должны отстоять в очереди 2 часа и 20 минут."
/*/

using System;

namespace Task07Polyclinic
{
    internal class Program
    {
        static void Main()
        {
            int peopleCount;
            decimal patientAppointmentTime = 10;
            decimal waitingTimeEndQueue;
            decimal waitingTimeEndQueueInHours;
            decimal waitingTimeEndQueueInMinutes;

            Console.Write("Введите кол-во старушек: ");
            peopleCount = Convert.ToInt32(Console.ReadLine());

            waitingTimeEndQueue = peopleCount * patientAppointmentTime;
            waitingTimeEndQueueInHours = Math.Round(waitingTimeEndQueue / 60, 0);
            waitingTimeEndQueueInMinutes = waitingTimeEndQueue % 60;

            Console.WriteLine($"\nВы должны отстоять в очереди {waitingTimeEndQueueInHours} час(а/ов) и {waitingTimeEndQueueInMinutes} минут(ы)");

            Console.ReadKey();
        }
    }
}
