using System;
using System.Collections.Generic;
using System.Text;

namespace Cecs475.Vending.Model {
	/// <summary>
	/// Represents a form of payment presented to a vending machine to complete a purchase,
	/// which may or may not actually accept that type of payment. Derived types should add 
	/// properties needed to represent a payment attempt of that type.
	/// </summary>
	public interface IPayment {
		/// <summary>
		/// A display name for this type of payment.
		/// </summary>
		string Name { get; }
	}
}