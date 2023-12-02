﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.LogicLayer;

namespace Tests
{
    public class DayTwoTests
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
                fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayTwo\\TestInput_1.txt");
#else
                fileContent = FileHelpers.GetFileContentFromRelativePath("/Shared/Input/DayTwo/TestInput_1.txt");
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
                fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayTwo\\TestInput_1.txt");
#else
                fileContent = FileHelpers.GetFileContentFromRelativePath("/Shared/Input/DayTwo/TestInput_1.txt");
#endif

                // Assert
                Assert.IsTrue(fileContent.Count > 0);
            }

            [TestMethod]
            public void Game_IsValidInstance_IsNotPossible() // Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
            {
                // Arrange
                DayTwoGame.Set set1 = new()
                {
                    RedCubeCount = 20, BlueCubeCount = 6, GreenCubeCount = 8
                };

                DayTwoGame.Set set2 = new()
                {
                    RedCubeCount = 4, BlueCubeCount = 5, GreenCubeCount = 13
                };

                DayTwoGame.Set set3 = new()
                {
                    RedCubeCount = 1, GreenCubeCount = 5
                };

                List<DayTwoGame.Set> sets = [set1, set2, set3];

                int redCubeMax = 12, greenCubeMax = 13, blueCubeMax = 14;
                DayTwoGame game = new(3, sets, redCubeMax, greenCubeMax, blueCubeMax);

                // Assert
                Assert.IsFalse(game.IsPossible);
            }

            [TestMethod]
            public void Game_IsValidInstance_IsPossible() // Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
            {
                // Arrange
                DayTwoGame.Set set1 = new()
                {
                    RedCubeCount = 6, BlueCubeCount = 1, GreenCubeCount = 3
                };

                DayTwoGame.Set set2 = new()
                {
                    RedCubeCount = 1, BlueCubeCount = 2, GreenCubeCount = 2
                };

                List<DayTwoGame.Set> sets = [set1, set2];

                int redCubeMax = 12, greenCubeMax = 13, blueCubeMax = 14;
                DayTwoGame game = new(5, sets, redCubeMax, greenCubeMax, blueCubeMax);

                Assert.IsTrue(game.IsPossible);
            }

            [TestMethod]
            public void File_WithValidPath_AggregatesIdsOfPossibleGames()
            {
                // Arrange
                List<string>? fileContent = null;
#if DEBUG
                fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayTwo\\TestInput_1.txt");
#else
                fileContent = FileHelpers.GetFileContentFromRelativePath("/Shared/Input/DayTwo/TestInput_1.txt");
#endif
                int redCubeMax = 12, greenCubeMax = 13, blueCubeMax = 14;

                // Act
                (int idCount, int totalPower) = CollectionMethods.CalculateGameResults(fileContent, redCubeMax, greenCubeMax, blueCubeMax);

                // Assert
                Assert.AreEqual(8, idCount);
            }
        }

        [TestClass]
        public class PartTwo
        {
            [TestMethod]
            public void File_WithValidPath_IsNotNull()
            {
                // Arrange
                List<string> fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayTwo\\TestInput_1.txt");
#if DEBUG
                fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayTwo\\TestInput_1.txt");
#else
                fileContent = FileHelpers.GetFileContentFromRelativePath("/Shared/Input/DayTwo/TestInput_1.txt");
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
                fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayTwo\\TestInput_1.txt");
#else
                fileContent = FileHelpers.GetFileContentFromRelativePath("/Shared/Input/DayTwo/TestInput_1.txt");
#endif

                // Assert
                Assert.IsTrue(fileContent.Count > 0);
            }

            [TestMethod]
            public void File_WithValidPath_AggregatesTotalPowerValuesOfAllGames()
            {
                // Arrange
                List<string>? fileContent = null;
#if DEBUG
                fileContent = FileHelpers.GetFileContentFromRelativePath("\\Shared\\Input\\DayTwo\\TestInput_1.txt");
#else
                fileContent = FileHelpers.GetFileContentFromRelativePath("/Shared/Input/DayTwo/TestInput_1.txt");
#endif
                int redCubeMax = 12, greenCubeMax = 13, blueCubeMax = 14;

                // Act
                (int idCount, int totalPower) = CollectionMethods.CalculateGameResults(fileContent, redCubeMax, greenCubeMax, blueCubeMax);

                // Assert
                Assert.AreEqual(2286, totalPower);
            }
        }
    }
}