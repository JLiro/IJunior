using System;
using System.Collections.Generic;
using System.Linq;

namespace Task06WeaponReport
{
    public class Soldier
    {
        public Soldier(string lastName)
        {
            LastName = lastName;
        }

        public string LastName { get; private set; }
    }

    public class Database
    {
        private readonly List<Soldier> _firstUnitSoldiers;
        private readonly List<Soldier> _secondUnitSoldiers;

        public Database()
        {
            _firstUnitSoldiers = new List<Soldier>()
            {
                new Soldier("Белов"),
                new Soldier("Беляев"),
                new Soldier("Богданов"),
                new Soldier("Васильев"),
                new Soldier("Воробьев"),
                new Soldier("Герасимов"),
                new Soldier("Григорьев"),
                new Soldier("Дмитриев")
            };

            _secondUnitSoldiers = new List<Soldier>()
            {
                new Soldier("Иванов"),
                new Soldier("Кузнецов"),
                new Soldier("Петров"),
                new Soldier("Смирнов"),
                new Soldier("Федоров"),
                new Soldier("Соколов"),
                new Soldier("Михайлов"),
                new Soldier("Новиков")
            };
        }

        public void PrintUnitSoldiers()
        {
            Console.WriteLine("Первый отряд:");
            PrintSoldiers(_firstUnitSoldiers);

            Console.WriteLine("\nВторой отряд:");
            PrintSoldiers(_secondUnitSoldiers);

            Console.Write($"\nОбновленный второй отряд ");
            PrintSoldiersByLastName();
        }

        private void PrintSoldiersByLastName()
        {
            string firstLetter = "Б";

            _secondUnitSoldiers.AddRange(_firstUnitSoldiers.Where(soldier => soldier.LastName.StartsWith(firstLetter)).ToList());

            Console.WriteLine("(перенесены бойцы на букву [{0}]:", firstLetter);
            PrintSoldiers(_secondUnitSoldiers);
        }

        private void PrintSoldiers(IEnumerable<Soldier> soldiers)
        {
            foreach (var soldier in soldiers)
            {
                Console.WriteLine(soldier.LastName);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();
            database.PrintUnitSoldiers();
        }
    }
}