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

        public string Name { get; set; }
        public int Age { get; set; }
        public string Disease { get; set; }
    }

    class Hospital
    {
        private List<Patient> _patients = new List<Patient>()
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
            while (!exit)
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
                        SortByName();
                        
                        foreach (var patient in _patients)
                        {
                            Console.WriteLine("{0}, {1} лет, имеет заболивание {2}", patient.Name, patient.Age, patient.Disease);
                        }
                        break;

                    case CommandSortByAge:
                        Console.WriteLine("\nПациенты остортированы по возрасту:");
                        SortByAge();

                        foreach (var patient in _patients)
                        {
                            Console.WriteLine("{0}, {1} лет, имеет заболивание {2}", patient.Name, patient.Age, patient.Disease);
                        }
                        break;

                    case CommandPrintPatientsWithDisease:
                        Console.Write("Введите название заболевания: ");
                        PrintPatientsWithDisease(Console.ReadLine());
                        break;

                    case CommandExit:
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }

                Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню");
                Console.ReadKey();
            }
        }

        private void SortByName()
        {
            _patients.Sort((p1, p2) => p1.Name.CompareTo(p2.Name));
        }

        private void SortByAge()
        {
            _patients.Sort((p1, p2) => p1.Age.CompareTo(p2.Age));
        }

        private void PrintPatientsWithDisease(string disease)
        {
            var patientsWithDisease = _patients.Where(p => p.Disease == disease);
            Console.WriteLine($"\nПациенты с заболеванием {disease}:");

            foreach (var patient in patientsWithDisease)
            {
                Console.WriteLine("{0}, {1} лет, имеет заболивание {2}", patient.Name, patient.Age, patient.Disease);
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