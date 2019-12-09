using System;

namespace Checkers
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = Game.NewGame();
            var moves = 0;
            var tileColor = Tile.White;

            while (true)
            {
                Console.Clear();
                Console.WriteLine(board);
                Console.WriteLine();
                
                var pieceColor = moves % 2 == 0 ? "WHITE" : "BLACK";
                
                Console.WriteLine($"{pieceColor}");
                var suggestedMove = Player.NextBestMove(board, tileColor, 4);
                Console.WriteLine("Ale robot suggest to move: " + suggestedMove);
                Console.WriteLine($"Choose an option: {Environment.NewLine}\t 1)Apply move {Environment.NewLine}\t 2)Pick a move {Environment.NewLine}\t 3)List moves");
                var optionSelected = Console.ReadLine();
                if (optionSelected == "1")
                {
                    board = board.Move(suggestedMove);
                    moves++;
                    tileColor = tileColor == Tile.White ? Tile.Black : Tile.White;
                }
                else if (optionSelected == "3")
                {
                    foreach (var move in Player.Moves(board,tileColor))
                    {
                        Console.WriteLine(move);
                    }
                    Console.Read();
                }
                else
                {
                    Console.WriteLine("Write a move:");
                    var customMoveString = Console.ReadLine();
                    if(Move.TryParseMove(customMoveString, out Move move))
                    {
                        board = board.Move(move);
                    }
                    moves++;
                    tileColor = tileColor == Tile.White ? Tile.Black : Tile.White;
                }
                
            }

        }
    }
}
