using System;

namespace Task04CardDeck
{
    internal class CardBase
    {

        public int Power { get { return _power; } private set { Random random = new Random(); _power = random.Next(_minPower, _maxPower); } }
    }
}