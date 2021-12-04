/// <summary>
/// A runner for a day of Advent of Code
/// </summary>
public abstract class AocDay
{
    public int DayId { get; protected set; }

    public abstract string Part1();

    public abstract string Part2();

    public override string ToString()
    {
        return $"Day {DayId} Part 1: {Part1()}{Environment.NewLine}Day {DayId} Part 2: {Part2()}";
    }
}
