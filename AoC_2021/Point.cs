using System;
namespace AoC_2021
{
    public struct Point
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public Point(int x, int y, int z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point(string coordinates)
        {
            var values = coordinates.Split(',');
            X = int.Parse(values[0]);
            Y = int.Parse(values[1]);
            if (values.Length == 3)
            {
                Z = int.Parse(values[2]);
            }
            else
            {
                Z = 0;
            }
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }
}

