/*
 * Lecture 2: Card game program. 
 * Lessons:
 *		Multi-file project organization.
 *		Entry points.
 *		Members: fields, properties, methods.
 *		Enums.
 */

// In a multi-file project, we create 1 file for each class that we define, plus 1 file to 
// be the application's "entry point". The name of the entrypoint is unimportant, but we often
// choose "Program".

// In a single-file program, certain libraries are automatically included for us. But now we are 
// doing everything manually. The System library includes classes for I/O, random numbers, math, and more.
using System;

// Every class should belong in a namespace, which differentiates the class from others that might have
// the same name. (How many classes named "Program" might exist in the world?)
namespace Cecs475.War {
	// Classes begin with an access modifier, specifying which other classes in .NET should be able to use
	// this class. Since this class is just the entry point of the application, and not something reusable by others,
	// we mark it "internal": it can only be used by code in this Project, and nowhere else.
	internal class Program {

		// The entry point for the application in the Main method. It always has this signature:
		internal static void Main(string[] args) {
			// internal: this method can only be called by this project, and not from outside the project.
			// static: this method does not operate on an object of the "Program" class (there won't be any).
			// void: the return type; this method does not return anything.
			// Main: the name of the method; it must be Main to be the project's entry point.
			// string[] args: the parameter to the method, an array of strings passed in from the command line.

			
			// This class has access to any class in the same project, or in a library that has been imported.

			// Build a deck for player 1 and 2.
			Deck d1 = new Deck();
			Deck d2 = new Deck();

			// We need a source of random numbers in order to shuffle the decks.
			Random generator = new Random();

			// Some debugging output.
			Console.WriteLine($"Player 1's deck (debug): {d1}");
			
			// Shuffle the decks.
			d1.Shuffle(generator);
			d2.Shuffle(generator);

			// More debugging output.
			Console.WriteLine();
			Console.WriteLine($"Player 1's deck shuffled: {d1}");

			// Keep track of player wins.
			int playerOneWins = 0, playerTwoWins = 0;

			Console.WriteLine();
			Console.WriteLine("Let's play WAR!");
			while (d1.Count > 0) { // calls the Count property accessor
								   // Deal one card from each deck, compare, and print result.
				Card c1 = d1.DealOne();
				Card c2 = d2.DealOne();
				Console.WriteLine($"{c1} vs. {c2} ...");

				int comparison = c1.CompareTo(c2);
				if (comparison == 0) {
					Console.WriteLine("It's a tie!");
				}
				else if (comparison < 0) {
					Console.WriteLine("Player 2 wins!");
					++playerTwoWins;
				}
				else {
					Console.WriteLine("Player 1 wins!");
					++playerOneWins;
				}

				// Ask to go to next deal.
				Console.WriteLine("Continue? y/n:");
				string again = Console.ReadLine() ?? "n"; // if ReadLine returns null, use "n".
														  // Therefore, "again" cannot be null,
														  // so we can use "string" instead of "string?".
				if (again != "y") {
					break;
				}
				Console.WriteLine();
			}

			Console.WriteLine($"Game over... Player 1 has {playerOneWins} wins, " +
				$"and Player 2 has {playerTwoWins}. GG!");
		}
	}
}
