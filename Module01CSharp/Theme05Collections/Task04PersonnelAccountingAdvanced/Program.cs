using System;
using System.Collections.Generic;

namespace Task04PersonnelAccountingAdvanced
{
    internal class Program
    {
        static void Main()
        {
            const string CommandAddDossier = "1";
            const string CommandShowDossiers = "2";
            const string CommandDeleteDossier = "3";
            const string CommandExit = "4";

            Dictionary<int, (string, string)> dossiers = new Dictionary<int, (string, string)>();
            int maxDossierID = 0;

            string input;

            bool isWork = true;

            while (isWork)
            {
                Console.Clear();
                Console.Write(
                                $" - = == МЕНЮ == = -" +
                                $"\n[{CommandAddDossier}] Добавить досье" +
                                $"\n[{CommandShowDossiers}] Вывести все досье" +
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
                        AddDossier(dossiers, ref maxDossierID);
                        break;

                    case CommandShowDossiers:
                        ShowDossiers(dossiers);
                        break;

                    case CommandDeleteDossier:
                        DeleteDossier(dossiers);
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

        private static void AddDossier(Dictionary<int, (string, string)> dossiers, ref int maxDossierID)
        {   
            string fullNames = string.Empty;
            string posts = string.Empty;

            Console.Write("Введите ФИО: ");
            fullNames = Console.ReadLine();
            Console.Write("Введите должность: ");
            posts = Console.ReadLine();

            dossiers.Add(maxDossierID++, (fullNames, posts));

            Console.WriteLine("\nДосье добавлено. Нажмите любую клавишу для возвращения в меню");
        }

        private static void DeleteDossier(Dictionary<int, (string, string)> dossiers)
        {
            ShowDossiers(dossiers);

            if (dossiers.Count > 0)
            {
                Console.Write("\nВведите номер досье для удаления: ");

                if (int.TryParse(Console.ReadLine(), out int dossierID) && dossiers.ContainsKey(dossierID))
                {
                    dossiers.Remove(dossierID);

                    Console.WriteLine("\nДосье удалено. Нажмите любую клавишу для возвращения в меню");
                }
                else
                {
                    Console.WriteLine("\nНеверный номер досье. Нажмите любую клавишу для возвращения в меню");
                }
            }
        }

        private static void ShowDossiers(Dictionary<int, (string, string)> dossiers)
        {
            if (dossiers.Count > 0)
            {
                foreach (var dossier in dossiers)
                {
                    Console.WriteLine($"[{dossier.Key}] ФИО: {dossier.Value.Item1}" +
                                          $"\n    Должность: {dossier.Value.Item2}");
                }
            }
            else
            {
                Console.WriteLine("Список досье пуст. Нажмите любую клавишу для возвращения в меню");
            }
        }
    }
}