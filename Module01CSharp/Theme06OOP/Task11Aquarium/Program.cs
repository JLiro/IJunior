using System;
using System.Collections.Generic;
using System.Threading;

namespace Task11Aquarium
{
    public abstract class Fish
    {
        private bool IsKilled;

        protected Fish(string name, int age, int maxAge)
        {
            Name = name;
            Age = age;
            MaxAge = maxAge;
            IsKilled = false;
        }

        public string Name { get; private set; }
        public int Age { get; private set; }
        public int MaxAge { get; private set; }

        public string GetInfoDead()
        {
            if (IsKilled)
            {
                return $"Рыба {Name} была убита в возрасте {Age} года";
            }
            else if (Age == MaxAge)
            {
                return $"Рыба {Name} умерла от старости в {Age} года";
            }
            else
            {
                return $"Рыба {Name} жива {Age} года";
            }
        }

        public bool IsAlive { get =>  Age < MaxAge && IsKilled == false; private set { } }
        
        public void EnlargeAge()
        {
            if (IsAlive)
            {
                Age++;
            }
        }

        public void Kill() => IsKilled = true;

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

            List<Fish> fishTypes = new List<Fish>
            { 
                new Nemo("Немо", StartAge, 2),
                new Dory("Дори", StartAge, 3),
                new Marlin("Марлин", StartAge, 4)
            };
            
            int delayMs = 2000;

            bool isWork = true;
            
            while (isWork)
            {
                Console.Clear();

                PrintFishInfo();
                UpdateFishAges();
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

            Console.WriteLine($"\nНажмите [{CommandExit}] для выхода, или [{CommandKillFish}] для убийтсва рыбы");

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
            Console.WriteLine("\nНекролог:");
            
            for (int i = _fishes.Count - 1; i >= 0; i--)
            {
                _fishes[i].EnlargeAge();
            }
        }

        private void RemoveDeadFish()
        {
            for (int i = _fishes.Count - 1; i >= 0; i--)
            {
                Fish fish = _fishes[i];

                if (fish.IsAlive == false)
                {        
                    _fishes.RemoveAt(i);

                    Console.WriteLine($"{fish.GetInfoDead()} и была удалена из аквариума.");

                    int showInfoDelayMs = 2500;
                    Thread.Sleep(showInfoDelayMs);
                }
            }

            
        }

        private void PrintFishInfo()
        {
            Console.WriteLine($"Аквариум содержит {_fishes.Count}/{_maxFishCount} рыб:");

            for (int i = 0; i < _fishes.Count; i++)
            {
                int id = i + 1; 
                
                Console.WriteLine($"[{id}] Рыба {_fishes[i].Name} {_fishes[i].Age} года из {_fishes[i].MaxAge}");
            }
        }

        private Fish CreateRandomFish(List<Fish> fishTypes)
        {
            Random random = new Random();

            return fishTypes[random.Next(fishTypes.Count)].Clone();
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