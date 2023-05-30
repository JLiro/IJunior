using System;
using System.Collections.Generic;
using System.Linq;

namespace Task10War
{
    public class Soldier
    {
        public readonly string Description;

        private Random _random = new Random();

        public Soldier(string description, Random random)
        {
            SpecialAbility = GetRandomSpecialAbility(random);
            Description = description + " " + SpecialAbility;
        }

        public string SpecialAbility { get; private set; }

        public int Health { get; private set; } = 100;
        public bool IsDead => Health <= 0;

        public void Attack(Soldier enemy)
        {
            int minDamage = 0;
            int maxDamage = 15;

            int damage = _random.Next(minDamage, maxDamage);

            SetSpecialAbility(ref damage);

            enemy.TakeDamage(damage);
        }

        public void TakeDamage(int damage)
        {
            int minHealth = 0;

            Health = Math.Max(minHealth, Health - damage);
        }

        public override string ToString() => $"{Description} ({Health} здоровья)";

        private string GetRandomSpecialAbility(Random random)
        {
            var abilities = new[] { "Normal", "Sniper", "Tank" };
            return abilities[random.Next(abilities.Length)];
        }
    
        private void SetSpecialAbility(ref int damage)
        {
            const string SniperClass = "Sniper";
            const string TankClass = "Tank";

            int PowerSniperAbility = 2;
            int PowerTankAbility = 2;

            switch (SpecialAbility)
            {
                case SniperClass:
                    damage *= PowerSniperAbility;
                    break;
                case TankClass:
                    damage /= PowerTankAbility;
                    break;
            }
        }
    }

    public class Platoon
    {
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

        public void CreatePlatoon(Random random)
        {
            int minSolider = 2;
            int maxSolider = 4;

            int soliderCount = random.Next(minSolider, maxSolider);

            for (int i = 0; i < soliderCount; i++)
            {
                int id = i + 1;

                AddSoldier(new Soldier($"Солдат {id} взвода {Name}", random));
            }
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
        public void StartPlay(Random random)
        {
            var platoon1 = new Platoon("Альфа");
            platoon1.CreatePlatoon(random);

            var platoon2 = new Platoon("Браво");
            platoon2.CreatePlatoon(random);

            var logger = new BattleLogger();

            var simulator = new BattleSimulator(platoon1, platoon2, logger);

            simulator.StartBattle();

            Console.WriteLine(logger.BattleLog);
        }
    }

    class Program
    {
        static private Random _random = new Random();

        static void Main(string[] args)
        {
            Map map = new Map();
            map.StartPlay(new Random());

            Console.ReadLine();
        }
    }
}