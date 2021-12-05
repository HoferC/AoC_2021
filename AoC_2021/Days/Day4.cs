using System;
namespace AoC_2021.Days
{
    public class Day4 : AocDay
    {
        private int[] _callingOrder;

        List<BingoBoard> _boards = new List<BingoBoard>();

        public Day4(string fileName)
        {
            DayId = 4;

            var lines = File.ReadAllLines(fileName);
            // First line is the calling order
            string callingOrder = lines[0];
            _callingOrder = callingOrder.Split(',').Select(s => int.Parse(s)).ToArray();
            // Skip a line and then the boards start
            int startingLine = 2;
            while (startingLine < lines.Length)
            {

                BingoBoard board = new BingoBoard(lines.Skip(startingLine).Take(5));
                _boards.Add(board);
                startingLine += 6;
            }
        }

        public override string Part1()
        {
            foreach (var number in _callingOrder)
            {
                foreach (var board in _boards)
                {
                    board.MarkSquare(number);
                    if (board.IsWinner)
                    {
                        return $"Winner found. Score {board.Score}";
                    }
                }
            }
            return "No winner found.";
        }

        public override string Part2()
        {
            BingoBoard lastBoard = null;
            foreach (var number in _callingOrder)
            {
                foreach (var board in _boards)
                {
                    board.MarkSquare(number);
                }
                int nonWinningBoards = _boards.Count(b => !b.IsWinner);
                if (lastBoard == null && _boards.Count(b => !b.IsWinner) == 1)
                {
                    lastBoard = _boards.Where(b => !b.IsWinner).First();
                }
                if (lastBoard != null && lastBoard.IsWinner)
                {
                    return $"The last board has score {lastBoard.Score}";
                }
            }
            return "No winner found.";
        }
    }
}

