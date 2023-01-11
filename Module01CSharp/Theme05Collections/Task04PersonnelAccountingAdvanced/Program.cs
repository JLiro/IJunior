using System;
using System.Collections.Generic;

namespace Task04PersonnelAccountingAdvanced
{
    internal class Program
    {
        static void Main()
        {
            const string CommandAddDossier = "1";
            const string CommandShowAllDossiers = "2";
            const string CommandDeleteDossier = "3";
            const string CommandExit = "4";

            List<string> fullNames = new List<string>();
            List<string> posts = new List<string>();

            string input;

            bool isWork = true;

            while (isWork)
            {
                Console.Clear();

                Console.Write(
                                $" - = == МЕНЮ == = -" +
                                $"\n[{CommandAddDossier}] Добавить досье" +
                                $"\n[{CommandShowAllDossiers}] Вывести все досье" +
                                $"\n[{CommandDeleteDossier}] Удалить досье" +
                                $"\n[{CommandExit}] Выход" +
                                $"\n" +
                                $"\nВведите команду: "
                             );

                input = Console.ReadLine();

                Console.Clear();

                switch (input)
                {
                    case CommandAddDossier:
                        AddDossier(fullNames, posts);
                        break;

                    case CommandShowAllDossiers:
                        ShowDossiers(fullNames, posts);
                        break;

                    case CommandDeleteDossier:
                        DeleteDossier(fullNames, posts);
                        break;

                    case CommandExit:
                        isWork = false;
                        continue;

                    default:
                        Console.Write("Неверная команда. Нажмите любую клавишу для возвращения в меню");
                        break;
                }

                Console.ReadKey();
            }
        }

        private static void AddDossier(List<string> fullNames, List<string> posts)
        {
            string tempFullNames = string.Empty;
            string tempPosts = string.Empty;

            Console.Write("Введите ФИО: ");
            tempFullNames = Console.ReadLine();
            Console.Write("Введите должность: ");
            tempPosts = Console.ReadLine();

            fullNames.Add(tempFullNames);
            posts.Add(tempPosts);

            Console.WriteLine("\nДосье добавлено. Нажмите любую клавишу для возвращения в меню");
        }

        private static void DeleteDossier(List<string> fullNames, List<string> posts)
        {
            ShowDossiers(fullNames, posts);

            if (fullNames.Count > 0 && posts.Count > 0)
            {
                Console.Write("\nВведите номер досье для удаления: ");

                if (int.TryParse(Console.ReadLine(), out int dossierNumber))
                {
                    if (dossierNumber <= posts.Count && dossierNumber <= fullNames.Count && dossierNumber > 0)
                    {
                        dossierNumber--;

                        fullNames.RemoveAt(dossierNumber);
                        posts.RemoveAt(dossierNumber);

                        Console.WriteLine("\nДосье удалено. Нажмите любую клавишу для возвращения в меню");
                    }
                    else
                    {
                        Console.WriteLine("\nНеверный номер досье. Нажмите любую клавишу для возвращения в меню");
                    }
                }
                else
                {
                    Console.WriteLine("\nНеверный номер досье. Нажмите любую клавишу для возвращения в меню");
                }
            }
        }

        private static void ShowDossiers(List<string> fullNames, List<string> posts)
        {
            if (fullNames.Count > 0)
            {
                for (int i = 0; i < fullNames.Count; i++)
                {
                    int index = i + 1;
                    Console.WriteLine($"[{index}] ФИО: {fullNames[i]}" +
                                      $"\n    Должность: {posts[i]}");
                }
            }
            else
            {
                Console.Write("Нет ни одного досье. Нажмите любую клавишу для возвращения в меню");
            }
        }
    }
}