using System;
using System.Collections.Generic;
using System.Linq;

namespace Task10War
{
    public class Soldier
    {
        private Random _random;

        public readonly string Name;

        public Soldier(string name) => Name = name;

        public string SpecialAbility { get; set; }

        public int Health { get; private set; } = 100;
        public bool IsDead => Health <= 0;

        public void Attack(Soldier enemy)
        {
            const string SniperClass = "Sniper";
            const string TankClass   = "Tank";

            _random = new Random();

            int minDamage = 0;
            int maxDamage = 15;

            int damage = _random.Next(minDamage, maxDamage);

            int PowerSniperAbility = 2;
            int PowerTankAbility = 2;

            switch (SpecialAbility)
            {
                case SniperClass:
                    damage *= PowerSniperAbility;
                    break;
                case TankClass:
                    damage /= PowerTankAbility;
                    enemy.TakeDamage(damage);
                    break;
            }

            enemy.TakeDamage(_random.Next(damage));
        }

        public void TakeDamage(int damage)
        {
            int minHealth = 0;

            Health = Math.Max(minHealth, Health - damage);
        }

        public override string ToString() => $"{Name} ({Health} здоровья)";
    }

    public class Platoon
    {
        private Random _random;

        public readonly string Name;
        
        public readonly List<Soldier> Soldiers = new List<Soldier>();
        
        public Platoon(string name) => Name = name;

        public void AddSoldier(Soldier soldier) => Soldiers.Add(soldier);

        public Soldier GetRandomSoldier()
        {
            var aliveSoldiers = Soldiers.Where(soldier => !soldier.IsDead).ToList();

            if (aliveSoldiers.Count <= 0) return null;

            var randomIndex = new Random().Next(aliveSoldiers.Count);
                
            return aliveSoldiers[randomIndex];
        }

        public void CreatePlatoon()
        {
            _random = new Random();

            int minSolider = 2;
            int maxSolider = 4;

            int soliderCount = _random.Next(minSolider, maxSolider);

            for (int i = 0; i < soliderCount; i++)
            {
                int id = i + 1;

                AddSoldier(new Soldier($"Солдат {id} взвода {Name}") {SpecialAbility = GetRandomSpecialAbility()});
            }
        }

        private string GetRandomSpecialAbility()
        {
            var abilities = new[] { "Normal", "Sniper", "Tank" };

            return abilities[new Random().Next(abilities.Length)];
        }
    }

    public class BattleLogger
    {
        private string log = "";

        public void LogBattleEvent(string message) => log += message + "\n";

        public string BattleLog => log;
    }

    public class BattleSimulator
    {
        private readonly Platoon _platoon1;
        private readonly Platoon _platoon2;
        private readonly BattleLogger _logger;

        public BattleSimulator(Platoon platoon1, Platoon platoon2, BattleLogger logger)
        {
            _platoon1 = platoon1;
            _platoon2 = platoon2;
            _logger = logger;
        }

        public void StartBattle()
        {
            _logger.LogBattleEvent($"Бой начался! {_platoon1.Name} против {_platoon2.Name}\n");

            while (_platoon1.Soldiers.Any(soldier => !soldier.IsDead) && _platoon2.Soldiers.Any(soldier => !soldier.IsDead))
            {
                AttackPlatoon(_platoon1, _platoon2, _logger);
                AttackPlatoon(_platoon2, _platoon1, _logger);
            }

            var victoriousPlatoon = _platoon1.Soldiers.Any(soldier => !soldier.IsDead) ? _platoon1 : _platoon2;

            _logger.LogBattleEvent($"\nБой окончен. Взвод {victoriousPlatoon.Name} победил!");

            ShowSurvivingSoldiers(_platoon1);
            ShowSurvivingSoldiers(_platoon2);
        }

        private void AttackPlatoon(Platoon attacker, Platoon defender, BattleLogger logger)
        {
            foreach (var soldier in attacker.Soldiers.Where(soldier => !soldier.IsDead))
            {
                var targetSoldier = defender.GetRandomSoldier();

                if(targetSoldier == null)
                {
                    logger.LogBattleEvent($"Взвод {defender.Name} уже повержен");
                    break;
                }

                soldier.Attack(targetSoldier);
                logger.LogBattleEvent($"{soldier} атакует {targetSoldier} ({defender.Name})");
            }
        }

        private void ShowSurvivingSoldiers(Platoon platoon)
        {
            _logger.LogBattleEvent($"\nОставшиеся бойцы из взвода {platoon.Name}:");

            foreach (var soldier in platoon.Soldiers.Where(soldier => !soldier.IsDead))
            {
                _logger.LogBattleEvent($"- {soldier}");
            }
        }
    }

    class Map
    { 
        public void StartPlay()
        {
            var platoon1 = new Platoon("Альфа");
            platoon1.CreatePlatoon();

            var platoon2 = new Platoon("Браво");
            platoon2.CreatePlatoon();

            var logger = new BattleLogger();

            var simulator = new BattleSimulator(platoon1, platoon2, logger);

            simulator.StartBattle();

            Console.WriteLine(logger.BattleLog);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Map map = new Map();
            map.StartPlay();

            Console.ReadLine();
        }
    }
}