using System;
using System.Collections.Generic;
using System.Linq;

namespace Task04TopServerPlayers
{
    public class Player
    {
        public Player(string name, int level, int strength)
        {
            Name = name;
            Level = level;
            Strength = strength;
        }

        public string Name { get; private set; }
        public int Level { get; private set; }
        public int Strength { get; private set; }
    }

    public class Database
    {
        private readonly List<Player> _players;

        public Database()
        {
            _players = new List<Player>()
            {
                new Player("Player 01", 10, 99),
                new Player("Player 02", 20, 40),
                new Player("Player 03", 30, 30),
                new Player("Player 04", 40, 20),
                new Player("Player 05", 50, 10),
                new Player("Player 06", 60, 60),
                new Player("Player 07", 70, 70),
                new Player("Player 08", 80, 80),
                new Player("Player 09", 90, 90),
                new Player("Player 10", 99, 18)
            };
        }

        public void PrintAllPlayersInfo()
        {
            Console.WriteLine("Список всех игроков:");
            PrintPlayers(_players);

            Console.WriteLine("\nСписок топ 3 игроков отсортированных по уровню:");
            PrintPlayers(GetTopPlayersByLevel(3));

            Console.WriteLine("\nСписок топ 3 игроков отсортированных по силе:");
            PrintPlayers(GetTopPlayersByStrength(3));
        }

        private IEnumerable<Player> GetTopPlayersByLevel(int count)
        {
            return _players.OrderByDescending(player => player.Level).Take(count);
        }

        private IEnumerable<Player> GetTopPlayersByStrength(int count)
        {
            return _players.OrderByDescending(player => player.Strength).Take(count);
        }

        private void PrintPlayers(IEnumerable<Player> players)
        {
            foreach (var player in players)
            {
                Console.WriteLine("{0}, {1} уровень, имеет силу {2}", player.Name, player.Level, player.Strength);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();
            database.PrintAllPlayersInfo();
        }
    }
}