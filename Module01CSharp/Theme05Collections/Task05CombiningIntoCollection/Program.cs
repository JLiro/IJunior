using System;
using System.Collections.Generic;

namespace Task05CombiningIntoCollection
{
    internal class Program
    {
        static void Main()
        {
            {
                const string FirstArrayName = "1";
                const string SecondArrayName = "2";

                int[] firstArray  = { 1, 2, 3, 4, 4, 4 };
                int[] secondArray = { 5, 2, 6, 2 };
                
                List<int> сollection = new List<int>();

                ShowCollection(firstArray, FirstArrayName);
                ShowCollection(secondArray, SecondArrayName);

                AddUniqueElementsToCollection(сollection, firstArray );
                AddUniqueElementsToCollection(сollection, secondArray);

                ShowCollection(сollection);

                Console.ReadKey();
            }
        }

        static void AddUniqueElementsToCollection(List<int> сollection, int[] array)
        {
            foreach (int element in array)
            {
                if (сollection.Contains(element) == false)
                {
                    сollection.Add(element);
                }
            }
        }

        static void ShowCollection(List<int> сollection)
        {
            string output;
            output = "Полученняа коллекция уникальных элементов из масивов:";

            foreach (int element in сollection)
            {
                output += " " + element;
            }

            Console.Write("\n" + output);
        }

        static void ShowCollection(int[] сollection, string arrayNamber)
        {
            string output;
            output = $"Исходный массив #{arrayNamber}:";

            foreach (int element in сollection)
            {
                output += " " + element;
            }

            Console.WriteLine(output);
        }
    }
}
