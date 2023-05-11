using System;
using System.Collections.Generic;

public class Product
{
    public Product(string name, double price)
    {
        Name = name;
        Price = price;
    }

    public string Name { get; private set; }
    public double Price { get; private set; }
}

public class Storage
{
    private List<Product> _products = new List<Product>();

    public IReadOnlyList<Product> Products => _products;

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public void RemoveProduct(Product product)
    {
        _products.Remove(product);
    }

    public double GetTotalPrice()
    {
        double totalPrice = 0;

        foreach (var product in _products)
        {
            totalPrice += product.Price;
        }

        return totalPrice;
    }
}

public class Customer
{
    private Random _random = new Random();

    public Customer(string name, double money)
    {
        Name = name;
        Money = money;
    }

    public string Name { get; private set; }
    public double Money { get; set; }
    public Storage Storage { get; } = new Storage();

    public bool TryPay(double amount)
    {
        if (Money < amount)
        {
            return false;
        }

        Money -= amount;

        return true;
    }

    public void RemoveRandomProduct()
    {
        if (Storage.Products.Count > 0)
        {
            int index = _random.Next(Storage.Products.Count);
            Product product = Storage.Products[index];
            Storage.RemoveProduct(product);
        }
    }
}

public class Seller
{
    private List<Product> _products = new List<Product>();
    private Queue<Customer> _queue = new Queue<Customer>();
    private Random _random = new Random();

    public void AddRandomProductToCustomer(Customer customer)
    {
        if (_products.Count > 0)
        {
            int index = _random.Next(_products.Count);
            Product product = _products[index];
            customer.Storage.AddProduct(product);
        }
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public void AddCustomer(Customer customer)
    {
        if (!_queue.Contains(customer))
        {
            _queue.Enqueue(customer);
        }
    }

    public void ServeCustomers()
    {
        while (_queue.Count > 0)
        {
            Customer customer = _queue.Dequeue();

            double totalPrice = customer.Storage.GetTotalPrice();

            if (totalPrice == 0)
            {
                Console.WriteLine($"{customer.Name} не имеет товаров для покупки.");
            }
            else
            {
                bool canPay = customer.TryPay(totalPrice);

                while (canPay == false && customer.Storage.Products.Count > 0)
                {
                    customer.RemoveRandomProduct();

                    totalPrice = customer.Storage.GetTotalPrice();
                    canPay = customer.TryPay(totalPrice);
                }

                if (canPay)
                {
                    Console.WriteLine($"{customer.Name} совершил покупку на сумму {totalPrice}.");
                }
                else
                {
                    Console.WriteLine($"{customer.Name} не имеет достаточно денег, чтобы заплатить.");
                    Console.WriteLine($"{customer.Name} совершил покупку на сумму {totalPrice} после удаления некоторых продуктов.");
                }
            }
        }
    }
}

public class Supermarket
{
    private readonly List<Customer> _customers = new List<Customer>();
    private readonly List<Product> _products = new List<Product>();
    private Random _random = new Random();

    public void Work()
    {
        int minEach = 1;
        int maxEach = 11;

        AddProduct(new Product("Хлеб", 0.5));
        AddProduct(new Product("Молоко", 0.5));
        AddProduct(new Product("Яйца", 0.8));
        AddProduct(new Product("Рыба", 0.9));

        AddRandomCustomers();

        foreach (Customer customer in _customers)
        {
            int count = _random.Next(minEach, maxEach);

            AddRandomProductsToCustomer(customer, count);
        }

        ServeCustomers();

        Console.ReadKey();
    }

    private void AddProduct(Product product)
    {
        _products.Add(product);
    }

    private void AddRandomProductsToCustomer(Customer customer, int count)
    {
        for (int i = 0; i < count; i++)
        {
            AddRandomProductToCustomer(customer);
        }
    }

    private void AddRandomProductToCustomer(Customer customer)
    {
        if (_products.Count > 0)
        {
            int index = _random.Next(_products.Count);
            Product product = _products[index];
            customer.Storage.AddProduct(product);
        }
    }

    private void AddCustomer(Customer customer)
    {
        _customers.Add(customer);
    }

    private void AddRandomCustomers()
    {
        double minMoney = 0.1;
        double maxMoney = 3.5;

        int minCustomer = 1;
        int maxCustomer = 11;

        int count = _random.Next(minCustomer, maxCustomer);

        for (int i = 0; i < count; i++)
        {
            int customerID = i + 1;

            Customer customer = new Customer($"Клиент {customerID}", nextDouble(_random, minMoney, maxMoney));
            AddCustomer(customer);
        }
    }

    private void ServeCustomers()
    {
        foreach (Customer customer in _customers)
        {
            double totalPrice = customer.Storage.GetTotalPrice();

            if (totalPrice == 0)
            {
                Console.WriteLine($"{customer.Name} не имеет товаров для покупки.");
            }
            else
            {
                bool canPay = customer.TryPay(totalPrice);

                if (canPay)
                {
                    Console.WriteLine($"{customer.Name} совершил покупку на сумму {totalPrice}.");
                }
                else
                {
                    while (canPay == false && customer.Storage.Products.Count > 0)
                    {
                        customer.RemoveRandomProduct();

                        totalPrice = customer.Storage.GetTotalPrice();
                        canPay = customer.TryPay(totalPrice);
                    }

                    Console.WriteLine($"{customer.Name} не имеет достаточно денег, чтобы заплатить.");
                    Console.WriteLine($"{customer.Name} совершил покупку на сумму {totalPrice} после удаления некоторых продуктов.");
                }
            }
        }
    }

    private double nextDouble(Random random, double minValue, double maxValue)
    {
        return random.NextDouble() * (maxValue - minValue) + minValue;
    }
}

class Program
{
    static void Main()
    {
        Supermarket supermarket = new Supermarket();

        supermarket.Work();
    }
}