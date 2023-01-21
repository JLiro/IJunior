using System;
using System.Collections.Generic;

namespace Task07PassengerTrainConfigurator
{
    internal class Program
    {
        static void Main()
        {
            Station station = new Station();

            station.Begin();
            Console.ReadLine();
        }
    }

    class Route
    {
        private string _startPoint;
        private string _finalPoint;

        public Route(string startPoint, string finalPoint)
        {
            _startPoint = startPoint;
            _finalPoint = finalPoint;
        }

        public string GetRoute()
        {
            return $"из <<{_startPoint}>> в <<{_finalPoint}>>";
        }
    }

    class Station
    {
        private const ConsoleKey ComandExit = ConsoleKey.E;

        private List<Route> _routes = new List<Route>();
        private List<Train> _trains = new List<Train>();

        private int _passengersCount;

        private Random _random = new Random();

        public Station()
        {
            _passengersCount = GetPassengersCount();
        }

        private void ShowRoutes()
        {
            string output = "ОТПРАВЛЕННЫЕ РЕЙСЫ" +
                            "\n";

            for (int i = 0; i < _routes.Count && i < _trains.Count; i++)
            {
                output += $"{i + 1}. По маршруту { _routes[i].GetRoute() } отправлен поезд со следующими параметрами:" +
                          $"\nКоличество пассажиров в поезде: {_passengersCount}" +
                          $"{ _trains[i].GetInfo() }" +
                          $"\n";
            }

            Console.WriteLine(output);
        }

        public void Begin()
        {
            bool isWork = true;

            while (isWork)
            {
                Console.Clear();
                ShowRoutes();

                if (TryСreateRoute())
                {
                    SellTickets();
                    CreateTrain();
                    SendTrain();

                    Console.Write($"\nВведите значение клавиши [{ComandExit}] (на англ. раскладке) для выхода, или любую другую для создания нового маршрута: ");

                    if(Console.ReadKey().Key == ConsoleKey.E)
                    {
                        isWork = false;
                    }
                }
            }
        }

        private int GetPassengersCount()
        {
            int minPassengerCount = 10;
            int maxPassengerCount = 100;

            _passengersCount = _random.Next(minPassengerCount, maxPassengerCount);

            return _passengersCount;
        }

        private bool TryСreateRoute()
        {
            string startPoint = String.Empty;
            string finalPoint = String.Empty;

            Console.Write("[1] СОЗДАНИЕ НАПРАВЛЕНИЯ" +
                          "\n" +
                          "\n" +
                          "Введите пункт отбытия : ");
            startPoint = Console.ReadLine();
            Console.Write("Введите пункт прибытия: ");
            finalPoint = Console.ReadLine();
            Console.WriteLine();

            if (startPoint == String.Empty || finalPoint == String.Empty)
            {
                Console.Clear();
                Console.WriteLine("Не все поля заполнены. Пожалуйста, повторите попытку" +
                                  "\n");
                return false;
            }

            if (startPoint == finalPoint)
            {
                Console.Clear();
                Console.WriteLine("Пункт отбытия и пункт прибытия не могу совпадать. Пожалуйста, повторите попытку" +
                                  "\n");
                return false;
            }

            Console.WriteLine("Направление создано");
            
            _routes.Add(new Route(startPoint, finalPoint));

            return true;
        }

        private string ShowLastRout()
        {
            return _routes[_routes.Count - 1].GetRoute();
        }

        private void SellTickets()
        {
            Console.WriteLine("\n[2] ПРОДАЖА БИЛЕТОВ НА СОЗДАННЫЙ МАРШРУТ" +
                             $"\n\nКолличество пассажиров купивших билет {ShowLastRout()}: {_passengersCount}" +
                              "\n\nБилеты проданы");
        }

        private void CreateTrain()
        {
            Console.WriteLine("\n[3] СОЗДАНИЕ ПОЕЗДА");
            _trains.Add(new Train(_passengersCount));
            Console.WriteLine("\nПоезд создан");
        }

        public void SendTrain()
        {
            Console.WriteLine("\n[4] ОТПРАВКА ПОЕЗДА" +
                              "\n\nНажмите любую клавишу для подтверждения отправки поезда");
            Console.ReadKey();
            Console.WriteLine("\nПоезд отправлен");
        }
    }

    class Train
    {
        private Queue<Wagon> _wagons = new Queue<Wagon>();

        private int _wagonsCount = 0;
        private int _baseWagonCapasity;
        private int _lastWagonCapacity;

        private Random _random = new Random();

        public Train(int passengersCount)
        {
            CreateWagons(passengersCount);
        }

        private void CreateWagons(int passengersCount)
        {
            int minWagonsCount = 3;
            int maxWagonsCount = 9;

            _wagonsCount = _random.Next(minWagonsCount, maxWagonsCount);

            _baseWagonCapasity = passengersCount / _wagonsCount;
            _lastWagonCapacity = _baseWagonCapasity + (passengersCount % _wagonsCount);

            for (int i = 0; i < _wagonsCount - 1; i++)
            {
                _wagons.Enqueue(new Wagon(_baseWagonCapasity));
            }

            _wagons.Enqueue(new Wagon(_lastWagonCapacity));

            Console.WriteLine(GetInfo());
        }

        public string GetInfo()
        {
            return $"\nКоличество   созданных вагонов: {_wagonsCount}" +
                   $"\nВместимость  последнего вагона: {_lastWagonCapacity}" +
                   $"\nВместимость  остальных вагонов: {_baseWagonCapasity}";
        }
    }

    class Wagon
    {
        private int _capacity;

        public Wagon(int capacity)
        {
            _capacity = capacity;
        }
    }
}
