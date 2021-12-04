using System;
namespace AoC_2021.Days
{
    public class Day1 : AocDay
    {
        List<int> _input;

        public Day1(string fileName)
        {
            DayId = 1;
            _input = File.ReadAllLines(fileName).Select(l => int.Parse(l)).ToList() ;
        }

        public override string Part1()
        {
            int increaseCount = CountIncreases(_input);
            return $"The increase count is {increaseCount}";
        }

        private int CountIncreases(IList<int> input)
        {
            int increaseCount = 0;
            for (int i = 1; i < input.Count; i++)
            {
                if (input[i] > input[i - 1])
                {
                    increaseCount++;
                }
            }
            return increaseCount;
        }

        public override string Part2()
        {
            int increaseCount = 0;
            for (int i = 3; i < _input.Count; i++)
            {
                // Because the inner two values are the same,
                // we don't need them and can compare the outer edges.
                if (_input[i] > _input[i-3])
                {
                    increaseCount++;
                }
            }
            return $"The increase count is {increaseCount}";
        }
    }
}

