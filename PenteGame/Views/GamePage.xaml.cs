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
using PenteGame.Lib.Models;
using Point = PenteGame.Lib.Models.Point;
using PenteGame.ViewModels;
using PenteGame.Lib.Enums;

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
			//Loads all the images for the grid
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
            //var colorBrush = new BrushConverter().ConvertFromString("#87A885") as SolidColorBrush;
			//Sets the grid size to the seleced options
			int GridSize = OptionsPage.GridSizeNum;
			for (int i = 0; i < GridSize; i++)
			{
				for (int j = 0; j < GridSize; j++)
				{
					//Fills every rectangle with the default '+' image
					Rectangle rect = new Rectangle
					{
						Fill = intersection
					};

					//Replaces fill with side image
					//if it is the 0 index, or last for the first for loop, or if it is the first/last index for the second for floop, ir replaces the fill with a side image
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
					//if it is the first or last index for both for loops, it sets the fill as the corner image
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

        }

        public event Action<PageRequest> PageChangeRequested;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FillGrid();
        }
		PieceColor PlayerColor = PieceColor.Black;
		private void AddPiece(object sender, MouseButtonEventArgs e)
		{
			//On click, replace the fill with the right color 
			Rectangle rect = (Rectangle)sender;
			bool success = true;
			if (DataContext is MainPageData data)
			{
				success = data.Game.TakeTurn(GetPosition(rect), PlayerColor);
			if (success)
			{

			
				if (PlayerColor == PieceColor.Black)
				{
					rect.Fill = gray;
				}
				else
				{
					rect.Fill = purple;
				}


				
				PlayerColor = data.Game.CurrentTurn;
				}
			}
			


		}

		private Point GetPosition(Rectangle rect)
		{
			int rows = OptionsPage.GridSizeNum;
			int columns = OptionsPage.GridSizeNum;

			int index = GameGrid.Children.IndexOf(rect);

			int row = index / columns;  // divide
			int column = index % columns;  // modulus
			Point coord = new Point(column, row);
			Console.WriteLine("column " + column + " row " + row);
			return coord;
		}
	}
}
