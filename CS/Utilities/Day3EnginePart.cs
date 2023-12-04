using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Utilities
{
    public class Day3EnginePart(int xPosition, int yPosition, char partElement, int partNumberLineLength)
    {
        public int XPosition { get; set; } = xPosition;
        public int YPosition { get; set; } = yPosition;
        public char PartElement { get; set; } = partElement;
        public int PartNumberLineLength { get; set; } = partNumberLineLength;
        public List<(int x, int y)> SurroundingCoordinates { get; set; } = CalculateSurroundingCoordinates(xPosition, yPosition, partNumberLineLength);
        public Dictionary<int, List<char>>? SurroundingLines { get; set; }
        public List<Match>? SurroundingParts { get; private set; }
        public int SurroundingPartNumberAggregate { get; set; }
        public int GearRatio { get; set; }

        public void CalculateSurroundingPartsFromCoords(Dictionary<int, List<char>> surroundingLines, char partElement)
        {
            List<Match> surroundingParts = [];
            int partNumberAggregateTotal = 0;
            string pattern = @"\b\d+\b";
            bool partElementIsGear = partElement == '*';

            foreach (var coordLine in surroundingLines)
            {
                string coordLineValue = new string(coordLine.Value.ToArray());
                
                if (!string.IsNullOrEmpty(coordLineValue))
                {
                    MatchCollection matches = Regex.Matches(coordLineValue, pattern, RegexOptions.IgnoreCase);

                    foreach (Match match in matches.Cast<Match>())
                    {
                        if (match.Success)
                        {
                            SurroundingCoordinates.ForEach(coord =>
                            {
                                if (coordLine.Key == coord.y)
                                {
                                    if (coord.x >= match.Index && coord.x < (match.Index + match.Length))
                                    {                                        
                                        if (int.TryParse(match.Value, out int partNumber) && !surroundingParts.Contains(match))
                                        {
                                            surroundingParts.Add(match);
                                            partNumberAggregateTotal += partNumber;
                                        }
                                    }
                                }
                            });
                        }
                    }
                }
            }

            SurroundingParts = surroundingParts;
            SurroundingPartNumberAggregate = partNumberAggregateTotal;

            if (partElementIsGear && surroundingParts.Count == 2)
            {
                if (int.TryParse(surroundingParts[0].Value, out int firstPartNumber) && int.TryParse(surroundingParts[1].Value, out int secondPartNumber))
                {
                    GearRatio = firstPartNumber * secondPartNumber;
                }
            }
        }

        // TODO attempt to use outer bounds of the matrix grid to define invalid coordinates
        private static List<(int x, int y)> CalculateSurroundingCoordinates(int xPosition, int yPosition, int partNumberLineLength)
        {
            List<(int x, int y)> surroundingCoordinates = [];
            if (xPosition == 0) // element is at the start of partNumberLine
            {
                if (yPosition == 0) // element is in the first line
                {
                    surroundingCoordinates.Add((xPosition + 1, yPosition));
                    surroundingCoordinates.Add((xPosition, yPosition + 1));
                    surroundingCoordinates.Add((xPosition + 1, yPosition + 1));
                }
                else if (yPosition == (partNumberLineLength - 1)) // element is in the last line
                {
                    surroundingCoordinates.Add((xPosition, yPosition - 1));
                    surroundingCoordinates.Add((xPosition + 1, yPosition - 1));
                    surroundingCoordinates.Add((xPosition + 1, yPosition));
                }
                else // element is somewhere in the middle of the lines of the partsMatrix
                {
                    surroundingCoordinates.Add((xPosition, yPosition - 1));
                    surroundingCoordinates.Add((xPosition + 1, yPosition - 1));
                    surroundingCoordinates.Add((xPosition + 1, yPosition));
                    surroundingCoordinates.Add((xPosition, yPosition + 1));
                    surroundingCoordinates.Add((xPosition + 1, yPosition + 1));
                }
            }
            else if (xPosition == (partNumberLineLength - 1)) // element is at the end of a partNumberLine
            {
                if (yPosition == 0) // element is in the first line
                {
                    surroundingCoordinates.Add((xPosition - 1, yPosition));
                    surroundingCoordinates.Add((xPosition - 1, yPosition + 1));
                    surroundingCoordinates.Add((xPosition, yPosition + 1));
                }
                else if (yPosition == (partNumberLineLength - 1)) // element is in the last line
                {
                    surroundingCoordinates.Add((xPosition - 1, yPosition - 1));
                    surroundingCoordinates.Add((xPosition, yPosition - 1));
                    surroundingCoordinates.Add((xPosition - 1, yPosition));
                }
                else // element is somewhere in the middle of the lines of the partsMatrix
                {
                    surroundingCoordinates.Add((xPosition - 1, yPosition - 1));
                    surroundingCoordinates.Add((xPosition, yPosition - 1));
                    surroundingCoordinates.Add((xPosition - 1, yPosition));
                    surroundingCoordinates.Add((xPosition - 1, yPosition + 1));
                    surroundingCoordinates.Add((xPosition, yPosition + 1));
                }
            }
            else // element is somewhere in the middle of a partNumberLine
            {
                if (yPosition == 0) // element is in the first line
                {
                    surroundingCoordinates.Add((xPosition - 1, yPosition));
                    surroundingCoordinates.Add((xPosition + 1, yPosition));
                    surroundingCoordinates.Add((xPosition - 1, yPosition + 1));
                    surroundingCoordinates.Add((xPosition, yPosition + 1));
                    surroundingCoordinates.Add((xPosition + 1, yPosition + 1));
                }
                else if (yPosition == (partNumberLineLength - 1)) // element is in the last line
                {
                    surroundingCoordinates.Add((xPosition - 1, yPosition - 1));
                    surroundingCoordinates.Add((xPosition, yPosition - 1));
                    surroundingCoordinates.Add((xPosition + 1, yPosition - 1));
                    surroundingCoordinates.Add((xPosition - 1, yPosition));
                    surroundingCoordinates.Add((xPosition + 1, yPosition));
                }
                else // element is somewhere in the middle of the lines of the partsMatrix
                {
                    surroundingCoordinates.Add((xPosition - 1, yPosition - 1));
                    surroundingCoordinates.Add((xPosition, yPosition - 1));
                    surroundingCoordinates.Add((xPosition + 1, yPosition - 1));
                    surroundingCoordinates.Add((xPosition - 1, yPosition));
                    surroundingCoordinates.Add((xPosition + 1, yPosition));
                    surroundingCoordinates.Add((xPosition - 1, yPosition + 1));
                    surroundingCoordinates.Add((xPosition, yPosition + 1));
                    surroundingCoordinates.Add((xPosition + 1, yPosition + 1));
                }
            }

            return surroundingCoordinates;
        }
    }
}
