using System;
using System.Collections.Generic;
using System.Text;

namespace Cecs475.Vending.Model {
	/// <summary>
	/// Represents the ability of a vending machine to accept payments of some type.
	/// </summary>
	public interface IPaymentDevice {
		/// <summary>
		/// A display name for this payment device type.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Returns true only if this payment device can accept the given type of payment.
		/// </summary>
		/// <param name="payment">a payment of some type</param>
		public bool AcceptsPayment(IPayment payment);

		/// <summary>
		/// Returns true only if the given payment is enough to complete a purchase of the 
		/// given total amount.
		/// </summary>
		/// <param name="totalAmount">the cost of the item(s) being purchased</param>
		/// <param name="payment">a payment of some type</param>
		public bool CanPurchase(decimal totalAmount, IPayment payment);

		/// <summary>
		/// Computes the amount of "change" to return from the machine, for a purchase 
		/// of the given total amount using a payment of the given type.
		/// </summary>
		/// <param name="totalAmount">the cost of the item(s) being purchased</param>
		/// <param name="payment">a payment of some type</param>
		public decimal ComputeChange(decimal totalAmount, IPayment payment);
	}
}
