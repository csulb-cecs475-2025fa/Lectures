using Cecs475.Vending;
using Cecs475.Vending.Model;
using System.Reflection.PortableExecutable;

internal class Program {
	private static void Main(string[] args) {
		Console.WriteLine("\nVendingMachineBad, with cash payment");
		VendingMachineBad bad = new VendingMachineBad("Payday", 2.00m, 10);
		IPayment cashPayment = new CashPayment(2.50m);
		IPayment creditPayment = new CreditPayment("12345678");
		Console.WriteLine($"AcceptsPayment: {bad.AcceptsPayment(cashPayment)}");
		Console.WriteLine($"CanPurchase: {bad.CanPurchase(1, cashPayment)}");
		Console.WriteLine($"ComputeChange: {bad.ComputeChange(1, cashPayment)}");
		
		Console.WriteLine("\nVendingMachineBad, with credit payment");
		Console.WriteLine($"AcceptsPayment: {bad.AcceptsPayment(creditPayment)}");
		Console.WriteLine($"CanPurchase: {bad.CanPurchase(1, creditPayment)}");

		return;


		Console.WriteLine("\nCreditCardVendingMachine, with cash payment");
		VendingMachineBad cardBad = new CreditCardVendingMachine("Payday", 2.00m, 10);
		Console.WriteLine($"AcceptsPayment: {cardBad.AcceptsPayment(cashPayment)}");
		Console.WriteLine($"CanPurchase: {cardBad.CanPurchase(1, cashPayment)}");
		
		Console.WriteLine("\nCreditCardVendingMachine, with credit payment");
		Console.WriteLine($"AcceptsPayment: {cardBad.AcceptsPayment(creditPayment)}");
		Console.WriteLine($"CanPurchase: {cardBad.CanPurchase(1, creditPayment)}");
		Console.WriteLine($"ComputeChange: {cardBad.ComputeChange(1, creditPayment)}");
		return;

		Console.WriteLine("\nGood VendingMachine, with cash inserter and cash payment");
		VendingMachine good = new VendingMachine("Skittles", 1.50m, 100);
		good.AddPaymentDevice(new CashInserter());
		Console.WriteLine($"AcceptsPayment: {good.AcceptsPayment(cashPayment)}");
		Console.WriteLine($"CanPurchase: {good.CanPurchase(1, cashPayment)}");
		Console.WriteLine($"ComputeChange: {good.ComputeChange(1, cashPayment)}");
		Console.WriteLine($"AcceptsPayment: {good.AcceptsPayment(cashPayment)}");
		Console.WriteLine($"CanPurchase: {good.CanPurchase(1, cashPayment)}");
		Console.WriteLine($"ComputeChange: {good.ComputeChange(1, cashPayment)}");


		return;

		// Construct a vending machine with the serial number 1, and $5.00 of total cash inside.
		VendingMachine machine = new VendingMachine("Twix", 2.00M, 10);
		machine.AddPaymentDevice(new CreditCardReader());

		do {
			Console.WriteLine($"We are selling {machine.MachineName} for ${machine.Cost:F2}.");
			Console.WriteLine("How many do you want to buy? ");
			int count = int.Parse(Console.ReadLine() ?? "0");

			Console.Write("Enter an amount of cash with a $, or a credit card number: ");
			string paymentString = Console.ReadLine() ?? "$0.00";

			IPayment payment;
			if (paymentString.StartsWith("$")) {
				payment = new CashPayment(decimal.Parse(paymentString.Trim('$')));
			}
			else {
				payment = new CreditPayment(paymentString);
			}

			if (machine.CanPurchase(count, payment)) {
				decimal changeReturned = machine.ComputeChange(count, payment);
				Console.WriteLine($"You get ${changeReturned:F2} in change.");
			}
			else {
				Console.WriteLine($"Your payment was rejected.");
			}
			
		} while (true);
	}
}