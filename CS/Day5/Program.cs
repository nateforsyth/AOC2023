using System.Numerics;
using Utilities;
using Utilities.Controller;

namespace Day5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;

            List<string>? fileContent = null;
            try
            {
                fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayFive\\Input.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DayFive failed:\r\n\t{ex.Message}\r\n\t{ex.InnerException?.Message}");
            }


            if (fileContent != null )
            {
                #region Part One
                long lowestLocationNumber_PartOne = CollectionMethods.CalculateLowestLocationValueFromSeedPlantingList_1(fileContent);
                Console.WriteLine($"Day Five\r\n\tPart One, lowestLocationNumber: {lowestLocationNumber_PartOne}");
                #endregion

                #region Part Two
                long lowestLocationNumber_PartTwo = CollectionMethods.CalculateLowestLocationValueFromSeedPlantingList_2(fileContent);
                Console.WriteLine($"Day Five\r\n\tPart Two, lowestLocationNumber: {lowestLocationNumber_PartTwo}");
                #endregion
            }

            DateTime end = DateTime.Now;
            Console.WriteLine($"\r\nElapsed time: {(end - start).TotalMilliseconds}ms");
            Console.Read();
        }
    }
}
