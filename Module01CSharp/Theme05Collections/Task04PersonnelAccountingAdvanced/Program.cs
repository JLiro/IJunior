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

            string[] fullNamess = new string[0];
            string[] postss = new string[0];

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
                        Console.WriteLine("Неверная команда. Нажмите любую клавишу для возвращения в меню");
                        break;
                }

                Console.ReadLine();
            }
        }

        private static void AddDossier(List<string> fullNames, List<string> posts)
        {
            Console.Write("Введите ФИО: ");
            fullNames.Add(Console.ReadLine());

            Console.Write("Введите должность: ");
            posts.Add(Console.ReadLine());

            Console.WriteLine("\nДосье добавлено. Нажмите любую клавишу для возвращения в меню");
        }

        private static void DeleteDossier(List<string> fullNames, List<string> posts)
        {
            int dossierNumber;

            ShowDossiers(fullNames, posts);

            if (fullNames.Count > 0)
            {
                Console.Write("\nВведите номер досье для удаления: ");

                if (int.TryParse(Console.ReadLine(), out dossierNumber) && dossierNumber <= fullNames.Count && dossierNumber >= 0)
                {
                    fullNames.RemoveAt(dossierNumber);
                    posts.RemoveAt(dossierNumber);

                    Console.WriteLine("Досье удалено. Нажмите любую клавишу для возвращения в меню");
                }
                else
                {
                    Console.WriteLine("Неверный номер досье. Нажмите любую клавишу для возвращения в меню");
                }
            }
        }

        private static void ShowDossiers(List<string> fullNames, List<string> posts)
        {
            if (fullNames.Count > 0)
            {
                for (int i = 0; i < fullNames.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] ФИО: {fullNames[i]}" +
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