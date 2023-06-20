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
        private int _balance;

        public MoneyAccount(int balance) => _balance = balance;

        public void Deposit(int amount) => _balance += amount;

        public bool CheckSufficientFunds(int amount)
        {
            if (_balance < 0)
            {
                Console.WriteLine("Невозможно уменьшить баланс на отрицательное число");
                return false;
            }

            if (_balance < amount)
            {
                Console.WriteLine("Невозможно уменьшить баланс. Недостаточно средств");
                return false;
            }

            return true;
        }

        public int Withdraw(int amount)
        {
            _balance -= amount;

            return amount;
        }

        public string GetInfo() => _balance.ToString();
    }

    public abstract class Human
    {
        public string Name { get; protected set; }
        public Car ThisСar { get; protected set; }

        public void GetCar(Car car) => ThisСar = car;
    }

    public class Customer : Human
    {
        private readonly MoneyAccount _moneyAccount;

        public Customer(string name, int balance, Car car)
        {
            Name = name;
            _moneyAccount = new MoneyAccount(balance);
            ThisСar = car;
        }

        public int GiveMoney(int totalPrice) => _moneyAccount.Withdraw(totalPrice);

        public void TakeMoney(int totalPrice) => _moneyAccount.Deposit(totalPrice);

        public string GetBalance() => _moneyAccount.GetInfo();

        public bool CheckSufficientFunds(int totalPrice) => _moneyAccount.CheckSufficientFunds(totalPrice);
    }

    class CarMechanic : Human
    {
        public void ReplaceDetail(DetailsStorage detailsStorage, Detail detail)
        {
            ThisСar.TakeDetail(detailsStorage.GetDetail(detail));
        }

        public List<Detail> IdentifyWornDetails()
        {
            List<Detail> wornDetails = null;

            if (ThisСar != null)
            {
                wornDetails = ThisСar.GetWornDetails();
            }

            return wornDetails;
        }
    }

    class DetailsStorage
    {
        private List<Detail> _details = new List<Detail>();

        public DetailsStorage(int starterCount, int brakePadsCount, int frontSuspensionCount)
        {
            double maxWear = 1;

            for (int i = 0; i < starterCount; i++)
            {
                _details.Add(new Starter(maxWear));
            }

            for (int i = 0; i < brakePadsCount; i++)
            {
                _details.Add(new BrakePads(maxWear));
            }

            for (int i = 0; i < frontSuspensionCount; i++)
            {
                _details.Add(new FrontSuspension(maxWear));
            }
        }

        public bool CheckDetailsAvailability(Detail requestedDetail, out int count)
        {
            int detailsCount = 0;

            foreach (Detail detail in _details)
            {
                if (detail.GetType() == requestedDetail.GetType())
                {
                    detailsCount++;
                }
            }

            count = detailsCount;

            return detailsCount > 0;
        }

        public Detail GetDetail(Detail requestedDetail)
        {
            Detail foundDetail = null;

            foreach (Detail detail in _details)
            {
                if (detail.GetType() == requestedDetail.GetType())
                {
                    foundDetail = detail;
                    _details.Remove(detail);
                    break;
                }
            }

            return foundDetail;
        }
    }

    class CarService
    {
        private DetailsStorage _detailsStorage;
        private MoneyAccount _moneyAccount;
        private CarMechanic _carMechanic;
        private Queue<Customer> _customers;

        public CarService(Queue<Customer> customers, int balance)
        {
            _detailsStorage = new DetailsStorage(new Random().Next(10), new Random().Next(10), new Random().Next(10));
            _moneyAccount = new MoneyAccount(balance);
            _carMechanic = new CarMechanic();
            _customers = customers;
        }

        public void ServeCustomer()
        {
            foreach (var customer in _customers)
            {
                Console.WriteLine("---// Баланс автосервиса: " + _moneyAccount.GetInfo());

                Console.WriteLine($"{customer.Name} [{customer.GetBalance()}$] приехал в автомастерскую");
                _carMechanic.GetCar(customer.ThisСar);
                Console.WriteLine("Машина передана автомеханнику для диагностики неполадок\nРезультат диагностики:");

                foreach (Detail detail in _carMechanic.IdentifyWornDetails())
                {
                    CalculateCost(detail, out int totalPrice);

                    if (CheckTransactionDirection(totalPrice))
                    {
                        if (customer.CheckSufficientFunds(totalPrice))
                        {
                            _moneyAccount.Deposit(customer.GiveMoney(totalPrice));
                            Console.WriteLine($"Клиент оплатил замену {detail.Name}: -{detail.Price}");

                            _carMechanic.ReplaceDetail(_detailsStorage, detail);
                            Console.WriteLine("Деталь заменена");
                        }
                        else
                        {
                            Console.WriteLine($"У клиента недостаточно средств для оплаты замены детали {detail.Price}");
                        }
                    }
                    else
                    {
                        customer.TakeMoney(_moneyAccount.Withdraw(Math.Abs(totalPrice)));
                        Console.WriteLine($"К сожалению, на складе не оказалось нужной детали. Клиенту начислено {Math.Abs(totalPrice)}$ в качестве компенсации");
                    }
                }

                customer.GetCar(_carMechanic.ThisСar);
                Console.WriteLine($"Машина возвращена автовладельцу [{customer.GetBalance()}$]\n");
            }
        }

        private bool CheckTransactionDirection(int totalPrice) => totalPrice > 0;

        private void CalculateCost(Detail detail, out int totalPrice)
        {
            int fineAmount = 100;
            totalPrice = 0;

            if (_detailsStorage.CheckDetailsAvailability(detail, out int detailsCountInStorage) == true)
            {
                Console.WriteLine($"На скаладе {detailsCountInStorage} {detail.Name}. Замена обойдётся в {detail.Price}$");

                totalPrice += detail.Price;
            }
            else
            {
                Console.WriteLine($"На складе нет {detail.Name}. Мы сожалеем и выплатим Вам {fineAmount}$");

                totalPrice -= fineAmount;
            }
        }
    }

    class Street
    {
        private static Street instance = null;

        private Street() => GenerateCustomers(new Random());

        public static Street GetInstance()
        {
            if (instance == null)
            {
                instance = new Street();
            }

            return instance;
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
                new Car(0.11, 0.56, 0.24),
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
