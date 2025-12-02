using Cecs475.Othello.Model;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using System.Collections.Generic;
using System;

namespace Cecs475.Othello.AvaloniaApp {
	class OthelloSquareBackgroundConverter : IMultiValueConverter {
		private static readonly IBrush HIGHLIGHT_BRUSH = Brushes.Red;
		private static readonly IBrush CORNER_BRUSH = Brushes.Green;
		private static readonly IBrush SIDE_BRUSH = Brushes.LightGreen;
		private static readonly IBrush DANGER_BRUSH = Brushes.PaleVioletRed;
		private static readonly IBrush DEFAULT_BRUSH = Brushes.LightBlue;

		public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture) {
			// This converter will receive two properties: the Position of the square, and whether it
			// is being hovered.
			if (values.Count < 2 || values[0] is not BoardPosition pos) {
				return null;
			}
			if (values[1] is not bool isHighlighted) {
				return null;
			}

			// Hovered squares have a specific color.
			if (isHighlighted) {
				return HIGHLIGHT_BRUSH;
			}
			// Corner squares are very good, and drawn green.
			if ((pos.Row == 0 || pos.Row == 7) && (pos.Col == 0 || pos.Col == 7)) {
				return CORNER_BRUSH;
			}
			// Squares next to corners are very bad, and drawn pale red.
			if ((pos.Row == 0 || pos.Row == 1 || pos.Row == 6 || pos.Row == 7)
				&& (pos.Col == 0 || pos.Col == 1 || pos.Col == 6 || pos.Col == 7)) {
				return DANGER_BRUSH;
			}
			// Squares along the edge are good, and drawn light green.
			if (pos.Row == 0 || pos.Row == 7 || pos.Col == 0 || pos.Col == 7) {
				return SIDE_BRUSH;
			}
			// Inner squares are drawn light blue.
			return DEFAULT_BRUSH;
		}
	}
}