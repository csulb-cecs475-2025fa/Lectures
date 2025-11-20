using Cecs475.BoardGames.ConsoleView;
using Cecs475.BoardGames.Model;
using Cecs475.BoardGames.Othello.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecs475.BoardGames.Othello.ConsoleView {
	public class OthelloConsoleFactory : IConsoleFactory {
		public string Name => "Othello";

		IGameBoard IConsoleFactory.CreateBoard() {
			return CreateBoard();
		}

		public OthelloBoard CreateBoard() {
			return new OthelloBoard();
		}

		IConsoleView IConsoleFactory.CreateView() {
			return CreateView();
		}

		public OthelloConsoleView CreateView() {
			return new OthelloConsoleView();
		}
	}
}
