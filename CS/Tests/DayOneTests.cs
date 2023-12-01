using Utilities.Helpers;

namespace Tests
{
    [TestClass]
    public class DayOneTests
    {
        [TestMethod]
        public void File_WithValidPath_IsNotNull()
        {
            // Arrange
            List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayOne\\TestInput.txt");

            // Assert
            Assert.IsNotNull(fileContent);
        }

        [TestMethod]
        public void File_WithValidPath_GetsContent()
        {
            // Arrange
            List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayOne\\TestInput.txt");

            // Assert
            Assert.IsTrue(fileContent.Count > 0);
        }

        [TestMethod]
        public void File_WithValidPath_ValidatesTotalCalibrationValue()
        {
            // Arrange
            List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayOne\\TestInput.txt");
            var totalCalibrationValue = NumberHelpers.CalculateTotalCalibrationValue(fileContent);

            // Assert
            Assert.AreEqual(142, totalCalibrationValue);
        }
    }
}