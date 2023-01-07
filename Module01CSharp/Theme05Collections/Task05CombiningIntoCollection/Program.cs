using System;
using System.Collections.Generic;

namespace Task05CombiningIntoCollection
{
    internal class Program
    {
        static void Main()
        {
            {
                int[] firstArray =  { 1, 2, 3, 4, 4, 4 };
                int[] secondArray = { 5, 2, 6, 2 };
                List<int> сollection = new List<int>();

                AddUniqueElementsToCollection(сollection, firstArray );
                AddUniqueElementsToCollection(сollection, secondArray);

                Console.Write(ShowCollection(сollection));

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

        static string ShowCollection(List<int> сollection)
        {
            string output;
            output = "Полученняа коллекция уникальных элементов из масивов:";

            foreach (int element in сollection)
            {
                output += " " + element;
            }

            return output;
        }
    }
}
