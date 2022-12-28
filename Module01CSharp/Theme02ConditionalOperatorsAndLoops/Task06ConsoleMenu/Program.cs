using System;

namespace Task06ConsoleMenu
{
    internal class Program
    {
        static void Main()
        {
            string userName = null,
                   userPassword = null;

            string userCommand = null;


            while (userCommand != "Esc")
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
                    case "SetName":
                        Console.Write("Введите Ваше имя или команду Back чтобы вернуться в меню: ");
                        userCommand = Console.ReadLine();

                        if (userCommand != "Back")
                        {
                            userName = userCommand;
                            Console.Clear();
                            Console.WriteLine("Имя установлено\n");
                        }
                        else Console.Clear();
                        break;

                    case "ChangeTextColor":
                        Console.Write("\nДоступные команды выбора цвета: Red, Blue, Green" +
                                      "\n Введите желаемый цвет консоли: ");
                        userCommand = Console.ReadLine();

                        switch (userCommand)
                        {
                            case "Red":
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Clear();
                                break;
                            case "Blue":
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Clear();
                                break;
                            case "Green":
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Clear();
                                break;
                            case "Back":
                                Console.Clear();
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("Был введён неизвестный параметр. Пожалуйста, попробуйте снова\n");
                                break;
                        }
                        break;

                    case "SetPassword":
                        Console.Write("Введите Ваш пароль или команду Back чтобы вернуться в меню: ");
                        userCommand = Console.ReadLine();

                        if (userCommand != "Back")
                        {
                            userPassword = userCommand;
                            Console.Clear();
                            Console.WriteLine("Пароль установлен\n");
                        }
                        else Console.Clear();
                        break;

                    case "WriteName":
                        while (userCommand != "Back")
                        {
                            Console.Write("Введите Ваш пароль или команду Back чтобы вернуться в меню: ");
                            userCommand = Console.ReadLine();

                            if (userCommand == userPassword)
                            {
                                Console.Clear();
                                Console.WriteLine($"Ваше имя: {userName}\n");
                                userCommand = "Back";
                            }
                            else if (userCommand != "Back")
                            {
                                Console.WriteLine("\nНеверный пароль!\n\n" +
                                                  "Повторите попытку входа или вернитесь в меню с помощью команды Back");
                            }
                            else Console.Clear();
                        }
                        break;

                    case "Esc": return;

                    default:
                        Console.Clear();
                        Console.WriteLine("Неизвестная команда. Пожалуйста, введите корректную команду\n");
                        break;
                }
            }
        }
    }
}

