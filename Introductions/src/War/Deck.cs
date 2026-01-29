using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cecs475.War {
	/// <summary>
	/// Represents a deck of cards that can be shuffled and drawn from.
	/// </summary>
	public class Deck {
		// const values are automatically static, and cannot be changed.
		private const int NEW_DECK_SIZE = 52;

		// We have several choices of how to represent a deck of cards. We'll go with the simplest: an array of Card
		// objects, and a count of how many cards are still in the deck.
		private Card[] mCards;
		// We don't want the public to have access to each card in the deck; the order of the cards is private
		// to the deck, and cards can only be retrieved through methods below.

		// We need a public property to get the number of cards in the deck. We need to be able to change that
		// number, but the public should NOT. (WHY NOT?) So we can make the setter private; only we can call it.
		/// <summary>
		/// A count of the number of cards still in the deck.
		/// </summary>
		public int Count {
			get;
			private set;
		}

		// A computed property.
		public bool IsEmpty { get { return Count == 0; } }

		// An alternate syntax for read-only computed properties, where the computation is a single
		// return statement:
		public bool CanDeal => Count > 0;


		/// <summary>
		/// Construct a new unshuffled deck.
		/// </summary>
		public Deck() {
			mCards = new Card[NEW_DECK_SIZE];
			Count = NEW_DECK_SIZE; // this uses the private setter on Count.

			int i = 0;
			// For simplicity, we will abuse the fact that we know that CardSuit and CardKind are really just integers.
			foreach (Card.CardSuit suit in Enum.GetValues(typeof(Card.CardSuit))) {
				foreach (Card.CardKind kind in Enum.GetValues(typeof(Card.CardKind))) {
					mCards[i] = new Card(kind, suit);
					i++;
				}
			}
		}

		/// <summary>
		/// Performs a randomized shuffle of whichever cards are still in the deck.
		/// </summary>
		/// <param name="generator">A Random object to use when shuffling the deck.</param>
		public void Shuffle(Random generator) {
			// We could construct a Random object in this method, but that makes our code less testable.
			// We can't predict the outcome of Shuffle() when it makes its own Random object, because we can't
			// control the random sequence that results. 

			// We say the Shuffle method has a DEPENDENCY on a source of randomness. In good software, dependencies
			// are *inverted*: the code with the dependency does not construct that dependency itself; the dependency
			// is passed in as a parameter or otherwise made available to the code.

			// So here, instead of making a Random object ourselves (a hard-coded dependency), we require
			// the caller of this method to provide the Random object. (an inverted dependency)

			// Perform a Fisher-Yates shuffle.
			for (int i = Count - 1; i > 0; i--) {
				int j = generator.Next(i + 1);
				Card temp = mCards[j];
				mCards[j] = mCards[i];
				mCards[i] = temp;
			}
		}

		/// <summary>
		/// Deals one card, removing it from the top of the deck.
		/// </summary>
		/// <returns>the top card of the deck</returns>
		public Card DealOne() {
			// If Count is 52, we want to return array index 51, and end with a Count of 51.
			Count--;
			return mCards[Count];
		}

		// Return a string of all the cards in the deck, from top to bottom.
		public override string ToString() =>
			string.Join(", ", mCards.Take(Count).Reverse());
			// The => syntax is again shorthand for returning a single statement.
			// String.Join: creates a string by inserting the given delimiter between every element of a given collection.
			// Reverse(): reverses a sequence.
			// Take(n): returns only the first n elements of a sequence.


		// This is a poorly designed shuffle method. The algorithm is correct, but it hard-codes a *dependency*
		// -- the Random object used for random numbers. 
		public void ShuffleBadDesign() {
			Random generator = new Random();
			// Perform a Fisher-Yates shuffle.
			for (int i = Count - 1; i > 0; i--) {
				int j = generator.Next(i + 1);
				Card temp = mCards[j];
				mCards[j] = mCards[i];
				mCards[i] = temp;
			}
		}
	}
}
