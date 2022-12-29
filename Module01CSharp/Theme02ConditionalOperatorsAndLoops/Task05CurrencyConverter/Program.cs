using System;

namespace Task05CurrencyConverter
{
    internal class Program
    {
        static void Main()
        {
            float usdBalance = 500,
                  eurBalance = 500,
                  rubBalance = 500;

            float usdToRub = 10f , usdToEur = 0.5f,
                  eurToRub = 20f , eurToUsd = 2f  ,
                  rubToUsd = 0.1f, rubToEur = 0.2f;

            string fromСurrency = null,
                   intoСurrency = null;
            float  currencyCount;

            string userText = null;

            string exitCommand0 = "Exit",
                   exitCommand1 = "exit",
                   exitCommand2 = "EXIT";

            while ((userText != exitCommand0) && (userText != exitCommand1) && (userText != exitCommand2))
            {
                Console.WriteLine($"Ваш баланс счёта | {usdBalance} USD | {eurBalance} EUR | {rubBalance} RUB |\n");

                Console.WriteLine("ВВЕДИТЕ БУКВЕННЫЙ КОД ВАЛЮТЫ ИЛИ EXIT ДЛЯ ВЫХОДА\n");
                Console.Write("Сконвертировать из валюты: ");
                userText = Console.ReadLine();
                
                fromСurrency = userText;
                switch (fromСurrency)
                {
                    case "USD":
                        Console.Write("Сконвертировать  в валюту: ");
                        userText = Console.ReadLine();
                        intoСurrency = userText;
                        Console.WriteLine();

                        if (intoСurrency == "EUR")
                        {
                            Console.Write($"Сколько {fromСurrency} сконвертировать в {intoСurrency}: ");
                            userText = Console.ReadLine();

                            try
                            {
                                currencyCount = Convert.ToSingle(userText);

                                if (currencyCount <= usdBalance)
                                {
                                    usdBalance -= currencyCount;
                                    eurBalance += currencyCount * usdToEur;
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("Ошибка: Недостаточно средств на счёте!\n");
                                }
                            }
                            catch
                            {
                                Console.Clear();
                                Console.WriteLine("Ошибка: Не удалось считать число!\n");
                            }
                        }
                        else if (intoСurrency == "RUB")
                        {
                            Console.Write($"Сколько {fromСurrency} сконвертировать в {intoСurrency}: ");
                            userText = Console.ReadLine();

                            try
                            {
                                currencyCount = Convert.ToSingle(userText);

                                if (currencyCount <= usdBalance)
                                {
                                    usdBalance -= currencyCount;
                                    rubBalance += currencyCount * usdToRub;
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("Ошибка: Недостаточно средств на счёте!\n");
                                }
                            }
                            catch
                            {
                                Console.Clear();
                                Console.WriteLine("Ошибка: Не удалось считать число!\n");
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Ошибка: Неверно введен буквенный код валюты!\n");
                        }
                        break;

                    case "EUR":
                        Console.Write("Сконвертировать  в валюту: ");
                        userText = Console.ReadLine();
                        intoСurrency = userText;
                        Console.WriteLine();

                        if (intoСurrency == "USD")
                        {
                            Console.Write($"Сколько {fromСurrency} сконвертировать в {intoСurrency}: ");
                            userText = Console.ReadLine();

                            try
                            {
                                currencyCount = Convert.ToSingle(userText);

                                if (currencyCount <= eurBalance)
                                {
                                    eurBalance -= currencyCount;
                                    usdBalance += currencyCount * eurToUsd;
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("Ошибка: Недостаточно средств на счёте!\n");
                                }
                            }
                            catch
                            {
                                Console.Clear();
                                Console.WriteLine("Ошибка: Не удалось считать число!\n");
                            }

                        }
                        else if (intoСurrency == "RUB")
                        {
                            Console.Write($"Сколько {fromСurrency} сконвертировать в {intoСurrency}: ");
                            userText = Console.ReadLine();

                            try
                            {
                                currencyCount = Convert.ToSingle(userText);

                                if (currencyCount <= eurBalance)
                                {
                                    eurBalance -= currencyCount;
                                    rubBalance += currencyCount * eurToRub;
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("Ошибка: Недостаточно средств на счёте!\n");
                                }
                            }
                            catch
                            {
                                Console.Clear();
                                Console.WriteLine("Ошибка: Не удалось считать число!\n");
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Ошибка: Неверно введен буквенный код валюты!\n");
                        }
                        break;

                    case "RUB":
                        Console.Write("Сконвертировать  в валюту: ");
                        userText = Console.ReadLine();
                        intoСurrency = userText;
                        Console.WriteLine();

                        if (intoСurrency == "EUR")
                        {
                            Console.Write($"Сколько {fromСurrency} сконвертировать в {intoСurrency}: ");
                            userText = Console.ReadLine();

                            try
                            {
                                currencyCount = Convert.ToSingle(userText);

                                if (currencyCount <= rubBalance)
                                {
                                    rubBalance -= currencyCount;
                                    eurBalance += currencyCount * rubToEur;
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("Ошибка: Недостаточно средств на счёте!\n");
                                }
                            }
                            catch
                            {
                                Console.Clear();
                                Console.WriteLine("Ошибка: Не удалось считать число!\n");
                            }
                        }
                        else if (intoСurrency == "USD")
                        {
                            Console.WriteLine($"Сколько {fromСurrency} сконвертировать в {intoСurrency}: ");
                            userText = Console.ReadLine();

                            try
                            {
                                currencyCount = Convert.ToSingle(userText);

                                if (currencyCount <= rubBalance)
                                {
                                    rubBalance -= currencyCount;
                                    usdBalance += currencyCount * rubToUsd;
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("Ошибка: Недостаточно средств на счёте!\n");
                                }
                            }
                            catch
                            {
                                Console.Clear();
                                Console.WriteLine("Ошибка: Не удалось считать число!\n");
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Ошибка: Неверно введен буквенный код валюты!\n");
                        }
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Ошибка: Неверно введен буквенный код валюты!\n");
                        break;
                }
            }
        }
    }
}
