namespace Utilities.Helpers
{
    public class StringHelpers
    {

    }

    public class NumberHelpers
    {
        /// <summary>
        /// Day One, Part One - Extract and calculate total calibration values from a List of strings
        /// </summary>
        /// <param name="calibrationValues"></param>
        /// <returns></returns>
        public static int CalculateTotalCalibrationValue(List<string> calibrationValues)
        {
            int totalCalibrationValue = 0;

            // split string characters into Lists of integers and characters
            calibrationValues.ForEach(line =>
            {
                List<int> inputInts = [];
                List<char> inputChars = [];

                foreach (char c in line.ToCharArray())
                {
                    bool charParseSuccess = int.TryParse(c.ToString(), out int i);

                    if (charParseSuccess)
                    {
                        inputInts.Add(i);
                    }
                    else
                    {
                        inputChars.Add(c);
                    }
                }

                int inputIntsCount = inputInts.Count;
                int secondIntEl = inputInts.Count == 1 ? inputInts.ElementAt(0) : inputInts.ElementAt(inputIntsCount - 1);
                string concatInts = $"{inputInts.ElementAt(0)}{secondIntEl}";
                bool calibValParseSuccess = int.TryParse(concatInts, out int calibrationValue);

                if (calibValParseSuccess && calibrationValue > 0)
                {
                    totalCalibrationValue += calibrationValue;
                }
            });

            return totalCalibrationValue;
        }
    }

    public class FileHelpers
    {
        /// <summary>
        /// Get content of a file 
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns>List of strings (lines)</returns>
        /// <example>
        /// FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayOne\\TestInput.txt")
        /// "..\\..\\..\\..\\.." is prepended to the relativePath
        /// </example>
        /// <exception cref="Exception"></exception>
        public static List<string> GetFileContentFromRelativePath(string relativePath)
        {
            var path = $"..\\..\\..\\..\\..{relativePath}";
            try
            {
                var content = new List<string>();

                using (var stream = File.Open(path, FileMode.Open))
                {
                    var line = string.Empty;
                    using (var reader = new StreamReader(stream))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            content.Add(line);
                        }
                    }
                }

                return content;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to load file at path: {path}", ex);
            }
        }
    }
}
