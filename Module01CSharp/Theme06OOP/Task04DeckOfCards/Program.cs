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
            if (_cards.Count > 0)
            {
                Console.Write("Текущие карты у игрока:");
                
                foreach (Card card in _cards)
                {
                    Console.Write
                        (" " + card.Power);
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("У игрока нет карт");
            }
        }
    }

    class Deck
    {
        private Stack<Card> _cards = new Stack<Card>();

        private Random _random = new Random();

        public Deck(int cardCount)
        {
            for (int i = 0; i < cardCount; i++)
            {
                _cards.Push(CreateCard());
            }

            СardCount = cardCount;
        }

        public int СardCount { private set; get; }

        public Card RemoveCard()
        {
            if(TryGetCard(out Card card))
            {
                _cards.Pop();

                СardCount = _cards.Count;
            }

            return card;
        }

        public void ShowCards()
        {
            if (_cards.Count > 0)
            {
                Console.Write("Текущие карты в колоде:");

                foreach (Card card in _cards)
                {
                    Console.Write(" " + card.Power);
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("В колоде нет карт");
            }
        }

        private bool TryGetCard(out Card card)
        {
            card = null;
            bool isCard = ( _cards.Contains( _cards.Peek() ) );

            if (isCard)
            {
                foreach (Card tempCard in _cards)
                {
                    if (tempCard.Power == _cards.Peek().Power)
                    {
                        card = tempCard;

                        isCard = true;
                    }
                }
            }

            return isCard;
        }

        private Card CreateCard()
        {
            int minPowerCard = 10;
            int maxPowerCard = 100;

            return new Card( _random.Next(minPowerCard, maxPowerCard) );
        }
    }

    class Card
    {
        public int Power { get; private set; }

        public Card(int power) => Power = power;
    }
}
