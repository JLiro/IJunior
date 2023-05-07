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
                
                Console.Write($"\nНажмите [{CommandExit}] (на англ. раскладке) для выхода, или любую другую для выбора бойцов");

                if (Console.ReadKey().Key == CommandExit)
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

        private string _log;
        private int _eventCount;

        private void AddInfoToLog(string text)
        {
            _log += text + "\n";
        }

        public Colosseum()
        {
            _gladiatorСlasses.Add(new Bestiary("Бестиарий"));
            _gladiatorСlasses.Add(new Gallus("Галл"));
        }

        public void ShowGladiators()
        {
            for (int i = 0; i < _gladiatorСlasses.Count; i++)
            {
                int logIndex = i++;

                Console.WriteLine($"[{logIndex}]{_gladiatorСlasses[i].GetInfo()}");
            }
        }

        public void StartEvent()
        {
            Console.Clear();

            ShowGladiators();

            SelectGladiator(out int firstID , "первого");
            SelectGladiator(out int secondID, "второго");

            firstID--;
            secondID--;

            Gladiator first  = _gladiatorСlasses[firstID ].Clone();
            Gladiator second = _gladiatorСlasses[secondID].Clone();

            ShowStartFightInfo(first, second);

            Console.Clear();

            string currentLog = string.Empty;

            (int, int) logInfoPositionShow = (0, 10);
            int eventCount = 1;
            (int, int) headingInfoPosition = (0, 0);

            while (first.IsAlive && second.IsAlive)
            {
                ShowText("ГЛАДИАТОРЫ УЧАСТВУЮЩИЕ В БИТВЕ", headingInfoPosition.Item1, headingInfoPosition.Item2);

                currentLog = first.Attack(second, eventCount);
                AddInfoToLog(currentLog);

                _eventCount += ShowAttackInfo(first, second, _log, logInfoPositionShow);

                if (second.Health > 0)
                {
                    currentLog = second.Attack(first, eventCount);
                    AddInfoToLog(currentLog);

                    _eventCount += ShowAttackInfo(first, second, _log, logInfoPositionShow);
                }
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

        private void SelectGladiator(out int id, string description)
        {
            bool IsCorrect;

            do
            {
                Console.CursorVisible = true;

                Console.Write($"Введите номер {description} гладиатора: ");
                int.TryParse(Console.ReadLine(), out id);

                Console.CursorVisible = false;

                IsCorrect = id > 0 && id <= _gladiatorСlasses.Count;

                if (IsCorrect == false)
                {
                    Console.Write
                        (
                            "\nВведены некорректные данные. Бойцы не выбраны" +
                            "\nНажмие любую кливишу и попробуйте снова" +
                            "\n" +
                            "\n"
                        );
                    Console.ReadKey();
                }
            } while (IsCorrect == false);

            Console.WriteLine("Вышел из цикла");
        }

        private int ShowAttackInfo(Gladiator first, Gladiator second, string log, (int, int) logInfoPositionShow)
        {
            ShowText(log, logInfoPositionShow.Item1, logInfoPositionShow.Item2);
            ShowHealthGladiators(first, second);
            return 1;
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
            Console.WriteLine( new string(' ', emptyCharCount) + "\n" + new string(' ', emptyCharCount) );
        }
    }
    abstract class Gladiator
    {      
        public Gladiator(string @class, uint health, uint damage)
        {
            Class  = @class;
            Health = health;
            Damage = damage;
        }

        public string Class { get; }
        public uint Health { get; protected set; }
        public uint Damage { get; protected set; }
        public bool IsAlive => Health > 0;

        private Random _random = new Random();
        
        public void TakeDamage(uint damage)
        {
            Health -= Math.Min(Health, damage);
        }

        public virtual string Attack(Gladiator otherGladiator, int eventCount)
        {
            string log = string.Empty;

            if (CanUse())
            {
                log = UseSkill(otherGladiator, eventCount);
            }
            else
            {
                otherGladiator.TakeDamage(Damage);
                log += $"[{eventCount}] {Class} отнял {Damage} здоровья у {otherGladiator.Class}";
            }
            return log;
        }

        public void ShowHealthInfo((int, int) positionShow)
        {
            const int VerticalCursorPositionEncreaseValue = 1;

            Console.SetCursorPosition(positionShow.Item1, positionShow.Item2);
            Console.Write(Class);

            Console.SetCursorPosition(positionShow.Item1, positionShow.Item2 + VerticalCursorPositionEncreaseValue);
            Console.Write(Health + "hp");
        }

        public string GetInfo()
        {
            return $" Класс: {Class}" +
                   $"\n    Здоровье: {Health}" +
                   $"\n    Урон: {Damage}" +
                   $"\n    Навык: {GetInfoSkill()}" +
                   $"\n";
        }

        public abstract Gladiator Clone();

        protected abstract string GetInfoSkill();

        protected abstract bool CanUse();

        protected abstract string UseSkill(Gladiator otherGladiator, int eventCount);
    }

    class Bestiary : Gladiator
    {
        private int _attackCount;

        private readonly int SkillActivationValue = 3;

        public Bestiary(string name) : base(name, 250, 50) {}

        public override Gladiator Clone()
        {
            return new Bestiary($"{Class}");
        }

        public override string Attack(Gladiator otherGladiator, int eventCount)
        {
            _attackCount++;

            return base.Attack(otherGladiator, eventCount);
        }

        protected override bool CanUse()
        {
            return _attackCount == SkillActivationValue;
        }

        protected override string UseSkill(Gladiator otherGladiator, int eventCount)
        {
            string output = string.Empty;

            const int multiplier = 2;

            Damage *= multiplier;
            otherGladiator.TakeDamage(Damage);

            output += $"[{eventCount}] {Class} использовал навык <<{GetInfoSkill()}>>\n" +
                      $"    Он отнял {Damage} здоровья у {otherGladiator.Class}\n";

            Damage /= multiplier;

            return output;
        }

        protected override string GetInfoSkill()
        {
            return $"Удвоение урона каждую {SkillActivationValue} атаку";
        }
    }

    class Gallus : Gladiator
    {
        private readonly int _skillActivationValue = 0;
        
        private readonly int _minRandomIndex = 0;
        private readonly int _maxRandomIndex = 4;

        private readonly uint HealCount = 25;

        private Random _random = new Random();

        public Gallus(string name) : base(name, 200, 50) { }

        public override Gladiator Clone()
        {
            return new Gallus($"{Class}");
        }

        protected override bool CanUse()
        {
            return _random.Next(_minRandomIndex, _maxRandomIndex) == _skillActivationValue;
        }

        protected override string UseSkill(Gladiator otherGladiator, int eventCount)
        {
            otherGladiator.TakeDamage(Damage);

            Health += HealCount;

            return $"[{eventCount}] {Class} использовал навык <<{GetInfoSkill()}>>\n" +
                      $"    Он отнял {Damage} здоровья у {otherGladiator.Class} и прибавил {HealCount}hp себе";
        }

        protected override string GetInfoSkill()
        {
            int ChanceEvents = 100 / _maxRandomIndex;

            return $"Шанс {ChanceEvents}% восстановить {HealCount}hp при атаке";
        }
    }
}