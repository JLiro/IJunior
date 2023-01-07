using System;
using System.Collections.Generic;

namespace Task02QueueInTheStore
{
    internal class Program
    {
        static void Main()
        {
            Queue<int> purchaseAmountsBuyers = new Queue<int>();

            purchaseAmountsBuyers.Enqueue(10);
            purchaseAmountsBuyers.Enqueue(20);
            purchaseAmountsBuyers.Enqueue(30);
            purchaseAmountsBuyers.Enqueue(40);
            
            int purchaseAmountBuyer;
            int buyerIndex;

            string lastBayerInfo = string.Empty; 
            int storeBalance = 0;

            for (int i = 0; 0 < purchaseAmountsBuyers.Count; i++)
            {
                Console.Write("Список покупателей:" +
                              ShowQueueBuyers(purchaseAmountsBuyers) +
                              "\n" +
                              "\n" +
                              "Нажмите любую клавишу, чтобы обслужить клиента" +
                              "\n" +
                              lastBayerInfo);
                
                Console.ReadKey();

                    purchaseAmountBuyer = purchaseAmountsBuyers.Peek();
                    storeBalance += purchaseAmountsBuyers.Dequeue();

                    buyerIndex = i + 1;

                    lastBayerInfo =  "\n=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=" +
                                    $"\nПокупатель #{buyerIndex} совершил покупку на {purchaseAmountBuyer} руб." +
                                    $"\nВыручка магазина: {storeBalance} руб.";

                Console.Clear();
            }

            Console.WriteLine("Список покупателей пуст, все клиенты обслужены!" +
                              "\n" +
                              lastBayerInfo);

            Console.ReadKey();
        }

        static string ShowQueueBuyers(Queue<int> purchaseAmountsBuyers)
        {
            int buyerIndex = 0;
            string queueBuyer = string.Empty;

            foreach (var purchaseAmountBuyer in purchaseAmountsBuyers)
            {
                buyerIndex++;
                queueBuyer += $"\n#{buyerIndex} с покупкой на {purchaseAmountBuyer} руб.";
            }
            return queueBuyer;
        }
    }
}
