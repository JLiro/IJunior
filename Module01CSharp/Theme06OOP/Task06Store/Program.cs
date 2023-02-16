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

                if (seller.IsCheckProductAvailability(input, out Product product))
                {
                    if (player.Coins >= product.Price)
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

    class Player : Persone
    {
        private Random _random = new Random();

        public Player()
        {
            _products = new List<Product>();

            int minCoins = 0;
            int maxCoins = 1000;

            Coins = _random.Next(minCoins, maxCoins);
        }

        public override List<Product> _products { get; protected set; }
        public override int Coins { get; protected set; }

        public void Buy(Product product)
        {
            Coins -= product.Price;

            _products.Add(product);
        }
    }
    
    class Seller : Persone
    {
        public Seller()
        {
            _products = new List<Product>()
            {
                new Product("one", 100),
                new Product("bne", 300),
                new Product("tne", 600)
            };
        }

        public override List<Product> _products { get; protected set; }
        public override int Coins { get; protected set; }

        public void Sell(Product product)
        {
            Coins += product.Price;

            _products.Remove(product);
        }

        public bool IsCheckProductAvailability(string input, out Product product)
        {
            product = null;

            bool isProductAvailability = Int32.TryParse(input, out int productIndex) && productIndex >= 0 && productIndex < _products.Count;

            if(isProductAvailability == true)
            {
                product = _products[productIndex];
            }

            return isProductAvailability;
        }
    }

    abstract class Persone
    {
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

        public abstract List<Product> _products { get; protected set; }
        public abstract int Coins { get; protected set; }
    }

    class Product
    {
        private string _title;

        public Product(string title, int price)
        {
            _title = title;
            Price = price;
        }

        public int Price { get; private set; }

        public string GetInfo()
        {
            return $"{_title} стоит {Price} монет";
        }
    }
}