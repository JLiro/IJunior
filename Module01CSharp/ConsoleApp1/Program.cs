using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Arena arena = new Arena();

            arena.Fight();
        }
    }

    class Warrior
    {
        public string Name { get; protected set; }
        public int Health { get; protected set; }
        public int Armor { get; protected set; }
        public int Damage { get; protected set; }

        public virtual void TakeDamage(int damage)
        {
            if (Armor > damage)
            {
                damage = Armor;
            }

            Health -= damage - Armor;

            if (Health < 0)
            {
                Health = 0;
            }
        }

        public virtual void Attack(Warrior warrior)
        {
            warrior.TakeDamage(Damage);
        }

        public void ShowStatistic()
        {
            ChangeTextColor(ConsoleColor.Yellow);
            Console.WriteLine($"Имя: {Name}\nЖизни: {Health}\nБроня: {Armor}\nУрон: {Damage}\n");
            ChangeTextColor(ConsoleColor.White);
        }

        public virtual void ShowDescription() { }

        protected void ChangeTextColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
    }

    class Arena
    {
        private Warrior _firstWarrior;
        private Warrior _secondWarrior;

        public void Fight()
        {
            ConsoleKey desiredKey = ConsoleKey.Enter;

            _firstWarrior = ChooseFighter();
            _secondWarrior = ChooseFighter();

            Console.WriteLine($"\nНажмите {desiredKey}, чтобы начать бой!");
            PressEnter(desiredKey);

            while (_firstWarrior.Health > 0 && _secondWarrior.Health > 0)
            {
                Console.Clear();

                _firstWarrior.Attack(_secondWarrior);
                _secondWarrior.Attack(_firstWarrior);

                _firstWarrior.ShowStatistic();
                _secondWarrior.ShowStatistic();

                Console.WriteLine($"Для продолжения нажмите {desiredKey}.");
                PressEnter(desiredKey);
            }

            FightResult();
        }

        private Warrior ChooseFighter()
        {
            List<Warrior> warriors = new List<Warrior>
            {
                new Viking(),
                new Wizard(),
                new Knight(),
                new Barbarian(),
                new Druid()
            };

            Warrior warrior = null;

            string userInput;

            ShowFighters(warriors);

            while (warrior == null)
            {
                if (_firstWarrior == null)
                {
                    Console.Write($"Выберите первого бойца: ");
                    userInput = Console.ReadLine();
                }
                else
                {
                    Console.Write($"Выберите второго бойца: ");
                    userInput = Console.ReadLine();
                }

                bool isNumber = int.TryParse(userInput, out int numberOfWarrior);

                if (isNumber && numberOfWarrior <= warriors.Count && numberOfWarrior > 0)
                {
                    warrior = warriors[numberOfWarrior - 1];
                }
                else
                {
                    Console.WriteLine("Некорректный ввод, повторите выбор.");
                }
            }

            return warrior;
        }

        private void FightResult()
        {
            if (_firstWarrior.Health > _secondWarrior.Health)
            {
                Console.WriteLine($"Победил первый боец: {_firstWarrior.Name}!");
            }
            else if (_secondWarrior.Health > _firstWarrior.Health)
            {
                Console.WriteLine($"Победил второй боец: {_secondWarrior.Name}!");
            }
            else
            {
                Console.WriteLine("Ничья!");
            }
        }

        private void ShowFighters(List<Warrior> warriors)
        {
            for (int i = 0; i < warriors.Count; i++)
            {
                Console.Write($"{i + 1} - ");
                warriors[i].ShowDescription();
                warriors[i].ShowStatistic();
            }
        }

        private void PressEnter(ConsoleKey desiredKey)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            while (key.Key != desiredKey)
            {
                Console.WriteLine($"\nДля продолжения нажмите {desiredKey}.");
                key = Console.ReadKey();
            }

            Console.WriteLine();
        }
    }

    class Viking : Warrior
    {
        private int _multiplicityOfChances = 3;
        private int _attackCount = 1;

        public Viking()
        {
            Name = "Lesha";
            Health = 650;
            Armor = 15;
            Damage = 80;
        }

        public override void Attack(Warrior warrior)
        {
            if (_attackCount % _multiplicityOfChances == 0)
            {
                warrior.TakeDamage(Damage);
                warrior.TakeDamage(Damage);

                ChangeTextColor(ConsoleColor.Red);
                Console.WriteLine($"{Name}: Нанёс двойной урон.");
                ChangeTextColor(ConsoleColor.White);
            }
            else
            {
                base.Attack(warrior);
            }

            _attackCount++;
        }

        public override void ShowDescription()
        {
            Console.WriteLine($"Викинг каждый {_multiplicityOfChances}ий. удар наносит удвоенный урон.");
        }
    }

    class Wizard : Warrior
    {
        private int _multiplicityOfChances = 3;
        private int _absorbedDamage = 15;
        private int _poisonDamage = 40;
        private int _attackCount = 1;

        public Wizard()
        {
            Name = "Pavel";
            Health = 500;
            Armor = 10;
            Damage = 60;
        }

        public override void TakeDamage(int damage)
        {
            damage -= _absorbedDamage;

            ChangeTextColor(ConsoleColor.DarkGray);
            Console.WriteLine($"{Name}: Погасил {_absorbedDamage} ед. урона.");
            ChangeTextColor(ConsoleColor.White);

            base.TakeDamage(damage);
        }

        public override void Attack(Warrior warrior)
        {
            if (_attackCount % _multiplicityOfChances == 0)
            {
                warrior.TakeDamage(Damage + _poisonDamage);

                ChangeTextColor(ConsoleColor.Red);
                Console.WriteLine($"{Name}: Яд +{_poisonDamage} ед. урона.");
                ChangeTextColor(ConsoleColor.White);
            }
            else
            {
                base.Attack(warrior);
            }

            _attackCount++;
        }

        public override void ShowDescription()
        {
            Console.WriteLine($"Волшебник каждый ход поглощает {_absorbedDamage} ед. урона врага," +
                $"каждый {_multiplicityOfChances}ой. удар наносит урон ядом +{_poisonDamage} ед.");
        }
    }

    class Knight : Warrior
    {
        private int _multiplicityOfChances = 3;
        private int _takeDamageCount = 1;

        public Knight()
        {
            Name = "Vlad";
            Health = 600;
            Armor = 20;
            Damage = 80;
        }

        public override void TakeDamage(int damage)
        {
            if (_takeDamageCount % _multiplicityOfChances == 0)
            {
                ChangeTextColor(ConsoleColor.Green);
                Console.WriteLine($"{Name}: Уклонение.");
                ChangeTextColor(ConsoleColor.White);
            }
            else
            {
                base.TakeDamage(damage);
            }

            _takeDamageCount++;
        }

        public override void ShowDescription()
        {
            Console.WriteLine($"Рыцарь каждый {_multiplicityOfChances}ий ход прикрывается щитом уклоняясь от атаки.");
        }
    }

    class Barbarian : Warrior
    {
        private static Random _random = new Random();
        private int _multiplicityOfChances = 3;
        private int _extraDamage = 80;
        private int _attackCount = 1;

        public Barbarian()
        {
            Name = "Regina";
            Health = 700;
            Armor = 5;
            Damage = 90;
        }

        public override void Attack(Warrior warrior)
        {
            int minimumRandomNumber = 0;
            int maximunRandomNumber = 2;
            int probabilityNumber = 1;

            if (_attackCount % _multiplicityOfChances == 0)
            {
                if (_random.Next(minimumRandomNumber, maximunRandomNumber) == probabilityNumber)
                {
                    TakeDamage(Damage);

                    ChangeTextColor(ConsoleColor.DarkRed);
                    Console.WriteLine($"{Name}: Нанёс урон себе -{Damage}.");
                    ChangeTextColor(ConsoleColor.White);
                }
                else
                {
                    warrior.TakeDamage(Damage + _extraDamage);

                    ChangeTextColor(ConsoleColor.Red);
                    Console.WriteLine($"{Name}: Урон +{_extraDamage}.");
                    ChangeTextColor(ConsoleColor.White);
                }
            }
            else
            {
                base.Attack(warrior);
            }

            _attackCount++;
        }

        public override void ShowDescription()
        {
            Console.WriteLine($"Варвар каждый {_multiplicityOfChances}ий ход наносить размашистый удар" +
                $" +{_extraDamage} ед. урона(есть вероятно нанести урон себе).");
        }
    }

    class Druid : Warrior
    {
        private int _multiplicityOfChances = 3;
        private int _attackCount = 1;

        public Druid()
        {
            Name = "Kirill";
            Health = 580;
            Armor = 10;
            Damage = 60;
        }

        public override void Attack(Warrior warrior)
        {
            int rage = 0;

            rage += warrior.Damage;

            if (_attackCount % _multiplicityOfChances == 0)
            {
                warrior.TakeDamage(Damage + rage);

                ChangeTextColor(ConsoleColor.Red);
                Console.WriteLine($"{Name}: Урон +{rage}.");
                ChangeTextColor(ConsoleColor.White);
            }
            else
            {
                base.Attack(warrior);
            }

            _attackCount++;
        }

        public override void ShowDescription()
        {
            Console.WriteLine($"Друид накапливает ярость, высвобождая её каждый {_multiplicityOfChances}ий. удар.");
        }
    }
}