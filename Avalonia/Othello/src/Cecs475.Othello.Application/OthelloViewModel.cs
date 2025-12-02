using System.Collections.Generic;
using System.Linq;
using Cecs475.Othello.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace Cecs475.Othello.AvaloniaApp {
	
	/// <summary>
	/// Represents the state of a View showing an Othello board. 
	/// </summary>
	public class OthelloViewModel : INotifyPropertyChanged {
		private OthelloBoard mBoard;
		// An ObservableCollection has events to notify listeners whenever a value is added, removed,
		// or an index of the collection is set.
		private ObservableCollection<OthelloSquare> mSquares;

		public event PropertyChangedEventHandler? PropertyChanged;
		private void OnPropertyChanged([CallerMemberName]string? name = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		public OthelloViewModel() {
			mBoard = new OthelloBoard();
			mSquares = new ObservableCollection<OthelloSquare>(
				BoardPosition.GetRectangularPositions(8, 8)
				.Select(p =>new OthelloSquare() {
					Position = p,
					Player = mBoard.GetPlayerAtPosition(p)
				})
			);

			PossibleMoves = mBoard.GetPossibleMoves().Select(m => m.Position).ToHashSet();
		}

		public void ApplyMove(BoardPosition position) {
			var possMoves = mBoard.GetPossibleMoves();
			foreach (var move in possMoves) {
				if (move.Position.Equals(position)) {
					mBoard.ApplyMove(move);
					break;
				}
			}

			// Rebind the mSquares list. We could be more efficient and detect exactly which 
			// squares have changed; or we can be lazy and just rebind everything.
			PossibleMoves = mBoard.GetPossibleMoves().Select(m => m.Position).ToHashSet();
			var newSquares = BoardPosition.GetRectangularPositions(8, 8);
			int i = 0;
			foreach (var pos in newSquares) {
				mSquares[i].Player = mBoard.GetPlayerAtPosition(pos);
				i++;
			}
			OnPropertyChanged("CurrentAdvantage");
		}

		/// <summary>
		/// The 64 squares of the othello board, in row-major order.
		/// </summary>
		public ObservableCollection<OthelloSquare> Squares {
			get { return mSquares; }
		}

		/// <summary>
		/// A set of all possible moves for the current game state.
		/// </summary>
		public HashSet<BoardPosition> PossibleMoves {
			get; private set;
		}

		/// <summary>
		/// The CurrentAdvantage object for the current game state.
		/// </summary>
		public GameAdvantage CurrentAdvantage {
			get { 
				return mBoard.CurrentAdvantage; 
			}
		}

	}

	/// <summary>
	/// The state of one square on an active othello board.
	/// </summary>
	public class OthelloSquare : INotifyPropertyChanged {
		private int mPlayer;
		/// <summary>
		/// The player that has a piece on this square, or 0 if it is empty.
		/// </summary>
		public int Player {
			get { return mPlayer; }
			set {
				if (value != mPlayer) {
					mPlayer = value;
					OnPropertyChanged();
				}
			}
		}

		/// <summary>
		/// The BoardPosition that this square represents.
		/// </summary>
		public BoardPosition Position {
			get; init;
		}

		private bool mIsHighlighted;
		/// <summary>
		/// Whether the square should be highlighted because of a user action.
		/// </summary>
		public bool IsHighlighted {
			get { return mIsHighlighted; }
			set {
				if (value != mIsHighlighted) {
					mIsHighlighted = value;
					OnPropertyChanged();
				}
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		private void OnPropertyChanged([CallerMemberName]string? name = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
