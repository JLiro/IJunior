using System;
using System.Collections.Generic;

namespace Task09Supermarket
{
    internal class Program
    {
        static void Main()
        {
            Store cash = new Store();
            cash.Work();
        }
    }

    public enum ProductType
    {
        Milk,
        Cheese,
        Sausage,
        Cookies,
        Sugar,
        Salt,
        Sausages,
        Potatoes,
        Carrots
    }

    class Product
    {
        public ProductType ProductType { get; private set; }
        public int ProductPrice { get; private set; }

        public Product(ProductType productType, int productPrice)
        {
            ProductType = productType;
            ProductPrice = productPrice;
        }
    }

    class Buyer
    {
        private List<Product> _cart;
        public int Money { get; private set; }
        public int DesiredCountOfProduct { get; private set; }

        public Buyer(int money, int desiredCountOfProduct)
        {
            Money = money;
            DesiredCountOfProduct = desiredCountOfProduct;
            _cart = new List<Product>();
        }

        public void AddToCart(List<Product> products, Random random)
        {
            for (int i = 0; i < DesiredCountOfProduct; i++)
            {
                _cart.Add(products[random.Next(0, products.Count)]);
                Console.WriteLine($"{_cart[i].ProductType} {_cart[i].ProductPrice}");
            }
        }

        public void RemoveFromCart(Random random)
        {
            Product currentproduct = _cart[random.Next(0, _cart.Count)];
            _cart.Remove(currentproduct);
            Console.Write($"\nПокупатель отказался от покупки: {currentproduct.ProductType}");
        }

        public void CountUpMoney(out int moneyToPay)
        {
            moneyToPay = 0;

            foreach (var product in _cart)
            {
                moneyToPay += product.ProductPrice;
            }
        }
    }

    class Store
    {
        private Random _random = new Random();
        
        private int _storeMoneyCount = 0;

        private List<Product> _products = new List<Product>();
        private Queue<Buyer>  _buyers = new Queue<Buyer>();

        public Store()
        {
            int _productsCount = Enum.GetNames(typeof(ProductType)).Length;
            AddProduct(_productsCount);

            int minBuyersCount = 0;
            int maxBuyersCount = 12;

            int _buyersCount = _random.Next(minBuyersCount, maxBuyersCount);
            AddBuyer(_buyersCount);
        }

        public void Work()
        {
            while (_buyers.Count > 0)
            {
                Buyer buyer = _buyers.Dequeue();
                int moneyToPay;

                Console.WriteLine
                (
                    $"В кассе: {_storeMoneyCount}руб." +
                    $"\n\n" +
                    $"Покупатель выложил {buyer.DesiredCountOfProduct} продуктов на стойку кассы" +
                    $"\n"
                );

                buyer.AddToCart(_products, _random);
                buyer.CountUpMoney(out moneyToPay);

                while (buyer.Money < moneyToPay)
                {
                    buyer.RemoveFromCart(_random);
                    buyer.CountUpMoney(out moneyToPay);
                }

                Console.Write
                (
                    $"\n" +
                    $"\n" +
                    $"Покупатель оплатил покупку на {moneyToPay}руб."
                );
                _storeMoneyCount += moneyToPay;

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void AddProduct(int productCount)
        {
            for (int i = 0; i < productCount; i++)
            {
                Product product = new Product((ProductType)i, _random.Next(50, 400));
                _products.Add(product);
            }
        }

        private void AddBuyer(int buyerCount)
        {
            for (int i = 0; i < buyerCount; i++)
            {
                _buyers.Enqueue(new Buyer(_random.Next(100, 5000), _random.Next(1, _products.Count)));
            }
        }
    }
}