using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecs475.Employees {
	// A simple employee class that calculates wages based on a base value and years of service.
	public class Employee {
		public int Id { get; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime StartDate { get; }
		public string FullName => FirstName + " " + LastName;

		public virtual decimal GetMonthlyWage() {
			decimal baseValue = 50_000;
			decimal yearsOfService = (decimal)(5_000 * (DateTime.Today - StartDate).TotalDays / 365);
			return baseValue + yearsOfService;
		}

		public Employee(int id, string first, string last, DateTime startDate) {
			Id = id;
			FirstName = first;
			LastName = last;
			StartDate = startDate;
		}
	}
}
