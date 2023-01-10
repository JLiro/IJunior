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

            Dictionary<string, string> dossiers = new Dictionary<string, string>();

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
                        AddDossier(dossiers);
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

                Console.ReadLine();
            }
        }

        private static void AddDossier(Dictionary<string, string> dossiers)
        {
            string fullNames = string.Empty;
            string posts = string.Empty;

            Console.Write("Введите ФИО: ");
            fullNames = Console.ReadLine();
            Console.Write("Введите должность: ");
            posts = Console.ReadLine();

            dossiers.Add(fullNames, posts);

            Console.WriteLine("\nДосье добавлено. Нажмите любую клавишу для возвращения в меню");
        }

        private static void DeleteDossier(Dictionary<string, string> dossiers)
        {
            int index = 0;
            int dossierNumber;

            ShowDossiers(dossiers);

            if (dossiers.Count > 0)
            {
                Console.Write("\nВведите номер досье для удаления: ");

                if (int.TryParse(Console.ReadLine(), out dossierNumber))
                {
                    if (dossierNumber <= dossiers.Count && dossierNumber > 0)
                    {

                        foreach (var dossier in dossiers)
                        {
                            index++;

                            if (index == dossierNumber)
                            {
                                dossiers.Remove(dossier.Key);
                                break;
                            }    
                        }

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

        private static void ShowDossiers(Dictionary<string, string> dossiers)
        {
            if (dossiers.Count > 0)
            {
                int index = 0;

                foreach (var dossier in dossiers)
                {
                    index++;

                    Console.WriteLine($"[{index}] ФИО: {dossier.Key}" +
                                          $"\n    Должность: {dossier.Value}");
                }

            }
            else
            {
                Console.WriteLine("Список досье пуст. Нажмите любую клавишу для возвращения в меню");
            }
        }
    }
}