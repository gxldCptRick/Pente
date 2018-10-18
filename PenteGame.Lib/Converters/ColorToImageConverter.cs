using PenteGame.Lib.Enums;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PenteGame.Converters
{
    internal class ColorToImageConverter : IValueConverter
    {
        private static BitmapImage purplePiece;
        private static BitmapImage greyPiece;

        static ColorToImageConverter()
        {
            purplePiece = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "/Resources/purplePiece.png"));
            greyPiece = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "/Resources/greyPiece.png"));
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BitmapImage image = null;
            if (value is PieceColor color)
            {
                image = color == PieceColor.Black ? greyPiece: purplePiece;
            }
            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
