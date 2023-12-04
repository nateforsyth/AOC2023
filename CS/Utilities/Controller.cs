using System.Text.RegularExpressions;
using Utilities.Enums;

namespace Utilities.Controller
{
    public class AggregationMethods
    {
        public static (int aggregatedPartNumberTotal, int aggregatedGearRatioTotal) AggregatePartsData(List<char> partPositioningSymbols, Dictionary<int, List<char>> partsMatrix)
        {
            Dictionary<(int xPosition, int yPosition), (char partElement, int partNumberLineLength)> partPositions = [];
            List<Day3EnginePart> parts = [];
            int currentX = 0;
            int currentY = 0;

            foreach (KeyValuePair<int, List<char>> line in partsMatrix)
            {
                foreach (char c in line.Value)
                {
                    //Console.Write(c);
                    if (partPositioningSymbols.Contains(c))
                    {
                        partPositions.Add((currentX, currentY), (c, line.Value.Count));
                    }
                    currentX++;
                }
                currentY++;
                currentX = 0;
                //Console.Write("\n");
            }

            foreach (var partElement in partPositions)
            {
                int currentXPos = partElement.Key.xPosition;
                int currentYPos = partElement.Key.yPosition;

                Day3EnginePart part = new(currentXPos, currentYPos, partElement.Value.partElement, partElement.Value.partNumberLineLength);

                parts.Add(part);
            }

            int aggregatedPartNumberTotal = 0;
            int aggregatedGearRatioTotal = 0;

            parts.ForEach(part =>
            {
                Dictionary<int, List<char>> partsMatrixSubset = [];

                part.SurroundingCoordinates.ForEach(coord =>
                {
                    if (!partsMatrixSubset.ContainsKey(coord.y))
                    {
                        KeyValuePair<int, List<char>> partsLine = partsMatrix.Where(e => e.Key == coord.y).FirstOrDefault();

                        if (partsLine.Key >= 0 && partsLine.Value.Count > 0)
                        {
                            partsMatrixSubset.Add(partsLine.Key, partsLine.Value);
                        }
                    }
                });

                part.SurroundingLines = partsMatrixSubset;
                part.CalculateSurroundingPartsFromCoords(part.SurroundingLines, part.PartElement);
                aggregatedPartNumberTotal += part.SurroundingPartNumberAggregate;
                aggregatedGearRatioTotal += part.GearRatio;
            });

            return (aggregatedPartNumberTotal, aggregatedGearRatioTotal);
        }
    }

    public class ValidationMethods
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileContent"></param>
        /// <returns></returns>
        public static (List<char> partPositioningSymbols, Dictionary<int, List<char>> partsMatrix) CreatePartsMatrix(List<string> fileContent)
        {
            List<char> partPositioningSymbols = []; // & * # % $ - @ = + /
            Dictionary<int, List<char>> partsMatrix = [];
            int currentLine = 0;

            // get supported symbols from input
            fileContent?.ForEach(line =>
            {
                List<char> partNumberLine = [];
                foreach (char c in line.ToCharArray())
                {
                    if (!partPositioningSymbols.Contains(c))
                    {
                        if (!int.TryParse(c.ToString(), out int parsedInt) && c != '.')
                        {
                            partPositioningSymbols.Add(c);
                        }
                    }
                    partNumberLine.Add(c);
                }
                partsMatrix.Add(currentLine++, partNumberLine);
            });

            return (partPositioningSymbols, partsMatrix);
        }
    }

    public class CollectionMethods
    {
        /// <summary>
        /// Day Four, Part Two - Process scratch cards adding winners to the queue to also be processed, and count how many were played
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static Dictionary<int, List<Day4Card>> ProcessCards(List<Day4Card> cards)
        {
            int currentCardIndex = 0;
            Dictionary<int, List<Day4Card>> processedCards = [];

            int lastCardIndex = cards.Count - 1;
            Queue<Day4Card> cardsToProcessQueue = new Queue<Day4Card>(cards);

            while (cardsToProcessQueue.Count > 0)
            {
                Day4Card card = cardsToProcessQueue.Dequeue();
                if (!processedCards.TryGetValue(card.ID, out List<Day4Card>? value))
                {
                    processedCards.Add(card.ID, [card]);
                }
                else
                {
                    value.Add(card);
                }

                for (int cardIndexOffset = 0; cardIndexOffset < card.WinCount; cardIndexOffset++)
                {
                    int cardCopyIndex = card.ID + cardIndexOffset;
                    if (cardCopyIndex <= lastCardIndex)
                    {
                        Day4Card cardCopy = cards.ElementAt(cardCopyIndex);
                        cardCopy.IsCopy = true;
                        cardsToProcessQueue.Enqueue(cardCopy);
                    }
                }

                currentCardIndex++;
            }

            return processedCards;
        }

        /// <summary>
        /// Day Two, Part One - Extract string input to validate game results
        /// </summary>
        /// <param name="gameInput"></param>
        /// <param name="redCubeMax"></param>
        /// <param name="greenCubeMax"></param>
        /// <param name="blueCubeMax"></param>
        /// <param name="partTwo"></param>
        /// <returns></returns>
        public static (int idCount, int totalPower) CalculateGameResults(List<string> gameInput, int redCubeMax, int greenCubeMax, int blueCubeMax)
        {
            List<Day2Game> dayTwoGames = [];
            int idCount = 0;
            int power = 0;

            gameInput.ForEach(x =>
            {
                string[] baseGameEls = x.Split(": ");
                string[] gameIdEls = baseGameEls[0].Split(" ");
                string[] gameCubeSets = baseGameEls[1].Split("; ");

                List<Day2Game.Set> sets = new List<Day2Game.Set>();

                foreach (var subset in gameCubeSets)
                {
                    Day2Game.Set set = new Day2Game.Set();
                    string[] cubeSubsets = subset.Split(", ");

                    foreach (var cubeAndCount in cubeSubsets)
                    {
                        string[] cubeEls = cubeAndCount.Split(" ");

                        if (int.TryParse(cubeEls[0], out int cubeCount) && cubeCount > 0)
                        {
                            switch (cubeEls[1])
                            {
                                case "red":
                                    set.RedCubeCount = cubeCount;
                                    break;
                                case "green":
                                    set.GreenCubeCount = cubeCount;
                                    break;
                                case "blue":
                                    set.BlueCubeCount = cubeCount;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    sets.Add(set);
                }

                if (int.TryParse(gameIdEls[1], out int gameId) && gameId > 0)
                {
                    Day2Game game = new(gameId, sets, redCubeMax, greenCubeMax, blueCubeMax);
                    dayTwoGames.Add(game);
                }
            });

            dayTwoGames.ForEach(game =>
            {
                if (game.GameIsPossible(redCubeMax, greenCubeMax, blueCubeMax))
                {
                    idCount += game.ID;
                }

                power += game.TotalSetPower;
            });

            return (idCount, power);
        }
    }

    public class StringMethods
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
                    if (int.TryParse(match.ToString(), out int i))
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

                totalCalibrationValue += NumberMethods.CalculateCalibrationLine(inputInts);
            });

            return totalCalibrationValue;
        }
    }

    public class NumberMethods
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
                    if (int.TryParse(c.ToString(), out int i))
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

            if (int.TryParse(concatInts, out int calibrationValue) && calibrationValue > 0)
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
