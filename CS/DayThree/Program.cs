using System.Collections.Generic;
using System.Text;
using Utilities;
using Utilities.LogicLayer;

namespace DayThree
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
                //fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayThree\\TestInput.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DayTwo failed:\r\n\t{ex.Message}\r\n\t{ex.InnerException?.Message}");
            }

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
                            bool parsed = int.TryParse(c.ToString(), out int parsedInt);
                            if (!parsed && c != '.')
                            {
                                partPositioningSymbols.Add(c);
                            }
                        }
                        partNumberLine.Add(c);
                    }
                    partsMatrix.Add(currentLine++, partNumberLine);
                });

            Dictionary<(int xPosition, int yPosition), (char partElement, int partNumberLineLength)> partPositions = [];
            List<DayThreeEnginePart> parts = [];
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

                DayThreeEnginePart part = new(currentXPos, currentYPos, partElement.Value.partElement, partElement.Value.partNumberLineLength);
                                
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

            Console.WriteLine($"Day Three\r\n\tPart One\r\n\t\tAggregate of all adjacent part numbers: {aggregatedPartNumberTotal}\r\n\tPart Two\r\n\t\tAggregate of all Gear Ratios: {aggregatedGearRatioTotal}\r\n");

            DateTime end = DateTime.Now;
            Console.WriteLine($"Elapsed time: {(end - start).TotalMilliseconds}ms");
            Console.Read();
        }
    }
}
