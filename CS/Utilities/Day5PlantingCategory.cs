using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Utilities.Day5PlantingCategory;

namespace Utilities
{
    public class Day5PlantingCategory(string name)
    {
        public string Name { get; set; } = name;
        public List<CategoryMap> CategoryMaps { get; set; } = [];

        public void AddCategoryMapInstance(string categoryMapString)
        {
            if (string.IsNullOrEmpty(categoryMapString))
            {
                throw new ArgumentException($"'{nameof(categoryMapString)}' cannot be null or empty.", nameof(categoryMapString));
            }
            else
            {
                List<string> categories = [.. categoryMapString.Split(" ")];

                if (BigInteger.TryParse(categories[0], out BigInteger destinationRangeStart) &&
                    BigInteger.TryParse(categories[1], out BigInteger sourceRangeStart) &&
                    BigInteger.TryParse(categories[2], out BigInteger rangeLength))
                {

                    CategoryMaps.Add(new CategoryMap(destinationRangeStart, sourceRangeStart, rangeLength));
                }
                else
                {
                     throw new ArgumentOutOfRangeException($"'{nameof(categoryMapString)}' was malformed.", nameof(categoryMapString));
                }
            }
        }

        public List<KeyValuePair<BigInteger, BigInteger>> MapSeedsToCategoryMaps(List<BigInteger> seeds)
        {
            List <KeyValuePair<BigInteger, BigInteger>> seedsAndCategories = [];
            foreach (BigInteger seed in seeds)
            {
                BigInteger categoryNumber = -1;
                foreach (CategoryMap categoryMap in CategoryMaps)
                {
                    if (seed >= categoryMap.SourceRangeStart && seed <= categoryMap.SourceRangeEnd)
                    {
                        categoryNumber = seed + categoryMap.Offset;
                        continue;
                    }
                }
                
                if (categoryNumber == -1)
                {
                    categoryNumber = seed;
                }

                seedsAndCategories.Add(new KeyValuePair<BigInteger, BigInteger>(seed, categoryNumber));
            }

            return seedsAndCategories;
        }

        public class CategoryMap(BigInteger destinationRangeStart, BigInteger sourceRangeStart, BigInteger rangeLength)
        {
            public BigInteger DestinationRangeStart { get; set; } = destinationRangeStart;
            public BigInteger DestinationRangeEnd { get; set; } = destinationRangeStart + rangeLength - 1;
            public BigInteger SourceRangeStart { get; set; } = sourceRangeStart;
            public BigInteger SourceRangeEnd { get; set; } = sourceRangeStart + rangeLength - 1;
            public BigInteger RangeLength { get; set; } = rangeLength;
            public BigInteger Offset { get; set; } = destinationRangeStart - sourceRangeStart;
        }
    }
}
