using System;
using System.Collections.Generic;
using System.Linq;

namespace Task01SearchCriminal
{
    public class Criminal
    {
        public Criminal(string name, bool isInJail, int height, int weight, string nationality)
        {
            Name = name;
            IsInJail = isInJail;
            Height = height;
            Weight = weight;
            Nationality = nationality;
        }

        public string Name { get; private set; }
        public bool IsInJail { get; private set; }
        public int Height { get; private set; }
        public int Weight { get; private set; }
        public string Nationality { get; private set; }
    }

    public class DataBase
    {
        public readonly List<Criminal> Criminals = new List<Criminal>
        {
            new Criminal("John Smith", true, 180, 80, "American"),
            new Criminal("Anna Johnson", false, 165, 55, "British"),
            new Criminal("Ivan Petrov", true, 175, 70, "Russian"),
            new Criminal("Maria Garcia", false, 170, 65, "Spanish"),
            new Criminal("Mohammed Ahmed", true, 185, 90, "Egyptian"),
            new Criminal("Hiroshi Nakamura", false, 175, 75, "Japanese"),
            new Criminal("Chen Wei", true, 170, 60, "Chinese"),
            new Criminal("Ludmila Ivanova", false, 165, 50, "Ukrainian"),
            new Criminal("Abdul Rahman", true, 190, 100, "Saudi Arabian"),
            new Criminal("Kim Min-ji", false, 160, 45, "South Korean")
        };

        public void SearchCriminal()
        {
            Console.Write("Введите рост: ");
            int height = int.Parse(Console.ReadLine());
            Console.Write("Введите вес: ");
            int weight = int.Parse(Console.ReadLine());
            Console.Write("Введите национальность: ");
            string nationality = Console.ReadLine();

            var filteredCriminals = from Criminal criminal in Criminals where criminal.Height == height && criminal.Weight == weight && criminal.Nationality == nationality && criminal.IsInJail == false select criminal.Name;

            foreach (var creminal in filteredCriminals)
            {
                Console.Write(creminal);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            DataBase dataBase = new DataBase();
            dataBase.SearchCriminal();
        }
    }
}
