using Cecs475.Vending.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cecs475.Vending {
	public class VendingMachine {

		// This private hash set collects all payment devices that this vending machine supports.
		// The public can use AddPaymentDevice to add a new payment device to a vending machine.
		private ISet<IPaymentDevice> mPaymentDevices = new HashSet<IPaymentDevice>();


		public string MachineName { get; }
		public decimal Cost { get; }
		public int Inventory { get; private set; }

		public VendingMachine(string machineName, decimal cost, int inventory) {
			MachineName = machineName;
			Cost = cost;
			Inventory = inventory;
		}

		/// <summary>
		/// Adds a new payment device to the set of devices that this machine accepts.
		/// </summary>
		/// <param name="paymentDevice"></param>
		public void AddPaymentDevice(IPaymentDevice paymentDevice) {
			mPaymentDevices.Add(paymentDevice);
		}

		// Complete this class according to the homework specification.
		public bool AcceptsPayment(IPayment payment) {
			return mPaymentDevices.Any(pd => pd.AcceptsPayment(payment));
		}

		public bool CanPurchase(int itemCount, IPayment payment) {
			return itemCount <= Inventory &&
				mPaymentDevices.Any(pd => pd.CanPurchase(itemCount * Cost, payment));
		}

		public decimal ComputeChange(int itemCount, IPayment payment) {
			IPaymentDevice? device = mPaymentDevices.FirstOrDefault(
				pd => pd.AcceptsPayment(payment) && pd.CanPurchase(itemCount * Cost, payment)
			);

			if (device is null) {
				throw new InvalidOperationException("No device accepts that payment");
			}

			return device.ComputeChange(itemCount * Cost, payment);	
		}
	}
}