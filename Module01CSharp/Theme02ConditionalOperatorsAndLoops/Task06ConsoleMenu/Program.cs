using System;

namespace Task06ConsoleMenu
{
    internal class Program
    {
        static void Main()
        {
            const string ExitCommand = "1";
            const string BackCommand = "2";
            const string SetNameCommand = "3";
            const string ChangeTextColorCommand = "4";
            const string SetPasswordCommand = "5";
            const string WriteNameCommand = "6";

            const string RedColorCommand = "Red";
            const string BlueColorCommand = "Blue";
            const string GreenColorCommand = "Green";

            string userName = string.Empty;
            string userPassword = string.Empty;

            string userCommand = string.Empty;

            bool isProgramRanning = true;
            bool isСorrectPassword;
            bool isOpen = true;

            while (isProgramOpen)
            {
                Console.Write(
                    "ДОСТУПНЫЕ КОМАНДЫ\n\n" +
                    $"[{SetNameCommand}] – установить имя\n" +
                    $"[{ChangeTextColorCommand}] - изменить цвет текста в консоли\n" +
                    $"[{SetPasswordCommand}] – установить пароль\n" +
                    $"[{WriteNameCommand}] – вывести имя(после ввода пароля)\n" +
                    $"[{ExitCommand}] – выход из программы\n");
                Console.Write("\n=========================================0" +
                              "\nВведите команду: "); ;
                userCommand = Console.ReadLine();

                switch (userCommand)
                {
                    case SetNameCommand:
                        Console.Write("Введите Ваше имя: ");
                        userCommand = Console.ReadLine();

                            userName = userCommand;
                            Console.Clear();
                            Console.WriteLine("Имя установлено\n");
                        break;

                    case ChangeTextColorCommand:
                        Console.Write($"\nДоступные команды выбора цвета: {RedColorCommand}, {BlueColorCommand}, {GreenColorCommand}" +
                                       "\n Введите желаемый цвет консоли: ");
                        userCommand = Console.ReadLine();

                        switch (userCommand)
                        {
                            case RedColorCommand:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Clear();
                                break;

                            case BlueColorCommand:
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Clear();
                                break;

                            case GreenColorCommand:
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Clear();
                                break;

                            case BackCommand:
                                Console.Clear();
                                break;

                            default:
                                Console.Clear();
                                Console.WriteLine("Был введён неизвестный параметр. Пожалуйста, попробуйте снова\n");
                                break;
                        }
                        break;

                    case SetPasswordCommand:
                        Console.Write("Введите Ваш пароль: ");
                        userCommand = Console.ReadLine();

                        userPassword = userCommand;
                        Console.Clear();
                        Console.WriteLine("Пароль установлен\n");
                        break;

                    case WriteNameCommand:
                        Console.Write("Введите пароль: ");
                        userCommand = Console.ReadLine();

                        Console.Clear();
                        Console.WriteLine($"Ваше имя: {userName}\n");
                        break;

                    case ExitCommand:
                        isOpen = false;
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Неизвестная команда. Пожалуйста, введите корректную команду\n");
                        break;
                }
            }
        }
    }
}

