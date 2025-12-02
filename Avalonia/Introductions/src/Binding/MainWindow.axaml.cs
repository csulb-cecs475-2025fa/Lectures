using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.ObjectModel;
using Cecs475.Avalonia.Binding;
namespace Binding {
	public partial class MainWindow : Window {
		private ObservableCollection<string> mItems = new ObservableCollection<string>();

		public MainWindow() {
			InitializeComponent();
			Employee e = new Employee() {
				Age = 40
			};
			mSalaryLabel.DataContext = e;
			mAgeText.DataContext = e;
			// View this binding in action....




			// Create an Employee that notifies when its properties change.
			NotifyingEmployee ne = new NotifyingEmployee() {
				Age = 30
			};
			mSalary2Label.DataContext = ne;
			mAge2Text.DataContext = ne;


			// Some controls act on collections of data. If we give them an ObservableCollection, they will rebuild their
			// UI every time the collection changes.
			mList.ItemsSource = mItems;
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			// Set the first employee's age to 100, which does not update the UI.
			((Employee)mSalaryLabel.DataContext!).Age = 100;
		}

		private void Button2_Click(object sender, RoutedEventArgs e) {
			// Set the second employee's age to 100. Because we have two-way binding with a model that implements
			// INotifyPropertyChanged, the UI will automatically update.
			((NotifyingEmployee)mSalary2Label.DataContext!).Age = 100;
		}


		private void mAddBtn_Click(object sender, RoutedEventArgs e) {
			// Add an element to our member list, which will rebuild the ListView object.
			mItems.Add(mAddText.Text);
			mAddText.Focus();
			mAddText.SelectAll();
		}
	}
}