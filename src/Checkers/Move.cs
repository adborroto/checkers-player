using System.Collections.Generic;
using System.Linq;

namespace Checkers
{
    public class Move
    {
        public Position From { get; set; }
        public Position To { get; set; }

        public override string ToString()
        {
            return $"Move {From.Number}: from [{From.Column},{From.Row}] to [{To.Column},{To.Row}]";
        }

        public static bool TryParseMove(string stringMove, out Move move)
        {
            move = null;
            if (stringMove.StartsWith("E"))
            {

            }

            if (stringMove.StartsWith("M"))
            {
                var result = stringMove.Substring(1).Split(' ');
                var from = Position.FromCoors(int.Parse(result[0].Split(',')[1]), int.Parse(result[0].Split(',')[0]));
                var to = Position.FromCoors(int.Parse(result[1].Split(',')[1]), int.Parse(result[1].Split(',')[0]));
                move = new Move
                {
                    From = from,
                    To = to
                };
            }
            return true;
        }
    }

    public class Eat : Move { 
        public List<Position> Eaten { get; set; }

        public override string ToString()
        {
            return base.ToString() + $". And eat [{Eaten.Select(x=>x.Number).Aggregate("",(a,b)=>$"{a} {b}")}]";
        }
    }
}