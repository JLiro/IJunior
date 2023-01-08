using System;
using System.Collections.Generic;

namespace Task01WorkingWithClass
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player("Игрок", 100, 100, 25);

            player.ShowInfo();

            Console.ReadKey();
        }
    }

    class Player
    {
        private string _name;
        private int _health;
        private int _armor;
        private int _damage;

        public Player(string name, int health, int armor, int damage)
        {
            _name = name;
            _health = health;
            _armor = armor;
            _damage = damage;
        }

        public void ShowInfo()
        {
            Console.Write
            (
                $"Имя игрока: {_name}" +
                $"\nЗдоровье игрока: {_health}" +
                $"\nБроня игрока: {_armor}" +
                $"\nУрон игрока: {_damage}"
            );
        }
    }
}
