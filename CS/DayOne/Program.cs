using Utilities.LogicLayer;
using Utilities.Enums;

namespace DayOne
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;

            List<string>? fileContent = null;
            try
            {
#if DEBUG
                fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayOne\\Input.txt");
#else
                fileContent = FileHelpers.GetFileContentFromRelativePath("/Shared/Input/DayOne/Input.txt");
#endif
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DayOne failed:\r\n\t{ex.Message}\r\n\t{ex.InnerException?.Message}");
            }

            if (fileContent != null)
            {
                PartOne(fileContent);
                PartTwo(fileContent);
            }

            DateTime end = DateTime.Now;
            Console.WriteLine($"Elapsed time: {(end - start).TotalMilliseconds}ms");
            Console.Read();
        }

        /// <summary>
        /// Process Calibration Document input for Day One, Part One.
        /// </summary>
        static void PartOne(List<string> fileContent)
        {
            int totalCalibrationValue = NumberMethods.CalculateTotalCalibrationValue(fileContent);

            if (totalCalibrationValue > 0)
            {
                Console.WriteLine($"Day One, Part One; totalCalibrationValue: {totalCalibrationValue}");
            }
        }

        /// <summary>
        /// Process Calibration Document input for Day One, Part Two.
        /// </summary>
        static void PartTwo(List<string> fileContent)
        {
            int totalCalibrationValue = StringMethods.ExtractNumbersAndCalculateTotalCalibrationValue(fileContent);

            if (totalCalibrationValue > 0)
            {
                Console.WriteLine($"Day One, Part Two; totalCalibrationValue: {totalCalibrationValue}");
            }
        }
    }
}
