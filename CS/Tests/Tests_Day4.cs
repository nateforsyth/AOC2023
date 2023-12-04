using Utilities;
using Utilities.Controller;

namespace Tests
{
    [TestClass]
    public class Tests_Day4
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

        [TestClass]
        public class PartOne
        {
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

        [TestClass]
        public class PartTwo
        {
            [TestMethod]
            public void File_WithValidPath_CalculatesTotalScratchCardScore()
            {
                // Arrange
                List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayFour\\TestInput.txt");
                List<Day4Card> cards = [];
                Dictionary<int, int> cardsAndCounts = [];

                fileContent?.ForEach(line =>
                {
                    var card = new Day4Card(line);
                    cards.Add(card);
                });

                // Act
                Dictionary<int, List<Day4Card>> processedCards = CollectionMethods.ProcessCards(cards);
                int cardsProcessed = processedCards.Sum(c => c.Value.Count);

                // Assert
                Assert.AreEqual(30, cardsProcessed);
            }
        }
    }
}
