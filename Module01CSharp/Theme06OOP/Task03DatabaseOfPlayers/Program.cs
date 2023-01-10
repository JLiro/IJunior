using System;
using System.Collections.Generic;

namespace Task03DatabaseOfPlayers
{
    internal class Program
    {
        static void Main()
        {
            const string CommandIncrease = "1";
            const string CommandChange = "2";
            const string CommandDecrease = "3";
            const string CommandShow = "4"; 
            const string CommandExit = "5";

            Database database = new Database();

            string input;

            bool isWork = true;

            while (isWork)
            {
                Console.CursorVisible = true;

                Console.Clear();
                Console.Write(
                                $"  - = == МЕНЮ == = -" +
                                $"\n [{CommandIncrease}] Добавить игрока" +
                                $"\n [{CommandChange}] Заблокировать / Разблокировать игрока" +
                                $"\n [{CommandDecrease}] Удалить игрока" +
                                $"\n [{CommandShow}] Показать всех игроков" +
                                $"\n [{CommandExit}] Выход" +
                                $"\n" +
                                $"\n > Введите команду: "
                             );

                input = Console.ReadLine();

                Console.Clear();

                switch (input)
                {
                    case CommandIncrease:
                        database.Increase();
                        break;

                    case CommandChange:
                        database.Change();
                        break;

                    case CommandDecrease:
                        database.Decrease();
                        break;

                    case CommandShow:
                        database.Show();
                        break;

                    case CommandExit:
                        isWork = false;
                        continue;

                    default:
                        Console.Write(" > Неверная команда. Нажмите любую клавишу для возвращения в меню");
                        break;
                }
            }
        }
    }

    class Database
    {
        private int _playerID;

        public List<Player> _players = new List<Player>();

        public void Show()
        {
            ShowPlayers();
            ShowResultOfMethod("Выведен список всех игроков");
        }

        private void ShowResultOfMethod(string input)
        {
            Console.CursorVisible = false;

            Console.Write("\n"
                        + "──?──?──?──?──?──?───?───?──?──?──?──?──?──"
                        + "\n < > "
                        + input
                        + "\n"
                        + "\n < Нажмите любую клавишу, чтобы вернуться в меню"); 
            Console.ReadKey();
        }

        private void ShowPlayers()
        {
            foreach (Player player in _players)
            {
                player.ShowInfo();
            }

            Console.WriteLine("──────────────────── ? ────────────────────");
        }

        public void Increase()
        {
            ShowPlayers();

            Console.Write(" > Введите никнейм нового игрока: ");
            _players.Add(new Player(_players.Count, Console.ReadLine()));

            ShowResultOfMethod("Новый игрок добавлен");
        }

        public void Decrease()
        {
            ShowPlayers();

            Console.Write(" > Введите ID для удаления игрока: ");

            if (int.TryParse(Console.ReadLine(), out _playerID) && _playerID >= 0 && _playerID <= _players.Count)
            {
                _players.RemoveAt(_playerID);

                ShowResultOfMethod("Игрок с данным ID удалён");
            }
            else
            {
                ShowResultOfMethod("Игрок с данным ID не найден");
            }
        }

        public void Change()
        {
            string banStatus   = "заблокирован";
            string unbanStatus = "разблокирован";

            ShowPlayers();

            Console.Write($" > Введите ID игрока для смены статуса на {banStatus} / {unbanStatus}: ");

            if(int.TryParse(Console.ReadLine(), out _playerID) && _playerID >= 0 && _playerID <= _players.Count)
            {
                if(_players[_playerID].IsBanned == false)
                {
                    ValidateBanStatusChange(_playerID, unbanStatus, banStatus);
                }
                else
                {
                    ValidateBanStatusChange(_playerID, banStatus, unbanStatus);
                }
            }
            else
            {
                ShowResultOfMethod("Игрок с данным ID не найден");
            }
        }

        private void ValidateBanStatusChange(int playerID, string currentBanStatus, string newBanStatus)
        {
            Console.Write($"\n > Игрок сейчас {currentBanStatus}. Вы точно хотите изменить его статус на {newBanStatus}?" +
                           "\n > Введите ID повторно, для подверждения: ");

            if (int.TryParse(Console.ReadLine(), out _playerID) && _playerID == playerID)
            {
                _players[playerID].InvertBanStatus();
                ShowResultOfMethod($"Игрок {newBanStatus}");
            }
            else
            {
                ShowResultOfMethod($"Статус игрока не изменился, он {currentBanStatus}");
            }
        }
    }

    class Player
    {
        private int _baseLevel = 1;
        
        private int _id;
        private string _nickname;
        private int _level;
        public bool IsBanned { private set; get; }

        public Player(int id, string nickname)
        {
            _id = id;
            _nickname = nickname;
            _level = _baseLevel;
            IsBanned = false;
        }

        public void InvertBanStatus()
        {
            IsBanned = !IsBanned;
        }

        public void ShowInfo()
        {
            string banStatus;

            banStatus = IsBanned ? "Заблокирован" : "Разблокирован";

            Console.WriteLine($"[{_id}] Псевдоним: {_nickname}"
                            + $"\n      Уровень: {_level}"
                            + $"\n       Статус: {banStatus}"
                            +  "\n");
        }
    }
}
