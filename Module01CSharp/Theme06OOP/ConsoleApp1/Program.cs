using System;
using System.Collections.Generic;

namespace Task04CardDeck
{
    internal class Program
    {
        static void Main()
        {
            const ConsoleKey ComandTakeCard = ConsoleKey.A;
            const ConsoleKey ComandExit = ConsoleKey.S;

            Player player = new Player("Игрок");
            Deck deck = new Deck(12);

            int playerTakeCardCount = 1;

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine($"Нажмите [{ComandTakeCard}] чтобы {player.Name} взял {playerTakeCardCount} карту"
                                + $"\nНажмите [{ComandExit}] для выхода"
                                + $"\n");
                deck.ShowCards();
                player.ShowCards();

                ConsoleKey сonsoleKey = Console.ReadKey(true).Key;

                switch (сonsoleKey)
                {
                    case ComandTakeCard:
                        player.TakeCard(deck, playerTakeCardCount);
                        break;

                    case ComandExit:
                        isWork = false;
                        break;
                }

                Console.Clear();
            }
        }
    }

    class Player
    {
        private List<Card> _cards;

        public Player(string name)
        {
            Name = name;
            _cards = new List<Card>();
        }

        public string Name { private set; get; }

        public void TakeCard(Deck deck, int takeCount)
        {
            if (deck.СardCount > 0)
            {
                for (int i = 0; i < takeCount; i++)
                {
                    _cards.Add(deck.RemoveCard());
                }
            }
        }

        public void ShowCards()
        {
            string output = "Текущие карты у игрока:";

            if (_cards.Count > 0)
            {
                foreach (Card card in _cards)
                {
                    output += " " + card.Power;
                }

                Console.WriteLine(output);
            }
            else
            {
                Console.WriteLine("У игрока нет карт");
            }
        }
    }

    class Deck
    {
        private Queue<Card> _cards = new Queue<Card>();
        
        private Random _random = new Random();

        public Deck(int cardCount)
        {
            for (int i = 0; i < cardCount; i++)
            {
                _cards.Enqueue(CreateCard());
            }

            СardCount = cardCount;
        }

        public int СardCount { private set; get; }

        public Card RemoveCard()
        {
            Card card = _cards.Peek();

            _cards.Dequeue();

            СardCount = _cards.Count;

            return card;
        }

        public void ShowCards()
        {
            string output = "Текущие карты в колоде:";

            if (_cards.Count > 0)
            {
                foreach (Card card in _cards)
                {
                    output += " " + card.Power;
                }

                Console.WriteLine(output);
            }
            else
            {
                Console.WriteLine("В колоде нет карт");
            }
        }

        private Card CreateCard()
        {
            int minPowerCard = 1;
            int maxPowerCard = 11;

            int power = _random.Next(minPowerCard, maxPowerCard);

            return new Card(_random.Next(minPowerCard, maxPowerCard));
        }
    }

    class Card
    {
        public int Power { get; private set; }

        public Card(int power) => Power = power;
    }
}
