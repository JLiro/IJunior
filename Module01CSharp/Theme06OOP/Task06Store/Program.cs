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
            const string CommandExit = "E";

            string input  = String.Empty;
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

                Console.Write
                (
                     "\n" +
                    $"Последнее событие: {output}" +
                     "\n"
                );

                Console.Write($"\nВведите номер товара для покупки или [{CommandExit}] для выхода: ");
                input = Console.ReadLine();

                if (seller.IsCheckProductAvailability(input, out Product product))
                {
                    if(player.Coins >= product.Price)
                    {
                        seller.Sell(product);
                        player.Buy (product);

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

    class Player
    {
        private List<Product> _products = new List<Product>();
        
        private Random _random = new Random();

        public Player()
        {
            int minCoins = 0;
            int maxCoins = 1000;

            Coins = _random.Next(minCoins, maxCoins);
        }

        public int Coins { get; private set; }

        public void Buy(Product product)
        {
            Coins -= product.Price;

            _products.Add(product);
        }

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
    }
    
    class Seller
    {
        private List<Product> _products = new List<Product>();

        public Seller()
        {
            _products = new List<Product>()
            {
                new Product("one", 100),
                new Product("bne", 300),
                new Product("tne", 600)
            };
        }

        public int Coins { get; private set; }

        public void Sell(Product product)
        {
            Coins += product.Price;

            _products.Remove(product);
        }

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
                output = " Пусто";
            }

            Console.WriteLine(output);
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

    class Product
    {
        public Product(string title, int price)
        {
            Title = title;
            Price = price;
        }

        public string Title { get; set; }
        public int Price { get; set; }

        public string GetInfo()
        {
            return $"{Title} стоит {Price} монет";
        }
    }
}