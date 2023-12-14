using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    public class Enums
    {
        public enum Cards
        {
            Ace = 13,
            King = 12,
            Queen = 11,
            Jack = 10,
            Ten = 9,
            Nine = 8,
            Eight = 7,
            Seven = 6,
            Six = 5,
            Five = 4,
            Four = 3,
            Three = 2,
            Two = 1,
            Joker = 0
        }

        public enum HandType
        {
            FiveOfAKind = 6,
            FourOfAKind = 5,
            FullHouse = 4,
            ThreeOfAKind = 3,
            TwoPair = 2,
            OnePair = 1,
            HighCard = 0
        }
    }
}
