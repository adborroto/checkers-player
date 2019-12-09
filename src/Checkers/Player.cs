using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkers
{
    public static class Player
    {
        public class MoveResult
        {
            public MoveResult(Move move, int weight)
            {
                Weight = weight;
                Move = move;
            }
            public int Weight { get; set; }

            public Move Move { get; set; }
        }

        public static Move NextBestMove(Game game, Tile color, int depth)
        {
            List<MoveResult> bestMoves = new List<MoveResult>();


            foreach (var position in game.GetPositions(Helper.GetTileColors(color)))
            {
                var move = NextBestMove(game, color, position, depth);
                if (move != null)
                {
                    bestMoves.Add(move);
                }
            }

            //Eat is obligatory
            var eatMoves = bestMoves.Where(x => x.Move is Eat);
            if (eatMoves.Any())
            {
                return eatMoves.OrderByDescending(x => x.Weight).First().Move;
            }
            return bestMoves.OrderByDescending(x => x.Weight).FirstOrDefault()?.Move ?? null;
        }

        public static IEnumerable<Move> Moves(Game game, Tile color)
        {
            List<Move> moves = new List<Move>();
            foreach (var position in game.GetPositions(Helper.GetTileColors(color)))
            {
                var tileMoves = Helper.CalculateMoves(game, position);
                moves.AddRange(tileMoves);
            }
            return moves;
        }


        public static MoveResult NextBestMove(Game game, Tile color, Position position, int depth)
        {
            if (depth == 0)
                return null;

            Move bestMove = null;
            var bestResult = int.MinValue;

            var moves = Helper.CalculateMoves(game, position);
            moves = RandomizeMoves(moves);

            foreach (var move in moves)
            {
                var newGame = game.Move(move);
                var nextBestMoveOther = NextBestMove(newGame, Helper.ChangeColor(color), depth - 1);
                newGame = newGame.Move(nextBestMoveOther);
                
                var nextBestMove = NextBestMove(newGame, color, depth - 1);
                newGame = newGame.Move(nextBestMove);
                var result = Score(newGame, color);
                if(result > bestResult)
                {
                    bestMove = move;
                    bestResult = result;
                }
            }

            return new MoveResult(bestMove,bestResult);
        }

        static IEnumerable<Move> RandomizeMoves(IEnumerable<Move> moves)
        {
           return moves.Select(x => new { Move = x, I = Guid.NewGuid() }).OrderBy(x => x.I).Select(x=>x.Move);
        }
       

        private static int Score(Game game, Tile color)
        {
            return 
                ScoreAmountTiles(game,color) + 
                ScoreQueens(game,color) + 
                ScoreDefencePosition(game,color);
        }

        static int ScoreAmountTiles(Game game, Tile color)
        {
            var mePieces = game.GetPositions(Helper.GetTileColors(color));
            var otherPieces = game.GetPositions(Helper.GetTileColors(Helper.ChangeColor(color)));

            var diferentBettwenPieces = mePieces.Count() - otherPieces.Count();
            return diferentBettwenPieces;

        }

        static int ScoreQueens(Game game, Tile color)
        {
            var mePieces = game.GetPositions(Helper.GetTileColors(color));
            var otherPieces = game.GetPositions(Helper.GetTileColors(Helper.ChangeColor(color)));

            var meQueens = mePieces.Select(x => game.GetTile(x)).Count(x => (int)x > 5) * 5;
            var otherQueens = otherPieces.Select(x => game.GetTile(x)).Count(x => (int)x > 5) * 5;
            var diferentBetwwenQueens = meQueens - otherQueens;
            return diferentBetwwenQueens;
        }

        static int ScoreDefencePosition(Game game, Tile color)
        {
           var mePieces = game.GetPositions(Helper.GetTileColors(color));

            var defencePositionRow = color == Tile.White ? 0 : 7;
            var amountDefending = mePieces.Where(x => x.Row == defencePositionRow).Count();
            
            return amountDefending;
        }
    }
}
