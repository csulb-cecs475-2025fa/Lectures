namespace Cecs475.Employees {
	internal class Program {
		static void Main(string[] args) {
			Console.WriteLine("Hello, World!");
			Employee e = new CommissionEmployee(1, "Neal", "Terrell", DateTime.Today,
				1000, (decimal)0.1, 100_000);
				/*new HourlyEmployee(1, "Neal", "Terrell",
				DateTime.Parse("2013-01-22"),
				20,
				160);*/

			Console.WriteLine(e.FullName);
			Console.WriteLine(e.GetMonthlyWage());
			
		}
	}
}
