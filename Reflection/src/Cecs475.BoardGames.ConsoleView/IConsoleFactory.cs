using Cecs475.BoardGames.Model;

namespace Cecs475.BoardGames.ConsoleView {
	public interface IConsoleFactory {
		string Name { get; }

		IGameBoard CreateBoard();
		IConsoleView CreateView();
	}
}
