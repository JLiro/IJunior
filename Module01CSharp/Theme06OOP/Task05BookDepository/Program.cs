using System;
using System.Collections.Generic;

namespace Task05BookDepository
{
    internal class Program
    {
        static void Main()
        {
            {
                const string CommandAddBook = "1";
                const string CommandDeleteBook = "2";
                const string CommandShowAllBooks = "3";
                const string CommandSearchBooks = "4";
                const string CommandExit = "5";

                Library library = new Library();

                bool isWorking = true;

                while (isWorking)
                {
                    Console.Clear();
                    Console.Write(" - = == МЕНЮ == = -"
                               + $"\n[{CommandAddBook}] Добавить книгу"
                               + $"\n[{CommandDeleteBook}] Удалить книгу"
                               + $"\n[{CommandShowAllBooks}] Показать все книги"
                               + $"\n[{CommandSearchBooks}] Поиск книг"
                               + $"\n[{CommandExit}] Выход"
                               +  "\n"
                               + $"\nВведите команду: ");
                    string input = Console.ReadLine();
                    Console.WriteLine();

                    switch (input)
                    {
                        case CommandAddBook:
                            library.AddBook();
                            break;

                        case CommandDeleteBook:
                            library.DeleteBook();
                            break;

                        case CommandShowAllBooks:
                            library.ShowBooks();
                            break;

                        case CommandSearchBooks:
                            library.SearchBooks();
                            break;

                        case CommandExit:
                            isWorking = false;
                            break;
                    }

                    Console.ReadLine();
                }
            }
        }

        internal class Library
        {
            private List<Book> _books = new List<Book>();

            public void AddBook()
            {
                Console.Write("Введите название: ");
                string title = Console.ReadLine();

                Console.Write("Введите автора: ");
                string author = Console.ReadLine();

                Console.Write("Введите год выпуска: ");
                Int32.TryParse(Console.ReadLine(), out int releaseYear);

                _books.Add(new Book(title, author, releaseYear));
                Console.WriteLine("Книга успешно добавлена");
            }

            public void DeleteBook()
            {
                ShowBooks();

                Console.Write("\nВведите номер для удаления: ");
                string input = Console.ReadLine();

                if (Int32.TryParse(input, out int value))
                {
                    if (value > 0 && value <= _books.Count)
                    {
                        _books.RemoveAt(value - 1);
                        ShowBooks();
                        Console.WriteLine("\nКнига успешно удалена");
                    }
                    else
                    {
                        Console.WriteLine("Неверный номер книги");
                    }
                }
                else
                {
                    Console.WriteLine("Неверный номер книги");
                }
            }

            public void SearchBooks()
            {
                const string CommandSearchByTitle = "1";
                const string CommandSearchByAuthor = "2";
                const string CommandSearchByReleaseYear = "3";

                Console.Clear();
                Console.Write(" - = == МЕНЮ == = -"
                            + $"\n[{CommandSearchByTitle}] Поиск по названию"
                            + $"\n[{CommandSearchByAuthor}] Поиск по автору"
                            + $"\n[{CommandSearchByReleaseYear}] Поиск по году выпуска"
                            +  "\n"
                            + $"\nВведите команду: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case CommandSearchByTitle:
                        SearchBooksByTitle();
                        break;
                    case CommandSearchByAuthor:
                        SearchBooksByAuthor();
                        break;
                    case CommandSearchByReleaseYear:
                        SearchBooksByReleaseYear();
                        break;
                }
            }

            public void ShowBooks()
            {
                for (int i = 0; i < _books.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {_books[i].GetInfo()}");
                }
            }

            private void SearchBooksByTitle()
            {
                string output = string.Empty;

                Console.Write($"Введите название: ");
                string input = Console.ReadLine();
                Console.WriteLine();

                foreach (Book book in _books)
                {
                    if (book.Title.ToLower().Contains(input.ToLower()))
                    {
                        output += book.GetInfo() + "\n";
                    }
                }

                output = output == string.Empty ? "Книг с таким названием не найдено" : output;
                Console.WriteLine(output);
            }

            private void SearchBooksByAuthor()
            {
                string output = string.Empty;

                Console.Write($"Введите автора: ");
                string input = Console.ReadLine();

                foreach (Book book in _books)
                {
                    if (book.Author.ToLower().Contains(input.ToLower()))
                    {
                        Console.WriteLine(book.GetInfo());
                    }
                }

                output = output == string.Empty ? "Книг с таким названием не найдено" : output;
                Console.WriteLine(output);
            }

            private void SearchBooksByReleaseYear()
            {
                string output = string.Empty;

                Console.Write($"Введите год выпуска: ");
                Int32.TryParse(Console.ReadLine(), out int input);

                foreach (Book book in _books)
                {
                    if (book.ReleaseYear == input)
                    {
                        Console.WriteLine(book.GetInfo());
                    }
                }

                output = output == string.Empty ? "Книг с такой датой издания не найдено" : output;
                Console.WriteLine(output);
            }
        }

        internal class Book
        {
            public string Title { get; private set; }
            public string Author { get; private set; }
            public int ReleaseYear { get; private set; }

            public Book(string title, string author, int releaseYear)
            {
                Title = title;
                Author = author;
                ReleaseYear = releaseYear;
            }

            public string GetInfo()
            {
                string output = ReleaseYear == 0 ? "Год издания не указан" : $"Впервые оно было опубликовано в {ReleaseYear}г.";

                return $"Произведение <<{Title}>> написал {Author}. {output}";
            }

        }
    }
}