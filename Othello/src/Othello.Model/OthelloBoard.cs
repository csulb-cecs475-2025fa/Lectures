using System;
using System.Collections.Generic;
using System.Linq;

namespace Othello.Game {
	/// <summary>
	/// Implements the board model for a game of othello. Tracks which squares of the 8x8 grid are occupied
	/// by which player, as well as state for the current player and move history.
	/// </summary>
	public class OthelloBoard {
		/// <summary>
		/// Represents a memory of all flips made in one particular direction 
		/// by the application of a move.
		/// </summary>x
		private struct FlipSet {
			/// <summary>
			/// The direction that flips were made.
			/// </summary>
			public BoardDirection Direction { get; set; }
			/// <summary>
			/// How many enemy pieces were flipped.
			/// </summary>
			public sbyte Count { get; set; }

			public FlipSet(BoardDirection direction, sbyte count) {
				Direction = direction;
				Count = count;
			}
		}

		#region Fields
		public const int BOARD_SIZE = 8;

		// Internally, we will represent pieces for each player as 1 or -1 (for player 2), which makes certain game 
		// operations easier to code. Those values don't make sense to the public, however, so we will expose them in a 
		// public property by mapping -1 to a value of 2. This will reduce coupling between other components and the 
		// private model logic.
		private int mCurrentPlayer = 1;

		// The board is represented by a 10x10 matrix of signed bytes. Each entry represents one square on the board,
		// except for row 0, row 9, column 0, and column 9, which represent out-of-bounds squares.
		// This representation would be confusing for the public, so we keep this field private and do not
		// expose it as a property. Instead, we will provide methods for retrieving specific positions on
		// the board.
		private sbyte[,] mBoard = {
			{8, 8, 8, 8, 8,  8, 8, 8, 8, 8},
			{8, 0, 0, 0, 0,  0, 0, 0, 0, 8},
			{8, 0, 0, 0, 0,  0, 0, 0, 0, 8},
			{8, 0, 0, 0, 0,  0, 0, 0, 0, 8},
			{8, 0, 0, 0, -1, 1, 0, 0, 0, 8},
			{8, 0, 0, 0, 1, -1, 0, 0, 0, 8},
			{8, 0, 0, 0, 0,  0, 0, 0, 0, 8},
			{8, 0, 0, 0, 0,  0, 0, 0, 0, 8},
			{8, 0, 0, 0, 0,  0, 0, 0, 0, 8},
			{8, 8, 8, 8, 8,  8, 8, 8, 8, 8},
		};

		// We need to keep track of the "history" of moves applied to the board. However,
		// we DON'T want the public to be able to mutate this list, only to inspect/iterate it.
		// So we make it a private field, then expose it to the public later as a "read only list".
		// This lets US mutate the list, but not the public.
		private List<BoardPosition> mMoveHistory = [];

		// The public has no need to know about flip sets.
		private List<List<FlipSet>> mFlipSets = [];

		// We'll store "advantage" using a +/- integer value that doesn't make sense to the public.
		// A computed property will let us convert our private representation to an easier form for the public.
		private int mAdvantageValue;
		#endregion

		#region Auto properties.
		/// <summary>
		/// How many "pass" moves have been applied in a row.
		/// </summary>
		public int PassCount { get; private set; }

		public GameAdvantage CurrentAdvantage { get; private set; }
		#endregion

		#region Computed properties.
		public IReadOnlyList<BoardPosition> MoveHistory {
			get { return mMoveHistory; }
		}

		public int CurrentPlayer {
			get {
				return mCurrentPlayer == 1 ? 1 : 2;
			}
		}

		public bool IsFinished {
			get { return PassCount == 2; }
		}
		#endregion

		#region Public methods
		// This is how we will expose the state of the gameboard in a way that reduces coupling.
		// No one needs to know HOW the data is represented; they simply need to know which player is
		// at which position.
		/// <summary>
		/// Returns an integer representing which player has a piece at the given position, or 0 if the position
		/// is empty.
		/// </summary>
		public int GetPlayerAtPosition(BoardPosition boardPosition) {
			sbyte pos = mBoard[boardPosition.Row + 1, boardPosition.Col + 1];
			return pos switch {
				-1 => 2, // -1 maps to player 2.
				8 => -1, // out of bounds
				_ => pos // otherwise the value is correct
			};
		}

		public void ApplyMove(BoardPosition p) {
			List<FlipSet> currentFlips = [];
			// If the move is a pass, then we do very little.
			if (p.Row == -1 && p.Col == -1) {
				PassCount++;
			}
			else {
				PassCount = 0;
				// Otherwise update the board at the move's position with the current player.
				SetPlayerAtPosition(p, CurrentPlayer);
				mAdvantageValue += mCurrentPlayer;

				// Iterate through all 8 directions radially from the move's position.
				foreach (BoardDirection dir in BoardDirection.CardinalDirections) {
					// Repeatedly move in the selected direction, as long as we find "enemy" squares.
					BoardPosition newPos = p;
					int steps = 0;
					do {
						newPos = newPos.Translate(dir);
						steps++;
					} while (PositionIsEnemy(newPos, CurrentPlayer));

					// This is a valid direction of flips if we moved at least 2 squares, and ended in bounds and on a
					// "friendly" square.
					if (steps > 1 && GetPlayerAtPosition(newPos) == CurrentPlayer) {
						// Record a FlipSet for this direction
						currentFlips.Add(new FlipSet(dir, (sbyte)(steps - 1)));

						var reverse = -dir;
						// Repeatedly walk back the way we came, updating the board with the current player's piece.
						do {
							newPos = newPos.Translate(reverse);
							SetPlayerAtPosition(newPos, CurrentPlayer);
							mAdvantageValue += 2 * mCurrentPlayer;

							steps--;
						}
						while (steps > 1);
					}
				}
			}

			// Update the rest of the board state.
			mCurrentPlayer = -mCurrentPlayer;
			SetAdvantage();
			mMoveHistory.Add(p);
			mFlipSets.Add(currentFlips);
		}

		/// <summary>
		/// Returns an enumeration of moves that would be valid to apply to the current board state.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<BoardPosition> GetPossibleMoves() {
			var moves = new List<BoardPosition>();

			foreach (BoardPosition position in BoardPosition.GetRectangularPositions(BOARD_SIZE, BOARD_SIZE)) {
				if (!PositionIsEmpty(position)) {
					continue;
				}

				// Iterate through all 8 cardinal directions from the current position.
				foreach (BoardDirection dir in BoardDirection.CardinalDirections) {
					// Repeatedly move in the selected direction, as long as we find "enemy" squares.
					BoardPosition newPos = position; // newPos = (2, 1); dir = <0, 1>
					int steps = 0;
					do {
						newPos = newPos.Translate(dir); // newPos = (2, 3)
						steps++; // 2
					} while (PositionIsEnemy(newPos, CurrentPlayer));

					// This is a valid direction of flips if we moved at least 2 squares, and ended in bounds and on a
					// "friendly" square.
					if (steps > 1 && GetPlayerAtPosition(newPos) == CurrentPlayer) {
						moves.Add(position);
						break;
					}
				}
			}

			// If no positions were valid, return a "pass" move.
			if (moves.Count == 0) {
				moves.Add(new BoardPosition(-1, -1));
			}

			return moves;
		}

		/// <summary>
		/// Undoes the last move, restoring the game to its state before the move was applied.
		/// </summary>
		public void UndoLastMove() {
			BoardPosition m = mMoveHistory[^1];

			if (m.Row != -1 || m.Col != -1) {
				// Reset the board at the move's position.
				SetPlayerAtPosition(m, 0);

				// Iterate through the move's recorded flipsets.
				foreach (var flipSet in mFlipSets[^1]) {
					BoardPosition pos = m;
					// For each flipset, walk along the flipset's direction resetting pieces.
					for (int i = 0; i < flipSet.Count; i++) {
						pos = pos.Translate(flipSet.Direction);
						// At this moment, CurrentPlayer is actually the enemy of the move that
						// we are undoing, whose pieces we must restore.
						SetPlayerAtPosition(pos, CurrentPlayer);
					}
				}

				// Check to see if the second-to-last move was a pass; if so, set PassCount.
				if (mMoveHistory.Count > 1 && mMoveHistory[^2] is { Row: -1, Col: -1} ) {
					PassCount = 1;
				}
			}
			else {
				PassCount--;
			}
			// Reset the remaining game state.
			SetAdvantage();
			mCurrentPlayer = -mCurrentPlayer;
			mMoveHistory.RemoveAt(mMoveHistory.Count - 1);
			mFlipSets.RemoveAt(mFlipSets.Count - 1);
		}
		#endregion

		#region Private methods
		private void SetAdvantage() {
			CurrentAdvantage = new GameAdvantage(mAdvantageValue > 0 ? 1 : mAdvantageValue < 0 ? 2 : 0,
				Math.Abs(mAdvantageValue));
		}

		private void SetPlayerAtPosition(BoardPosition position, int player) {
			mBoard[position.Row + 1, position.Col + 1] = (sbyte)(player <= 1 ? player : -1);
		}

		/// <summary>
		/// Returns true if the given in-bounds position is an enemy of the given player.
		/// </summary>
		/// <param name="pos">assumed to be in bounds</param>
		private bool PositionIsEnemy(BoardPosition pos, int player) => GetPlayerAtPosition(pos) + player == 3;

		private bool PositionIsEmpty(BoardPosition position) => GetPlayerAtPosition(position) == 0;
		#endregion
	}
}
