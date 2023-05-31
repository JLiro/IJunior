using System;
using System.Collections.Generic;

namespace Task11Aquarium
{
    public abstract class Fish
    {
        protected Fish(string name, int age)
        {
            Name = name;
            Age = age;
            IsDead = false;
        }

        public string Name { get; private set; }
        public int Age { get; private set; }
        public bool IsDead { get; private set; }

        public void EncreaseAge() => Age++;

        public void Die() => IsDead = true;
    }

    public class Nemo : Fish
    {
        public Nemo(string name, int age) : base(name, age) { }
    }

    public class Dory : Fish
    {
        public Dory(string name, int age) : base(name, age) { }
    }

    public class Marlin : Fish
    {
        public Marlin(string name, int age) : base(name, age) { }
    }

    public class Aquarium
    {
        private readonly List<Fish> _fishList;
        private readonly int _maxFishCount;
        private readonly int _maxFishAge = 10;
        private readonly int _addFishDelayMs = 2000;

        public Aquarium(int maxFishCount)
        {
            _maxFishCount = maxFishCount;
            _fishList = new List<Fish>();
        }

        public void Work()
        {
            while (true)
            {
                Console.Clear();

                PrintAquariumState();
                UpdateFishAges();

                var newFish = CreateRandomFish();

                AddFish(newFish);

                System.Threading.Thread.Sleep(_addFishDelayMs);
            }
        }

        private void AddFish(Fish fish)
        {
            if (_fishList.Count < _maxFishCount)
            {
                _fishList.Add(fish);
            }
            else
            {
                Console.WriteLine("Аквариум полон, больше нельзя добавлять рыб.");
            }
        }

        private void RemoveFish(Fish fish)
        {
            if (_fishList.Contains(fish))
            {
                _fishList.Remove(fish);
            }
            else
            {
                Console.WriteLine("Рыба не найдена в аквариуме.");
            }
        }

        private void UpdateFishAges()
        {
            for (int i = _fishList.Count - 1; i >= 0; i--)
            {
                var fish = _fishList[i];

                if (fish.IsDead) continue;

                fish.EncreaseAge();

                if (fish.Age < _maxFishAge) continue;

                fish.Die();
                RemoveFish(fish);

                Console.WriteLine($"Рыба {fish.Name} умерла от старости и была удалена из аквариума.");
            }
        }

        private void PrintAquariumState()
        {
            Console.WriteLine($"Аквариум содержит {_fishList.Count} рыб:");

            foreach (var fish in _fishList)
            {
                var status = fish.IsDead ? "мертва" : $"{fish.Age} лет";
                Console.WriteLine($"Рыба {fish.Name} {status}");
            }
        }

        private Fish CreateRandomFish()
        {
            const string TypeNemo = "Nemo";
            const string TypeDory = "Dory";
            const string TypeMarlin = "Marlin";

            const int StartAge = 0;

            List<string> fishTypes = new List<string> { TypeNemo, TypeDory, TypeMarlin };

            Random random = new Random();

            string fishType = fishTypes[random.Next(fishTypes.Count)];

            string name = String.Empty;

            Fish newFish = null;

            switch (fishType)
            {
                case TypeNemo:
                    name = "Немо";
                    newFish = new Nemo(name, StartAge);
                    break;
                case TypeDory:
                    name = "Дори";
                    newFish = new Dory(name, StartAge);
                    break;
                case TypeMarlin:
                    name = "Марлин";
                    newFish = new Marlin(name, StartAge);
                    break;
            }

            return newFish;
        }
    }

    public class Program
    {
        public static void Main()
        {
            const int maxFishCount = 5;
            var aquarium = new Aquarium(maxFishCount);
            aquarium.Work();
        }
    }
}