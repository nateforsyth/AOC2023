using System.Text.RegularExpressions;
using System.Text;
using Utilities.Enums;

namespace Utilities.Helpers
{
    public class StringHelpers
    {
        /// <summary>
        /// Day One, Part Two - Extract number values from Input strings using a Regular Expression and calculate total calibration value
        /// </summary>
        /// <param name="calibrationValues"></param>
        /// <returns>Total calibration value</returns>
        public static int ExtractNumbersAndCalculateTotalCalibrationValue(List<string> calibrationValues)
        {
            int totalCalibrationValue = 0;
            string pattern = @"[0-9]|(?=(one|two|three|four|five|six|seven|eight|nine))";

            calibrationValues.ForEach((s) =>
            {
                MatchCollection matches = Regex.Matches(s, pattern, RegexOptions.IgnoreCase);
                List<int> inputInts = [];

                matches.ToList().ForEach((match) =>
                {
                    bool charParseSuccess = int.TryParse(match.ToString(), out int i);

                    if (charParseSuccess)
                    {
                        inputInts.Add(i);
                    }
                    else
                    {
                        if (Enum.TryParse(match.ToString(), out NumberStrings result))
                        {
                            inputInts.Add((int)result);
                        }
                        else
                        {
                            if (Enum.TryParse(match.Groups[1].Value, out NumberStrings result1))
                            {
                                inputInts.Add((int)result1);
                            }
                        }
                    }
                });

                totalCalibrationValue += NumberHelpers.CalculateCalibrationLine(inputInts);
            });

            return totalCalibrationValue;
        }
    }

    public class NumberHelpers
    {
        /// <summary>
        /// Day One, Part One - Extract and calculate total calibration values from a List of strings
        /// </summary>
        /// <param name="calibrationValues"></param>
        /// <returns>Total calibration value</returns>
        public static int CalculateTotalCalibrationValue(List<string> calibrationValues)
        {
            int totalCalibrationValue = 0;

            // split string characters into Lists of integers and characters
            calibrationValues.ForEach(line =>
            {
                List<int> inputInts = [];

                foreach (char c in line.ToCharArray())
                {
                    bool charParseSuccess = int.TryParse(c.ToString(), out int i);

                    if (charParseSuccess)
                    {
                        inputInts.Add(i);
                    }
                }

                totalCalibrationValue += CalculateCalibrationLine(inputInts);
            });

            return totalCalibrationValue;
        }

        /// <summary>
        /// Calculate a calibration line based upon a provided List of integers
        /// </summary>
        /// <param name="inputInts">Calibration line integers</param>
        /// <returns>Calculated calibration line value</returns>
        public static int CalculateCalibrationLine(List<int> inputInts)
        {
            int inputIntsCount = inputInts.Count;
            int secondIntEl = inputInts.Count == 1 ? inputInts.ElementAt(0) : inputInts.ElementAt(inputIntsCount - 1);
            string concatInts = $"{inputInts.ElementAt(0)}{secondIntEl}";
            bool calibValParseSuccess = int.TryParse(concatInts, out int calibrationValue);

            if (calibValParseSuccess && calibrationValue > 0)
            {
                return calibrationValue;
            }
            else
            {
                return 0;
            }
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
