using System;
using System.Collections.Generic;

namespace Task12Zoo
{
    public interface IAnimal
    {
        string Gender { get; }
        string Name { get; }
        string MakeSound();
    }

    public interface IEnclosure
    {
        string Name { get; }
        IReadOnlyList<IAnimal> Animals { get; }
    }

    public class Lion : IAnimal
    {
        public Lion(string gender, string name)
        {
            Gender = gender;
            Name = name;
        }

        public string Gender { get; private set; }
        public string Name { get; private set; }

        public string MakeSound() => $"рычит";
    }

    public class Tiger : IAnimal
    {
        public Tiger(string gender, string name)
        {
            Gender = gender;
            Name = name;
        }

        public string Gender { get; private set; }
        public string Name { get; private set; }

        public string MakeSound() => $"рычит";
    }

    public class Zebra : IAnimal
    {
        public Zebra(string gender, string name)
        {
            Gender = gender;
            Name = name;
        }

        public string Gender { get; private set; }
        public string Name { get; private set; }

        public string MakeSound() => $"издает звуковые сигналы";
    }

    public class Giraffe : IAnimal
    {
        public Giraffe(string gender, string name)
        {
            Gender = gender;
            Name = name;
        }

        public string Gender { get; private set; }
        public string Name { get; private set; }

        public string MakeSound() => $"издает звуковые сигналы";
    }

    public class LionEnclosure : IEnclosure
    {
        public IReadOnlyList<IAnimal> Animals { get; private set; } = new List<IAnimal> { new Lion("Мужской", "Лев 1"), new Lion("Женский", "Лев 2") };
        public string Name { get; private set; } = "Вольер 1";
    }

    public class TigerEnclosure : IEnclosure
    {
        public IReadOnlyList<IAnimal> Animals { get; private set; } = new List<IAnimal> { new Tiger("Мужской", "Тигр 1"), new Tiger("Женский", "Тигр 2") };
        public string Name { get; private set; } = "Вольер 2";
    }

    public class ZebraEnclosure : IEnclosure
    {
        public IReadOnlyList<IAnimal> Animals { get; private set; } = new List<IAnimal> { new Zebra("Мужской", "Зебра 1"), new Zebra("Женский", "Зебра 2") };
        public string Name { get; private set; } = "Вольер 3";
    }

    public class GiraffeEnclosure : IEnclosure
    {
        public IReadOnlyList<IAnimal> Animals { get; private set; } = new List<IAnimal> { new Giraffe("Мужской", "Жираф 1"), new Giraffe("Женский", "Жираф 2") };
        public string Name { get; private set; } = "Вольер 4";
    }

    public class Zoo
    {
        private IReadOnlyList<IEnclosure> enclosures = new List<IEnclosure>
        {
            new LionEnclosure(),
            new TigerEnclosure(),
            new ZebraEnclosure(),
            new GiraffeEnclosure()
        };

        public Zoo() { }

        public IReadOnlyList<IEnclosure> Enclosures { get { return enclosures; } private set { enclosures = value; } }
    }

    public class MainMenu
    {
        public void SelectEnclosure()
        {
            const string ExitCommand = "E";

            Zoo zoo = new Zoo();

            while (true)
            {
                Console.Clear();

                Console.WriteLine("Выберите вольер:");

                ShowEnclosure(zoo.Enclosures);

                Console.Write("\nДля выхода введите E на англ. раскладке\n> ");

                string input = Console.ReadLine();

                if (input.ToUpper() == ExitCommand) break;

                ApproachEnclosure(input, zoo.Enclosures);
            }
        }

        private void ShowEnclosure(IReadOnlyList<IEnclosure> enclosures)
        {
            for (int i = 0; i < enclosures.Count; i++)
            {
                int id = i + 1;

                Console.WriteLine($"{id}. {enclosures[i].Name}");
            }
        }

        private void ApproachEnclosure(string input, IReadOnlyList<IEnclosure> enclosures)
        {
            if (int.TryParse(input, out int selectedIndex) && selectedIndex > 0 && selectedIndex <= enclosures.Count)
            {
                IEnclosure selectedEnclosure = enclosures[selectedIndex - 1];

                Console.WriteLine($"\nВы выбрали {selectedEnclosure.Name}. В вольере обитает {selectedEnclosure.Animals.Count} животных:");

                foreach (IAnimal animal in selectedEnclosure.Animals)
                {
                    Console.WriteLine($"Имя: {animal.Name} ({animal.Gender}). Звук: {animal.MakeSound()}");
                }

                Console.WriteLine("\nДля возврата к выбору вольеров нажмите любую клавишу");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Неправильный ввод. Попрравьте еще раз.");
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.SelectEnclosure();
        }
    }
}