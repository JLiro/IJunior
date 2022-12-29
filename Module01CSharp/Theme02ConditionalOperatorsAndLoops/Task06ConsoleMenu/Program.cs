using System;

namespace Task06ConsoleMenu
{
    internal class Program
    {
        static void Main()
        {
            string userName = string.Empty;
            string userPassword = string.Empty;

            string userCommand = string.Empty;

            const string exitCommand = "Esc";
            const string backCommand = "Back";
            const string setNameCommand = "SetName";
            const string changeTextColorCommand = "ChangeTextColor";
            const string setPasswordCommand = "SetPassword";
            const string writeNameCommand = "WriteName";

            const string redColorCommand = "Red";
            const string blueColorCommand = "Blue";
            const string greenColorCommand = "Green";

            bool isProgramRanning = true;
            bool isСorrectPassword = true;

            while (isProgramRanning)
            {
                Console.Write(
                    "             ДОСТУПНЫЕ КОМАНДЫ\n\n" +
                    "              SetName – установить имя\n" +
                    "      ChangeTextColor - изменить цвет текста в консоли\n" +
                    "          SetPassword – установить пароль\n" +
                    "            WriteName – вывести имя(после ввода пароля)\n" +
                    "                  Esc – выход из программы\n");
                Console.Write("\n=========================================0" +
                              "\nВведите команду: "); ;
                userCommand = Console.ReadLine();

                switch (userCommand)
                {
                    case setNameCommand:
                        Console.Write("Введите Ваше имя или команду Back чтобы вернуться в меню: ");
                        userCommand = Console.ReadLine();

                        isProgramRanning = userCommand != exitCommand;
                        if (isProgramRanning)
                        {
                            userName = userCommand;
                            Console.Clear();
                            Console.WriteLine("Имя установлено\n");
                        }
                        else Console.Clear();
                        break;

                    case changeTextColorCommand:
                        Console.Write("\nДоступные команды выбора цвета: Red, Blue, Green" +
                                      "\n Введите желаемый цвет консоли: ");
                        userCommand = Console.ReadLine();

                        isProgramRanning = userCommand == exitCommand;
                        if (isProgramRanning) { Console.Clear(); break; }

                        switch (userCommand)
                        {
                            case redColorCommand:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Clear();
                                break;
                            case blueColorCommand:
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Clear();
                                break;
                            case greenColorCommand:
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Clear();
                                break;
                            case backCommand:
                                Console.Clear();
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("Был введён неизвестный параметр. Пожалуйста, попробуйте снова\n");
                                break;
                        }
                        break;

                    case setPasswordCommand:
                        Console.Write("Введите Ваш пароль: ");
                        userCommand = Console.ReadLine();

                            userPassword = userCommand;
                            Console.Clear();
                            Console.WriteLine("Пароль установлен\n");
                        break;

                    case writeNameCommand:
                        Console.Write("Введите пароль: ");
                        userCommand = Console.ReadLine();
                            
                            isСorrectPassword = userCommand == userPassword;
                            if (isСorrectPassword)
                            {
                                Console.Clear();
                                Console.WriteLine($"Ваше имя: {userName}\n");
                                userCommand = backCommand;
                            }
                            else
                            {
                                Console.WriteLine("\nНеверный пароль!\n" +
                                                  "\nПовторите попытку");
                            }
                        break;

                    case exitCommand: return;

                    default:
                        Console.Clear();
                        Console.WriteLine("Неизвестная команда. Пожалуйста, введите корректную команду\n");
                        break;
                }
            }
        }
    }
}

