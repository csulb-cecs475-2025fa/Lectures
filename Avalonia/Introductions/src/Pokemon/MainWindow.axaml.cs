using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;

namespace Pokemon {
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			Model.Pokemon togekiss = (Model.Pokemon)this.FindResource("Togekiss")!;
			Model.Pokemon charmander = (Model.Pokemon)this.FindResource("Charmander")!;

			togekiss.AttackTarget(charmander, togekiss.Power);
			charmander.AttackTarget(togekiss, charmander.Power);
		}

		private void mImage2_MouseDown(object sender, PointerPressedEventArgs e) {
			for (int i = 0; i < 900000000; i++) {
				int x = i * i;
				Console.WriteLine(x);
			}

			var point = e.GetCurrentPoint((Control)sender);
			if (point.Properties.IsLeftButtonPressed && e.ClickCount == 2) {
				mImage2.Source = new Bitmap(AssetLoader.Open(new Uri("avares://Pokemon/Resources/charizard.png")));
				Model.Pokemon charmander = (Model.Pokemon)this.FindResource("Charmander")!;
				charmander.HP = 150;
				charmander.Attack = 150;
				charmander.Defense = 100;
				charmander.Power = 120;
			}
		}
	}
}