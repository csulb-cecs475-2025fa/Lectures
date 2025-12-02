using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;

namespace Cecs475.Othello.AvaloniaApp;

public partial class OthelloView : UserControl
{
	public static SolidColorBrush RED_BRUSH = new SolidColorBrush(Colors.Red);
	public static SolidColorBrush GREEN_BRUSH = new SolidColorBrush(Colors.Green);

	public OthelloView()
    {
        InitializeComponent();
    }

	private void Panel_PointerEntered(object sender, PointerEventArgs e) {
		if (sender is not Control b) {
			throw new ArgumentException(nameof(sender));
		}
		var square = (OthelloSquare)b.DataContext!;
		var vm = (OthelloViewModel)Resources["vm"]!;
		if (vm.PossibleMoves.Contains(square.Position)) {
			square.IsHighlighted = true;
		}
	}

	private void Panel_PointerExited(object sender, PointerEventArgs e) {
		if (sender is not Control b) { throw new ArgumentException(nameof(sender)); }
		var square = (OthelloSquare)b.DataContext!;
		square.IsHighlighted = false;
	}

	public OthelloViewModel Model {
		get { return (OthelloViewModel)this.Resources["vm"]!; }
	}

	private void Panel_PointerReleased(object? sender, Avalonia.Input.PointerReleasedEventArgs e) {
		if (sender is not Control b) {
			throw new ArgumentException(nameof(sender));
		}

		var square = (OthelloSquare)b.DataContext!;
		var vm = (OthelloViewModel)Resources["vm"]!;
		if (vm.PossibleMoves.Contains(square.Position)) {
			vm.ApplyMove(square.Position);
			square.IsHighlighted = false;
		}
	}
}