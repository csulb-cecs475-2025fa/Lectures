using System;
using System.Collections.Generic;
using System.Text;

namespace Cecs475.Vending.Model {
	public class CashInserter : IPaymentDevice {
		public string Name => "Cash Inserter";

		public bool AcceptsPayment(IPayment payment) {
			return payment is CashPayment;
		}

		public bool CanPurchase(decimal totalAmount, IPayment payment) {
			return payment is CashPayment cp && cp.CashAmount >= totalAmount;
		}

		public decimal ComputeChange(decimal totalAmount, IPayment payment) {
			if (payment is not CashPayment cp) {
				throw new InvalidOperationException("Can only accept cash payments");
			}
			return cp.CashAmount - totalAmount;
		}
	}
}
