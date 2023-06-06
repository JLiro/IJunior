using System;
using System.Collections.Generic;

namespace Task12Zoo
{
    public abstract class Animal
    {
        public string Gender { get; }
        public string Name { get; }

        public abstract string Sound { get; }

        protected Animal(string gender, string name)
        {
            Gender = gender;
            Name = name;
        }
    }

    public interface IReadOnlyEnclosure
    {
        string Name { get; }
        IReadOnlyList<Animal> Animals { get; }
        void SetAnimals(IReadOnlyList<Animal> animals);
    }

    public class Lion : Animal
    {
        public Lion(string gender, string name) : base(gender, name) { }

        public override string Sound => "рычит";
    }

    public class Tiger : Animal
    {
        public Tiger(string gender, string name) : base(gender, name) { }

        public override string Sound => "рычит";
    }

    public class Zebra : Animal
    {
        public Zebra(string gender, string name) : base(gender, name) { }

        public override string Sound => "издает звуковые сигналы";
    }

    public class Giraffe : Animal
    {
        public Giraffe(string gender, string name) : base(gender, name) { }

        public override string Sound => "издает звуковые сигналы";
    }

    public class Enclosure : IReadOnlyEnclosure
    {
        public string Name { get; }
        public IReadOnlyList<Animal> Animals { get; private set; }

        public Enclosure(string name)
        {
            Name = name;
            Animals = new List<Animal>();
        }

        public void SetAnimals(IReadOnlyList<Animal> animals)
        {
            ((List<Animal>)Animals).Clear();
            ((List<Animal>)Animals).AddRange(animals);
        }
    }

    public class Zoo
    {
        private List<IReadOnlyEnclosure> _enclosures;

        public Zoo()
        {
            _enclosures = new List<IReadOnlyEnclosure>
            {
                new Enclosure("Вольер 1"),
                new Enclosure("Вольер 2"),
                new Enclosure("Вольер 3"),
                new Enclosure("Вольер 4")
            };

            _enclosures[0].SetAnimals(new List<Animal> { new Lion("Мужской", "Лев 1"), new Lion("Женский", "Лев 2") });
            _enclosures[1].SetAnimals(new List<Animal> { new Tiger("Мужской", "Тигр 1"), new Tiger("Женский", "Тигр 2") });
            _enclosures[2].SetAnimals(new List<Animal> { new Zebra("Мужской", "Зебра 1"), new Zebra("Женский", "Зебра 2") });
            _enclosures[3].SetAnimals(new List<Animal> { new Giraffe("Мужской", "Жираф 1"), new Giraffe("Женский", "Жираф 2") });
        }

        public IReadOnlyList<IReadOnlyEnclosure> Enclosures => _enclosures;
    }

    public class MainMenu
    {
        public void SelectEnclosure()
        {
            string exitCommand = "E";

            Zoo zoo = new Zoo();

            bool isWork = true;

            while (isWork)
            {
                Console.Clear();

                Console.WriteLine("Выберите вольер:");

                ShowEnclosures(zoo.Enclosures);

                Console.Write($"\nДля выхода введите {exitCommand} на англ. раскладке\n> ");

                string input = Console.ReadLine();

                if (input.ToUpper() == exitCommand)
                {
                    isWork = false;
                }

                ApproachEnclosure(input, zoo.Enclosures);
            }
        }

        private void ShowEnclosures(IReadOnlyList<IReadOnlyEnclosure> enclosures)
        {
            for (int i = 0; i < enclosures.Count; i++)
            {
                int id = i + 1;

                Console.WriteLine($"{id}. {enclosures[i].Name}");
            }
        }

        private void ApproachEnclosure(string input, IReadOnlyList<IReadOnlyEnclosure> enclosures)
        {
            if (int.TryParse(input, out int enclosureId) && enclosureId > 0 && enclosureId <= enclosures.Count)
            {
                int index = enclosureId - 1;

                IReadOnlyEnclosure enclosure = enclosures[index];

                Console.WriteLine($"\nВы выбрали {enclosure.Name}. В вольере обитает {enclosure.Animals.Count} животных:");

                foreach (Animal animal in enclosure.Animals)
                {
                    Console.WriteLine($"Имя: {animal.Name} ({animal.Gender}). Звук: {animal.Sound}");
                }

                Console.WriteLine("\nДля возврата к выбору вольеров нажмите любую клавишу");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Неправильный ввод. Поправьте еще раз.");
            }
        }
    }

    public class Program
    {
        public static void Main()
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.SelectEnclosure();
        }
    }
}