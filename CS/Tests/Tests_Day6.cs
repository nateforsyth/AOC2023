using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Controller;

namespace Tests
{
    [TestClass]
    public class Tests_Day6
    {
        [TestMethod]
        public void File_WithValidPath_IsNotNull()
        {
            // Arrange
            List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DaySix\\TestInput.txt");

            // Assert
            Assert.IsNotNull(fileContent);
        }

        [TestMethod]
        public void File_WithValidPath_GetsContent()
        {
            // Arrange
            List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DaySix\\TestInput.txt");

            // Assert
            Assert.IsTrue(fileContent.Count > 0);
        }

        [TestClass]
        public class PartOne
        {
            [TestMethod]
            public void File_WithValidPath_ExtractsTimesAndDistances()
            {
                // Arrange
                List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DaySix\\TestInput.txt");

                // Act
                var (times, distances) = CollectionMethods.GetTimesAndDistancesFromFileContent(fileContent);
                List<long> staticTimes = [7, 15, 30];
                List<long> staticDistances = [9, 40, 200];

                // Assert
                for (int i = 0; i < staticTimes.Count; i++)
                {
                    Assert.AreEqual(staticTimes[i], times[i]);
                }

                for (int i = 0; i < staticDistances.Count; i++)
                {
                    Assert.AreEqual(staticDistances[i], distances[i]);
                }
            }

            [TestMethod]
            public void File_WithValidPath_CalculatesShortRaceMarginOfError()
            {
                // Arrange
                List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DaySix\\TestInput.txt");

                // Act
                var (times, distances) = CollectionMethods.GetTimesAndDistancesFromFileContent(fileContent);
                long margin = AggregationMethods.CalculateRaceOutcome(times, distances);

                // Assert
                Assert.AreEqual(288, margin);
            }

            [TestMethod]
            public void File_WithValidPath_UsingMethod_CalculatesShortRaceMarginOfError()
            {
                // Arrange
                List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DaySix\\TestInput.txt");

                // Act
                long margin = AggregationMethods.CalculateShortRaceMarginOfError(fileContent);

                // Assert
                Assert.AreEqual(288, margin);
            }
        }

        [TestClass]
        public class PartTwo
        {
            [TestMethod]
            public void File_WithValidPath_ExtractsTimeAndDistance()
            {
                // Arrange
                List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DaySix\\TestInput.txt");

                // Act
                var (times, distances) = CollectionMethods.GetTimesAndDistancesFromFileContent(fileContent, true);
                long staticTime = 71530;
                long staticDistance = 940200;

                // Assert
                Assert.AreEqual(staticTime, times.FirstOrDefault());
                Assert.AreEqual(staticDistance, distances.FirstOrDefault());
            }

            [TestMethod]
            public void File_WithValidPath_CalculatesSingleRaceWinPossibilityCount()
            {
                // Arrange
                List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DaySix\\TestInput.txt");

                // Act
                var (times, distances) = CollectionMethods.GetTimesAndDistancesFromFileContent(fileContent);
                long margin = AggregationMethods.CalculateRaceOutcome(times, distances);

                // Assert
                Assert.AreEqual(288, margin);
            }

            [TestMethod]
            public void File_WithValidPath_UsingMethod_CalculatesSingleRaceWinPossibilityCount()
            {
                // Arrange
                List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DaySix\\TestInput.txt");

                // Act
                long possibilityCount = AggregationMethods.CalculateLongRacePossibilities(fileContent);

                // Assert
                Assert.AreEqual(71503, possibilityCount);
            }
        }
    }
}
