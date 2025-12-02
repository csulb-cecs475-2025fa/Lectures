using Xunit;
using Othello.Game;
using FluentAssertions;

/// <summary>
/// A test file for the Othello model classes. 
/// </summary>
namespace Othello.Tests {
	public class Tests {
		[Fact]
		public void NewBoard() {
			// Test setup of new board.
			OthelloBoard board = new OthelloBoard();
			foreach (var pos in BoardPosition.GetRectangularPositions(8, 8)) {
				// (3, 3) and (4, 4) are white (player 2).
				if (pos is { Row: 3, Col: 3 } or { Row: 4, Col: 4 }) {
					//Assert.True(board.GetPlayerAtPosition(pos) == 2, "white has two pieces at the start");

					board.GetPlayerAtPosition(pos).Should().Be(2, "white has two pieces at the start");
				}
				// (3, 4) and (4, 3) are black (player 1).
				else if (pos is { Row: 3, Col: 4 } or { Row: 4, Col: 3 }) {
					board.GetPlayerAtPosition(pos).Should().Be(1);
				}
				// All others are empty.
				else {
					board.GetPlayerAtPosition(pos).Should().Be(0);
				}
			}
		}

		[Fact]
		public void NewAdvantage() {
			// New board CurrentAdvantage is 0.
			OthelloBoard board = new OthelloBoard();
			board.CurrentAdvantage.Advantage.Should().Be(0, "New board advantage is 0");
		}

		[Fact]
		public void NewHistory() {
			// New board MoveHistory is empty.
			OthelloBoard board = new OthelloBoard();
			board.MoveHistory.Should().BeEmpty("New board has no history");
		}

		[Fact]
		public void StartingMoves() {
			OthelloBoard b = new OthelloBoard();
			var possMoves = b.GetPossibleMoves();
			possMoves.Should().HaveCount(4).And.BeEquivalentTo(
				[new BoardPosition(2, 3), new BoardPosition(3, 2), new BoardPosition(4, 5), 
				 new BoardPosition(5, 4)]
			);
		}

		[Fact]
		public void OneDirectionOneFlip() {
			OthelloBoard b = new OthelloBoard();
			b.ApplyMove(new(3, 2));
			b.GetPlayerAtPosition(new(3, 2)).Should().Be(1);
			b.GetPlayerAtPosition(new(3, 3)).Should().Be(1);
			b.GetPlayerAtPosition(new(3, 4)).Should().Be(1);
			b.GetPlayerAtPosition(new(4, 3)).Should().Be(1);
			b.GetPlayerAtPosition(new(4, 4)).Should().Be(2);
		}

		[Fact]
		public void OneDirectionManyFlips() {
			OthelloBoard b = new OthelloBoard();
			b.ApplyMove(new(3, 2));
			b.ApplyMove(new(4, 2));
			b.ApplyMove(new(5, 2));
			
			b.GetPlayerAtPosition(new(4, 2)).Should().Be(1);
			b.GetPlayerAtPosition(new(4, 3)).Should().Be(1);
			b.GetPlayerAtPosition(new(4, 4)).Should().Be(2);

			b.ApplyMove(new(4, 1));
			b.GetPlayerAtPosition(new(4, 1)).Should().Be(2);
			b.GetPlayerAtPosition(new(4, 2)).Should().Be(2);
			b.GetPlayerAtPosition(new(4, 3)).Should().Be(2);
			b.GetPlayerAtPosition(new(4, 4)).Should().Be(2);
		}

			[Fact]
		public void MultiDirectionMove() {
			// Value and board updated correctly after doing a multi-directional move.
			OthelloBoard board = new OthelloBoard();
			board.ApplyMove(new BoardPosition(3, 2));
			board.ApplyMove(new BoardPosition(4, 2));
			board.CurrentAdvantage.Advantage.Should().Be(0);
			board.ApplyMove(new BoardPosition(5, 2));
			board.CurrentAdvantage.Advantage.Should().Be(5);
			
			// Check both "up" and "up-right" directions to make sure pieces were flipped.
			board.GetPlayerAtPosition(new BoardPosition(5, 2)).Should().Be(1);
			board.GetPlayerAtPosition(new BoardPosition(4, 2)).Should().Be(1);
			board.GetPlayerAtPosition(new BoardPosition(3, 2)).Should().Be(1);
			board.GetPlayerAtPosition(new BoardPosition(4, 3)).Should().Be(1);
			board.GetPlayerAtPosition(new BoardPosition(3, 4)).Should().Be(1);

			// But not the one remaining black piece.
			board.GetPlayerAtPosition(new BoardPosition(4, 4)).Should().Be(2);
		}
	}
}
