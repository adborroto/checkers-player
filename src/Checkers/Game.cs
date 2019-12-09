using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers
{
    public class Game
    {
        public Tile[,] Tiles = new Tile[8, 8];
        protected Game() { }
        
        public void Set(int position, Tile piece)
        {
            var pos = Position.FromNumber(position);
            Set(pos, piece);
        }

        public void Set(Position pos, Tile piece)
        {
            Tiles[pos.Column, pos.Row] = piece;
        }

        public Tile GetTile(Position pos)
        {
            return Tiles[pos.Column, pos.Row];
        }

        public IEnumerable<Position> GetPositions(params Tile[] colors)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (colors.Contains(Tiles[i, j]))
                        yield return Position.FromCoors(j, i);
                }
            }
        }

        public Game Move(Move move)
        {
            Tile[,] tiles = Tiles.Clone() as Tile[,];
            var game = new Game
            {
                Tiles = tiles
            };

            if (move == null)
                return game;

            if (move is Move)
            {
                var tile = game.GetTile(move.From);
                game.Set(move.From, Tile.Empty);
                game.Set(move.To, tile);
            }

            if (move is Eat)
            {
                foreach (var toEat in (move as Eat).Eaten)
                {
                    game.Set(toEat, Tile.Empty);
                }
            }

            //Make a queen
            if(move.To.Row == 7 && game.GetTile(move.To) == Tile.White)
                game.Set(move.To, Tile.QueenWhite);
            if (move.To.Row == 0 && game.GetTile(move.To) == Tile.Black)
                game.Set(move.To, Tile.QueenBlack);

            return game;
        }

        public static Game Empty()
        {
            return new Game();
        }

        public static Game NewGame()
        {
            var board = new Game();
            for (int i = 1; i <= 12; i++)
            {
                board.Set(i, Tile.Black);
            }

            for (int i = 21; i <= 32; i++)
            {
                board.Set(i, Tile.White);
            }
            return board;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            Func<Tile,string> getTileString = (Tile t) => {
                switch (t)
                {
                    case Tile.Empty:
                        return " ";
                    case Tile.White:
                        return "x";
                    case Tile.Black:
                        return "o";
                    case Tile.QueenWhite:
                        return "X";
                    case Tile.QueenBlack:
                        return "O";
                    default:
                        return null;
                }

            };
            builder.Append("   _______________________" + Environment.NewLine);
            for (int y = 7; y >= 0; y--)
            {
                builder.Append(y + " ");
                for (int x = 0; x < 8; x++)
                {
                    builder.Append($"| {getTileString(Tiles[x, y])}");
                }
                builder.Append("|" +Environment.NewLine + "   _______________________" + Environment.NewLine);
            }
            builder.Append(Environment.NewLine+ "   0  1  2  3  4  5  6  7");
            return builder.ToString();
        }
    }
}
