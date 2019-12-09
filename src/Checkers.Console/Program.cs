using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkers
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Stack<Tuple<Game, Tile>>();
            var board = Game.NewGame();
            var color = Tile.White;

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Play: {color}");
                Console.WriteLine(board);
                Console.WriteLine();


                var suggestedMove = Player.NextBestMove(board, color, 4);
                Console.WriteLine("Ale robot suggest to move: " + suggestedMove);
                Console.WriteLine(
                    $"Choose an option: {Environment.NewLine}\t " +
                    $"1)Play suggested move {Environment.NewLine}\t " +
                    $"2)Play custom move {Environment.NewLine}\t " +
                    $"3)Undo last move{Environment.NewLine}\t ");

                var optionSelected = Console.ReadLine();
                if (optionSelected == "1")
                {
                    game.Push(new Tuple<Game, Tile>(board, color));

                    board = board.Move(suggestedMove);
                    color = Helper.ChangeColor(color);
                }
                else if (optionSelected == "2")
                {
                    game.Push(new Tuple<Game, Tile>(board, color));

                    var moves = Player.Moves(board, color).ToArray();
                    for (int i = 0; i < moves.Length; i++)
                    {
                        Console.WriteLine($"{i}) " + moves[i]);
                    }
                    var option = Console.ReadLine();
                    board = board.Move(moves[int.Parse(option)]);
                    color = Helper.ChangeColor(color);
                }
                else if (optionSelected == "3")
                {
                    var undoMove = game.Pop();
                    board = undoMove.Item1;
                    color = undoMove.Item2;
                }
            }
        }
    }
}
