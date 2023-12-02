using Utilities.LogicLayer;
using Utilities.Enums;
using System.Text;
using System.Text.RegularExpressions;

namespace Tests
{
    public class DayOneTests
    {
        [TestClass]
        public class PartOne
        {
            [TestMethod]
            public void File_WithValidPath_IsNotNull()
            {
                // Arrange
                List<string>? fileContent = null;
#if DEBUG
                fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayOne\\TestInput_1.txt");
#else
                fileContent = FileHelpers.GetFileContentFromRelativePath("/Shared/Input/DayOne/TestInput_1.txt");
#endif

                // Assert
                Assert.IsNotNull(fileContent);
            }

            [TestMethod]
            public void File_WithValidPath_GetsContent()
            {
                // Arrange
                List<string>? fileContent = null;
#if DEBUG
                fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayOne\\TestInput_1.txt");
#else
                fileContent = FileHelpers.GetFileContentFromRelativePath("/Shared/Input/DayOne/TestInput_1.txt");
#endif

                // Assert
                Assert.IsTrue(fileContent.Count > 0);
            }

            [TestMethod]
            public void File_WithValidPath_ValidatesTotalCalibrationValue()
            {
                // Arrange
                List<string>? fileContent = null;
#if DEBUG
                fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayOne\\TestInput_1.txt");
#else
                fileContent = FileHelpers.GetFileContentFromRelativePath("/Shared/Input/DayOne/TestInput_1.txt");
#endif

                // Act
                int totalCalibrationValue = NumberMethods.CalculateTotalCalibrationValue(fileContent);

                // Assert
                Assert.AreEqual(142, totalCalibrationValue);
            }
        }

        [TestClass]
        public class PartTwo
        {
            [TestMethod]
            public void File_WithValidPath_IsNotNull()
            {
                // Arrange
                List<string>? fileContent = null;
#if DEBUG
                fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayOne\\TestInput_2.txt");
#else
                fileContent = FileHelpers.GetFileContentFromRelativePath("/Shared/Input/DayOne/TestInput_2.txt");
#endif

                // Assert
                Assert.IsNotNull(fileContent);
            }

            [TestMethod]
            public void File_WithValidPath_GetsContent()
            {
                // Arrange
                List<string>? fileContent = null;
#if DEBUG
                fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayOne\\TestInput_2.txt");
#else
                fileContent = FileHelpers.GetFileContentFromRelativePath("/Shared/Input/DayOne/TestInput_2.txt");
#endif

                // Assert
                Assert.IsTrue(fileContent.Count > 0);
            }

            [TestMethod]
            public void Strings_WithValidData_ExtractsValidIntegerStringsFromInputString()
            {
                // Arrange
                List<string>? fileContent = null;
#if DEBUG
                fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayOne\\TestInput_2.txt");
#else
                fileContent = FileHelpers.GetFileContentFromRelativePath("/Shared/Input/DayOne/TestInput_2.txt");
#endif

                // Act
                int totalCalibrationValue = StringMethods.ExtractNumbersAndCalculateTotalCalibrationValue(fileContent);

                // Assert
                Assert.AreEqual(281, totalCalibrationValue);
            }
        }
    }
}