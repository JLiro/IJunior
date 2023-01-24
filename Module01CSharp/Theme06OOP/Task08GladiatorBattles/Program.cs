using System;
using System.Collections.Generic;

namespace Task08GladiatorBattles
{
    internal class Program
    {
        static void Main()
        {
            const ConsoleKey CommandExit = ConsoleKey.E;

            Colosseum colosseum = new Colosseum();

            Console.CursorVisible = false;

            bool isWork = true;

            while(isWork)
            {
                Console.Clear();

                colosseum.ShowGladiators();
                Console.Write
                    (
                       $"\nНажмите [{CommandExit}] (на англ. раскладке) для выхода, или любую другую для выбора бойцов"
                    );

                if (Console.ReadKey().Key == ConsoleKey.E)
                {
                    isWork = false;
                }
                else
                {
                    colosseum.StartEvent();
                }
            }
        }
    }

    class Colosseum
    {
        private List<Gladiator> _gladiatorСlasses = new List<Gladiator>();
        
        public Colosseum()
        {
            /*
             * 1.1	Бестиарий
            1.2	Велит
            1.3	Гопломах
            1.4	Галл
            1.5	Димахер
            .https://en.wikipedia.org/wiki/List_of_Roman_gladiator_types
             1.1 Bestiary
1.2 Orders
1.3 Hoplomakh
1.4 Gal
1.5 Dimacher


             */

            _gladiatorСlasses.Add(new Bestiary("Бестиарий"));
            _gladiatorСlasses.Add(new Gallus("Галл"));

        }

        private void ChangeGladiators(out int firstID, out int secondID)
        {
            Console.CursorVisible = true;

            Console.Write("Введите номер первого гладиатора: ");
            int.TryParse(Console.ReadLine(), out firstID);

            Console.Write("Введите номер второго гладиатора: ");
            int.TryParse(Console.ReadLine(), out secondID);

            Console.CursorVisible = false;
        }

        public void StartEvent()
        {
            Console.Clear();
            ShowGladiators();
            ChangeGladiators(out int firstID, out int secondID);

            if(firstID <= 0 || secondID <= 0 || firstID > _gladiatorСlasses.Count || secondID > _gladiatorСlasses.Count)
            {
                Console.Write
                    (
                        "\nВведены некорректные данные. Бойцы не выбраны" +
                        "\nНажмие любую кливишу и попробуйте снова"
                    );
                Console.ReadKey();
                return;
            }

            firstID--;
            secondID--;

            Gladiator first  = _gladiatorСlasses[firstID ].Clone();
            Gladiator second = _gladiatorСlasses[secondID].Clone();
            
            ShowStartFightInfo(first, second);

            Console.Clear();

            string log = string.Empty;

            (int, int) logInfoPositionShow = (0, 10);
            int eventCount = 1;
            (int, int) headingInfoPosition = (0, 0);


            while (first.Health > 0 || second.Health > 0)
            {
                ShowText("ГЛАДИАТОРЫ УЧАСТВУЮЩИЕ В БИТВЕ", headingInfoPosition.Item1, headingInfoPosition.Item2);

                first.Attack(second, ref log, eventCount);
                ShowAttackInfo(first, second, log, logInfoPositionShow, ref eventCount);

                if (second.Health <= 0)
                {
                    break;
                }

                second.Attack(first, ref log, eventCount);
                ShowAttackInfo(first, second, log, logInfoPositionShow, ref eventCount);
            }

            string winner = first.Health > second.Health ? first.Class : second.Class;

            int horizontalPosition = 4;
            int verticalPosition = 6;

            int additionVerticalPosition = 1;

            ShowText("Победил: " + winner, horizontalPosition, verticalPosition);
            ShowText("Нажмите любую клавишу чтобы вернуться в меню", horizontalPosition, verticalPosition + additionVerticalPosition);

            Console.ReadKey();
            Console.Clear();
        }

        private int ShowAttackInfo(Gladiator first, Gladiator second, string log, (int, int) logInfoPositionShow, ref int eventCount)
        {
            eventCount++;
            ShowText(log, logInfoPositionShow.Item1, logInfoPositionShow.Item2);
            ShowHealthGladiators(first, second);
            return eventCount;
        }

        private void ShowStartFightInfo(Gladiator first, Gladiator second)
        {
            (int, int) headingInfoPosition = (0, 0);
            (int, int) startFightInfoPosition = (0, 6);

            Console.Clear();

            ShowText("ГЛАДИАТОРЫ УЧАСТВУЮЩИЕ В БИТВЕ", headingInfoPosition.Item1, headingInfoPosition.Item2);
            ShowHealthGladiators(first, second);
            ShowText("Нажмите любую клавишу для начала сражения", startFightInfoPosition.Item1, startFightInfoPosition.Item2);
            Console.ReadKey();
        }

        private static void ShowText(string text, int ShowPositionHorizontal, int ShowPositionVertical)
        {
            Console.SetCursorPosition(ShowPositionHorizontal, ShowPositionVertical);
            Console.Write(text);
        }

        private void ShowHealthGladiators(Gladiator first, Gladiator second)
        {
            (int, int) firstPositionShow  = (4, 2);
            (int, int) secondPositionShow = (20, 2);
            
            ConsoleClear(firstPositionShow);

            first.ShowHealthInfo(firstPositionShow);
            second.ShowHealthInfo(secondPositionShow);

            System.Threading.Thread.Sleep(1000);
        }

        private void ConsoleClear( (int, int) showPosition )
        {
            const int emptyCharCount = 35;

            Console.SetCursorPosition(showPosition.Item1, showPosition.Item2);
            Console.WriteLine
            (
                new string(' ', emptyCharCount) +
                "\n" +
                new string(' ', emptyCharCount)
            );
        }

        public void ShowGladiators()
        {
            for (int i = 0; i < _gladiatorСlasses.Count; i++)
            {
                Console.WriteLine($"[{i + 1}]{_gladiatorСlasses[i].GetInfo()}");
            }
        }
    }

    abstract class Gladiator
    {
        protected int AttackCount;
         
        private Random _random = new Random();

        public Gladiator(string @class, int health, int damage)
        {
            Class  = @class;
            Health = health;
            Damage = damage;
        }

        public string Class { get; }
        public int Health { get; protected set; }
        public int Damage { get; protected set; }

        public void TakeDamage(int damage)
        {
            Health -= Math.Abs(damage);

            if(Health < 0)
            {
                Health = 0;
            }
        }

        public void Attack(Gladiator otherGladiator, ref string log, int eventCount)
        {
            AttackCount++;

            if (TrySkill())
            {
                UseSkill(otherGladiator, ref log, eventCount);
            }
            else
            {
                otherGladiator.TakeDamage(Damage);
                log += $"[{eventCount}] {Class} отнял {Damage} здоровья у {otherGladiator.Class}\n";
            }
        }

        public void ShowHealthInfo((int, int) positionShow)
        {
            const int VerticalCursorPositionEncreaseValue = 1;

            Console.SetCursorPosition(positionShow.Item1, positionShow.Item2);
            Console.Write(Class);

            Console.SetCursorPosition(positionShow.Item1, positionShow.Item2 + VerticalCursorPositionEncreaseValue);
            Console.Write(Health + "hp");
        }

        protected abstract string GetInfoSkill();

        public string GetInfo()
        {
            return $" Класс: {Class}" +
                   $"\n    Здоровье: {Health}" +
                   $"\n    Урон: {Damage}" +
                   $"\n    Навык: {GetInfoSkill()}" +
                   $"\n";
        }

        protected abstract bool TrySkill();

        protected abstract void UseSkill(Gladiator otherGladiator, ref string output, int eventCount);

        public abstract Gladiator Clone();
    }

    class Bestiary : Gladiator
    {
        private const int SkillActivationValue = 3;

        public Bestiary(string name) : base(name, 250, 50) {}

        protected override bool TrySkill()
        {

            return AttackCount == SkillActivationValue;
        }

        protected override void UseSkill(Gladiator otherGladiator, ref string output, int eventCount)
        {
            const int multiplier = 2;

            Damage *= multiplier;
            otherGladiator.TakeDamage(Damage);

            output += $"[{eventCount}] {Class} использовал навык <<{GetInfoSkill()}>>\n" +
                      $"    Он отнял {Damage} здоровья у {otherGladiator.Class}\n";

            Damage /= multiplier;
        }

        protected override string GetInfoSkill()
        {
            return $"Удвоение урона каждую {SkillActivationValue} атаку";
        }

        public override Gladiator Clone()
        {
            return new Bestiary($"{Class}");
        }
    }

    class Gallus : Gladiator
    {
        private const int SkillActivationValue = 0;
        
        private const int _minRandomIndex = 0;
        private const int _maxRandomIndex = 4;

        private const int HealCount = 25;

        private Random _random = new Random();

        public Gallus(string name) : base(name, 200, 50) { }

        protected override bool TrySkill()
        {
            return _random.Next(_minRandomIndex, _maxRandomIndex) == SkillActivationValue;
        }

        protected override void UseSkill(Gladiator otherGladiator, ref string output, int eventCount)
        {
            otherGladiator.TakeDamage(Damage);

            Health += HealCount;

            output += $"[{eventCount}] {Class} использовал навык <<{GetInfoSkill()}>>\n" +
                      $"    Он отнял {Damage} здоровья у {otherGladiator.Class} и прибавил {HealCount}hp себе\n";
        }

        protected override string GetInfoSkill()
        {
            int ChanceEvents = 100 / _maxRandomIndex;

            return $"Шанс {ChanceEvents}% восстановить {HealCount}hp при атаке";
        }

        public override Gladiator Clone()
        {
            return new Gallus($"{Class}");
        }
    }
}
