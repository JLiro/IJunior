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
            const string CommandSearchDossiersByLastName = "4";
            const string CommandExit = "5";

            string[] fullNames = new string[0];
            string[] posts = new string[0];

            string userText;

            bool isOpen = true;

            while (isOpen)
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

                userText = Console.ReadLine();

                Console.Clear();

                switch (userText)
                {
                    case CommandAddDossier:
                        AddDossier(ref fullNames, ref posts);
                        break;

                    case CommandShowAllDossiers:
                        ShowDossiers(fullNames, posts);
                        break;

                    case CommandDeleteDossier:
                        DeleteDossier(ref fullNames, ref posts);
                        break;

                    case CommandExit:
                        isOpen = false;
                        continue;

                    default:
                        Console.WriteLine("Неверная команда. Нажмите любую клавишу для возвращения в меню");
                        break;
                }

                Console.ReadLine();
            }
        }

        private static void AddDossier(ref string[] fullNames, ref string[] posts)
        {
            Console.Write("Введите ФИО: ");
            Array.Resize(ref fullNames, fullNames.Length + 1);
            fullNames[fullNames.Length - 1] = Console.ReadLine();

            Console.Write("Введите должность: ");
            Array.Resize(ref posts, posts.Length + 1);
            posts[posts.Length - 1] = Console.ReadLine();

            Console.WriteLine("Досье добавлено. Нажмите любую клавишу для возвращения в меню");
        }

        private static void DeleteDossier(ref string[] fullNames, ref string[] posts)
        {
            ShowDossiers(fullNames, posts);

            if (fullNames.Length > 0)
            {
                Console.Write("\nВведите номер досье для удаления: ");
                int dossierNumber = Convert.ToInt32(Console.ReadLine());

                if (dossierNumber <= fullNames.Length && dossierNumber > 0)
                {
                    RemoveElementByIndex(ref fullNames, dossierNumber - 1);
                    RemoveElementByIndex(ref posts, dossierNumber - 1);

                    Console.WriteLine("Досье удалено. Нажмите любую клавишу для возвращения в меню");
                }
                else
                {
                    Console.WriteLine("Неверный номер досье. Нажмите любую клавишу для возвращения в меню");
                }
            }
        }

        private static void RemoveElementByIndex(ref string[] array, int index)
        {
            for (int i = index; i < array.Length - 1; i++)
            {
                array[i] = array[i + 1];
            }

            Array.Resize(ref array, array.Length - 1);
        }

        private static void ShowDossiers(string[] fullNames, string[] posts)
        {
            if (fullNames.Length > 0)
            {
                for (int i = 0; i < fullNames.Length; i++)
                {
                    Console.WriteLine($"[{i + 1}] ФИО: {fullNames[i]}" +
                                  $"\n    Должность: {posts[i]}");

                    if (i < fullNames.Length - 1)
                    {
                        Console.WriteLine();
                    }
                }
            }
            else
            {
                Console.Write("Нет ни одного досье. Нажмите любую клавишу для возвращения в меню");
            }
        }
    }
}