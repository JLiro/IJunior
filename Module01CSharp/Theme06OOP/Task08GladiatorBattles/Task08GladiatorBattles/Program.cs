using System;
using System.Collections.Generic;

namespace Task08GladiatorBattles
{
    interface IDamageable
    {
        public void TakeDamage(uint opponentDamage);
    }

    class Health : IDamageable
    {
        public uint Value { get; private set; }

        public Health(uint value) => Value = value;

        public void TakeDamage(uint opponentDamage) => Value -= Math.Min(Value, opponentDamage);

        public void TakeHeal(uint health) => Value += health;

        public bool IsDead() => Value == 0;
    }

    abstract class Gladiator : IDamageable
    {
        protected Health _health;
        protected uint _maxHealth;

        public Gladiator(char icon, uint health, uint damage) => (Icon, _health, _maxHealth, Damage) = (icon, new Health(health), health, damage);

        public char Icon { get; private set; }
        public uint Damage { get; private set; }

        public virtual void Attack(IDamageable opponent) => opponent.TakeDamage(Damage);

        public virtual void TakeDamage(uint opponentDamage)
        {
            _health.TakeDamage(opponentDamage);
            Console.WriteLine($"{Icon} [{GetHealth()}/{_maxHealth}] HP был атакован и потерял {opponentDamage} здоровья");
        }

        public bool IsDead() => _health.IsDead();

        public uint GetHealth() => _health.Value;

        public string GetName() => GetType().Name;

        public abstract Gladiator Clone();
    }

    class Thracian : Gladiator
    {
        private int _attackCount;

        public Thracian() : base('T', 100, 25) { }

        public override void Attack(IDamageable opponent)
        {
            uint powerDamage = Damage;

            _attackCount++;

            if (TryUseSpecialAbilities())
            {
                UseSpecialAbilities(out powerDamage);
                Console.WriteLine("Использован <<Трикратный удар>> (три атаки подряд, увеличивающие урон на следующей атаке в два раза)");
            }

            opponent.TakeDamage(powerDamage);
        }

        public override Gladiator Clone() => new Thracian();

        protected bool TryUseSpecialAbilities()
        {
            uint requiredAttackCount = 3;

            return requiredAttackCount == _attackCount;
        }

        protected void UseSpecialAbilities(out uint powerDamage)
        {
            uint powerMultiplier = 2;

            powerDamage = Damage * powerMultiplier;
        }
    }

    class Murmillo : Gladiator
    {
        private uint _armor;

        public Murmillo() : base('M', 100, 25) => _armor = 100;

        public override void TakeDamage(uint opponentDamage)
        {
            if (TryUseSpecialAbilities())
            {
                opponentDamage = UseSpecialAbilities(opponentDamage);
                Console.WriteLine("Использован <<Стальной щит>> (блокировка урона и уменьшение получаемой защиты)");
            }

            _health.TakeDamage(opponentDamage);
            Console.WriteLine($"{Icon} [{GetHealth()}/{_maxHealth}] HP был атакован и потерял {Damage} здоровья");
        }

        public override Gladiator Clone() => new Murmillo();

        protected bool TryUseSpecialAbilities() => _armor > 0;

        protected uint UseSpecialAbilities(uint opponentDamage)
        {
            double damageReductionFactor = 1.25;
            double armorDamageReductionFactor = 0.1;
            double armorReductionPercentage = 100.0;

            uint actualDamage = (uint)(opponentDamage * (damageReductionFactor - _armor / armorReductionPercentage));

            _armor -= (uint)(actualDamage * armorDamageReductionFactor);

            return actualDamage;
        }
    }

    class Retiarius : Gladiator
    {
        private int _dodgeChance;

        public Retiarius() : base('R', 100, 25) => _dodgeChance = 0;

        public override void TakeDamage(uint opponentDamage)
        {
            if (TryUseSpecialAbilities())
            {
                Console.WriteLine("Использована <<Плеть быстрее молнии>> (уклонение от атак и увеличение шанса уклонения после каждой атаки)");
                return;
            }

            _health.TakeDamage(opponentDamage);
            Console.WriteLine($"{Icon} [{GetHealth()}/{_maxHealth}] HP был атакован и потерял {Damage} здоровья");
        }

        public override Gladiator Clone() => new Retiarius();

        private bool TryUseSpecialAbilities() => new Random().Next(dodgeChanceVariants) == _dodgeChance; int dodgeChanceVariants = 4;
    }

    class Secutor : Gladiator
    {
        private uint _healAmount;

        public Secutor() : base('S', 100, 25) => _healAmount = 25;

        public override void Attack(IDamageable opponent)
        {
            uint powerDamage = Damage;

            if (TryUseSpecialAbilities())
            {
                UseSpecialAbilities();
                Console.WriteLine("Использован <<Целительный клинок>> (восстановление здоровья после каждой пятой успешной атаки)");
            }

            opponent.TakeDamage(powerDamage);
        }

        public override Gladiator Clone() => new Secutor();

        protected bool TryUseSpecialAbilities()
        {
            int requiredAttackCount = 5;
            uint attackCount = 4;

            return new Random().Next(requiredAttackCount) == attackCount;
        }

        protected void UseSpecialAbilities() => _health.TakeHeal(_healAmount);
    }

    class Velitus : Gladiator
    {
        private double _criticalStrikeChance;
        private uint _criticalStrikeMultiplier;

        public Velitus() : base('V', 100, 25)
        {
            _criticalStrikeChance = 15;
            _criticalStrikeMultiplier = 3;
        }

        public override void Attack(IDamageable opponent)
        {
            uint powerDamage = Damage;

            if (TryUseSpecialAbilities())
            {
                powerDamage = UseSpecialAbilities(powerDamage);
                Console.WriteLine("Использован <<Решающий удар>> (шанс нанести в три раза больший урон при критическом ударе)");
            }

            opponent.TakeDamage(powerDamage);
        }

        public override Gladiator Clone() => new Velitus();

        protected bool TryUseSpecialAbilities() => new Random().NextDouble() < _criticalStrikeChance;

        protected uint UseSpecialAbilities(uint damage) => damage * _criticalStrikeMultiplier;
    }

    public delegate void GladiatorInfoDelegate(char icon, uint health, uint damage);

    class Colosseum
    {
        private List<Gladiator> _gladiatorClasses;

        public Colosseum() => _gladiatorClasses = new List<Gladiator> { new Thracian(), new Murmillo(), new Retiarius(), new Secutor(), new Velitus() };

        public void StartMenu()
        {
            int idOffset = 1;
            bool isExitSelected = false;

            while (isExitSelected == false)
            {
                Console.Clear();

                Console.WriteLine("Выберите двух гладиаторов для битвы:");

                for (int i = 0; i < _gladiatorClasses.Count; i++)
                {
                    int id = i + idOffset;

                    Console.WriteLine($"{id}. {_gladiatorClasses[i].GetName()}");
                }

                int exitId = _gladiatorClasses.Count + idOffset;
                Console.WriteLine($"{exitId}. Выход\n");

                int choice1 = GetChoice("Выберите первого гладиатора: ");
                int choice2 = 0;

                if (choice1 != exitId)
                {
                    choice2 = GetChoice("Выберите второго гладиатора: ");
                }

                if (choice1 == exitId || choice2 == exitId)
                {
                    isExitSelected = true;
                }
                else
                {
                    Gladiator gladiator1 = _gladiatorClasses[choice1 - idOffset].Clone();
                    Gladiator gladiator2 = _gladiatorClasses[choice2 - idOffset].Clone();

                    Console.WriteLine($"\nБитва между {gladiator1.GetName()} и {gladiator2.GetName()} начинается!");

                    StartBattle(gladiator1, gladiator2);

                    Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню.");
                    Console.ReadKey();
                }
            }
        }

        private int GetChoice(string message)
        {
            int choice = 0;
            int gladiatorNumber = 1;

            while (choice < gladiatorNumber || choice > _gladiatorClasses.Count + gladiatorNumber)
            {
                Console.Write(message);
                int.TryParse(Console.ReadLine(), out choice);
            }

            return choice;
        }

        private void StartBattle(Gladiator gladiator1, Gladiator gladiator2)
        {
            while (gladiator1.IsDead() == false && gladiator2.IsDead() == false)
            {
                if (gladiator1.IsDead() == false)
                {
                    gladiator1.Attack(gladiator2);
                }

                if (gladiator2.IsDead() == false)
                {
                    gladiator2.Attack(gladiator1);
                }
            }

            if (gladiator1.IsDead())
            {
                Console.WriteLine($"{gladiator2.GetName()} победил!");
            }
            else
            {
                Console.WriteLine($"{gladiator1.GetName()} победил!");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Colosseum colosseum = new Colosseum();
            colosseum.StartMenu();
        }
    }
}