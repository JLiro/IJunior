using System;
using System.Collections.Generic;

namespace Task06Store
{
    internal class Program
    {
        static void Main()
        {
            Market market = new Market();
        }
    }

    class Market
    {
        public Market()
        {
            OpenMarket();
        }

        private void OpenMarket()
        {
            const string CommandExit = "E";

            string input = String.Empty;
            string output = String.Empty;

            Seller seller = new Seller();
            Player player = new Player();

            while (input.ToUpper() != CommandExit)
            {
                Console.WriteLine($"[ ПРОДАВЕЦ ] {seller.Coins} монет");
                seller.ShowProducts();

                Console.WriteLine();

                Console.WriteLine($"[ ПОКУПАТЕЛЬ ] {player.Coins} монет");
                player.ShowProducts();

                Console.WriteLine($"\nПоследнее событие: {output}");

                Console.Write($"\nВведите номер товара для покупки или [{CommandExit}] для выхода: ");
                input = Console.ReadLine();

                bool isProductAvailability = Int32.TryParse(input, out int productIndex) && productIndex >= 0 && productIndex < seller.GetProductsCount();

                if (isProductAvailability == true)
                {
                    Product product = seller.GetProduct(productIndex);

                    if (player.CanPay(product.Price))
                    {
                        seller.Sell(product);
                        player.Buy(product);

                        output = "Покупка успешно совершена";
                    }
                    else
                    {
                    output = "У покупателя недостаточно монет";
                    }

                }
                else
                {
                    output = $"Товар с таким номером не найден";
                }

                Console.Clear();
            }
        }
    }

    abstract class Persone
    {
        private List<Product> _products;

        protected Persone()
        {
            _products = new List<Product>();
        }

        public int Coins { get; private set; }

        public void ShowProducts()
        {
            string output = "Инвентарь:";

            if (_products.Count > 0)
            {
                for (int i = 0; i < _products.Count; i++)
                {
                    output += $"\n[{i}] {_products[i].GetInfo()}";
                }
            }
            else
            {
                output += " Пусто";
            }

            Console.WriteLine(output);
        }

        protected void FillProducts(List<Product> products)
        {
            _products = products;
        }

        protected void AddCoins(int coins)
        {
            if (coins > 0)
            {
                Coins += coins;
            }
        }

        protected void Pay(int coins)
        {
            if (coins > 0)
            {
                Coins -= coins;
            }
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            _products.Remove(product);
        }

        public Product GetProduct(int id)
        {
            return _products[id];
        }

        public int GetProductsCount()
        {
            return _products.Count;
        }
    }

    class Player : Persone
    {
        private Random _random = new Random();

        public Player()
        {
            int minCoins = 0;
            int maxCoins = 1000;

            int coins = _random.Next(minCoins, maxCoins);
            AddCoins(coins);
        }

        public void Buy(Product product)
        {
            Pay(product.Price);

            AddProduct(product);
        }

        public bool CanPay(int price)
        {
            return Coins >= price;
        }
    }

    class Seller : Persone
    {
        public Seller()
        {
            int coins = 0;
            AddCoins(coins);

            List<Product> products = new()
            {
                new Product("one", 100),
                new Product("bne", 300),
                new Product("tne", 600)
            };

            FillProducts(products);
        }

        public void Sell(Product product)
        {
            AddCoins(product.Price);

            RemoveProduct(product);
        }
    }

    class Product
    {
        private string _title;

        public Product(string title, int price)
        {
            _title = title;
            Price = price;
        }

        public int Price { get; }

        public string GetInfo()
        {
            return $"{_title} стоит {Price} монет";
        }
    }
}