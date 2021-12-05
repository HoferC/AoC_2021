using System;
namespace AoC_2021
{

    /// <summary>
    /// Represents a bingo board (5x5 set of numbers)
    /// </summary>
    public class BingoBoard
    {
        // Store the squares as a flat list, by rows, and by columns
        List<BingoSquare> _squares = new List<BingoSquare>();
        List<List<BingoSquare>> _rows = new List<List<BingoSquare>>();
        List<List<BingoSquare>> _columns = new List<List<BingoSquare>>();
        private int _lastNumberCalled;

        /// <summary>
        /// Gets whether this board is a "winner" (all squares of one row or column marked)
        /// </summary>
        public bool IsWinner => _rows.Any(r => r.All(sq => sq.Marked)) || _columns.Any(c => c.All(sq => sq.Marked));

        /// <summary>
        /// Gets the sum of the values of all unmarked squares on this board.
        /// </summary>
        public int SumOfUnmarked => _squares.Where(sq => !sq.Marked).Sum(sq => sq.Value);

        public int Score => SumOfUnmarked * _lastNumberCalled;


        public BingoBoard(IEnumerable<string> input)
        {
            // Establish the columns since we don't iterate by them
            for (int i = 0; i < 5; i++)
            {
                _columns.Add(new List<BingoSquare>());
            }
            foreach (var row in input)
            {
                var thisRow = new List<BingoSquare>();
                int colIndex = 0;
                foreach (var number in row.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                {
                    var sq = new BingoSquare(int.Parse(number));
                    _squares.Add(sq);
                    thisRow.Add(sq);
                    _columns[colIndex].Add(sq);
                    colIndex++;
                }

                _rows.Add(thisRow);
            }
        }

        public void MarkSquare(int number)
        {
            if (IsWinner)
            {
                return;
            }

            _squares.Where(sq => sq.Value == number).FirstOrDefault()?.Mark();
            _lastNumberCalled = number;
        }

        
    }

    internal class BingoSquare
    {
        public int Value { get; }
        public bool Marked { get; private set; }

        public BingoSquare(int value)
        {
            Value = value;
        }

        public void Mark()
        {
            Marked = true;
        }
    }
}

