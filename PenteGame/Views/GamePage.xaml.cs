using PenteGame.Views.Intefaces;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

			ImageBrush gray = new ImageBrush(new BitmapImage(new Uri(@"./Resources/GreyPiece.png", UriKind.Relative)));
			ImageBrush purple = new ImageBrush(new BitmapImage(new Uri(@"./Resources/PurplePiece.png", UriKind.Relative)));
			ImageBrush intersection =  new ImageBrush(new BitmapImage(new Uri(@"./Resources/Intersection.png", UriKind.Relative)));
			ImageBrush topSide = new ImageBrush(new BitmapImage(new Uri(@"./Resources/Side.png", UriKind.Relative)));
			ImageBrush leftSide = new ImageBrush(new BitmapImage(new Uri(@"./Resources/LeftSide.png", UriKind.Relative)));
			ImageBrush rightSide = new ImageBrush(new BitmapImage(new Uri(@"./Resources/RightSide.png", UriKind.Relative)));
			ImageBrush bottomSide = new ImageBrush(new BitmapImage(new Uri(@"./Resources/BottomSide.png", UriKind.Relative)));
			ImageBrush upperLeft = new ImageBrush(new BitmapImage(new Uri(@"./Resources/Corner.png", UriKind.Relative)));
			ImageBrush upperRight = new ImageBrush(new BitmapImage(new Uri(@"./Resources/UpperRight.png", UriKind.Relative)));
			ImageBrush lowerLeft = new ImageBrush(new BitmapImage(new Uri(@"./Resources/LowerLeft.png", UriKind.Relative)));
			ImageBrush lowerRight = new ImageBrush(new BitmapImage(new Uri(@"./Resources/LowerRight.png", UriKind.Relative)));
		private void FillGrid()
        {
            var colorBrush = new BrushConverter().ConvertFromString("#87A885") as SolidColorBrush;
			//Brush gray = new Brush
			int GridSize = OptionsPage.GridSizeNum;

			for (int i = 0; i < GridSize; i++)
			{
				for (int j = 0; j < GridSize; j++)
				{
					Rectangle rect = new Rectangle
					{
						Fill = intersection
					};

					//Replaces fill with side image
					if (i==0)
					{
						rect.Fill = topSide;
					}
					if (i== GridSize-1)
					{
						rect.Fill = bottomSide;
					}
					if (j == 0)
					{
						rect.Fill = leftSide;
					}
					if (j == GridSize-1)
					{
						rect.Fill = rightSide;
					}

					//replaces corner with corner image
					if (i == 0 && j == 0)
					{
						rect.Fill = upperLeft;
					}
					if (i == 0 && j == GridSize-1)
					{
						rect.Fill = upperRight;
					}
					if (i == GridSize-1 && j == 0)
					{
						rect.Fill = lowerLeft;
					}
					if (i == GridSize-1 && j == GridSize-1)
					{
						rect.Fill = lowerRight;
					}



					rect.MouseDown += (s, e) => AddPiece(s, e);
					this.GameGrid.Children.Add(rect);
				}
			}
			//for (int i = 0; i < 19*19; i++)
   //         {

			//	var rect = new Rectangle
			//	{
			//		Fill = intersection,
			//		//Stroke = colorBrush
			//	};

			//	//if (i%2==0)
			//	//{
			//	//	rect.Fill = purple;
			//	//}
			//	//rect.MouseDown += (s, e) => MessageBox.Show($"Bonjour {e.GetPosition(this.GameGrid)}");
			//	//rect.MouseDown += (s, e) => AddPiece(s,e);
			//	//this.GameGrid.Children.Add(rect);
   //         }
        }

        public event Action<PageRequest> PageChangeRequested;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FillGrid();
        }
		bool pTurn = true;
		private void AddPiece(object sender, MouseButtonEventArgs e)
		{

			Rectangle rect = (Rectangle)sender;
			if (!rect.Fill.Equals(gray) || !rect.Fill.Equals(purple))
			{
				if (pTurn)
				{
					rect.Fill = gray;
				}
				else
				{
					rect.Fill = purple;
				}
			pTurn = !pTurn;

			}
		}
	}
}
