using Utilities;
using Utilities.Controller;

namespace Day3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;

            List<string>? fileContent = null;
            try
            {
                fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayThree\\Input.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DayThree failed:\r\n\t{ex.Message}\r\n\t{ex.InnerException?.Message}");
            }

            if (fileContent != null)
            {
                (List<char> partPositioningSymbols, Dictionary<int, List<char>> partsMatrix) = ValidationMethods.CreatePartsMatrix(fileContent);
                (int aggregatedPartNumberTotal, int aggregatedGearRatioTotal) = AggregationMethods.AggregatePartsData(partPositioningSymbols, partsMatrix);

                Console.WriteLine($"Day Three\r\n\tPart One\r\n\t\tAggregate of all adjacent part numbers: {aggregatedPartNumberTotal}\r\n\tPart Two\r\n\t\tAggregate of all Gear Ratios: {aggregatedGearRatioTotal}\r\n");
            }            

            DateTime end = DateTime.Now;
            Console.WriteLine($"Elapsed time: {(end - start).TotalMilliseconds}ms");
            Console.Read();
        }
    }
}
