using System;
using System.Collections.Generic;
using System.Threading;

namespace Task11Aquarium
{
    public abstract class Fish
    {
        private bool _isKilled;
        private int  _age;

        protected Fish(string name, int age, int maxAge)
        {
            Name = name;
            _age = age;
            MaxAge = maxAge;
            _isKilled = false;
        }

        public string Name { get; private set; }
        public int MaxAge { get; private set; }
        public bool IsAlive => _age < MaxAge && _isKilled == false;

        public string GetInfoDead()
        {
            if (_isKilled)
            {
                return $"Рыба {Name} была убита в возрасте {_age} года";
            }
            else if (_age == MaxAge)
            {
                return $"Рыба {Name} умерла от старости в {_age} года";
            }
            else
            {
                return $"Рыба {Name} живёт {_age} года из {MaxAge}";
            }
        }

        
        public void EnlargeAge()
        {
            if (IsAlive)
            {
                _age++;
            }
        }

        public void Kill() => _isKilled = true;

        public abstract Fish Clone();
    }

    public class Nemo : Fish
    {
        public Nemo(string name, int age, int maxAge) : base(name, age, maxAge) { }

        public override Fish Clone() => new Nemo(Name, 0, MaxAge);
    }

    public class Dory : Fish
    {
        public Dory(string name, int age, int maxAge) : base(name, age, maxAge) { }

        public override Fish Clone() => new Dory(Name, 0, MaxAge);
    }

    public class Marlin : Fish
    {
        public Marlin(string name, int age, int maxAge) : base(name, age, maxAge) { }

        public override Fish Clone() => new Marlin(Name, 0, MaxAge);
    }

    public class Aquarium
    {
        private static Aquarium instance;

        private readonly List<Fish> _fishes;
        private readonly int _maxFishCount;

        private Aquarium(int maxFishCount)
        {
            _maxFishCount = maxFishCount;
            _fishes = new List<Fish>();
        }

        public static Aquarium GetInstance(int fishCount)
        {
            if (instance == null)
            {
                instance = new Aquarium(fishCount);
            }

            return instance;
        }

        public void Work()
        {
            const int StartAge = 0;

            Console.CursorVisible = false;

            List<Fish> fishTypes = new List<Fish>
            { 
                new Nemo("Немо", StartAge, 2),
                new Dory("Дори", StartAge, 3),
                new Marlin("Марлин", StartAge, 4)
            };
            
            int delayMs = 4000;

            bool isWork = true;
            
            while (isWork)
            {
                Console.Clear();

                UpdateFishAges();
                PrintFishInfo();
                RemoveDeadFish();
                AddFish(fishTypes);
                CheckUserMenuChoices(isWork);

                Thread.Sleep(delayMs);
            }
        }

        private void CheckUserMenuChoices(bool isWork)
        {
            const char CommandExit = 'E';
            const char CommandKillFish = 'K';

            Console.WriteLine($"\nНажмите (на англ. раскладке) [{CommandExit}] для выхода, или [{CommandKillFish}] для убийтсва рыбы");

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                char command = char.ToUpper(keyInfo.KeyChar);

                if (command == CommandExit)
                {
                    isWork = false;
                }

                if (command == CommandKillFish)
                {
                    KillFish();
                }
            }
        }

        private void KillFish()
        {
            Console.Write("Введите номер рыбы для убийства: ");

            string input = Console.ReadLine();

            if (Int32.TryParse(input, out int fishIndex) && fishIndex <= _fishes.Count && fishIndex > 0)
            {
                fishIndex--;

                _fishes[fishIndex].Kill();
            }
            else
            {
                Console.WriteLine("Введен некорректный номер");
            }
        }

        private void AddFish(List<Fish> fishTypes)
        {
            if (_fishes.Count < _maxFishCount)
            {
                var newFish = CreateRandomFish(fishTypes);
         
                _fishes.Add(newFish);
            }
        }

        private void UpdateFishAges()
        {
            for (int i = _fishes.Count - 1; i >= 0; i--)
            {
                _fishes[i].EnlargeAge();
            }
        }

        private void RemoveDeadFish()
        {
            Console.WriteLine("\nОчищение аквариума:");

            for (int i = _fishes.Count - 1; i >= 0; i--)
            {
                Fish fish = _fishes[i];

                if (fish.IsAlive == false)
                {        
                    _fishes.RemoveAt(i);

                    Console.WriteLine($"Рыба {fish.Name} была удалена из аквариума.");
                }
            }
        }

        private void PrintFishInfo()
        {
            Console.WriteLine($"Аквариум содержит {_fishes.Count}/{_maxFishCount} рыб:");

            for (int i = 0; i < _fishes.Count; i++)
            {
                int id = i + 1; 
                
                Console.WriteLine($"[{id}] {_fishes[i].GetInfoDead()}");
            }
        }

        private Fish CreateRandomFish(List<Fish> fishTypes)
        {
            int id = new Random().Next(fishTypes.Count);

            return fishTypes[id].Clone();
        }
    }

    public class Program
    {
        public static void Main()
        {
            const int maxFishCount = 8;
            Aquarium aquarium = Aquarium.GetInstance(maxFishCount);
            aquarium.Work();
        }
    }
}