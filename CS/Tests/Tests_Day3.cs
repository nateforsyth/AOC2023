using Utilities.Controller;

namespace Tests
{
    [TestClass]
    public class Tests_Day3
    {
        [TestMethod]
        public void File_WithValidPath_IsNotNull()
        {
            // Arrange
            List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayThree\\TestInput.txt");

            // Assert
            Assert.IsNotNull(fileContent);
        }

        [TestMethod]
        public void File_WithValidPath_GetsContent()
        {
            // Arrange
            List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayThree\\TestInput.txt");

            // Assert
            Assert.IsTrue(fileContent.Count > 0);
        }

        [TestMethod]
        public void File_WithValidPath_ExtractsSupportedPartsPositioningSymbols()
        {
            // Arrange
            List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayThree\\TestInput.txt");

            // Act
            var (partPositioningSymbols, _) = ValidationMethods.CreatePartsMatrix(fileContent);

            // Assert
            Assert.IsNotNull(partPositioningSymbols);
            Assert.IsTrue(partPositioningSymbols.Count > 0);
        }

        [TestClass]
        public class PartOne
        {
            [TestMethod]
            public void File_WithValidPath_AggregatesPartNumberTotal()
            {
                // Arrange
                List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayThree\\TestInput.txt");

                // Act
                var (partPositioningSymbols, partsMatrix) = ValidationMethods.CreatePartsMatrix(fileContent);
                (int aggregatedPartNumberTotal, int _) = AggregationMethods.AggregatePartsData(partPositioningSymbols, partsMatrix);

                // Assert
                Assert.AreEqual(4361, aggregatedPartNumberTotal);
            }
        }

        [TestClass]
        public class PartTwo
        {
            [TestMethod]
            public void File_WithValidPath_AggregatesGearRatioTotal()
            {
                // Arrange
                List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayThree\\TestInput.txt");

                // Act
                var (partPositioningSymbols, partsMatrix) = ValidationMethods.CreatePartsMatrix(fileContent);
                (int _, int aggregatedGearRatioTotal) = AggregationMethods.AggregatePartsData(partPositioningSymbols, partsMatrix);

                // Assert
                Assert.AreEqual(467835, aggregatedGearRatioTotal);
            }
        }
    }
}
