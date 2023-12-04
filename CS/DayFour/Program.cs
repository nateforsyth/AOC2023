using Utilities;
using Utilities.Controller;

namespace Day4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;

            List<string>? fileContent = null;
            try
            {
                fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayFour\\Input.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DayFour failed:\r\n\t{ex.Message}\r\n\t{ex.InnerException?.Message}");
            }

            #region Part One

            int scratchCardScore = 0;

            List<Day4Card> cards = [];
            Dictionary<int, int> cardsAndCounts = [];

            fileContent?.ForEach(line =>
                {
                    var card = new Day4Card(line);
                    cards.Add(card);

                    scratchCardScore += card.Score;
                });

            Console.WriteLine($"Day Four\r\n\tPart One, scratchCardScore: {scratchCardScore}");

            #endregion

            #region Part Two

            Dictionary<int, List<Day4Card>> processedCards = CollectionMethods.ProcessCards(cards);
            int cardsProcessed = processedCards.Sum(c => c.Value.Count);

            Console.WriteLine($"\tPart Two, cardsProcessed: {cardsProcessed}\r\n");

            #endregion

            DateTime end = DateTime.Now;
            Console.WriteLine($"Elapsed time: {(end - start).TotalMilliseconds}ms");
            Console.Read();
        }
    }
}
