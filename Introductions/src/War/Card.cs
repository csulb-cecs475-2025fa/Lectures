using System;

// A namespace is a logical grouping of types under one name. A namespace is added to a type's name to create its
// Fully Qualified Name. Namespaces are similar to Python modules, except Python modules define a physical on-disk
// grouping in addition to a logical grouping.
namespace Cecs475.War {
	// A comment like the one below is a documentation comment.
	/// <summary>
	/// Represents a single card in a 52-card deck of playing cards.
	/// </summary>
	public struct Card : IComparable<Card> {
		// : IComparable<Card> states that the Card class implements an *interface* called IComparable<Card>.
		// We will talk about interfaces later; for now, this communicates to other programmers that Card objects
		// can be compared to other Card objects to see which is "larger".

		// An enum is a new type whose values can only be taken from the names in the enum declaration. Each value
		// in the enum is secretly an integer counting up from 0.
		// Because this type is declared inside Card, other types will have to use the name "Card.Suit"
		// "public" here means that anyone can reference this declaration.
		public enum CardSuit {
			Spades, // 0
			Clubs,  // 1, etc.
			Diamonds,
			Hearts
		}

		public enum CardKind {
			Two = 2, // a value can be supplied explicitly, and other values will count up from there.
			Three,
			Four,
			Five,
			Six,
			Seven,
			Eight,
			Nine,
			Ten,
			Jack,
			Queen,
			King,
			Ace // == 14
		}

		/// <summary>
		/// The card's suit.
		/// </summary>
		public CardSuit Suit { get; }

		/// <summary>
		/// The card's kind.
		/// </summary>
		public CardKind Kind { get; }

		public Card(CardKind kind, CardSuit suit) {
			this.Suit = suit;
			this.Kind = kind;
		}

		// Every class can define a method ToString for creating a string representation of an object.
		// The override keyword is mandatory and indicates we are changing the behavior of a method defined in a base
		// class.
		public override string ToString() {
			int kindValue = (int)Kind;
			string r;
			if (kindValue >= 2 && kindValue <= 10) {
				r = kindValue.ToString();
			}
			else {
				r = Kind.ToString(); // ToString on an enum returns the name given in code, e.g., "Jack", "Two", etc.
			}
			return $"{r} of {Suit}";
		}

		// Compare this card to another, to decide which wins the War game. 
		public int CompareTo(Card other) {
			// Compare the cards based on the integer value of their Kind.
			return this.Kind.CompareTo(other.Kind);
		}
	}
}
