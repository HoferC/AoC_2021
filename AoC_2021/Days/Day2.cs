using System;
namespace AoC_2021.Days
{
    public class Day2 : AocDay
    {
        class SubmarineInstruction
        {
            public Direction Direction { get; }
            public int Distance { get; }
            public SubmarineInstruction(string line)
            {
                var fields = line.Split(' ');
                Direction = (Direction)Enum.Parse(typeof(Direction), fields[0]);
                Distance = int.Parse(fields[1]);
            }
        }

        List<SubmarineInstruction> _input;

        public Day2(string fileName)
        {
            DayId = 2;
            _input = File.ReadAllLines(fileName).Select(l => new SubmarineInstruction(l)).ToList() ;
        }

        public override string Part1()
        {
            int depth = 0;
            int horizontal = 0;
            foreach (var instruction in _input)
            {
                if (instruction.Direction == Direction.forward)
                {
                    horizontal += instruction.Distance;
                }
                else if (instruction.Direction == Direction.up)
                {
                    depth -= instruction.Distance;
                }
                else if (instruction.Direction == Direction.down)
                {
                    depth += instruction.Distance;
                }
            }
            return $"Depth: {depth}. Horiz: {horizontal}. Product: {depth * horizontal}";
        }

        public override string Part2()
        {
            int depth = 0;
            int horizontal = 0;
            int aim = 0;
            foreach (var instruction in _input)
            {
                if (instruction.Direction == Direction.forward)
                {
                    horizontal += instruction.Distance;
                    depth += aim * instruction.Distance;
                }
                else if (instruction.Direction == Direction.up)
                {
                    aim -= instruction.Distance;
                }
                else if (instruction.Direction == Direction.down)
                {
                    aim += instruction.Distance;
                }
            }
            return $"Depth: {depth}. Horiz: {horizontal}. Aim: {aim}. Product: {depth * horizontal}";
        }
    }
}

