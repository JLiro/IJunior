using System;
using System.Collections.Generic;

public class Product
{
    public string Name { get; private set; }
    public double Price { get; private set; }

    public Product(string name, double price)
    {
        Name = name;
        Price = price;
    }
}

public class Storage
{
    private List<Product> products = new List<Product>();

    public IReadOnlyList<Product> Products => products;

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public void RemoveProduct(Product product)
    {
        products.Remove(product);
    }

    public double GetTotalPrice()
    {
        double totalPrice = 0;

        foreach (var product in products)
        {
            totalPrice += product.Price;
        }

        return totalPrice;
    }
}

public class Customer
{
    private Random random = new Random();

    public string Name { get; private set; }
    public double Money { get; set; }
    public Storage Storage { get; } = new Storage();

    public Customer(string name, double money)
    {
        Name = name;
        Money = money;
    }

    public bool Pay(double amount)
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
            int index = random.Next(Storage.Products.Count);
            Product product = Storage.Products[index];
            Storage.RemoveProduct(product);
        }
    }
}

public class Seller
{
    private List<Product> products = new List<Product>();
    private Queue<Customer> queue = new Queue<Customer>();
    private Random random = new Random();

    public void AddRandomProductToCustomer(Customer customer)
    {
        if (products.Count > 0)
        {
            int index = random.Next(products.Count);
            Product product = products[index];
            customer.Storage.AddProduct(product);
        }
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public void AddCustomer(Customer customer)
    {
        if (!queue.Contains(customer))
        {
            queue.Enqueue(customer);
        }
    }

    public void ServeCustomers()
    {
        while (queue.Count > 0)
        {
            Customer customer = queue.Dequeue();

            double totalPrice = 0;
            foreach (Product product in customer.Storage.Products)
            {
                totalPrice += product.Price;
            }

            if (totalPrice == 0)
            {
                Console.WriteLine($"{customer.Name} не имеет товаров для покупки.");
            }
            else if (customer.Pay(totalPrice))
            {
                Console.WriteLine($"{customer.Name} совершил покупку на сумму {totalPrice}.");
            }
            else
            {
                Console.WriteLine($"{customer.Name} не имеет достаточно денег, чтобы заплатить.");

                while (totalPrice > customer.Money && customer.Storage.Products.Count > 0)
                {
                    customer.RemoveRandomProduct();

                    totalPrice = 0;

                    foreach (Product product in customer.Storage.Products)
                    {
                        totalPrice += product.Price;
                    }
                }

                if (totalPrice == 0)
                {
                    Console.WriteLine($"{customer.Name} не имеет товаров для покупки.");
                }
                else
                {
                    customer.Pay(totalPrice);
                    Console.WriteLine($"{customer.Name} совершил покупку на сумму {totalPrice} после удаления некоторых продуктов.");
                }
            }
        }
    }
}

public class Supermarket
{
    private readonly Seller seller;
    private readonly List<Customer> customers = new List<Customer>();
    private Random random = new Random();

    private IReadOnlyList<Customer> Customers => customers;

    public Supermarket()
    {
        seller = new Seller();
    }

    public void Work()
    {
        int minEach = 1;
        int maxEach = 11;

        AddProduct(new Product("Хлеб", 0.5));
        AddProduct(new Product("Молоко", 0.5));
        AddProduct(new Product("Яйца", 0.8));
        AddProduct(new Product("Рыба", 0.9));

        AddRandomCustomers();

        foreach (Customer customer in Customers)
        {
            int count = random.Next(minEach, maxEach);

            AddRandomProductsToCustomer(customer, count);
        }

        ServeCustomers();
    }

    private void AddRandomProductsToCustomer(Customer customer, int count)
    {
        for (int i = 0; i < count; i++)
        {
            seller.AddRandomProductToCustomer(customer);
        }
    }

    private void AddProduct(Product product)
    {
        seller.AddProduct(product);
    }

    private void AddCustomer(Customer customer)
    {
        customers.Add(customer);
        seller.AddCustomer(customer);
    }

    private void AddRandomCustomers()
    {
        double minMoney = 0.5;
        double maxMoney = 3.5;

        int minCustomer = 1;
        int maxCustomer = 11;

        int count = random.Next(minCustomer, maxCustomer);

        for (int i = 0; i < count; i++)
        {
            int customerID = i + 1;

            Customer customer = new Customer($"Клиент {customerID}", nextDouble(random, minMoney, maxMoney));
            AddCustomer(customer);
        }
    }

    private void ServeCustomers()
    {
        seller.ServeCustomers();
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