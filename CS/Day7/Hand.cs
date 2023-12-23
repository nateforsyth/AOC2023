using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Day7.Enums;

namespace Day7
{
    public class Hand (List<Enums.Cards> cards, int bid, bool partTwo = false)
    {
        public List<Enums.Cards> Cards { get; set; } = cards;
        public int Bid { get; set; } = bid;
        public int Winnings { get; set; }

        public bool PartTwo { get; set; } = partTwo;

        public Enums.Cards HighCard { get; set; } = GetHighCardInHand(cards);
        public Enums.HandType HandType { get; set; } = GetHandType(cards);

        private static Enums.Cards GetHighCardInHand(List<Enums.Cards> cards)
        {
            Enums.Cards highCard = cards.Aggregate((agg, next) => (int)agg > (int)next ? agg : next);

            return highCard;
        }

        private static Enums.HandType GetHandType(List<Enums.Cards> cards)
        {
            Dictionary<Enums.Cards, int> validatedCards = [];
            int jokerCount = cards.Where(c => c == Enums.Cards.Joker).Count();

            foreach (Enums.Cards card in cards)
            {
                if (validatedCards.TryGetValue(card, out int value))
                {
                    validatedCards[card] = ++value;
                }
                else
                {
                    validatedCards.Add(card, 1);
                }
            }

            if (validatedCards.Any(c => (c.Value + jokerCount) == 5))
            {
                return Enums.HandType.FiveOfAKind;
            }
            else if (validatedCards.Any(c => (c.Value + jokerCount) == 4))
            {
                return Enums.HandType.FourOfAKind;
            }
            else if (validatedCards.Any(c => (c.Value + jokerCount) == 3))
            {
                if (validatedCards.Any(p => p.Value == 2))
                {
                    return Enums.HandType.FullHouse;
                }
                else
                {
                    return Enums.HandType.ThreeOfAKind;
                }
            }
            //else if (validatedCards.Count(c => c.Value == 2) == 2)
            //{
            //    return Enums.HandType.TwoPair;
            //}
            else if (validatedCards.Any(c => (c.Value + jokerCount) == 2))
            {
                if ((validatedCards.Count(c => c.Value == 2) == 2) && jokerCount > 0)
                {

                }
                Dictionary<Enums.Cards, int> pairValidatedCards = [];
                foreach (var card in validatedCards)
                {
                    if (card.Value != 0 && card.Value != validatedCards.Max(c => c.Value))
                    {
                        pairValidatedCards.Add(card.Key, card.Value);
                    }
                }
                if (pairValidatedCards.Any(c => c.Value == 2))
                {
                    return Enums.HandType.TwoPair;
                }
                else
                {
                    return Enums.HandType.OnePair;
                }
            }
            else
            {
                return Enums.HandType.HighCard;
            }
        }

        public static List<Enums.Cards> ConvertHandElementsToCards(char[] handElements, bool partTwo = false)
        {
            List<Enums.Cards> cards = [];

            foreach (var handElement in handElements)
            {

                switch (handElement)
                {
                    case 'A':
                        cards.Add(Enums.Cards.Ace);
                        break;
                    case 'K':
                        cards.Add(Enums.Cards.King);
                        break;
                    case 'Q':
                        cards.Add(Enums.Cards.Queen);
                        break;
                    case 'J':
                        if (!partTwo)
                        {
                            cards.Add(Enums.Cards.Jack);
                        }
                        else
                        {
                            cards.Add(Enums.Cards.Joker);
                        }
                        break;
                    case 'T':
                        cards.Add(Enums.Cards.Ten);
                        break;
                    case '9':
                        cards.Add(Enums.Cards.Nine);
                        break;
                    case '8':
                        cards.Add(Enums.Cards.Eight);
                        break;
                    case '7':
                        cards.Add(Enums.Cards.Seven);
                        break;
                    case '6':
                        cards.Add(Enums.Cards.Six);
                        break;
                    case '5':
                        cards.Add(Enums.Cards.Five);
                        break;
                    case '4':
                        cards.Add(Enums.Cards.Four);
                        break;
                    case '3':
                        cards.Add(Enums.Cards.Three);
                        break;
                    case '2':
                        cards.Add(Enums.Cards.Two);
                        break;
                    default:
                        break;
                }

            }

            return cards;
        }

        public class HandComparer : IComparer<Hand>
        {
            public int Compare(Hand? x, Hand? y)
            {
                int comparison = 0;

                if (x == null)
                {
                    if (y == null) // both null, they're equal
                    {
                        comparison = 0;
                    }
                    else // y is greater
                    {
                        comparison = -1;
                    }
                }
                else // x is not null
                {
                    if (y == null) // x is greater
                    {
                        comparison = 1;
                    }
                    else // neither are null, compare hands
                    {
                        if (x.HandType == y.HandType) // same hand type, compare cards
                        {
                            int cardsIndex = 0;
                            string xValidated = x.Cards.Contains(Enums.Cards.Joker) ? "|1" : "";
                            string xString = x.Cards.Select(c => c.ToString()).Aggregate((a, b) => a + b);
                            string yValidated = y.Cards.Contains(Enums.Cards.Joker) ? "|2" : "";
                            string yString = y.Cards.Select(c => c.ToString()).Aggregate((a, b) => a + b);
                            Console.WriteLine($"hand type: {x.HandType}{xValidated}{yValidated}, x: {xString}, y: {yString}");
                            foreach (Enums.Cards xCard in x.Cards)
                            {
                                if (x.PartTwo && (xCard.Equals(Enums.Cards.Joker) || y.Cards[cardsIndex].Equals(Enums.Cards.Joker)))
                                {

                                }
                                Enums.Cards xCardValidated = x.PartTwo ? (xCard.Equals(Enums.Cards.Joker) ? Enums.Cards.Jack : xCard) : xCard;
                                Enums.Cards yCard = y.PartTwo ? (y.Cards[cardsIndex].Equals(Enums.Cards.Joker) ? Enums.Cards.Jack : y.Cards[cardsIndex]) : y.Cards[cardsIndex];
                                Console.WriteLine($"\tx: {xCard}, valX: {xCardValidated}, y: {y.Cards[cardsIndex]}, valY: {yCard}");

                                if ((int)xCardValidated > (int)yCard)
                                {
                                    comparison = 1;
                                    break;
                                }
                                else if ((int)yCard > (int)xCardValidated)
                                {
                                    comparison = -1;
                                    break;
                                }
                                else // cards are the same, continue to next
                                {
                                    cardsIndex++;
                                    comparison = 0;
                                }
                            }
                            Console.WriteLine();
                        }
                        else // different hand type, compare hand types
                        {
                            if (y.HandType > x.HandType)
                            {
                                comparison = -1;
                            }
                            else
                            {
                                comparison = 1;
                            }
                        }
                    }
                }

                return comparison;
            }
        }
    }
}
