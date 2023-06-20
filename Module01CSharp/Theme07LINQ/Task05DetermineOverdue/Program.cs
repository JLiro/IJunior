using System;
using System.Collections.Generic;
using System.Linq;

namespace Task05DetermineOverdue
{
    public class CannedFood
    {
        public CannedFood(string name, int productionYear, int shelfLife)
        {
            Name = name;
            ProductionYear = productionYear;
            ShelfLife = shelfLife;
        }

        public string Name { get; private set; }
        public int ProductionYear { get; private set; }
        public int ShelfLife { get; private set; }
    }

    public class Database
    {
        private readonly List<CannedFood> _cannedFoods;

        public Database()
        {
            _cannedFoods = new List<CannedFood>()
            {
                new CannedFood("Beef stew", 2020, 2),
                new CannedFood("Vegetable soup", 2019, 3),
                new CannedFood("Chicken and dumplings", 2021, 2),
                new CannedFood("Pork and beans", 2018, 4),
                new CannedFood("Spaghetti and meatballs", 2022, 2),
                new CannedFood("Chili con carne", 2017, 5),
                new CannedFood("Beef ravioli", 2021, 2),
                new CannedFood("Chicken noodle soup", 2018, 4),
                new CannedFood("Tomato soup", 2020, 3),
                new CannedFood("Corned beef hash", 2019, 4)
            };
        }

        public void PrintExpiredCannedFoods()
        {
            Console.WriteLine("Список всех просроченных банок тушенки:");

            var expiredFoods = _cannedFoods.Where(food => (DateTime.Now.Year - food.ProductionYear) > food.ShelfLife);

            PrintCannedFoods(expiredFoods);
        }

        private void PrintCannedFoods(IEnumerable<CannedFood> cannedFoods)
        {
            foreach (var food in cannedFoods)
            {
                Console.WriteLine("{0} ({1}), произведено в {2} году, срок годности {3} года / лет", food.Name, (DateTime.Now.Year - food.ProductionYear), food.ProductionYear, food.ShelfLife);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();
            database.PrintExpiredCannedFoods();
        }
    }
}