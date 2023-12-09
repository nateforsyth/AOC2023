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

                if (long.TryParse(categories[0], out long destinationRangeStart) &&
                    long.TryParse(categories[1], out long sourceRangeStart) &&
                    long.TryParse(categories[2], out long rangeLength))
                {

                    CategoryMaps.Add(new CategoryMap(destinationRangeStart, sourceRangeStart, rangeLength));
                }
                else
                {
                     throw new ArgumentOutOfRangeException($"'{nameof(categoryMapString)}' was malformed.", nameof(categoryMapString));
                }
            }
        }

        public IEnumerable<long> MapSeedsToCategoryMaps(IEnumerable<long> seeds)
        {
            foreach (long seed in seeds)
            {
                yield return GetLowestCategoryNumberForSeed(seed);
            }
        }

        public long GetLowestCategoryNumberForSeed(long seed)
        {
            long categoryNumber = -1;
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

            return categoryNumber;
        }

        public class CategoryMap(long destinationRangeStart, long sourceRangeStart, long rangeLength)
        {
            public long DestinationRangeStart { get; set; } = destinationRangeStart;
            public long DestinationRangeEnd { get; set; } = destinationRangeStart + rangeLength - 1;
            public long SourceRangeStart { get; set; } = sourceRangeStart;
            public long SourceRangeEnd { get; set; } = sourceRangeStart + rangeLength - 1;
            public long RangeLength { get; set; } = rangeLength;
            public long Offset { get; set; } = destinationRangeStart - sourceRangeStart;
        }
    }
}
