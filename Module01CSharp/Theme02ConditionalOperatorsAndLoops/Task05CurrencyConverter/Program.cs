using System;

namespace Task05CurrencyConverter
{
    internal class Program
    {
        static void Main()
        {
            CurrencyConverter currencyConverter = new CurrencyConverter();
            currencyConverter.Start();
        }
    }

    class CurrencyConverter
    {
        private const string UsdToRubСommand = "1";
        private const string UsdToEurСommand = "2";
        private const string EurToRubСommand = "3";
        private const string EurToUsdСommand = "4";
        private const string RubToUsdСommand = "5";
        private const string RubToEurСommand = "6";
        private const string ExitСommand = "7";

        private float usdBalance = 500;
        private float eurBalance = 500;
        private float rubBalance = 500;

        private float usdToRubCost = 10f;
        private float usdToEurCost = 0.5f;
        private float eurToRubCost = 20f;
        private float eurToUsdCost = 2f;
        private float rubToUsdCost = 0.1f;
        private float rubToEurCost = 0.2f;

        public void Start()
        {
            string userText;

            bool isProgramRunning = true;

            while (isProgramRunning)
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
                        СonvertСurrency(ref usdBalance, ref rubBalance, usdToRubCost);
                        break;
                    case UsdToEurСommand:
                        Console.Write("Введите сколько USD хотите конвертировать в EUR: ");
                        СonvertСurrency(ref usdBalance, ref eurBalance, usdToEurCost);
                        break;
                    case EurToRubСommand:
                        Console.Write("Введите сколько EUR хотите конвертировать в RUB: ");
                        СonvertСurrency(ref eurBalance, ref rubBalance, eurToRubCost);
                        break;
                    case EurToUsdСommand:
                        Console.Write("Введите сколько EUR хотите конвертировать в USD: ");
                        СonvertСurrency(ref eurBalance, ref usdBalance, eurToUsdCost);
                        break;
                    case RubToUsdСommand:
                        Console.Write("Введите сколько RUB хотите конвертировать в USD: ");
                        СonvertСurrency(ref rubBalance, ref usdBalance, rubToUsdCost);
                        break;
                    case RubToEurСommand:
                        Console.Write("Введите сколько RUB хотите конвертировать в EUR: ");
                        СonvertСurrency(ref rubBalance, ref eurBalance, rubToEurCost);
                        break;
                    case ExitСommand: return;
                }

                isProgramRunning = userText.ToLower() != ExitСommand;
            }
        }

        private void СonvertСurrency(ref float fromCurrencyBalance, ref float toCurrencyBalance, float currencyCost)
        {
            float currencyCount = Convert.ToSingle(Console.ReadLine());

            bool isEnoughBalance = currencyCount <= fromCurrencyBalance;
            if (isEnoughBalance)
            {
                fromCurrencyBalance -= currencyCount;
                toCurrencyBalance += currencyCount * currencyCost;
                Console.Clear();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Ошибка: Недостаточно средств на счёте!\n");
            }
        }
    }
}
