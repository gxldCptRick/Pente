using PenteGame.Lib.Enums;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PenteGame.Converters
{
    internal class PieceColorToActualColorConverter : IValueConverter
    {
        private static Brush purple;
        private static Brush grey;

        static PieceColorToActualColorConverter()
        {
            purple = Brushes.MediumPurple;
            grey = new BrushConverter().ConvertFromString("#C4C4BD") as Brush;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush brushColor = null;
            if (value is PieceColor color)
            {
                brushColor = color == PieceColor.Black ? grey : purple;
            }
            return brushColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
