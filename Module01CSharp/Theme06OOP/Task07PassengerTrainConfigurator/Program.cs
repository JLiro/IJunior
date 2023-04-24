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

        public string GetInfo()
        {
            return $"из <<{_startPoint}>> в <<{_finalPoint}>>";
        }
    }

    class Station
    {
        private List<Train> _trains = new List<Train>();

        private int _passengersCount;

        private Random _random = new Random();

        public Station()
        {
            _passengersCount = CreatePassengersCount();
        }

        private void ShowRoutes()
        {
            string output = "ОТПРАВЛЕННЫЕ РЕЙСЫ" +
                            "\n";

            for (int i = 0; i < _trains.Count && i < _trains.Count; i++)
            {
                output += $"{i + 1}. По маршруту { _trains[i].Route.GetInfo() } отправлен поезд со следующими параметрами:" +
                          $"\nКоличество пассажиров в поезде: {_passengersCount}" +
                          $"{ _trains[i].GetInfo() }" +
                          $"\n";
            }

            Console.WriteLine(output);
        }

        public void SendTrain()
        {
            Console.WriteLine("\n[4] ОТПРАВКА ПОЕЗДА" +
                              "\n\nНажмите любую клавишу для подтверждения отправки поезда");
            Console.ReadKey();
            Console.WriteLine("\nПоезд отправлен");
        }

        public void Begin()
        {
            const ConsoleKey CommandExit = ConsoleKey.E;

            bool isWork = true;

            while (isWork)
            {
                Console.Clear();
                ShowRoutes();

                if (TryСreateRoute(out Route route))
                {
                    SalesResult(route);
                    CreateTrain(route);
                    SendTrain();

                    Console.Write($"\nВведите значение клавиши [{CommandExit}] (на англ. раскладке) для выхода, или любую другую для создания нового маршрута: ");

                    if(Console.ReadKey().Key == CommandExit)
                    {
                        isWork = false;
                    }
                }
            }
        }

        private int CreatePassengersCount()
        {
            int minPassengerCount = 10;
            int maxPassengerCount = 100;

            _passengersCount = _random.Next(minPassengerCount, maxPassengerCount);

            return _passengersCount;
        }

        private bool TryСreateRoute(out Route route)
        {
            route = null;

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
                Console.WriteLine("Пункт отбытия и пункт прибытия не могут совпадать. Пожалуйста, повторите попытку" +
                                  "\n");
                return false;
            }

            Console.WriteLine("Направление создано");
            
            route = new Route(startPoint, finalPoint);

            return true;
        }

        private void SalesResult(Route route)
        {
            Console.WriteLine("\n[2] ПРОДАЖА БИЛЕТОВ НА СОЗДАННЫЙ МАРШРУТ" +
                             $"\n\nКолличество пассажиров купивших билет { route.GetInfo() }: {SellTickets()}" +
                              "\n\nБилеты проданы");
        }

        private void CreateTrain(Route route)
        {
            Console.WriteLine("\n[3] СОЗДАНИЕ ПОЕЗДА");
            _trains.Add(new Train(route, _passengersCount));
            Console.WriteLine("\nПоезд создан");
        }

        private int SellTickets()
        {
            return _passengersCount;
        }
    }

    class Train
    {
        private Queue<Wagon> _wagons = new Queue<Wagon>();

        private int _wagonsCount = 0;
        private int _baseWagonCapasity;
        private int _lastWagonCapacity;

        private Random _random = new Random();

        public Train(Route route, int passengersCount)
        {
            Route = route;
            CreateWagons(passengersCount);
        }

        public Route Route { get; private set; }

        private void CreateWagons(int passengersCount)
        {
            int minWagonsCount = 3;
            int maxWagonsCount = 9;

            _wagonsCount = _random.Next(minWagonsCount, maxWagonsCount);

            _baseWagonCapasity = passengersCount / _wagonsCount;
            _lastWagonCapacity = passengersCount % _wagonsCount;

            for (int i = 0; i < _wagonsCount - 1; i++)
            {
                _wagons.Enqueue(new Wagon(_baseWagonCapasity));
            }

            _wagons.Enqueue(new Wagon(_lastWagonCapacity));

            Console.WriteLine(GetInfo());
        }

        public string GetInfo()
        {
            return $"\nКоличество  созданных вагонов: {_wagonsCount}" +
                   $"\nВместимость последнего вагона: {_lastWagonCapacity}" +
                   $"\nВместимость остальных вагонов: {_baseWagonCapasity}";
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
