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
