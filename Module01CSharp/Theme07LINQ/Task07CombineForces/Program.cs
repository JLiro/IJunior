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
        private List<Soldier> _firstUnitSoldiers;
        private List<Soldier> _secondUnitSoldiers;

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

            MoveSoldiersByLastNameToSecondUnit();

            Console.WriteLine($"\nОбновленный второй отряд:");
            PrintSoldiers(_secondUnitSoldiers);
        }

        private void MoveSoldiersByLastNameToSecondUnit()
        {
            string firstLetter = "Б";

            var soldiersToMove = _firstUnitSoldiers.Where(soldier => soldier.LastName.StartsWith(firstLetter));
            
            _firstUnitSoldiers  = _firstUnitSoldiers.Except(soldiersToMove).ToList();
            _secondUnitSoldiers = _secondUnitSoldiers.Concat(soldiersToMove).ToList();
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