using PenteGame.Views.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PenteGame.Views
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page, IPageChanger
    {
        public GamePage()
        {
            InitializeComponent();
        }

        private void FillGrid()
        {
            var colorBrush = new BrushConverter().ConvertFromString("#87A885") as SolidColorBrush;
            for (int i = 0; i < 19*19; i++)
            {
                var rect = new Rectangle();
                rect.Stroke = colorBrush;
				//rect.MouseDown += (s, e) => MessageBox.Show($"Bonjour {e.GetPosition(this.GameGrid)}");
				rect.MouseDown += (s, e) => AddPiece(s,e);
				this.GameGrid.Children.Add(rect);
            }
        }

        public event Action<PageRequest> PageChangeRequested;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FillGrid();
        }
		bool pTurn = true;
		private void AddPiece(object sender, MouseButtonEventArgs e)
		{
			Uri peice = new Uri("/Resources/PurplePiece.png", UriKind.Relative);
			if (pTurn)
			{
				peice = new Uri("/Resources/GreyPiece.png", UriKind.Relative);
			}
			string BodyName = "Piece";
			Image BodyImage = new Image
			{
				Width = 20,
				Height = 20,
				Name = BodyName,
				Source = new BitmapImage(peice)
			};
			Console.WriteLine("Add");
			PieceGrid.Children.Add(BodyImage);
			Canvas.SetTop(BodyImage, e.GetPosition(this.GameGrid).Y);
			Canvas.SetLeft(BodyImage, e.GetPosition(this.GameGrid).X);
			pTurn = !pTurn;
		}
	}
}
