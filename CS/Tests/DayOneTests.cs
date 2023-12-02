using Utilities.Helpers;
using Utilities.Enums;
using System.Text;
using System.Text.RegularExpressions;

namespace Tests
{
    public class DayOneTests
    {
        [TestClass]
        public class DayOneTests_PartOne
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

                // Act
                int totalCalibrationValue = NumberHelpers.CalculateTotalCalibrationValue(fileContent);

                // Assert
                Assert.AreEqual(142, totalCalibrationValue);
            }
        }

        [TestClass]
        public class DayTwoTests_PartTwo
        {
            [TestMethod]
            public void File_WithValidPath_IsNotNull()
            {
                // Arrange
                List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayOne\\TestInput_2.txt");

                // Assert
                Assert.IsNotNull(fileContent);
            }

            [TestMethod]
            public void File_WithValidPath_GetsContent()
            {
                // Arrange
                List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayOne\\TestInput_2.txt");

                // Assert
                Assert.IsTrue(fileContent.Count > 0);
            }

            [TestMethod]
            public void Strings_WithValidData_ExtractsValidIntegerStringsFromInputString()
            {
                // Arrange
                List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayOne\\TestInput_2.txt");

                // Act
                int totalCalibrationValue = StringHelpers.ExtractNumbersAndCalculateTotalCalibrationValue(fileContent);

                // Assert
                Assert.AreEqual(281, totalCalibrationValue);
            }
        }
    }
}