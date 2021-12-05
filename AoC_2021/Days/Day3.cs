using System;
namespace AoC_2021.Days
{
    public class Day3 : AocDay
    {
        List<string> _lines = new List<string>();
        public Day3(string inputFileName)
        {
            DayId = 3;
            _lines = File.ReadAllLines(inputFileName).ToList();
        }

        public override string Part1()
        {
            // We need the count column-wise for ones and zeroes
            int[] onesCounts = GenerateCounts(_lines, '1');
            // Now we have counts in the array, use them to build the result
            string gammaString = "";
            string epsilonString = "";
            foreach (int count in onesCounts)
            {
                // Checking to see if the count is greater than the halfway point
                // means that 1 is more frequent than 0 in position `count`
                if (count > _lines.Count / 2)
                {
                    gammaString += "1";
                    epsilonString += "0";
                }
                else
                {
                    gammaString += "0";
                    epsilonString += "1";
                }
            }
            // Now we have the string representation in binary, convert to decimal
            int gamma = Convert.ToInt32(gammaString, 2);
            int epsilon = Convert.ToInt32(epsilonString, 2);

            return $"Gamma: {gamma}. Epsilon: {epsilon}. Product: {gamma * epsilon}";
        }

        /// <summary>
        /// Get the counts of the given character across all strings in the input
        /// </summary>
        /// <param name="input">IEnumerable of input strings</param>
        /// <param name="target">Character to look for within each string</param>
        /// <returns>An array with counts of the given target character across all input strings</returns>
        private int[] GenerateCounts(IList<string> input, char target)
        {
            int[] counts = new int[input[0].Length];
            foreach (var line in input)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    char digit = line[i];
                    if (digit == target)
                    {
                        counts[i]++;
                    }
                }
            }

            return counts;
        }

        public override string Part2()
        {
            int[] onesCounts;
            // Oxygen
            List<string> oxygenCandidates = new List<string>(_lines);
            int index = 0;
            while (oxygenCandidates.Count > 1)
            {
                onesCounts = GenerateCounts(oxygenCandidates, '1');
                // Determine the most common value in the current position and keep only those
                if (onesCounts[index] >= oxygenCandidates.Count / 2.0f)
                {
                    // Keep the ones
                    oxygenCandidates = oxygenCandidates.Where(c => c[index] == '1').ToList();
                }
                else
                {
                    // Keep the zeroes
                    oxygenCandidates = oxygenCandidates.Where(c => c[index] == '0').ToList();
                }
                index++;
            }
            int oxygenRating = Convert.ToInt32(oxygenCandidates[0], 2);

            // CO2
            List<string> co2Candidates = new List<string>(_lines);
            index = 0;
            while (co2Candidates.Count > 1)
            {
                onesCounts = GenerateCounts(co2Candidates, '1');
                // Determine the most common value in the current position and keep only those
                if (onesCounts[index] < co2Candidates.Count / 2.0f)
                {
                    // Keep the ones
                    co2Candidates = co2Candidates.Where(c => c[index] == '1').ToList();
                }
                else
                {
                    // Keep the zeroes
                    co2Candidates = co2Candidates.Where(c => c[index] == '0').ToList();
                }
                index++;
            }
            int co2Rating = Convert.ToInt32(co2Candidates[0], 2);

            return $"Oxygen: {oxygenRating}. CO2: {co2Rating}. Product: {oxygenRating * co2Rating}";
        }
    }
}

