using System;

namespace Task08PasswordProtectedProgram
{
    internal class Program
    {
        static void Main()
        {
            string password = "secret";
            string secretMessage = "Это секретное сообщение";
            string userText = string.Empty;
            bool isСorrectPassword = false;
            int attemptsCount = 3;

            for (int attemptsCountTemp = attemptsCount; attemptsCountTemp > 0; attemptsCountTemp--)
            {
                Console.Write($"Осталось попыток: {attemptsCountTemp} из {attemptsCount}\n\n" +
                              $"Для доступа к тайному сообщению ведите пароль: ");
                userText = Console.ReadLine();

                isСorrectPassword = userText == password;

                if (isСorrectPassword)
                {
                    Console.WriteLine($"\n{secretMessage}\n\n" +
                                      $"\nНажмите любую клавишу для завершения программы");
                    Console.ReadKey();
                    break;
                }
                else if (isСorrectPassword == false && attemptsCountTemp == 1)
                {
                    Console.WriteLine($"\nКолличество попыток закончилось\n" +
                                      $"Нажмите любую клавишу для выхода");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine($"\nПароль введён не верно. Пожалуйста, попробуйте ещё раз.\n" +
                                      $"Нажмите любую клавишу для новой попытки");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
    }
}
