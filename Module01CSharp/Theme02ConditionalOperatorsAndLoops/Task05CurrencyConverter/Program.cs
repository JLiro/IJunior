using System;

namespace Task05CurrencyConverter
{
    internal class Program
    {
        static void Main()
        {
            const string UsdToRubСommand = "1";
            const string UsdToEurСommand = "2";
            const string EurToRubСommand = "3";
            const string EurToUsdСommand = "4";
            const string RubToUsdСommand = "5";
            const string RubToEurСommand = "6";
            const string ExitСommand = "7";

            float usdBalance = 500;
            float eurBalance = 500;
            float rubBalance = 500;

            float usdToRubCost = 10f;
            float usdToEurCost = 0.5f;
            float eurToRubCost = 20f;
            float eurToUsdCost = 2f;
            float rubToUsdCost = 0.1f;
            float rubToEurCost = 0.2f;

            float currencyCount;
            string userText;

            bool isProgramOpen = true;
            bool isEnoughBalance;

            while (isProgramOpen)
            {
                Console.WriteLine($"Ваш баланс счёта | {usdBalance} USD | {eurBalance} EUR | {rubBalance} RUB |" +
                                $"\n" +
                                $"\n[1] Сконвертировать из валюты USD в валюту RUB" +
                                $"\n[2] Сконвертировать из валюты USD в валюту EUR" +
                                $"\n[3] Сконвертировать из валюты EUR в валюту RUB" +
                                $"\n[4] Сконвертировать из валюты EUR в валюту USD" +
                                $"\n[5] Сконвертировать из валюты RUB в валюту USD" +
                                $"\n[6] Сконвертировать из валюты RUB в валюту EUR" +
                                $"\n[7] Выйти из программы" +
                                $"\n");

                Console.Write("Введите номер команды: ");
                userText = Console.ReadLine();

                switch (userText)
                {
                    case UsdToRubСommand:
                        Console.Write("Введите сколько USD хотите конвертировать в RUB: ");
                        currencyCount = Convert.ToSingle(Console.ReadLine());

                        isEnoughBalance = currencyCount <= usdBalance;
                        if (isEnoughBalance)
                        {
                            usdBalance -= currencyCount;
                            rubBalance += currencyCount * usdToRubCost;
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Ошибка: Недостаточно средств на счёте!\n");
                        }
                        break;

                    case UsdToEurСommand:
                        Console.Write("Введите сколько USD хотите конвертировать в EUR: ");
                        currencyCount = Convert.ToSingle(Console.ReadLine());

                        isEnoughBalance = currencyCount <= usdBalance;
                        if (isEnoughBalance)
                        {
                            usdBalance -= currencyCount;
                            eurBalance += currencyCount * usdToEurCost;
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Ошибка: Недостаточно средств на счёте!\n");
                        }
                        break;

                    case EurToRubСommand:
                        Console.Write("Введите сколько EUR хотите конвертировать в RUB: ");
                        currencyCount = Convert.ToSingle(Console.ReadLine());

                        isEnoughBalance = currencyCount <= eurBalance;
                        if (isEnoughBalance)
                        {
                            eurBalance -= currencyCount;
                            rubBalance += currencyCount * eurToRubCost;
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Ошибка: Недостаточно средств на счёте!\n");
                        }
                        break;

                    case EurToUsdСommand:
                        Console.Write("Введите сколько EUR хотите конвертировать в USD: ");
                        currencyCount = Convert.ToSingle(Console.ReadLine());

                        isEnoughBalance = currencyCount <= eurBalance;
                        if (isEnoughBalance)
                        {
                            eurBalance -= currencyCount;
                            usdBalance += currencyCount * eurToUsdCost;
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Ошибка: Недостаточно средств на счёте!\n");
                        }
                        break;

                    case RubToUsdСommand:
                        Console.Write("Введите сколько RUB хотите конвертировать в USD: ");
                        currencyCount = Convert.ToSingle(Console.ReadLine());

                        isEnoughBalance = currencyCount <= rubBalance;
                        if (isEnoughBalance)
                        {
                            rubBalance -= currencyCount;
                            usdBalance += currencyCount * rubToUsdCost;
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Ошибка: Недостаточно средств на счёте!\n");
                        }
                        break;

                    case RubToEurСommand:
                        Console.Write("Введите сколько RUB хотите конвертировать в EUR: ");
                        currencyCount = Convert.ToSingle(Console.ReadLine());

                        isEnoughBalance = currencyCount <= rubBalance;
                        if (isEnoughBalance)
                        {
                            rubBalance -= currencyCount;
                            eurBalance += currencyCount * rubToEurCost;
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Ошибка: Недостаточно средств на счёте!\n");
                        }
                        break;

                    case ExitСommand:
                        isProgramOpen = false;
                        return;
                }
            }
        }
    }
}
