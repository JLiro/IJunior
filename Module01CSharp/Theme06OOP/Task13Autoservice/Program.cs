using System;
using System.Collections.Generic;

namespace Task13Autoservice
{
    using System;
    using System.Collections.Generic;

    // класс Деталь
    public class Detail
    {
        public string Name { get; }
        public double Price { get; }

        public Detail(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }

    // класс Ремонт
    public class Repair
    {
        public Detail Detail { get; }
        public double RepairPrice { get; }

        public Repair(Detail detail, double repairPrice)
        {
            Detail = detail;
            RepairPrice = repairPrice;
        }
    }

    // интерфейс для фабрики Ремонтов
    public interface IRepairFactory
    {
        Repair CreateRepair(string detailName, double repairPrice);
        Detail CreateDetail(string name);
    }

    // класс фабрики Ремонтов
    public class RepairFactory : IRepairFactory
    {
        public Repair CreateRepair(string detailName, double repairPrice)
        {
            var detail = CreateDetail(detailName);
            return new Repair(detail, repairPrice);
        }

        public Detail CreateDetail(string name)
        {
            return new Detail(name, 0);
        }
    }

    // класс СкладДеталей (Одиночка)
    public sealed class DetailStorage
    {
        private static DetailStorage instance = null;
        private Dictionary<string, int> details = new Dictionary<string, int>();

        private DetailStorage() { }

        public static DetailStorage Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DetailStorage();
                }
                return instance;
            }
        }

        public void AddDetail(Detail detail, int quantity)
        {
            if (!details.ContainsKey(detail.Name))
            {
                details.Add(detail.Name, quantity);
            }
            else
            {
                details[detail.Name] += quantity;
            }
        }

        public bool GetDetail(string detailName)
        {
            if (details.ContainsKey(detailName) && details[detailName] > 0)
            {
                details[detailName]--;
                return true;
            }
            return false;
        }

        public int GetQuantity(string detailName)
        {
            return details.ContainsKey(detailName) ? details[detailName] : 0;
        }
    }

    // интерфейс для поставщика деталей
    public interface IDetailProvider
    {
        bool ProvideDetail(string detailName);
    }

    // класс поставщика деталей (Стратегия)
    public abstract class DetailProvider : IDetailProvider
    {
        protected IDetailProvider nextProvider;

        public void SetNextProvider(IDetailProvider provider)
        {
            nextProvider = provider;
        }

        public abstract bool ProvideDetail(string detailName);
    }

    // класс поставщика деталей со склада
    public class DetailProviderFromStorage : DetailProvider
    {
        private DetailStorage storage;

        public DetailProviderFromStorage(DetailStorage storage)
        {
            this.storage = storage;
        }

        public override bool ProvideDetail(string detailName)
        {
            if (storage.GetDetail(detailName))
            {
                return true;
            }
            else if (nextProvider != null)
            {
                return nextProvider.ProvideDetail(detailName);
            }
            else
            {
                return false;
            }
        }
    }

    // класс поставщика деталей от внешнего поставщика
    public class DetailProviderFromSupplier : DetailProvider
    {
        private DetailSupplier supplier;

        public DetailProviderFromSupplier(DetailSupplier supplier)
        {
            this.supplier = supplier;
        }

        public override bool ProvideDetail(string detailName)
        {
            if (supplier.OrderDetail(detailName))
            {
                return true;
            }
            else if (nextProvider != null)
            {
                return nextProvider.ProvideDetail(detailName);
            }
            else
            {
                return false;
            }
        }
    }

    // класс поставщика деталей (Цепочка обязанностей)
    public abstract class DetailSupplier
    {
        protected DetailSupplier nextSupplier;

        public void SetNextSupplier(DetailSupplier supplier)
        {
            nextSupplier = supplier;
        }

        public abstract bool OrderDetail(string detailName);
    }

    // класс поставщика деталей со склада
    public class DetailSupplierFromWarehouse : DetailSupplier
    {
        private DetailStorage storage;

        public DetailSupplierFromWarehouse(DetailStorage storage)
        {
            this.storage = storage;
        }

        public override bool OrderDetail(string detailName)
        {
            if (storage.GetQuantity(detailName) > 0)
            {
                return true;
            }
            else if (nextSupplier != null)
            {
                return nextSupplier.OrderDetail(detailName);
            }
            else
            {
                return false;
            }
        }
    }

    // класс поставщика деталей от внешнего поставщика
    public class DetailSupplierFromExternal : DetailSupplier
    {
        public override bool OrderDetail(string detailName)
        {
            // код для заказа детали у внешнего поставщика
            return true;
        }
    }

    // класс РемонтныйЦех
    public class RepairShop
    {
        private IRepairFactory repairFactory;
        private DetailStorage detailStorage;
        private IDetailProvider detailProvider;
        private DetailSupplier detailSupplier;

        public RepairShop()
        {
            repairFactory = new RepairFactory();
            detailStorage = DetailStorage.Instance;
            detailProvider = new DetailProviderFromStorage(detailStorage);
            detailSupplier = new DetailSupplierFromWarehouse(detailStorage);
            detailSupplier.SetNextSupplier(new DetailSupplierFromExternal());
        }

        public (bool, double) ProcessRepair(string detailName, double repairPrice)
        {
            // создаем объект ремонта
            var repair = repairFactory.CreateRepair(detailName, repairPrice);

            // ищем деталь на складе
            if (!detailProvider.ProvideDetail(detailName))
            {
                // если деталь не найдена, заказываем у поставщика
                if (!detailSupplier.OrderDetail(detailName))
                {
                    // если деталь не удалось заказать, отказываем клиенту и выплачиваем штраф
                    return (false, repair.RepairPrice * 2);
                }
            }

            // выполняем ремонт
            var success = PerformRepair(repair);

            // если ремонт не удался, возмещаем ущерб клиенту
            if (!success)
            {
                return (false, repair.RepairPrice * 2);
            }

            // если ремонт удался, добавляем выплату за ремонт в баланс автосервиса
            return (true, repair.RepairPrice);
        }

        private bool PerformRepair(Repair repair)
        {
            // сохраняем состояние ремонта перед заменой деталей
            var currentState = SaveCurrentState();

            // выполняем ремонт
            try
            {
                // код для выполнения ремонта
                return true;
            }
            catch (Exception ex)
            {
                // в случае ошибки восстанавливаем предыдущее состояние ремонта
                RestorePreviousState(currentState);
                return false;
            }
        }

        private object SaveCurrentState()
        {
            // код для сохранения текущего состояния ремонта
            return null;
        }

        private void RestorePreviousState(object state)
        {
            // код для восстановления предыдущего состояния ремонта
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // создаем объект ремонтного цеха
            var repairShop = new RepairShop();

            // запускаем программу
            while (true)
            {
                Console.WriteLine("Введите название детали:");
                var detailName = Console.ReadLine();

                Console.WriteLine("Введите стоимость ремонта:");
                var repairPrice = double.Parse(Console.ReadLine());

                var result = repairShop.ProcessRepair(detailName, repairPrice);

                if (result.Item1)
                {
                    Console.WriteLine("Ремонт выполнен успешно. Стоимость ремонта: " + result.Item2);
                }
                else
                {
                    Console.WriteLine("Ремонт не выполнен. Стоимость штрафа: " + result.Item2);
                }

                Console.WriteLine("Желаете продолжить работу программы? (y/n)");
                if (Console.ReadLine().ToLower() != "y")
                {
                    break;
                }
            }
        }
    }
}
