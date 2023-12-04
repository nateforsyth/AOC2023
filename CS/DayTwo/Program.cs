using Utilities.Controller;

namespace Day2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;

            List<string>? fileContent = null;
            try
            {
                fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayTwo\\Input.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DayTwo failed:\r\n\t{ex.Message}\r\n\t{ex.InnerException?.Message}");
            }

            if (fileContent != null)
            {
                PartsOneAndTwo(fileContent);
            }

            DateTime end = DateTime.Now;
            Console.WriteLine($"Elapsed time: {(end - start).TotalMilliseconds}ms");
            Console.Read();
        }

        /// <summary>
        /// Process Cube Game input for Day Two, Part One.
        /// </summary>
        static void PartsOneAndTwo(List<string> fileContent)
        {
            int redCubeMax = 12, greenCubeMax = 13, blueCubeMax = 14;
            (int idCount, int totalPower) = CollectionMethods.CalculateGameResults(fileContent, redCubeMax, greenCubeMax, blueCubeMax);

            Console.WriteLine($"ID count of possible games: {idCount}");
            Console.WriteLine($"Total power of possible games: {totalPower}");
        }
    }
}
