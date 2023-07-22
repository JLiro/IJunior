using System;
using System.Collections.Generic;

namespace OnlineShop
{
    class Good
    {
        public string Name { get; }

        public Good(string name)
        {
            Name = name;
        }
    }

    class Warehouse
    {
        private Dictionary<Good, int> _goods;

        public Warehouse()
        {
            _goods = new Dictionary<Good, int>();
        }

        public void Deliver(Good good, int quantity)
        {
            if (_goods.ContainsKey(good))
            {
                _goods[good] += quantity;
            }
            else
            {
                _goods.Add(good, quantity);
            }
        }

        public void DisplayGoods()
        {
            Console.WriteLine("Товары на складе:");

            foreach (var item in _goods)
            {
                Console.WriteLine($"{item.Key.Name}: {item.Value}");
            }
        }

        public int GetQuantity(Good good)
        {
            if (_goods.ContainsKey(good))
            {
                return _goods[good];
            }
            else
            {
                return 0;
            }
        }

        public bool IsGoodsAvailable(int quantityToAdd) => quantityToAdd <= _goods.Count;
    }

    class Cart
    {
        private Dictionary<Good, int> _items;

        public Cart()
        {
            _items = new Dictionary<Good, int>();
        }

        public void Add(Good good, int quantity, Warehouse warehouse)
        {
            if (warehouse.IsGoodsAvailable(quantity))
            {
                _items[good] = quantity;
            }
            else
            {
                Console.WriteLine("Ошибка: Недостаточно товара на складе");
            }
        }

        public void DisplayCartItems()
        {
            Console.WriteLine("Товары в корзине:");

            foreach (var item in _items)
            {
                Console.WriteLine($"{item.Key.Name}: {item.Value}");
            }
        }

        public string Order()
        {
            _items.Clear();

            return GeneratePaylink();
        }

        private string GeneratePaylink()
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            Random random = new Random();

            int paylinkLength = 10;

            char[] paylinkChars = new char[paylinkLength];

            for (int i = 0; i < paylinkChars.Length; i++)
            {
                paylinkChars[i] = characters[random.Next(characters.Length)];
            }

            return new string(paylinkChars);
        }
    }

    class Shop
    {
        private Warehouse _warehouse;
        private Cart _cart;

        public Shop()
        {
            _warehouse = new Warehouse();
            _cart = new Cart();
        }

        public void Work()
        {
            Good iPhone12 = new Good("IPhone 12");
            Good iPhone11 = new Good("IPhone 11");

            int iPhone12Quantity = 10;
            int iPhone11Quantity = 1;
            _warehouse.Deliver(iPhone12, iPhone12Quantity);
            _warehouse.Deliver(iPhone11, iPhone11Quantity);

            _warehouse.DisplayGoods();
            Console.WriteLine();

            int quantityToAdd1 = 4;
            int quantityToAdd2 = 3;
            int quantityToAdd3 = 9;

            _cart.Add(iPhone12, quantityToAdd1, _warehouse);
            _cart.Add(iPhone11, quantityToAdd2, _warehouse);

            Console.WriteLine();
            _cart.DisplayCartItems();
            Console.WriteLine();

            Console.WriteLine(_cart.Order());
            Console.WriteLine();

            _cart.Add(iPhone12, quantityToAdd3, _warehouse);

            Console.ReadLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Shop shop = new Shop();
            shop.Work();
        }
    }
}