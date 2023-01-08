using System;

namespace Task01PersonnelRecords
{
    internal class Program
    {
        static void Main()
        {
            const string CommandAddDossier = "1";
            const string CommandShowAllDossiers = "2";
            const string CommandDeleteDossier = "3";
            const string CommandShowSearchResult = "4";
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
                                $"\n[{CommandShowSearchResult}] Поиск досье по фамилии" +
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

                    case CommandShowSearchResult:
                        ShowByLastName(fullNames, posts);
                        break;

                    case CommandExit:
                        isOpen = false;
                        break;

                    default:
                        Console.WriteLine("Неверная команда. Нажмите любую клавишу для возвращения в меню");
                        break;
                }

                Console.ReadLine();
            }
        }

        private static void AddDossier(ref string[] fullNames, ref string[] posts)
        {
            string newFullName;
            string newPost;

            Console.Write("Введите ФИО: ");
            newFullName = Console.ReadLine();
            EncreaseArray(ref fullNames, newFullName);

            Console.Write("Введите должность: ");
            newPost = Console.ReadLine();
            EncreaseArray(ref posts, newPost);

            Console.WriteLine("\nДосье добавлено. Нажмите любую клавишу для возвращения в меню");
        }

        static void EncreaseArray(ref string[] array, string information)
        {
            string[] tempArray = new string[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
            {
                tempArray[i] = array[i];
            }

            tempArray[tempArray.Length - 1] = information;
            array = tempArray;
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
                    DecreaseArray(ref fullNames, dossierNumber - 1);
                    DecreaseArray(ref posts, dossierNumber - 1);

                    Console.WriteLine("Досье удалено. Нажмите любую клавишу для возвращения в меню");
                }
                else
                {
                    Console.WriteLine("Неверный номер досье. Нажмите любую клавишу для возвращения в меню");
                }
            }
        }

        private static void DecreaseArray(ref string[] array, int index)
        {
            string[] tempArray = new string[array.Length - 1];

            for (int i = 0; i < index; i++)
            {
                tempArray[i] = array[i];
            }

            for (int i = index; i < tempArray.Length; i++)
            {
                tempArray[i] = array[i + 1];
            }

            array = tempArray;
        }

        private static void ShowByLastName(string[] fullNames, string[] posts)
        {
            if (fullNames.Length > 0)
            {
                string[] subStrings;
                int dosiersCount = 0;

                string output = String.Empty;
                string input = String.Empty;

                Console.Write("Введите фамилию: ");
                input = Console.ReadLine().ToLower();

                for (int i = 0; i < fullNames.Length; i++)
                {
                    subStrings = fullNames[i].Split(' ');

                    if (subStrings[0].ToLower() == input)
                    {
                        Console.WriteLine($"[{++dosiersCount}] ФИО: {fullNames[i]}" +
                                         $"\n    Должность: {posts[i]}" +
                                         $"\n" +
                                         $"\n");
                    }
                    else if (dosiersCount == 0)
                    {
                        Console.WriteLine("Не найдено ни одного досье. Нажмите любую клавишу для возвращения в меню");
                    }
                }
            }
            else
            {
                Console.WriteLine("Нет ни одного досье. Нажмите любую клавишу для возвращения в меню");
            }
        }

        private static void ShowDossiers(string[] fullNames, string[] posts)
        {
            if (fullNames.Length > 0)
            {
                for (int i = 0; i < fullNames.Length; i++)
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