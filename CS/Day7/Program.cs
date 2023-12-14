using System.Reflection.Metadata;
using Utilities.Controller;
using static Day7.Hand;

namespace Day7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;

            List<string>? fileContent = null;
            try
            {
                fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DaySeven\\Input.txt");
                //fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DaySeven\\TestInput.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Day Seven failed:\r\n\t{ex.Message}\r\n\t{ex.InnerException?.Message}");
            }


            if (fileContent != null)
            {
                List<Hand> hands = [];
                foreach (var line in fileContent)
                {
                    var lineElements = line.Split(" ");
                    if (int.TryParse(lineElements[1], out int bid))
                    {
                        var handElements = lineElements[0].ToCharArray();

                        var cards = ConvertHandElementsToCards(handElements);

                        var hand = new Hand(cards, bid);
                        hands.Add(hand);
                    }
                }

                HandComparer hc = new();
                hands.Sort(hc);

                int handIndex = 1;
                int runningTotal = 0;
                foreach (Hand sortedHand in hands)
                {
                    runningTotal += (sortedHand.Bid * handIndex++);
                }

                #region Part One
                long partOne = runningTotal;
                Console.WriteLine($"Day Seven\r\n\tPart One, partOne: {partOne}");
                #endregion

                #region Part Two
                long partTwo = -1;
                Console.WriteLine($"Day Seven\r\n\tPart Two, partTwo: {partTwo}");
                #endregion
            }

            DateTime end = DateTime.Now;
            Console.WriteLine($"\r\nElapsed time: {(end - start).TotalMilliseconds}ms");
            Console.Read();
        }
    }
}
