using Cecs475.Employees;

namespace CompositionEmployees {
	internal class Program {
		static void Main(string[] args) {
			Employee e = new Employee(1, "Neal", "Terrell", DateTime.Today, new HourlyCompensation(20, 30));
			Console.WriteLine(e.GetMonthlyWage());


			// later on, e gets promoted.
			e.Compensation = new CommissionCompensation((decimal)0.1, 100_000, 10_000);
			
			Console.WriteLine(e.GetMonthlyWage());
		}
	}
}
