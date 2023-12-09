using Utilities;
using Utilities.Controller;

namespace Day6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;

            List<string>? fileContent = null;
            try
            {
                //fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DaySix\\TestInput.txt");
                fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DaySix\\Input.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Day Six failed:\value\n\t{ex.Message}\value\n\t{ex.InnerException?.Message}");
            }


            if (fileContent != null)
            {
                #region Part One
                int marginOfError_1 = AggregationMethods.CalculateShortRaceMarginOfError(fileContent);
                Console.WriteLine($"Day Six\r\n\tPart One, marginOfError: {marginOfError_1}");
                #endregion

                #region Part Two
                long marginOfError_2 = AggregationMethods.CalculateLongRacePossibilities(fileContent);
                Console.WriteLine($"Day Six\r\n\tPart Two, marginOfError: {marginOfError_2}");
                #endregion
            }

            DateTime end = DateTime.Now;
            Console.WriteLine($"\r\nElapsed time: {(end - start).TotalMilliseconds}ms");
            Console.Read();
        }
    }
}
