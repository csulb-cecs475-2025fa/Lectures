using System;
using System.Collections.Generic;
using System.Text;

namespace Cecs475.Vending.Model {
	public class MobilePayment : IPayment {
		public string Name => "Mobile payment";
		public string UniqueCode { get; }

		public MobilePayment(string uniqueCode) {
			UniqueCode = uniqueCode;
		}
	}
}
