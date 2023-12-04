namespace Utilities
{
    public class Day2Game
    {
        public int ID { get; set; }
        public List<Set> Sets { get; set; }
        public bool IsPossible { get; set; }
        public int TotalSetPower { get; set; }

        public Day2Game(int id, List<Set> sets, int redCount, int greenCount, int blueCount)
        {
            ID = id;
            Sets = sets;
            IsPossible = GameIsPossible(redCount, greenCount, blueCount);
            TotalSetPower = CalculateSetPowerFromMinimumCubes();
        }

        internal bool GameIsPossible(int redCount, int greenCount, int blueCount)
        {
            foreach (Set set in Sets)
            {
                if (!set.CanBePlayed(redCount, greenCount, blueCount))
                {
                    return false;
                }
            }

            return true;
        }

        public int CalculateSetPowerFromMinimumCubes()
        {
            int minRedCubes = 0;
            int minGreenCubes = 0;
            int minBlueCubes = 0;

            foreach (Set set in Sets)
            {
                if (set.RedCubeCount > minRedCubes)
                {
                    minRedCubes = set.RedCubeCount;
                }

                if (set.GreenCubeCount > minGreenCubes)
                {
                    minGreenCubes = set.GreenCubeCount;
                }

                if (set.BlueCubeCount > minBlueCubes)
                {
                    minBlueCubes = set.BlueCubeCount;
                }
            }

            int power = (minRedCubes * minGreenCubes) * minBlueCubes;

            return power;
        }

        public class Set
        {
            public int RedCubeCount { get; set; }
            public int GreenCubeCount { get; set; }
            public int BlueCubeCount { get; set; }

            internal bool CanBePlayed(int redCount, int greenCount, int blueCount)
            {
                bool redOk = RedCubeCount <= redCount;
                bool greenOk = GreenCubeCount <= greenCount;
                bool blueOk = BlueCubeCount <= blueCount;
                bool setPossible = redOk && greenOk && blueOk;

                return setPossible;
            }
        }
    }
}
