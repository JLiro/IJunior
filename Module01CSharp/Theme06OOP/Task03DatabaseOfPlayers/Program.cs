using System;
using System.Collections.Generic;

namespace Task03DatabaseOfPlayers
{
    internal class Program
    {
        static void Main()
        {
            const string CommandAddPlayer = "1";
            const string CommandBanPlayer = "2";
            const string CommandUnbanPlayer = "3";
            const string CommandRemovePlayer = "4";
            const string CommandShowPlayersForMineMethod = "5"; 
            const string CommandExit = "6";

            Database database = new Database();

            string input;

            bool isWork = true;

            while (isWork)
            {
                Console.CursorVisible = true;

                Console.Clear();
                Console.Write(  
                                $"  - = == МЕНЮ == = -" +
                                $"\n [{CommandAddPlayer}] Добавить игрока" +
                                $"\n [{CommandBanPlayer}] Заблокировать  игрока" +
                                $"\n [{CommandUnbanPlayer}] Разблокировать игрока" +
                                $"\n [{CommandRemovePlayer}] Удалить игрока" +
                                $"\n [{CommandShowPlayersForMineMethod}] Показать всех игроков" +
                                $"\n [{CommandExit}] Выход" +
                                 "\n" +
                                $"\n  > Введите команду: "
                             );

                input = Console.ReadLine();

                Console.Clear();

                switch (input)
                {
                    case CommandAddPlayer:
                        database.AddPlayer();
                        break;

                    case CommandBanPlayer:
                        database.BanPlayer();
                        break;

                    case CommandUnbanPlayer:
                        database.UnbanPlayer();
                        break;

                    case CommandRemovePlayer:
                        database.RemovePlayer();
                        break;

                    case CommandShowPlayersForMineMethod:
                        database.ShowPlayersForMineMethod();
                        break;

                    case CommandExit:
                        isWork = false;
                        continue;

                    default:
                        Console.Write("  > Неверная команда. Нажмите любую клавишу для возвращения в меню");
                        break;
                }
            }
        }
    }

    class Database
    {
        private List<Player> _players = new List<Player>();

        public void ShowPlayersForMineMethod()
        {
            ShowPlayers();
            ShowResultOfMethod("Выведен список всех игроков");
        }

        public void AddPlayer()
        {
            ShowPlayers();

            Console.Write("  > Введите никнейм нового игрока: ");
            _players.Add(new Player(_players.Count, Console.ReadLine()));

            ShowResultOfMethod("Новый игрок добавлен");
        }

        public void RemovePlayer()
        {
            ShowPlayers();

            Console.Write("  > Введите ID для удаления игрока: ");
            bool isCorrectID = TryGetPlayer(out Player player);

            if (isCorrectID)
            {
                _players.Remove(player);

                ShowResultOfMethod("Игрок с данным ID удалён");
            }
            else
            {
                ShowResultOfMethod("Игрок с данным ID не найден");
            }
        }

        public void BanPlayer()
        {
            ShowPlayers();

            Console.Write($"  > Введите ID игрока для смены статуса на заблокирован: ");
            bool isCorrectID = TryGetPlayer(out Player player);

            if (isCorrectID)
            {
                if (player.IsBanned == false)
                {
                    player.Banned();
                    ShowResultOfMethod("Игрок заблокирован");
                }
                else
                {
                    ShowResultOfMethod("Статус не изменился, т.к. игрок был заблокирован ранее");
                }
            }
            else
            {
                ShowResultOfMethod("Игрок с данным ID не найден");
            }
        }

        public void UnbanPlayer()
        {
            ShowPlayers();

            Console.Write($"  > Введите ID игрока для смены статуса на разблокирован: ");
            bool isCorrectID = TryGetPlayer(out Player player);

            if (isCorrectID)
            {
                if (player.IsBanned == true)
                {
                    player.Unbanned();
                    ShowResultOfMethod("Игрок разблокирован");
                }
                else
                {
                    ShowResultOfMethod("Статус не изменился, т.к. игрок был разблокирован ранее");
                }
            }
            else
            {
                ShowResultOfMethod("Игрок с данным ID не найден");
            }
        }

        private bool TryGetPlayer(out Player player)
        {
            player = null;
            bool isCorrectID = int.TryParse(Console.ReadLine(), out int inputID) && inputID >= 0 && inputID < _players.Count;

            if(isCorrectID)
            {
                foreach (Player tempPlayer in _players)
                {
                    if (tempPlayer.ID == inputID)
                    {
                        player = tempPlayer;
                        isCorrectID  = true;
                    }
                }
            }

            return isCorrectID;
        }

        private void ShowResultOfMethod(string input)
        {
            Console.CursorVisible = false;

            Console.Write("\n"
                        + "───?──?──?──?──?──?───?───?──?──?──?──?──?───"
                        + "\n  < > "
                        + input
                        + "\n"
                        + "\n  < Нажмите любую клавишу, чтобы вернуться в меню");
            Console.ReadKey();
        }

        private void ShowPlayers()
        {
            foreach (Player player in _players)
            {
                player.ShowInfo();
            }

            Console.WriteLine("───────────────────── ? ─────────────────────");
        }
    }

    class Player
    {
        private int _baseLevel = 1;
        
        private string _nickname;
        private int _level;

        public Player(int id, string nickname)
        {
            ID = id;
            _nickname = nickname;
            _level = _baseLevel;
            IsBanned = false;
        }

        public int ID { get; private set; }
        public bool IsBanned { get; private set; }

        public void Banned()
        {
            IsBanned = true;
        }

        public void Unbanned()
        {
            IsBanned = false;
        }

        public void ShowInfo()
        {
            string banStatus;

            banStatus = IsBanned ? "Заблокирован" : "Разблокирован";

            Console.WriteLine($"  [{ID}] Псевдоним: {_nickname}"
                            + $"\n        Уровень: {_level}"
                            + $"\n         Статус: {banStatus}"
                            +  "\n");
        }
    }
}
