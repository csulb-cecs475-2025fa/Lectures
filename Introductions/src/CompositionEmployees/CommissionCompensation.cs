using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositionEmployees {
	public class CommissionCompensation : ICompensation {
		public decimal CommissionRate { get; set; }
		public decimal CommissionSales { get; set; }
		public decimal BaseWage { get; set; }

		public CommissionCompensation(decimal commissionRate, decimal commissionSales, decimal baseWage) {
			CommissionRate = commissionRate;
			CommissionSales = commissionSales;
			BaseWage = baseWage;
		}

		public decimal GetWages() {
			return BaseWage + CommissionRate * CommissionSales;
		}
	}
}
