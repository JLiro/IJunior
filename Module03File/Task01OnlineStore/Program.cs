using System;
using System.Collections.Generic;

namespace Task01OnlineStore
{
    interface IProduct
    {
        string Name { get; }
    }

    class Good : IProduct
    {
        public string Name { get; }

        public Good(string name)
        {
            Name = name;
        }
    }

    class Warehouse
    {
        private Dictionary<IProduct, int> _goods;

        public Warehouse()
        {
            _goods = new Dictionary<IProduct, int>();
        }

        public void AddGoods(IProduct product, int quantity)
        {
            if (_goods.ContainsKey(product))
            {
                _goods[product] += quantity;
            }
            else
            {
                _goods.Add(product, quantity);
            }
        }

        public void RemoveGoods(IProduct product, int quantity)
        {
            if (_goods.ContainsKey(product))
            {
                _goods[product] -= quantity;

                if (_goods[product] <= 0)
                {
                    _goods.Remove(product);
                }
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

        public int GetQuantity(IProduct product)
        {
            if (_goods.ContainsKey(product))
            {
                return _goods[product];
            }
            else
            {
                return 0;
            }
        }
    }

    class Cart
    {
        private Dictionary<IProduct, int> _items;

        public Cart()
        {
            _items = new Dictionary<IProduct, int>();
        }

        public void AddToCart(IProduct product, int quantity, Warehouse warehouse)
        {
            int availableQuantity = warehouse.GetQuantity(product);

            if (quantity <= availableQuantity)
            {
                if (_items.ContainsKey(product))
                {
                    _items[product] += quantity;
                }
                else
                {
                    _items.Add(product, quantity);
                }

                warehouse.RemoveGoods(product, quantity);
            }
            else
            {
                Console.WriteLine($"Ошибка: Недостаточно товаров на складе. Доступное количество: {availableQuantity}");
            }
        }

        public void Order(Warehouse warehouse)
        {
            foreach (var item in _items)
            {
                warehouse.AddGoods(item.Key, item.Value);
            }

            _items.Clear();

            Console.WriteLine("\nСгенерирована ссылка на платеж\n");
        }

        public void DisplayCartItems()
        {
            Console.WriteLine("\nТовары в корзине:");

            foreach (var item in _items)
            {
                Console.WriteLine($"{item.Key.Name}: {item.Value}");
            }
        }
    }

    class Store
    {
        private Warehouse _warehouse;
        private Cart _cart;

        public Store()
        {
            _warehouse = new Warehouse();
            _cart = new Cart();
        }

        public void Work()
        {
            IProduct iPhone12 = new Good("IPhone 12");
            IProduct iPhone11 = new Good("IPhone 11");

            _warehouse.AddGoods(iPhone12, 10);
            _warehouse.AddGoods(iPhone11, 1);
            _warehouse.DisplayGoods();

            _cart.AddToCart(iPhone12, 4, _warehouse);
            _cart.AddToCart(iPhone11, 3, _warehouse);
            _cart.AddToCart(iPhone12, 9, _warehouse);
            _cart.DisplayCartItems();

            _cart.Order(_warehouse);

            _warehouse.DisplayGoods();
            _cart.DisplayCartItems();

            Console.ReadLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Store store = new Store();
            store.Work();
        }
    }
}
