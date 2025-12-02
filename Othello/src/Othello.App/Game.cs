using System;
using System.IO;
using System.Linq;
using Othello.Game;

namespace Othello.App
{
	/// <summary>
	/// The View and Controller class for our Othello game.
	/// </summary>
	public class Game {
		static void Main(string[] args) {
			// The model and view for the game.
			OthelloBoard board = new OthelloBoard();

			while (!board.IsFinished) {
				// Print the view.
				Console.WriteLine();
				Console.WriteLine();
				PrintBoard(board);
				Console.WriteLine();
				Console.WriteLine();

				// Print the possible moves.
				var possMoves = board.GetPossibleMoves();
				Console.WriteLine("Possible moves:");
				Console.WriteLine(string.Join(", ", possMoves));

				// Print the turn indication.
				Console.WriteLine();
				Console.Write($"{GetPlayerString(board.CurrentPlayer)}'s turn: ");

				// Parse user input and apply their command.
				string? input = Console.ReadLine();
				if (input is null) {
					throw new Exception("Expected input from console");
				}

				if (input.StartsWith("move ")) {
					// Parse the move and validate that it is one of the possible moves before applying it.
					BoardPosition move = ParseMove(input[5..]);
					bool foundMove = false;
					foreach (var poss in possMoves) {
						if (poss.Equals(move)) {
							board.ApplyMove(poss);
							foundMove = true;
							break;
						}
					}
					if (!foundMove) {
						Console.WriteLine("That is not a possible move, please try again.");
					}
				}
				else if (input.StartsWith("undo ")) {
					// Parse the number of moves to undo and repeatedly undo one move.
					if (!int.TryParse(input.Split(' ')[1], out int undoCount)) {
						undoCount = 1;
					}
					for (int i = 0; i < undoCount && board.MoveHistory.Count > 0; i++) {
						board.UndoLastMove();
					}
				}
				else if (input == "history") {
					// Show the move history in reverse order.
					Console.WriteLine("History:");
					foreach (var move in board.MoveHistory.Reverse()) {
						Console.WriteLine($"{move}");
					}
				}
				else if (input == "advantage") {
					Console.WriteLine($"Advantage: {board.CurrentAdvantage.Advantage} " +
						$"in favor of {GetPlayerString(board.CurrentAdvantage.Player)}");
				}
			}
		}

		public static BoardPosition ParseMove(string move) {
			// Remove the () and split on the ,
			string[] split = move.Trim(['(', ')']).Split(',');
			return new BoardPosition(int.Parse(split[0]), int.Parse(split[1]));
		}

		/// <summary>
		/// Gets a string representing the given player.
		/// </summary>
		public static string GetPlayerString(int player) {
			return player == 1 ? "Black" : "White";
		}

		/// <summary>
		/// Prints a text representation of an OthelloBoard to the console.
		/// </summary>
		public static void PrintBoard(OthelloBoard board) {
			Console.WriteLine("- 0 1 2 3 4 5 6 7");
			for (int i = 0; i < OthelloBoard.BOARD_SIZE; i++) {
				Console.Write("{0} ", i);
				for (int j = 0; j < OthelloBoard.BOARD_SIZE; j++) {
					int space = board.GetPlayerAtPosition(new BoardPosition(i, j));
					char label = space switch {
						0 => '.',
						1 => 'B',
						_ => 'W'
					};
					Console.Write($"{label} ");
				}
				Console.WriteLine();
			}
		}
	}
}
