using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositionEmployees {
	public class HourlyCompensation : ICompensation {
		public decimal HourlyRate { get; set; }
		public decimal HoursWorked { get; set; }

		public HourlyCompensation(decimal hourlyRate, decimal hoursWorked) {
			HourlyRate = hourlyRate;
			HoursWorked = hoursWorked;
		}

		public decimal GetWages() {
			return HoursWorked * HourlyRate;
		}
	}
}
