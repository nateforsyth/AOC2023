namespace Utilities
{
    public class Day4Card
    {
        public int ID { get; set; }
        public List<int> PotentiallyWinningNumbers { get; set; } = new List<int>();
        public List<int> PlayerNumbers { get; set; } = new List<int>();
        public List<int> WinningNumbers { get; set; } = new List<int>();
        public int Score { get; set; }

        public Day4Card(string cardString)
        {
            List<string> cardEls = [.. cardString.Split(":", StringSplitOptions.RemoveEmptyEntries)];
            List<string> cardIdEls = [.. cardEls[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)];
            List<string> cardValueEls = [.. cardEls[1].Split("|", StringSplitOptions.RemoveEmptyEntries)];

            bool idParsed = int.TryParse(cardIdEls[1], out int id);
            if (idParsed)
            {
                ID = id;

                List<string> winningNumberStringEls = [.. cardValueEls.ElementAt(0).Split(" ", StringSplitOptions.RemoveEmptyEntries)];
                List<string> playerNumberStringEls = [.. cardValueEls.ElementAt(1).Split(" ", StringSplitOptions.RemoveEmptyEntries)];

                PotentiallyWinningNumbers = ConvertStringListToIntList(winningNumberStringEls);
                PlayerNumbers = ConvertStringListToIntList(playerNumberStringEls);

                int winNumber = 0;
                PotentiallyWinningNumbers.ForEach(number =>
                {
                    if (PlayerNumbers.Contains(number))
                    {
                        winNumber++;
                        WinningNumbers.Add(number);
                        Score = winNumber == 1 ? 1 : Score * 2;
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
                bool numberParsed = int.TryParse(numberString, out int number);

                if (numberParsed)
                {
                    parsedNumbers.Add(number);
                }
            });
            return parsedNumbers;
        }
    }
}
