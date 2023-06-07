using System;
using System.Collections.Generic;

namespace Task13Autoservice
{
    public interface IPart
    {
        string Name { get; }
        decimal Price { get; }
    }

    public interface IRepair
    {
        string Name { get; }
        IPart Part { get; }
        decimal LaborCost { get; }
        decimal TotalCost { get; }
    }

    public interface ICustomer
    {
        string Name { get; }
        ICar Car { get; }
    }

    public interface ICar
    {
        string Model { get; }
        IList<IRepair> Repairs { get; }
    }

    public abstract class Part : IPart
    {
        public string Name { get; }
        public decimal Price { get; }

        protected Part(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }

    public class Starter : Part
    {
        public Starter() : base("Starter", new Random().Next(200, 600)) { }
    }

    public class BrakePads : Part
    {
        public BrakePads() : base("Engine", new Random().Next(150, 300)) { }
    }

    public class FrontSuspension : Part
    {
        public FrontSuspension() : base("FrontSuspension", new Random().Next(500, 1000)) { }
    }

    public class OilPump : Part
    {
        public OilPump() : base("OilPump", new Random().Next(300, 600)) { }
    }

    public class Alternator : Part
    {
        public Alternator() : base("Alternator", new Random().Next(300, 700)) { }
    }

    public class Repair : IRepair
    {
        public string Name { get; }
        public IPart Part { get; }
        public decimal LaborCost { get; }
        public decimal TotalCost => Part.Price + LaborCost;

        public Repair(string name, IPart part, decimal laborCost)
        {
            Name = name;
            Part = part;
            LaborCost = laborCost;
        }
    }

    public class Customer : ICustomer
    {
        public string Name { get; }
        public ICar Car { get; }

        public Customer(string name, ICar car)
        {
            Name = name;
            Car = car;
        }
    }

    public class Car : ICar
    {
        public string Model { get; }
        public IList<IRepair> Repairs { get; }

        public Car(string model, IList<IRepair> repairs)
        {
            Model = model;
            Repairs = repairs;
        }
    }

    public class AutoService
    {
        private decimal balance;
        private IDictionary<Type, int> partsInventory;
        private IList<ICustomer> customers;

        public AutoService(decimal balance, IDictionary<Type, int> partsInventory)
        {
            this.balance = balance;
            this.partsInventory = partsInventory;
            customers = new List<ICustomer>();
        }

        public bool RepairCar(ICustomer customer)
        {
            // Проверяем наличие всех необходимых деталей на складе
            foreach (var repair in customer.Car.Repairs)
            {
                if (!partsInventory.ContainsKey(repair.Part.GetType()) || partsInventory[repair.Part.GetType()] == 0)
                {
                    Console.WriteLine($"Для ремонта {repair.Name} необходима деталь {repair.Part.Name}, которой нет на складе.");
                    return false;
                }
            }

            // Вычитаем детали со склада и увеличиваем баланс
            foreach (var repair in customer.Car.Repairs)
            {
                partsInventory[repair.Part.GetType()]--;
                balance += repair.TotalCost - repair.Part.Price;
            }

            // Добавляем клиента и возвращаем успешность ремонта
            customers.Add(customer);
            return true;
        }

        public void PrintBalance()
        {
            Console.WriteLine($"Баланс: {balance}");
        }

        public void PrintInventory()
        {
            Console.WriteLine("Детали на складе:");
            foreach (var part in partsInventory)
            {
                Console.WriteLine($"{part.Key.Name}: {part.Value}");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Создаем склад деталей
            var partsInventory = new Dictionary<Type, int>
            {
                { typeof(Starter), 2 },
                { typeof(BrakePads), 1 },
                { typeof(FrontSuspension), 1 },
                { typeof(OilPump), 1 },
                { typeof(Alternator), 1 }
            };

            // Создаем автосервис
            var autoService = new AutoService(10000, partsInventory);

            // Создаем клиента с автомобилем
            var car = new Car("Toyota Camry", new List<IRepair>
            {
                new Repair("Замена", new BrakePads(), 2000),
                new Repair("Замена", new FrontSuspension(), 1000)
            });

            var customer = new Customer("Иван", car);

            string output = string.Empty;

            foreach (var repair in customer.Car.Repairs)
            {
                output += "\n" + repair.Part.Name;
            }
            
            Console.WriteLine($"{customer.Name} приехал чинить:{output}");

            // Пытаемся починить автомобиль
            if (autoService.RepairCar(customer))
            {
                Console.WriteLine("Автомобиль починен успешно!");
            }
            else
            {
                Console.WriteLine("Не удалось починить автомобиль.");
            }

            // Печатаем баланс и склад деталей
            autoService.PrintBalance();
            autoService.PrintInventory();

            Console.ReadKey();
        }
    }
}
