using System;
using System.Collections.Generic;
using System.Linq;

namespace Task02Amnesty
{
    public class Prisoner
    {
        public Prisoner(string name, string сrimeType)
        {
            Name = name;
            CrimeType = сrimeType;
        }

        public string Name { get; private set; }
        public string CrimeType { get; private set; }
    }

    public class DataBase
    {
        private List<Prisoner> Prisoners = new List<Prisoner>
        {
            new Prisoner("John Smith", "Кража"),
            new Prisoner("Anna Johnson", "Антиправительственное"),
            new Prisoner("Ivan Petrov", "Убийство"),
            new Prisoner("Maria Garcia", "Убийство"),
            new Prisoner("Mohammed Ahmed", "Кража"),
            new Prisoner("Hiroshi Nakamura", "Антиправительственное"),
            new Prisoner("Chen Wei", "Убийство"),
            new Prisoner("Ludmila Ivanova", "Антиправительственное"),
            new Prisoner("Abdul Rahman", "Кража"),
            new Prisoner("Kim Min-ji", "Антиправительственное")
        };

        public void ShowPrisoners()
        {
            Console.WriteLine("До амнистии:");
            ShowPrisonersBeforeAmnesty();

            Console.WriteLine("\nПосле амнистии:");
            ShowPrisonersAfterAmnesty();
        }

        private void ShowPrisonersBeforeAmnesty()
        {
            foreach (var prisoner in Prisoners)
            {
                Console.WriteLine(prisoner.Name);
            }
        }

        private void ShowPrisonersAfterAmnesty()
        {
            var filteredPrisoners = Prisoners.Where(prisoner => prisoner.CrimeType != "Антиправительственное").Select(prisoner => prisoner.Name);

            foreach (var creminal in filteredPrisoners)
            {
                Console.WriteLine(creminal);
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            DataBase dataBase = new DataBase();
            dataBase.ShowPrisoners();
        }
    }
}