using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Controller;
using Utilities;
using System.Numerics;

namespace Tests
{
    [TestClass]
    public class Tests_Day5
    {
        [TestMethod]
        public void File_WithValidPath_IsNotNull()
        {
            // Arrange
            List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayFive\\TestInput.txt");

            // Assert
            Assert.IsNotNull(fileContent);
        }

        [TestMethod]
        public void File_WithValidPath_GetsContent()
        {
            // Arrange
            List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayFive\\TestInput.txt");

            // Assert
            Assert.IsTrue(fileContent.Count > 0);
        }

        [TestClass]
        public class PartOne
        {
            [TestMethod]
            public void File_WithValidPath_CalculatesLowestLocationValue()
            {
                // Arrange
                List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayFive\\TestInput.txt");

                // Act
                long lowestLocationNumber_PartOne = CollectionMethods.CalculateLowestLocationValueFromSeedPlantingList_1(fileContent);

                // Assert
                Assert.AreEqual(35, lowestLocationNumber_PartOne);
            }
        }

        [TestClass]
        public class PartTwo
        {
            [TestMethod]
            public void File_WithValidPath_CalculatesLowestLocationValueWithExtendedSeedRange()
            {
                // Arrange
                List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayFive\\TestInput.txt");

                // Act
                long lowestLocationNumber_PartTwo = CollectionMethods.CalculateLowestLocationValueFromSeedPlantingList_2(fileContent);

                // Assert
                Assert.AreEqual(46, lowestLocationNumber_PartTwo);
            }
        }
    }
}
