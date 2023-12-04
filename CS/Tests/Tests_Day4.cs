using Utilities;
using Utilities.LogicLayer;

namespace Tests
{
    public class Tests_Day4
    {
        [TestClass]
        public class PartOne
        {
            [TestMethod]
            public void File_WithValidPath_IsNotNull()
            {
                // Arrange
                List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayFour\\TestInput.txt");

                // Assert
                Assert.IsNotNull(fileContent);
            }

            [TestMethod]
            public void File_WithValidPath_GetsContent()
            {
                // Arrange
                List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayFour\\TestInput.txt");

                // Assert
                Assert.IsTrue(fileContent.Count > 0);
            }

            [TestMethod]
            public void File_WithValidPath_CalculatesTotalScratchCardScore()
            {
                // Arrange
                List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayFour\\TestInput.txt");
                int scratchCardScore = 0;

                // Act
                fileContent?.ForEach(line =>
                {
                    var card = new Day4Card(line);

                    scratchCardScore += card.Score;
                });

                // Assert
                Assert.AreEqual(13, scratchCardScore);
            }
        }
    }
}
