using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecs475.Avalonia.Binding {
	class Employee {
		public int Age { 
			get; 
			set; 
		}
		public int Salary { get { return Age * 50; } }
	}

	class NotifyingEmployee : INotifyPropertyChanged {
		private int mAge;
		public event PropertyChangedEventHandler? PropertyChanged;

		private void OnPropertyChanged(string name) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		public int Age {
			get { return mAge; }
			set {
				mAge = value;
				OnPropertyChanged("Age");
				OnPropertyChanged("Salary");
			}
		}

		public int Salary { get { return Age * 50; } }

	}
}
