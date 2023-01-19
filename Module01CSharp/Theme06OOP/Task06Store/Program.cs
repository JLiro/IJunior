using System;
using System.Collections.Generic;

namespace Task06Store
{
    internal class Program
    {
        static void Main()
        {
            const string CommandExit = "E";

            int playerCoins = 2500;
            int sellerCoins = 0;

            Seller seller = new Seller(sellerCoins);
            Player player = new Player(playerCoins);

            string input  = string.Empty;
            string lastAction = string.Empty;

            while (true)
            {
                Console.WriteLine($"Последнее событие: {lastAction}\n");

                Console.WriteLine($"  [ ПРОДАВЕЦ ] {seller.Money} монет");
                seller.ShowProducts();

                Console.WriteLine($"  [ ИГРОК ] {player.Money} монет");
                player.ShowProducts();

                Console.Write($"\nВведите номер товары для покупки или [{CommandExit}] для выхода: ");
                input = Console.ReadLine();

                if (input.ToUpper() == "E")
                    return;

                lastAction = seller.SellProduct(player, input);

                Console.Clear();
            }
        }
    }

    internal class Player : Person
    {
        public Player(int money) : base(money) { }

        public string BuyProduct(Product product)
        {
            Money -= product.Price;
            Products.Add(product);

            return $"Игрок купил {product.Title}";
        }
    }

    internal class Seller : Person
    {
        public Seller(int money) : base(money)
        {
            CreateProducts();
        }

        public string SellProduct(Player buyer, string input)
        {
            Console.WriteLine("\nВведите номер товара для покупки:");

            if (Int32.TryParse(input, out int value) && value > 0 && value <= Products.Count)
            {
                Product product = Products[value - 1];

                if (buyer.Money < product.Price)
                {
                    return "Недостаточно монет";
                }

                Products.RemoveAt(value - 1);
                Money += product.Price;

                return buyer.BuyProduct(product);
            }
            else
            {
                return "Введён невереный номер товара";
            }
        }

        private void CreateProducts()
        {
            Products.Add(new Product("Меч из дерева", 250));
            Products.Add(new Product("Меч из камня", 500));
            Products.Add(new Product("Меч из стали", 1000));
            Products.Add(new Product("Меч из обсидиана", 10000));
        }
    }

    internal class Person
    {
        protected List<Product> Products = new List<Product>();

        public int Money { get; protected set; }

        public Person(int money) => Money = money < 0 ? 0 : money;

        public void ShowProducts()
        {
            Console.WriteLine("    Инвертарь:");
            if (Products.Count == 0)
            {
                Console.WriteLine("    Товары отсутствуют");
                return;
            }

            for (int i = 0; i < Products.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {Products[i].GetInfo()}");
            }
            Console.WriteLine();
        }
    }

    internal class Product
    {
        public int Price { get; private set; }
        public string Title { get; private set; }

        public Product(string title, int price)
        {
            Title = title;
            Price = price;
        }

        public string GetInfo()
        {
            return $"{Title}" +
                   $"\n    {Price} золотых";
        }
    }
}