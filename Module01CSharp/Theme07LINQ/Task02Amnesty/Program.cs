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
        private List<Prisoner> _prisoners;

        public DataBase()
        {
            _prisoners = new List<Prisoner>
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
        }

        public void ShowLog()
        {
            Console.WriteLine("До амнистии:");
            ShowPrisoners();

            Console.WriteLine("\nПосле амнистии:");
            Amnesty();
            ShowPrisoners();
        }

        private void ShowPrisoners()
        {
            foreach (var prisoner in _prisoners)
            {
                Console.WriteLine(prisoner.Name);
            }
        }

        private void Amnesty()
        {
            string crimeType = "Антиправительственное";

            _prisoners = _prisoners.Where(prisoner => prisoner.CrimeType != crimeType).ToList();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            DataBase dataBase = new DataBase();
            dataBase.ShowLog();
        }
    }
}