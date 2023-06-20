using System;
using System.Collections.Generic;
using System.Linq;

namespace Task03HospitalAnarchy
{
    class Patient
    {
        public Patient(string name, int age, string disease)
        {
            Name = name;
            Age = age;
            Disease = disease;
        }

        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Disease { get; private set; }
    }

    class Hospital
    {
        private readonly List<Patient> _patients = new List<Patient>()
        {
            new Patient("Джон Смит", 30, "Грипп"),
            new Patient("Мэри Джонсон", 45, "Рак"),
            new Patient("Дэвид Ли", 25, "Грипп"),
            new Patient("Сара Браун", 50, "Болезнь сердца"),
            new Patient("Майкл Дэвис", 38, "Рак"),
            new Patient("Эмили Уилсон", 42, "Грипп"),
            new Patient("Дэниел Тейлор", 35, "Болезнь сердца"),
            new Patient("Оливия Мартинес", 28, "Грипп"),
            new Patient("Джейкоб Хернандес", 48, "Рак"),
            new Patient("София Ли", 31, "Болезнь сердца")
        };

        public void Menu()
        {
            const string CommandSortByName = "1";
            const string CommandSortByAge  = "2";
            const string CommandPrintPatientsWithDisease = "3";
            const string CommandExit = "4";

            bool exit = false;

            while (exit == false)
            {
                Console.Clear();

                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Сортировка пациентов по имени");
                Console.WriteLine("2. Сортировать пациентов по возрасту");
                Console.WriteLine("3. Печать пациентов с определенным заболеванием");
                Console.WriteLine("4. Выход");

                Console.Write("\n> ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case CommandSortByName:
                        Console.WriteLine("\nПациенты остортированы по имени:");
                        SortPatientsByName();
                        break;

                    case CommandSortByAge:
                        Console.WriteLine("\nПациенты остортированы по возрасту:");
                        SortPatientsByAge();
                        break;

                    case CommandPrintPatientsWithDisease:
                        Console.Write("Введите название заболевания: ");
                        SortPatientsByDisease(Console.ReadLine());
                        break;

                    case CommandExit:
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Неверный выбор");
                        break;
                }

                Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню");
                Console.ReadKey();
            }
        }

        private void SortPatientsByName()
        {
            var filteredPatients = _patients.OrderBy(patient => patient.Name);

            PrintPatients(filteredPatients);
        }

        private void SortPatientsByAge()
        {
            var filteredPatients = _patients.OrderBy(patient => patient.Age);

            PrintPatients(filteredPatients);
        }

        private void SortPatientsByDisease(string disease)
        {
            var filteredPatients = _patients.Where(patient => patient.Disease == disease);

            Console.WriteLine($"\nПациенты с заболеванием {disease}:");

            PrintPatients(filteredPatients);
        }

        private void PrintPatients(IEnumerable<Patient> patients)
        {
            foreach (var patient in patients)
            {
                Console.WriteLine("{0}, {1} лет, имеет заболевание {2}", patient.Name, patient.Age, patient.Disease);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Hospital hospital = new Hospital();
            hospital.Menu();
        }
    }
}