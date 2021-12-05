using System;
namespace AoC_2021.Days
{
    public class Day5 : AocDay
    {
        List<Vent> _vents;

        public Day5(string fileName)
        {
            DayId = 5;

            _vents = File.ReadAllLines(fileName).Select(line => new Vent(line)).ToList();
            
        }

        public override string Part1()
        {
            // Get all the points covered by all the vents
            var allCoveredPoints = _vents.SelectMany(v => v.CoveredPoints(false)).ToList();
            // Get all of the points which have a count greater than 1
            var groups = allCoveredPoints.GroupBy(p => p);
            int pointsOfOverlap = groups.Where(g => g.Count() > 1).Count();
            return $"Points of overlap (Horizontal and Vertical): {pointsOfOverlap}";
        }

        public override string Part2()
        {
            // Get all the points covered by all the vents
            var allCoveredPoints = _vents.SelectMany(v => v.CoveredPoints(true)).ToList();
            // Get all of the points which have a count greater than 1
            var groups = allCoveredPoints.GroupBy(p => p);
            int pointsOfOverlap = groups.Where(g => g.Count() > 1).Count();
            return $"Points of overlap (with Diagonals): {pointsOfOverlap}";
        }
    }

    internal class Vent
    {
        public Point From { get; }
        public Point To { get; }

        public bool IsHorizontal => From.Y == To.Y;
        public bool IsVertical => From.X == To.X;

        
        /// <summary>
        /// Create a new Vent using a definition like "1,2 -> 3,4"
        /// </summary>
        /// <param name="definition"></param>
        public Vent(string definition)
        {
            var parts = definition.Split(' ');
            From = new Point(parts[0]);
            To = new Point(parts[2]);
        }

        /// <summary>
        /// Gets the points covered by this Vent
        /// </summary>
        public IEnumerable<Point> CoveredPoints(bool withDiagonal = false)
        {
            List<Point> coveredPoints = new List<Point>();
            if (IsHorizontal)
            {
                // Points can be in any order
                for (int x = Math.Min(From.X, To.X); x <= Math.Max(From.X, To.X); x++)
                {
                    coveredPoints.Add(new Point(x, From.Y));
                }
            }
            else if (IsVertical)
            {
                for (int y = Math.Min(From.Y, To.Y); y <= Math.Max(From.Y, To.Y); y++)
                {
                    coveredPoints.Add(new Point(From.X, y));
                }
            }
            else if (withDiagonal)
            {
                // Diagonal
                // Determine the direction we need to move (up and right, down and right, up and left, down and left)
                Point currentPoint = From;
                int[] direction = { 0, 0 };
                if (From.X < To.X)
                {
                    // Moving left to right
                    direction[0] = 1;
                }
                else
                {
                    direction[0] = -1;
                }
                if (From.Y < To.Y)
                {
                    // Moving top to bottom (low Y to higher Y)
                    direction[1] = 1;
                }
                else
                {
                    direction[1] = -1;
                }
                while (currentPoint.X != To.X && currentPoint.Y != To.Y)
                {
                    coveredPoints.Add(currentPoint);
                    currentPoint = new Point(currentPoint.X + direction[0], currentPoint.Y + direction[1]);
                }
                // Add the To point because we are filtering it out with the while condition
                coveredPoints.Add(To);
            }
            return coveredPoints;
        }
    }
}

