using CompositionEmployees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecs475.Employees {
	public class Employee {
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName => FirstName + " " + LastName;

		public DateTime StartDate {get; set;}
		

		public int Id { get; set; }

		public ICompensation Compensation { get; set; }

		public decimal GetMonthlyWage() {
			return Compensation.GetWages();
		}

		public Employee(int id, string first, string last, DateTime startDate, ICompensation compensation) {
			Id = id;
			FirstName = first;
			LastName = last;
			StartDate = startDate;
			Compensation = compensation;
		}
	}
}
