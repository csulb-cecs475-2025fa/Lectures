using System;
using System.Collections.Generic;
using System.Text;
using Cecs475.BoardGames.Model;
using Cecs475.BoardGames.Othello.Model;
using Cecs475.BoardGames.ConsoleView;

namespace Cecs475.BoardGames.Othello.ConsoleView {
	public class OthelloConsoleView : IConsoleView {
		private static char[] LABELS = ['.', 'B', 'W'];

		public string BoardToString(OthelloBoard board) {
			StringBuilder str = new StringBuilder();
			str.AppendLine("- 0 1 2 3 4 5 6 7");
			for (int i = 0; i < OthelloBoard.BOARD_SIZE; i++) {
				str.Append(i);
				str.Append(" ");
				for (int j = 0; j < OthelloBoard.BOARD_SIZE; j++) {
					int space = board.GetPlayerAtPosition(new BoardPosition(i, j));
					str.Append(LABELS[space]);
					str.Append(' ');
				}
				str.AppendLine();
			}
			return str.ToString();
		}

		public string MoveToString(OthelloMove move) {
			return $"({move.Position.Row}, {move.Position.Column})";
		}

		public OthelloMove ParseMove(string moveText) {
			string[] split = moveText.Trim(['(', ')']).Split(',');
			if (split.Length != 2 || !int.TryParse(split[0], out int row) || !int.TryParse(split[1], out int col)) {
				throw new ArgumentException($"Could not parse the move string {moveText}");
			}
			return new OthelloMove(new BoardPosition(row, col));
		}

		public string PlayerToString(int player) {
			return player == 1 ? "Black" : "White";
		}

		string IConsoleView.BoardToString(IGameBoard board) {
			if (board is not OthelloBoard ob) {
				throw new ArgumentException($"{nameof(OthelloConsoleView)} can only work with {nameof(OthelloBoard)} objects");
			}
			return BoardToString(ob);
		}

		string IConsoleView.MoveToString(IGameMove move) {
			if (move is not OthelloMove om) {
				throw new ArgumentException($"{nameof(OthelloConsoleView)} can only work with {nameof(OthelloMove)} objects");
			}
			return MoveToString(om);
		}

		IGameMove IConsoleView.ParseMove(string moveText) {
			return ParseMove(moveText);
		}
	}
}
