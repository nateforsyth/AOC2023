using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Utilities.Enums;

namespace Utilities.Controller
{
    public class AggregationMethods
    {
        /// <summary>
        /// Day Three - Aggregate total part number value and total gear ratio
        /// </summary>
        /// <param name="partPositioningSymbols"></param>
        /// <param name="partsMatrix"></param>
        /// <returns>Tuple; aggregatedPartNumberTotal, aggregatedGearRatioTotal</returns>
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

        /// <summary>
        /// Day Six, Part Two - Calculate the the number of possible ways a race can be won for a collection of races from imported file content
        /// </summary>
        /// <param name="fileContent"></param>
        /// <returns></returns>
        public static long CalculateLongRacePossibilities(List<string> fileContent)
        {
            var (times, distances) = CollectionMethods.GetTimesAndDistancesFromFileContent(fileContent, true);
            long possibilityCount = CalculateRaceOutcome(times, distances);
            return possibilityCount;
        }

        /// <summary>
        /// Day Six - Calculate race overallOutcome possibilities or overallOutcome for error
        /// </summary>
        /// <param name="times"></param>
        /// <param name="distances"></param>
        /// <returns></returns>
        public static long CalculateRaceOutcome(List<long> times, List<long> distances)
        {
            long overallOutcome = 0;
            List<long> raceOutcomes = [];

            if (times.Count == distances.Count)
            {
                for (int i = 0; i < times.Count; i++)
                {
                    List<long> outcomes = [];
                    var speed = 0;
                    var time = times[i];
                    var distance = distances[i];

                    for (long countdown = time; countdown > 0; countdown--)
                    {
                        if ((countdown * speed) > distance)
                        {
                            outcomes.Add(countdown);
                        }

                        speed++;
                    }

                    raceOutcomes.Add(outcomes.Count);
                }
            }

            overallOutcome = raceOutcomes.Aggregate((m1, m2) => m1 * m2);
            return overallOutcome;
        }

        /// <summary>
        /// Day Six, Part One - Calculate the overallOutcome of error for a collection of races from imported file content
        /// </summary>
        /// <param name="fileContent"></param>
        /// <returns></returns>
        public static long CalculateShortRaceMarginOfError(List<string> fileContent)
        {
            var (times, distances) = CollectionMethods.GetTimesAndDistancesFromFileContent(fileContent);
            long margin = CalculateRaceOutcome(times, distances);
            return margin;
        }
    }

    public class ValidationMethods
    {
        /// <summary>
        /// Day Three - Create Parts Matrix and Part Positioning Symbols from file content
        /// </summary>
        /// <param name="fileContent"></param>
        /// <returns>Tuple; partPositioningSymbols, partsMatrix</returns>
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
        /// Day Six - Calculate the the number of possible ways a race can be won for a collection of races from imported file content
        /// </summary>
        /// <param name="fileContent"></param>
        /// <param name="partTwo"></param>
        /// <returns></returns>
        public static (List<long> times, List<long> distances) GetTimesAndDistancesFromFileContent(List<string> fileContent, bool partTwo = false)
        {
            string pattern = @"\b\d+\b";

            List<long> times = [];
            List<long> distances = [];

            int index = 0;
            foreach (var line in fileContent)
            {
                MatchCollection matches = Regex.Matches(line, pattern, RegexOptions.IgnoreCase);

                if (partTwo)
                {
                    string aggregatedMatchStr = matches.Select(m => m.Value).Aggregate((s1, s2) => s1 + s2);
                    if (long.TryParse(aggregatedMatchStr, out long matchVal))
                    {
                        if (index == 0)
                        {
                            times.Add(matchVal);
                        }
                        else
                        {
                            distances.Add(matchVal);
                        }
                    }
                }
                else
                {
                    foreach (var match in matches.Cast<Match>())
                    {
                        if (match.Success && long.TryParse(match.Value, out long value))
                        {
                            if (index == 0)
                            {
                                times.Add(value);
                            }
                            else
                            {
                                distances.Add(value);
                            }
                        }
                    }
                }

                index++;
            }

            return (times, distances);
        }

        /// <summary>
        /// Day Five, Part One - Calculate the Lowest seed location value from planting data, uses distinctly defined seeds
        /// </summary>
        /// <param name="fileContent"></param>
        /// <param name="partTwo">Is part two, default = false</param>
        /// <returns>Single seed location value</returns>
        public static long CalculateLowestLocationValueFromSeedPlantingList_1(List<string> fileContent)
        {
            long lowestLocationNumber = -1;

            if (fileContent != null)
            {
                var parsedData = ParseDay5Input(fileContent);
                IEnumerable<long> seeds = parsedData.seeds;

                foreach (var plantingCategory in parsedData.plantingCategoryList)
                {
                    seeds = plantingCategory.MapSeedsToCategoryMaps(seeds).ToList();
                }
                long min = seeds.Min();
                lowestLocationNumber = min;
            }

            return lowestLocationNumber;
        }

        /// <summary>
        /// Day Two, Part One - Calculate the Lowest seed location value from planting data, uses seed ranges
        /// </summary>
        /// <param name="fileContent"></param>
        /// <param name="partTwo">Is part two, default = false</param>
        /// <returns>Single seed location value</returns>
        public static long CalculateLowestLocationValueFromSeedPlantingList_2(List<string> fileContent)
        {
            ConcurrentBag<long> lows = [];
            long lowestLocationNumber = -1;
            bool useRange = true;

            if (fileContent != null)
            {
                var (plantingCategoryList, seeds) = ParseDay5Input(fileContent, useRange);

                Parallel.ForEach(seeds, seed =>
                {
                    //Console.Write($"\r\n{seed}");
                    int iteration = 0;
                    long min = 0;
                    foreach (Day5PlantingCategory plantingCategory in plantingCategoryList)
                    {
                        //Console.Write($"\r\n\i|{iteration}");
                        if (iteration == 0)
                        {
                            min = plantingCategory.GetLowestCategoryNumberForSeed(seed);
                        }
                        else
                        {
                            min = plantingCategory.GetLowestCategoryNumberForSeed(min);
                        }

                        //Console.Write($", min: {min}");

                        if (plantingCategory.Name == "humidity-to-location")
                        {
                            var currentLowest = Interlocked.Read(ref lowestLocationNumber);
                            if (min < currentLowest || currentLowest == -1)
                            {
                                Interlocked.CompareExchange(ref lowestLocationNumber, min, lowestLocationNumber);
                            }
                        }

                        iteration++;
                    }
                });
            }

            Console.WriteLine();

            return lowestLocationNumber;
        }

        /// <summary>
        /// Day Five - Parse Day 5 input and get Lists of planting categories and seeds
        /// </summary>
        /// <param name="fileContent"></param>
        /// <returns>Tuple, List of planting categories and a List of seeds</returns>
        public static (List<Day5PlantingCategory> plantingCategoryList, IEnumerable<long> seeds) ParseDay5Input(List<string> fileContent, bool useRange = false)
        {
            List<Day5PlantingCategory> plantingCategoryList = [];
            IEnumerable<long> seeds = [];
            for (int lineIndex = 0; lineIndex < fileContent.Count; lineIndex++)
            {
                string line = fileContent[lineIndex];

                string elTitle = string.Empty;
                if (lineIndex == 0) // first line
                {
                    List<string> firstLineEls = [.. line.Split(": ")];
                    List<string> firstLineSeedEls = [];

                    if (firstLineEls.Count == 2)
                    {
                        elTitle = firstLineEls[0];
                        firstLineSeedEls = [.. firstLineEls.ElementAt(1).Split(" ")];

                        if (firstLineSeedEls.Count > 0)
                        {
                            seeds = StringMethods.GetSeedCollectionFromFileContent(firstLineSeedEls, useRange);
                        }
                    }
                }
                else if (string.IsNullOrEmpty(line)) // first empty line, continue
                {
                    continue;
                }
                else if (line.Contains("map")) // subsequent header line
                {
                    string[] headerLineEls = line.Split(" map:");

                    if (headerLineEls.Length > 0)
                    {
                        elTitle = headerLineEls[0];
                    }

                    Day5PlantingCategory plantingCategory = new Day5PlantingCategory(elTitle);
                    bool endOfFile = false;

                    while (!string.IsNullOrEmpty(line) && !endOfFile)
                    {
                        lineIndex++;
                        line = fileContent[lineIndex];

                        if (!string.IsNullOrEmpty(line))
                        {
                            try
                            {
                                plantingCategory.AddCategoryMapInstance(line);
                            }
                            catch (ArgumentNullException aNEx)
                            {
                                Console.WriteLine($"aNEx: {aNEx.Message}");
                            }
                            catch (ArgumentOutOfRangeException aOOREx)
                            {
                                Console.WriteLine($"aOOREx: {aOOREx.Message}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"ex: {ex.Message}");
                            }
                        }

                        endOfFile = lineIndex == fileContent.Count - 1;
                    }

                    plantingCategoryList.Add(plantingCategory);

                    continue;
                }
                else // subsequent line, shouldn'i ever be hit
                {
                    Console.WriteLine($"Something's gone horribly wrong!");
                }
            }

            return (plantingCategoryList, seeds);
        }

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
        /// Day Five - Calculate a collection of seeds from seed string elements using either distinct values or ranges
        /// </summary>
        /// <param name="seedEls"></param>
        /// <param name="useRange"></param>
        /// <returns>List of Int64 values of individual seeds</returns>
        public static IEnumerable<long> GetSeedCollectionFromFileContent(List<string> seedEls, bool useRange = false)
        {
            IEnumerable<long> seeds = [];
            long maxRange = 0;
            if (!useRange) // part one
            {
                foreach (string seedStr in seedEls)
                {
                    if (int.TryParse(seedStr, out int seed))
                    {
                        yield return seed;
                    }
                }
            }
            else // part two
            {
                int numberOfSeedPairs = seedEls.Count / 2;

                for (int i = 0; i < seedEls.Count; i++)
                {
                    string seedStr = seedEls[i];
                    string rangeStr = seedEls[++i];

                    bool seedLongParsed = long.TryParse(seedStr, out long seedLong);
                    bool rangeLongParsed = long.TryParse(rangeStr, out long rangeLong);
                    maxRange += rangeLong;

                    if (seedLongParsed && rangeLongParsed)
                    {
                        long calculatedLastSeed = (seedLong + rangeLong);
                        for (long seed = seedLong; seed < calculatedLastSeed; seed++)
                        {
                            yield return seed;
                        }
                    }
                }
            }

            //return seeds;
        }

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
