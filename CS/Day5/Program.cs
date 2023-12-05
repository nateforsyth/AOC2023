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
                //fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayFive\\TestInput.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DayFive failed:\r\n\t{ex.Message}\r\n\t{ex.InnerException?.Message}");
            }


            if (fileContent != null )
            {
                #region Part One
                BigInteger lowestLocationNumber = CollectionMethods.CalculateLowestLocationValueFromSeedPlantingList(fileContent);
                Console.WriteLine($"Day Five\r\n\tPart One, lowestLocationNumber: {lowestLocationNumber}");
                #endregion

                #region Part Two

                Console.WriteLine($"\tPart Two, something: {0}\r\n");
                #endregion
            }

            DateTime end = DateTime.Now;
            Console.WriteLine($"Elapsed time: {(end - start).TotalMilliseconds}ms");
            Console.Read();
        }
    }
}
