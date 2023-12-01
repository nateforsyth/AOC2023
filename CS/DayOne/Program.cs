using Utilities.Helpers;

namespace DayOne
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string>? fileContent = null;
            try
            {
                fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayOne\\Input.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DayOne failed:\r\n\t{ex.Message}\r\n\t{ex.InnerException?.Message}");
            }

            if (fileContent != null)
            {
                var totalCalibrationValue = NumberHelpers.CalculateTotalCalibrationValue(fileContent);

                if (totalCalibrationValue > 0)
                {
                    Console.WriteLine($"totalCalibrationValue: {totalCalibrationValue}");
                }
            }

            Console.Read();
        }
    }
}
