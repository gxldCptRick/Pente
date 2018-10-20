using PenteGame.Converters;
using PenteGame.Lib.Enums;
using PenteGame.Lib.Models;
using PenteGame.ViewModels;
using PenteGame.Views.Intefaces;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Point = PenteGame.Lib.Models.Point;

namespace PenteGame.Views
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page, IPageChanger
    {
        private static Timer timer;

        private bool hasBeenSet = false;
        public GamePage()
        {
            InitializeComponent();
        }


        #region Images For The grid
        private static ImageBrush gray = new ImageBrush(new BitmapImage(new Uri(@"./Resources/GreyPiece.png", UriKind.Relative)));
        private static ImageBrush purple = new ImageBrush(new BitmapImage(new Uri(@"./Resources/PurplePiece.png", UriKind.Relative)));
        private static ImageBrush intersection = new ImageBrush(new BitmapImage(new Uri(@"./Resources/Intersection.png", UriKind.Relative)));
        private static ImageBrush topSide = new ImageBrush(new BitmapImage(new Uri(@"./Resources/Side.png", UriKind.Relative)));
        private static ImageBrush leftSide = new ImageBrush(new BitmapImage(new Uri(@"./Resources/LeftSide.png", UriKind.Relative)));
        private static ImageBrush rightSide = new ImageBrush(new BitmapImage(new Uri(@"./Resources/RightSide.png", UriKind.Relative)));
        private static ImageBrush bottomSide = new ImageBrush(new BitmapImage(new Uri(@"./Resources/BottomSide.png", UriKind.Relative)));
        private static ImageBrush upperLeft = new ImageBrush(new BitmapImage(new Uri(@"./Resources/Corner.png", UriKind.Relative)));
        private static ImageBrush upperRight = new ImageBrush(new BitmapImage(new Uri(@"./Resources/UpperRight.png", UriKind.Relative)));
        private static ImageBrush lowerLeft = new ImageBrush(new BitmapImage(new Uri(@"./Resources/LowerLeft.png", UriKind.Relative)));
        private static ImageBrush lowerRight = new ImageBrush(new BitmapImage(new Uri(@"./Resources/LowerRight.png", UriKind.Relative)));
		private static ImageBrush middle = new ImageBrush(new BitmapImage(new Uri(@"./Resources/Middle.png", UriKind.Relative)));
		#endregion


		private void FillGrid()
        {
            //Sets the grid size to the seleced options
            int GridSize = OptionsPage.GridSizeNum;
			int middleNum = (int)Math.Ceiling((double)(OptionsPage.GridSizeNum/2));
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
                    if (i == 0)
                    {
						if (j == 0)
						{
							rect.Fill = upperLeft;
						}
						else if (j == GridSize - 1)
						{
							rect.Fill = upperRight;
						}
						else
						{
							rect.Fill = topSide;

						}
                    }else if (i == GridSize - 1)
                    {
						if (j == 0)
						{
							rect.Fill = lowerLeft;
						}
						else if (j == GridSize - 1)
						{
							rect.Fill = lowerRight;
						}
						else
						{
							rect.Fill = bottomSide;
						}
                    }else if (j == 0)
                    {
                        rect.Fill = leftSide;
                    }else if (j == GridSize - 1)
                    {
                        rect.Fill = rightSide;
                    }
					if (i == middleNum && j == middleNum)
					{
						rect.Fill = Brushes.MediumPurple;
						rect.Stroke = Brushes.MediumPurple;
					}

                    rect.MouseDown += (s, e) => AddPiece(s, e);
                    GameGrid.Children.Add(rect);
                }
            }
        }

        public event Action<PageRequest> PageChangeRequested;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainPageData data)
            {
                //creating a timer if it is not already created
                if (timer is null)
                {
                    timer = new Timer();
                    timer.Interval = 1000;
                    timer.Elapsed += OnTimedEvent;
                }
                //painting the background of the current turn player
                UpdateHighlight();
                //draws the board if there are any pieces still on it.
                DrawPieces(data.Game.Pieces, data);
                if (!hasBeenSet)
                {
                    //making sure to not add more than one event handler to the game.
                    hasBeenSet = true;
                    //setting up for captures
                    data.Game.Capture += (color) =>
                    {
                        DrawPieces(data.Game.Pieces, data);
                        data.UpdateCaptureValues();
                    };
                    //setting up for all the main events in the game.
                    data.Game.Tria += (color) => MessageBox.Show($"{TranslateColor(color)} has formed a tria");
                    data.Game.Tessara += (color) => MessageBox.Show($"{TranslateColor(color)} has formed a tessera");
                    data.Game.Win += (color) => PageChangeRequested?.Invoke(PageRequest.GameOver);
                    //hijacking the property changed event in order to update the highlight and stop the timer when we change the current turn.
                    data.PropertyChanged += (s, eve) =>
                    {
                        if (eve.PropertyName == nameof(data.CurrentTurn))
                        {
                            timer.Stop();
                            UpdateHighlight();
                            DrawPieces(data.Game.Pieces, data);
                        }
                    };
                }
            }
        }

        /// <summary>
        /// static converter used to translate the pieceColor enum into an actual color.
        /// </summary>
        private static IValueConverter colorToColor = new PieceColorToActualColorConverter();
        private void UpdateHighlight()
        {
            if (DataContext is MainPageData data)
            {
                //if it is the turn paint it yellow if it is not paint it the opposing teams color for the background.
                if (data.CurrentTurn == PieceColor.Black)
                {
                    PlayerOneDisplay.Background = Brushes.Yellow;
                    PlayerTwoDisplay.Background = colorToColor.Convert(PieceColor.Black, typeof(Brush), null, null) as Brush;
                }
                else
                {
                    PlayerOneDisplay.Background = colorToColor.Convert(PieceColor.White, typeof(Brush), null, null) as Brush;
                    PlayerTwoDisplay.Background = Brushes.Yellow;
                }
            }

        }
        /// <summary>
        /// used to transform the enum of piece color to the actual name used for display purposes.
        /// </summary>
        /// <param name="color">color that we want to translate</param>
        /// <returns>the name of the color in the current context</returns>
        private string TranslateColor(PieceColor color)
        {
            return color == PieceColor.Black ? "gray" : "purple";
        }

        private void DrawPieces(IEnumerable<GamePiece> pieces, MainPageData data)
        {
            GameGrid.Children.Clear();
            FillGrid();

            foreach (var piece in pieces)
            {
                int index = (piece.Point.y * data.GridSize) + piece.Point.x;
                (GameGrid.Children[index] as Rectangle).Fill = piece.Color == PieceColor.Black ? gray : purple;
            }
        }

        private void AddPiece(object sender, MouseButtonEventArgs e)
        {
            //On click, replace the fill with the right color 
            Rectangle rect = (Rectangle)sender;
            if (DataContext is MainPageData data)
            {
                Point positionOnScreen = GetPosition(rect);
                bool validMoveMade = data.Game.TakeTurn(positionOnScreen, data.CurrentTurn);
                DrawPieces(data.Game.Pieces, data);
                if (validMoveMade)
                {
                    timer.Start();
                    UpdateHighlight();
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
            return coord;
        }

        private void ResetButton(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainPageData data)
            {
                data.ResetGame();
                DrawPieces(data.Game.Pieces, data);
            }
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                if (DataContext is MainPageData data)
                {
                    data.TimerCount--;
                    if (data.TimerCount == 0)
                    {
                        data.TimerCount = 20;
                    }
                }
            });

        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }
    }
}
