namespace Logging
{
    interface ILogger
    {
        void WriteError(string message);
    }

    class FileLogger : ILogger
    {
        public void WriteError(string message)
        {
            File.WriteAllText("log.txt", message);
        }
    }

    class ConsoleLogger : ILogger
    {
        public void WriteError(string message)
        {
            Console.WriteLine(message);
        }
    }

    class FridayFileLogger : ILogger
    {
        public void WriteError(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                File.WriteAllText("log.txt", message);
            }
        }
    }

    class FridayConsoleLogger : ILogger
    {
        public void WriteError(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                Console.WriteLine(message);
            }
        }
    }

    class FridayDualLogger : ILogger
    {
        private readonly FileLogger _fileLogger;
        private readonly ConsoleLogger _consoleLogger;

        public FridayDualLogger()
        {
            _fileLogger = new FileLogger();
            _consoleLogger = new ConsoleLogger();
        }

        public void WriteError(string message)
        {
            _consoleLogger.WriteError(message);

            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                _fileLogger.WriteError(message);
            }
        }
    }

    class Pathfinder
    {
        private readonly ILogger _logger;

        public Pathfinder(ILogger logger)
        {
            _logger = logger;
        }

        public void Find()
        {
            _logger.WriteError("Во время поиска произошла ошибка");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var fileLogger = new FileLogger();
            var consoleLogger = new ConsoleLogger();
            var fridayFileLogger = new FridayFileLogger();
            var fridayConsoleLogger = new FridayConsoleLogger();
            var fridayDualLogger = new FridayDualLogger();

            var pathfinder1 = new Pathfinder(fileLogger);
            var pathfinder2 = new Pathfinder(consoleLogger);
            var pathfinder3 = new Pathfinder(fridayFileLogger);
            var pathfinder4 = new Pathfinder(fridayConsoleLogger);
            var pathfinder5 = new Pathfinder(fridayDualLogger);

            pathfinder1.Find();
            pathfinder2.Find();
            pathfinder3.Find();
            pathfinder4.Find();
            pathfinder5.Find();

            Console.ReadLine();
        }
    }
}
