using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecs475.Employees {
	// An employee that is paid based on a base wage plus a commission on sales.
	public class CommissionEmployee : Employee {
		public decimal CommissionRate { get; set; }
		public decimal CommissionSales { get; set; }
		public decimal BaseWage { get; set; }

		public CommissionEmployee(int id, string first, string last, DateTime startDate,
			decimal baseWage, decimal commissionRate, decimal commissionSales)
			: base(id, first, last, startDate) {
			BaseWage = baseWage;
			CommissionRate = commissionRate;
			CommissionSales = commissionSales;
		}
		public override decimal GetMonthlyWage() {
			return BaseWage + CommissionRate * CommissionSales;
		}
	}
}
