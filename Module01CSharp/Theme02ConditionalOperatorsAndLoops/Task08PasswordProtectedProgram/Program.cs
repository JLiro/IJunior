using System;

namespace Task08PasswordProtectedProgram
{
    internal class Program
    {
        static void Main()
        {
            string password = "secret";
            string userText = string.Empty;
            bool isСorrectPassword;
            int attemptsCount = 3;

            for (int i = attemptsCount; i > 0; i--)
            {
                Console.Write($"Осталось попыток: {i} из 3\n\n" +
                              $"Для доступа к тайному сообщению ведите пароль: ");
                userText = Console.ReadLine();

                isСorrectPassword = userText == password;

                if (isСorrectPassword)
                {
                    Console.WriteLine("\nЭто секретное сообщение\n\n" +
                                     $"\nНажмите любую клавишу для завершения программы");
                    Console.ReadKey();
                    break;
                }
                else
                {
                    Console.WriteLine($"\nПароль введён не верно!\n" +
                                      $"Пожалуйста, попробуйте ещё раз\n" +
                                      $"Нажмите любую клавишу для новой попытки");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            Console.WriteLine($"\nКолличество попыток исчерпано\n" +
                              $"Нажмите любую клавишу для завершения программы\n");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
