using System;
using System.Collections.Generic;
using System.Linq;

namespace Task06WeaponReport
{
    public class Soldier
    {
        public Soldier(string name, string weapon, string rank, int serviceDurationInMonths)
        {
            Name = name;
            Weapon = weapon;
            Rank = rank;
            ServiceDurationInMonths = serviceDurationInMonths;
        }

        public string Name { get; private set; }
        public string Weapon { get; private set; }
        public string Rank { get; private set; }
        public int ServiceDurationInMonths { get; private set; }
    }

    public class Database
    {
        private readonly List<Soldier> _soldiers;

        public Database()
        {
            _soldiers = new List<Soldier>()
            {
                new Soldier("John Smith", "M16", "Private", 12),
                new Soldier("Jane Doe", "AK-47", "Corporal", 24),
                new Soldier("Bob Johnson", "M4", "Sergeant", 36),
                new Soldier("Alice Williams", "MP5", "Lieutenant", 48),
                new Soldier("David Brown", "Glock 17", "Captain", 60),
                new Soldier("Emily Davis", "UMP45", "Major", 72),
                new Soldier("Michael Wilson", "Sig Sauer P226", "Colonel", 84),
                new Soldier("Sarah Martinez", "Remington 870", "General", 96),
                new Soldier("Thomas Anderson", "Barrett M82", "Commander", 108),
                new Soldier("Olivia Garcia", "Steyr AUG", "Chief Commander", 120)
            };
        }

        public void PrintSoldiersInfo()
        {
            Console.WriteLine("Список всех солдат:");
            PrintSoldiers(GetQuerySoldiers());
        }

        private IEnumerable<dynamic> GetQuerySoldiers()
        {
            return _soldiers.Select(soldier => new { soldier.Name, soldier.Rank });
        }

        private void PrintSoldiers(IEnumerable<dynamic> soldiers)
        {
            foreach (var soldier in soldiers)
            {
                Console.WriteLine(soldier.Name + " " + soldier.Rank);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();
            database.PrintSoldiersInfo();
        }
    }
}