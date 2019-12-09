using System;

namespace Checkers
{
    public class Position
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public int Number { get; set; }

        public static Position FromNumber(int position)
        {
            var row = 7 - ((position - 1) / 4);
            var offset = row % 2;
            var column = ((position - 1) % 4) * 2 + offset;
            return new Position
            {
                Column = column,
                Number = position,
                Row = row
            };
        }

        public static Position FromCoors(int row, int column)
        {
            if (!IsCoordValid(row, column))
                throw new Exception("invalid row or column");
            var position = 4 * (7 - row) + 1;
            position = position + column / 2;

            return new Position
            {
                Column = column,
                Number = position,
                Row = row
            };
        }

        public static bool IsCoordValid(int row, int column)
        {
            return row >= 0 && row < 8 && column >= 0 && column < 8;
        }

        public static bool Try(int row, int column, out Position position)
        {
            if (IsCoordValid(row, column))
            {
                position = Position.FromCoors(row, column);
                return true;
            }
            position = null;
            return false;
        }
    }
}
