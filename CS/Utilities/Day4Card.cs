namespace Utilities
{
    public class Day4Card
    {
        public int ID { get; set; }
        public List<int> PotentiallyWinningNumbers { get; set; } = new List<int>();
        public List<int> PlayerNumbers { get; set; } = new List<int>();
        public List<int> WinningNumbers { get; set; } = new List<int>();
        public int Score { get; set; }
        public int WinCount { get; set; }
        public bool IsCopy { get; set; }

        public Day4Card(string cardString)
        {
            List<string> cardEls = [.. cardString.Split(":", StringSplitOptions.RemoveEmptyEntries)];
            List<string> cardIdEls = [.. cardEls[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)];
            List<string> cardValueEls = [.. cardEls[1].Split("|", StringSplitOptions.RemoveEmptyEntries)];

            IsCopy = false;

            if (int.TryParse(cardIdEls[1], out int id))
            {
                ID = id;

                List<string> winningNumberStringEls = [.. cardValueEls.ElementAt(0).Split(" ", StringSplitOptions.RemoveEmptyEntries)];
                List<string> playerNumberStringEls = [.. cardValueEls.ElementAt(1).Split(" ", StringSplitOptions.RemoveEmptyEntries)];

                PotentiallyWinningNumbers = ConvertStringListToIntList(winningNumberStringEls);
                PlayerNumbers = ConvertStringListToIntList(playerNumberStringEls);

                PotentiallyWinningNumbers.ForEach(number =>
                {
                    if (PlayerNumbers.Contains(number))
                    {
                        WinCount++;
                        WinningNumbers.Add(number);
                        Score = WinCount == 1 ? 1 : Score * 2;
                    }
                });
            }
            else
            {
                Console.WriteLine($"Unable to process card element: {cardString}");
            }
        }

        public List<int> ConvertStringListToIntList(List<string> numberStrings)
        {
            List<int> parsedNumbers = [];
            numberStrings.ForEach(numberString =>
            {
                if (int.TryParse(numberString, out int number))
                {
                    parsedNumbers.Add(number);
                }
            });
            return parsedNumbers;
        }
    }
}
