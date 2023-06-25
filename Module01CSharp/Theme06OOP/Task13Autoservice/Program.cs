using System;
using System.Collections.Generic;

namespace Task13Autoservice
{
    public abstract class Detail
    {
        public readonly string Name;
        public readonly int Price;
        public readonly double Wear;

        public Detail(string name, int productPrice, int laborPrice, double wear)
        {
            Name = name;
            Price = productPrice + laborPrice;
            Wear = wear;
        }
    }

    public class Starter : Detail
    {
        public Starter(double wear) : base("Starter", new Random().Next(200, 600), new Random().Next(50, 250), wear) { }
    }

    public class BrakePads : Detail
    {
        public BrakePads(double wear) : base("Engine", new Random().Next(150, 300), new Random().Next(50, 250), wear) { }
    }

    public class FrontSuspension : Detail
    {
        public FrontSuspension(double wear) : base("FrontSuspension", new Random().Next(500, 1000), new Random().Next(50, 250), wear) { }
    }

    public class Car
    {
        private List<Detail> _details;

        public Car(double starterWear, double brakePadsWear, double frontSuspensionWear)
        {
            _details = new List<Detail>
            {
                new Starter(starterWear),
                new BrakePads(brakePadsWear),
                new FrontSuspension(frontSuspensionWear)
            };
        }

        public void TakeDetail(Detail newDetail)
        {
            for (int i = 0; i < _details.Count; i++)
            {
                if (_details[i] == newDetail)
                {
                    _details[i] = newDetail;
                    Console.WriteLine($"Замена {_details[i].Name} прошла успешно!");
                }
            }
        }

        public List<Detail> GetWornDetails()
        {
            List<Detail> wornDetails = new List<Detail>();
            double allowableWear = 0.25;

            foreach (Detail detail in _details)
            {
                if (detail.Wear < allowableWear)
                {
                    wornDetails.Add(detail);
                    Console.WriteLine($"{detail.Name} нуждается в ремонте");
                }
            }

            if (wornDetails.Count < 1)
            {
                Console.WriteLine("Машина не нуждается в ремонте");
            }

            return wornDetails;
        }
    }

    public class MoneyAccount
    {

        public MoneyAccount(int balance) => Balance = balance;

        public int Balance { get; private set; }

        public void Add(int amount) => Balance += amount;
        
        public bool IsBalanceValidBeforeDecrease()
        {
            if (Balance < 0)
            {
                Console.WriteLine("Невозможно уменьшить баланс на отрицательное число");
                return false;
            }

            return true;
        }

        public bool EnsureSufficientBalanceForWithdrawal(int amount)
        {
            if (Balance < amount)
            {
                Console.WriteLine("Невозможно уменьшить баланс. Недостаточно средств");
                return false;
            }

            return true;
        }

        public void Withdraw(int amount) => Balance -= amount;
    }

    public class Customer
    {
        private readonly MoneyAccount _moneyAccount;

        public Customer(string name, int balance, Car car)
        {
            Name = name;
            _moneyAccount = new MoneyAccount(balance);
            Car = car;
        }

        public string Name { get; private set; }
        public Car Car { get; private set; }

        public void GiveMoney(int totalPrice) => _moneyAccount.Withdraw(totalPrice);

        public void TakeMoney(int totalPrice) => _moneyAccount.Add(totalPrice);

        public int GetBalance() => _moneyAccount.Balance;

        public bool CheckSufficientFunds(int totalPrice) => _moneyAccount.IsBalanceValidBeforeDecrease() && _moneyAccount.EnsureSufficientBalanceForWithdrawal(totalPrice);
    }

    public class Cell
    {
        public Cell(Detail detail, int count)
        {
            Detail = detail;
            Count = count;
        }

        public Detail Detail { get; private set; }
        public int Count { get; private set; }

        public bool TryTakeDetail() => Count > 0;

        public void TakeDetail() => Count--;
    }

    class DetailsStorage
    {
        private List<Cell> _details = new List<Cell>();

        public DetailsStorage(int starterCount, int brakePadsCount, int frontSuspensionCount)
        {
            double maxWear = 1;

            _details.Add(new Cell(new Starter(maxWear), starterCount));
            _details.Add(new Cell(new BrakePads(maxWear), brakePadsCount));
            _details.Add(new Cell(new FrontSuspension(maxWear), frontSuspensionCount));
        }

        public Detail GetDetail(Detail requestedDetail)
        {
            Detail foundDetail = null;

            foreach (Cell cell in _details)
            {
                if (cell.Detail.GetType() == requestedDetail.GetType() && cell.TryTakeDetail())
                {
                    cell.TakeDetail();
                    foundDetail = cell.Detail;
                    break;
                }
            }
            Console.WriteLine();
            return foundDetail;
        }
    }

    class CarService
    {
        private DetailsStorage _detailsStorage;
        private MoneyAccount _moneyAccount;
        private Queue<Customer> _customers;

        public CarService(Queue<Customer> customers, int balance)
        {
            int maxDetailsCount = 10;

            _detailsStorage = new DetailsStorage(new Random().Next(maxDetailsCount), new Random().Next(maxDetailsCount), new Random().Next(maxDetailsCount));
            _moneyAccount = new MoneyAccount(balance);
            _customers = customers;
        }

        public void ServeCustomer()
        {
            foreach (var customer in _customers)
            {
                Console.WriteLine("---// Баланс автосервиса: " + _moneyAccount.Balance);
                Console.WriteLine($"{customer.Name} [{customer.GetBalance()}$] приехал в автомастерскую\n");

                Console.WriteLine("Результат диагностики:\n");

                foreach (Detail detail in customer.Car.GetWornDetails())
                {
                    Detail newDetail = _detailsStorage.GetDetail(detail);

                    int totalPrice = newDetail.Price;
                    
                    if (newDetail != null)
                    {
                        if (customer.CheckSufficientFunds(totalPrice))
                        {
                            _moneyAccount.Add(totalPrice);
                            customer.GiveMoney(totalPrice);

                            Console.WriteLine($"Клиент оплатил замену {newDetail.Name}: -{newDetail.Price}");
                            customer.Car.TakeDetail(newDetail);
                            Console.WriteLine("Деталь заменена");
                        }
                        else
                        {
                            Console.WriteLine($"У клиента недостаточно средств для оплаты замены детали {newDetail.Price}");
                        }
                    }
                    else
                    {
                        int fineAmount = 100;
                        totalPrice -= fineAmount;

                        customer.TakeMoney(Math.Abs(totalPrice));
                        _moneyAccount.Withdraw(Math.Abs(totalPrice));

                        Console.WriteLine($"К сожалению, на складе не оказалось нужной детали. Клиенту начислено {Math.Abs(totalPrice)}$ в качестве компенсации");
                    }
                }

                Console.WriteLine($"\n{customer.Name} [{customer.GetBalance()}$] покинул   автомастерскую\n");
            }
        }
    }

    class Street
    {
        private static Street _instance = null;

        private Street() => GenerateCustomers(new Random());

        public static Street GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Street();
            }

            return _instance;
        }

        public void Work(Random random)
        {
            int maxBalance = 10000;

            CarService carService = new CarService(GenerateCustomers(random), random.Next(maxBalance));
            carService.ServeCustomer();
        }

        private Queue<Customer> GenerateCustomers(Random random)
        {
            Queue<Customer> customers = new Queue<Customer>();
            int customersMaxCount = 10;
            int customersCount = random.Next(customersMaxCount);
            int maxBalance = 2500;

            List<Car> carTypes = new List<Car>
            {
                new Car(0.90, 0.72, 0.12),
                new Car(0.30, 0.64, 0.28),
                new Car(0.02, 0.02, 0.86),
                new Car(0.11, 0.56, 0.24)
            };

            for (int i = 0; i < customersCount; i++)
            {
                int id = i + 1;

                customers.Enqueue(new Customer($"Водитель {id}", random.Next(maxBalance), carTypes[random.Next(carTypes.Count)]));
            }

            return customers;
        }
    }

    internal class Program
    {
        public static void Main()
        {
            Street street = Street.GetInstance();
            street.Work(new Random());

            Console.ReadKey();
        }
    }
}
