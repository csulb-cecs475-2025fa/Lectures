using System;
using System.Collections.Generic;
using System.Text;

namespace Cecs475.Vending.Model {
	public class CashPayment : IPayment {
		public string Name => "Cash";
		public decimal CashAmount { get; }

		public CashPayment(decimal amount) {
			CashAmount = amount;
		}
	}
}
