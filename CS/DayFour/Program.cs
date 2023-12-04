using Utilities;
using Utilities.LogicLayer;

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
                //fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayFour\\TestInput.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DayFour failed:\r\n\t{ex.Message}\r\n\t{ex.InnerException?.Message}");
            }

            int scratchCardScore = 0;

            fileContent?.ForEach(line =>
                {
                    var card = new Day4Card(line);

                    scratchCardScore += card.Score;
                });

            Console.WriteLine($"Day Four\r\n\tPart One, scratchCardScore: {scratchCardScore}\r\n");

            DateTime end = DateTime.Now;
            Console.WriteLine($"Elapsed time: {(end - start).TotalMilliseconds}ms");
            Console.Read();
        }
    }
}
