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
                List<Hand> partOneHands = [];
                List<Hand> partTwoHands = [];
                foreach (var line in fileContent)
                {
                    var lineElements = line.Split(" ");
                    if (int.TryParse(lineElements[1], out int bid))
                    {
                        char[] handElements = lineElements[0].ToCharArray();

                        List<Enums.Cards> partOneCards = ConvertHandElementsToCards(handElements);
                        Hand partOneHand = new(partOneCards, bid);
                        partOneHands.Add(partOneHand);

                        List<Enums.Cards> partTwoCards = ConvertHandElementsToCards(handElements, true);
                        Hand partTwoHand = new(partTwoCards, bid, true);
                        partTwoHands.Add(partTwoHand);
                    }
                }

                HandComparer hc = new();

                #region Part One
                partOneHands.Sort(hc);
                int partOneHandIndex = 1;
                int partOneRunningTotal = 0;
                foreach (Hand sortedHand in partOneHands)
                {
                    partOneRunningTotal += (sortedHand.Bid * partOneHandIndex++);
                }
                long partOne = partOneRunningTotal;
                Console.WriteLine($"Day Seven\r\n\tPart One, partOne: {partOne}");
                #endregion

                #region Part Two
                partTwoHands.Sort(hc);
                int partTwoHandIndex = 1;
                int partTwoRunningTotal = 0;
                foreach (Hand sortedHand in partTwoHands)
                {
                    partTwoRunningTotal += (sortedHand.Bid * partTwoHandIndex++);
                }
                long partTwo = partTwoRunningTotal;
                Console.WriteLine($"Day Seven\r\n\tPart Two, partTwo: {partTwo}");
                #endregion
            }

            DateTime end = DateTime.Now;
            Console.WriteLine($"\r\nElapsed time: {(end - start).TotalMilliseconds}ms");
            Console.Read();
        }
    }
}
