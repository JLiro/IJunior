﻿using System;
using System.Collections.Generic;

namespace Task04CardDeck
{
    internal class Program
    {
        static void Main()
        {
            const ConsoleKey ComandTakeCard = ConsoleKey.A;
            const ConsoleKey ComandExit     = ConsoleKey.S;

            Player player = new Player("Игрок");
            int cardCount = 1;
            
            Deck deck = new Deck(12);
            deck.Build();

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine($"Нажмите {ComandTakeCard} чтобы {player.Name} взял указанное кол-во карт: {cardCount}"
                                + $"\nили нажмите {ComandExit} для выхода"
                                + $"\n");
                deck.ShowCards();
                player.ShowCards();

                ConsoleKey сonsoleKey = Console.ReadKey(true).Key;

                switch (сonsoleKey)
                {
                    case ComandTakeCard:
                        player.TakeCard(deck, cardCount);
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
        
        public void TakeCard(Deck deck, int count)
        {
            if (deck.СardCount > 0)
            {
                for (int i = 0; i < count; i++)
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
                    output += " " + card.ShowPower();
                }

                Console.WriteLine(output);
            }
            else
            {
                Console.WriteLine("Колода игрока пуста");
            }
        }
    }

    class Deck
    {
        private Queue<Card> _cards = new Queue<Card>();
        
        public Deck(int cardCount)
        {
            СardCount = cardCount;
        }

        public int СardCount { private set; get; }
        
        public void Build()
        {
            Random random = new Random();
            int maxPower = 11;
            int minPower = 1;

            for (int i = 0; i < СardCount; i++)
            {
                _cards.Enqueue( new Card( random.Next(minPower, maxPower) ) );
            }
        }

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

            if(_cards.Count > 0)
            {
                foreach (Card card in _cards)
                {
                    output += " " + card.ShowPower();
                }

                Console.WriteLine(output);
            }
            else
            {
                Console.WriteLine("Общая колода пуста");
            }
        }
    }

    class Card
    {
        private int _power;

        public Card(int power)
        {
            int maxPower = 10;
            int minPower = 1;

            if (power <= minPower)
            {
                _power = minPower;
            }
            else if (power >= maxPower)
            {
                _power = maxPower;
            }
            else
            {
                _power = power;
            }
        }

        public int ShowPower()
        {
            return _power;
        }
    }
}
