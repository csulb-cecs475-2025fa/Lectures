using System;
using System.Collections.Generic;
using System.Text;

namespace Cecs475.Vending.Model {
	/// <summary>
	/// Represents a payment made with a credit card.
	/// </summary>
	public class CreditPayment : IPayment {
		public string Name => "A credit card";

		public string CardNumber { get; }

		public CreditPayment(string cardNumber) {
			CardNumber = cardNumber;
		}
	}










	/// <summary>
	/// Allows a vending machine to accept payments via credit card.
	/// </summary>
	public class CreditCardReader : IPaymentDevice {
		public string Name => "Credit Card Reader";

		/// <summary>
		/// A CreditCardReader only accepts CreditPayments.
		/// </summary>
		public bool AcceptsPayment(IPayment payment) {
			// The "is" operator returns true only if the variable is actually of the given derived type.
			return payment is CreditPayment;
		}

		/// <summary>
		/// A CreditCardReader always accepts a CreditPayment. (In reality, we might submit an authorization
		/// request; ask the credit company to confirm that we can charge the given amount to the card.)
		/// </summary>
		public bool CanPurchase(decimal itemCost, IPayment payment) {
			return payment is CreditPayment;
		}

		/// <summary>
		/// A CreditCardReader always charges the exact amount, and there is never any change
		/// to return to the purchaser.
		/// </summary>
		public decimal ComputeChange(decimal itemCost, IPayment payment) {
			return 0.0M; // "M" designates this literal as a decimal value, not a double.
		}
	}
}
