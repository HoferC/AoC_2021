using System;
namespace AoC_2021.Days
{
    public class Day6 : AocDay
    {
        List<int> _fish;
        public Day6(string fileName)
        {
            DayId = 6;
            string input = File.ReadAllText(fileName);
            _fish = input.Split(',').Select(i => int.Parse(i)).ToList();
        }

        public override string Part1()
        {
            // 80 days
            List<int> myFish = new List<int>(_fish);
            for (int day = 0; day < 80; day++)
            {
                List<int> fishToAdd = new List<int>();
                for (int i = 0; i < myFish.Count; i++)
                {
                    if (myFish[i] == 0)
                    {
                        myFish[i] = 6;
                        fishToAdd.Add(8);
                        continue;
                    }
                    myFish[i]--;
                }
                myFish.AddRange(fishToAdd);
            }
            return $"The final fish count after 80 days is: {myFish.Count}";
        }

        public override string Part2()
        {
            // Be smarter...
            // There is an equation for the behavior of a given fish
            // in terms of number of days
            // We can bucketize the population in terms of how many days remain on a given fish
            Dictionary<int, ulong> population = new Dictionary<int, ulong>();
            for (int i = 0; i < 9; i++)
            {
                population.Add(i, 0);
            }
            // Seed with the file input
            foreach (var fish in _fish)
            {
                population[fish] += 1;
            }

            for (int day = 0; day < 256; day++)
            {
                ulong spawningFish = population[0];
                // Everybody goes down one bucket
                for (int i = 1; i < 9; i++)
                {
                    population[i - 1] = population[i];
                }
                // Everyone in the spawning group will spawn new fish and reset themselves to 6
                population[8] = spawningFish;
                population[6] += spawningFish;
            }

            ulong populationCount = 0;
            foreach (var kvp in population)
            {
                populationCount += kvp.Value;
            }
            return $"The final fish count after 256 days is: {populationCount}";
        }
    }
}

