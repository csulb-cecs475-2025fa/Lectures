using Cecs475.BoardGames.Model;
using Cecs475.BoardGames.ConsoleView;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace Cecs475.TypeDemo {
	public struct DemoStruct {
		public int X { get; set; }
		public int Y { get; set; }

		public override string ToString() {
			return $"({X}, {Y}))";
		}

	}

	public class DemoProgram {
		public static void Main(string[] args) {
			// The Assembly class is a logical representation of a physical assembly file.
			// It contains information on the types defined in the assembly, among other things.
			Assembly current = Assembly.GetExecutingAssembly();

			Console.WriteLine($"Assembly: {current.FullName}");
			// the DefinedTypes property is a list of all Types defined in the assembly.
			foreach (Type t in current.DefinedTypes) {
				Console.WriteLine("{0} {1}", t.IsClass ? "class" : "struct", t.FullName);
				var propertiesList = t.GetProperties();

				if (propertiesList.Length > 0) {
					Console.WriteLine("Properties:");
					foreach (PropertyInfo property in propertiesList) {
						Console.WriteLine($"\t{property.PropertyType.Name} {property.Name}");
						
						//Console.WriteLine($"{property.CanRed ? 'g' : ' '} {property.CanWrite ? 's' : ' '}")
					}
				}

				Console.WriteLine();

				var meth = t.GetMethods();
				foreach (MethodInfo m in meth) {
					Console.WriteLine(m);
				}
			}

			// We can get a specific Type object with typeof, or by calling .GetType() on
			// an actual object instance.
			Type listType = typeof(List<int>);
			Type strType = "Hello".GetType();

			// We've seen we can enumerate a type's properties. We can also examine methods, constructors, etc.
			Console.WriteLine("List<int> methods:");
			foreach (var methodInfo in listType.GetMethods()) {
				Console.Write($"{methodInfo.ReturnType} {methodInfo.Name}(");
				Console.Write(
					$"{string.Join(", ", methodInfo.GetParameters().Select(p => p.ParameterType.Name + " " + p.Name))}");
				Console.WriteLine(")");
			}
			Console.WriteLine();

			// I can use a Type to make an object without "new".
			var board = (List<int>?)Activator.CreateInstance(listType); ; // Invoke with no parameters

			// Likewise, I can call a method without function-call syntax.
			var contains = (bool?)listType.GetMethod("Contains")?.Invoke(board, [99]); // Invoke "Contains(99)" on the new List<int> object.


			// This gives me an idea... can we find all types that match some criteria, as in,
			// all types that implement ICollection?
			Type iCollection = typeof(ICollection<>);
			var collectionTypes = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(a => a.GetTypes())
				.Where(t => t.GetInterfaces().Any(interf => interf.IsGenericType 
															&& interf.GetGenericTypeDefinition() == iCollection));

			Console.WriteLine("Found these ICollection types:");
			Console.WriteLine(string.Join(",\n", collectionTypes));
			Console.WriteLine();



			// We can also use Reflection to load a library that the application didn't know existed
			// when it was compiled.
			Assembly othelloAssembly = Assembly.LoadFrom("../../../../../lib/Cecs475.BoardGames.Othello.Model.dll");

			Type iGameBoard = typeof(IGameBoard);
			var gameTypes = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(a => a.GetTypes())
				.Where(t => iGameBoard.IsAssignableFrom(t) && t.IsClass);
			Console.WriteLine("Found these IGameBoard types:");
			Console.WriteLine(string.Join(",\n", gameTypes));

			// Now construct a game of an arbitrary type....
			IGameBoard newGame = (IGameBoard)Activator.CreateInstance(gameTypes.First())!;
			// Call methods from IGameBoard...
			IEnumerable<IGameMove> possible = newGame.GetPossibleMoves();
			// and profit!
			Console.WriteLine(string.Join(", ", possible.Select(m => m.ToString())));



			// NEW PROBLEM: this output is ugly, because OthelloMoves objects (from the Model) don't know 
			// how to display themselves on a console (a View problem). 
			// How do we define a dependency between OthelloMove and OthelloConsoleView, without breaking
			// the MVC pattern?

			// The "othello.json" file describes the libraries that are needed to play a game of othello.
			// Deserialize the json file into a C# object, and then work with it.
			GameInfo gamePlugin = JsonSerializer.Deserialize<GameInfo>(File.ReadAllText("../../../../../lib/othello.json"))!;
			Type? viewType = null;
			Type? boardType = null;

			// Load each library referenced by the json file.
			foreach (string libName in gamePlugin.libraries) {
				Assembly lib = Assembly.LoadFrom($"../../../../../lib/{libName}");
				// Check to see if the library contains the console view type listed in the json file.
				viewType = viewType ?? lib.GetType(gamePlugin.consoleViewType);
				boardType = boardType ?? lib.GetType(gamePlugin.boardType);
			}
			if (viewType != null && boardType != null) {
				// Now we can use this unknown viewType to work with its corresponding board object.
				IGameBoard letsPlay = (IGameBoard)Activator.CreateInstance(boardType)!; 
				IConsoleView view = (IConsoleView)Activator.CreateInstance(viewType)!;
				Console.WriteLine(view.BoardToString(letsPlay));
				Console.WriteLine(string.Join(", ", letsPlay.GetPossibleMoves().Select(view.MoveToString)));
			}
		}
	}



	public class GameInfo {
		public string version { get; set; }
		public string gameName { get; set; }
		public string[] libraries { get; set; }
		public string boardType { get; set; }
		public string consoleViewType { get; set; }
	}

}
