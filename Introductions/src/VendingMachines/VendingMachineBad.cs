using Cecs475.Vending.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cecs475.Vending {
	public class VendingMachineBad {
		public string MachineName { get; }
		public decimal Cost { get; }
		public int Inventory { get; private set; }

		// Code here for doing actual purchases, updating inventory count, etc.
		// Maybe even for storing more than one item in inventory?

		public VendingMachineBad(string machineName, decimal cost, int inventory) {
			MachineName = machineName;
			Cost = cost;
			Inventory = inventory;
		}

		public virtual bool AcceptsPayment(IPayment payment) {
			return payment is CashPayment;
		}

		public virtual bool CanPurchase(int count, IPayment payment) {
			return count <= Inventory &&
				payment is CashPayment cp && cp.CashAmount >= count * Cost;
		}

		public virtual decimal ComputeChange(int count, IPayment payment) {
			if (payment is not CashPayment cp) {
				throw new ArgumentException("Only accepts cash payments");
			}
			return cp.CashAmount - count * Cost;
		}
	}

	public class CreditCardVendingMachine : VendingMachineBad {
		public CreditCardVendingMachine(string machineName, decimal cost, int inventory) 
			: base(machineName, cost, inventory) {
		}

		public override bool AcceptsPayment(IPayment payment) {
			return payment is CreditPayment;
		}

		public override bool CanPurchase(int count, IPayment payment) {
			// Credit cards are always accepted as payment.
			return payment is CreditPayment;
		}

		public override decimal ComputeChange(int count, IPayment payment) {
			// Credit cards never give change.
			return 0.0M;
		}
	}

}