using System;

namespace Task07Polyclinic
{
    internal class Program
    {
        static void Main()
        {
            int minutesInAnHour = 60;
            int peopleCount;
            int patientAppointmentTime = 10;
            int waitingTimeEndQueue;
            int waitingTimeEndQueueInHours;
            int waitingTimeEndQueueInMinutes;

            Console.Write("Введите кол-во старушек: ");
            peopleCount = Convert.ToInt32(Console.ReadLine());

            waitingTimeEndQueue = peopleCount * patientAppointmentTime;
            waitingTimeEndQueueInHours = waitingTimeEndQueue / minutesInAnHour;
            waitingTimeEndQueueInMinutes = waitingTimeEndQueue - (waitingTimeEndQueueInHours * minutesInAnHour);

            Console.WriteLine($"\nВы должны отстоять в очереди {waitingTimeEndQueueInHours} час(а/ов) и {waitingTimeEndQueueInMinutes} минут(ы)");
            Console.ReadKey();
        }
    }
}
