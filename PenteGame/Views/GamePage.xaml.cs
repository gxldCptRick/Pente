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

        //Loads all the images for the grid
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
                    if (i == 0)
                    {
                        rect.Fill = topSide;
                    }
                    if (i == GridSize - 1)
                    {
                        rect.Fill = bottomSide;
                    }
                    if (j == 0)
                    {
                        rect.Fill = leftSide;
                    }
                    if (j == GridSize - 1)
                    {
                        rect.Fill = rightSide;
                    }

                    //replaces corner with corner image
                    //if it is the first or last index for both for loops, it sets the fill as the corner image
                    if (i == 0 && j == 0)
                    {
                        rect.Fill = upperLeft;
                    }
                    if (i == 0 && j == GridSize - 1)
                    {
                        rect.Fill = upperRight;
                    }
                    if (i == GridSize - 1 && j == 0)
                    {
                        rect.Fill = lowerLeft;
                    }
                    if (i == GridSize - 1 && j == GridSize - 1)
                    {
                        rect.Fill = lowerRight;
                    }

                    rect.MouseDown += (s, e) => AddPiece(s, e);
                    GameGrid.Children.Add(rect);
                }
            }
        }

        public event Action<PageRequest> PageChangeRequested;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GameGrid.Children.Clear();
            FillGrid();
            if (DataContext is MainPageData data)
            {
                if (timer is null)
                {
                    timer = new Timer();
                    timer.Interval = 1000;
                    timer.Elapsed += OnTimedEvent;
                }
                UpdateHighlight();
                DrawPieces(data.Game.Pieces, data);
                if (!hasBeenSet)
                {
                    hasBeenSet = true;
                    data.Game.Capture += (color) =>
                    {
                        DrawPieces(data.Game.Pieces, data);
                        data.Game.Removal.Clear();
                        if (color == PieceColor.Black) data.PlayerOne.NumberOfCaptures = data.Game.GetTotalCaptures(color);
                        else data.PlayerTwo.NumberOfCaptures = data.Game.GetTotalCaptures(color);
                    };

                    data.Game.Tria += (color) => MessageBox.Show($"{TranslateColor(color)} has formed a tria");
                    data.Game.Tessara += (color) => MessageBox.Show($"{TranslateColor(color)} has formed a tessera");
                    data.Game.Win += (color) => PageChangeRequested?.Invoke(PageRequest.GameOver);
                    data.PropertyChanged += (s, eve) =>
                    {
                        if (eve.PropertyName == nameof(data.CurrentTurn))
                        {
                            UpdateHighlight();
                            ResetTimer();
                            DrawPieces(data.Game.Pieces, data);
                        }
                    };
                }
            }
        }

        private void ResetTimer()
        {
            if (DataContext is MainPageData data)
            {
                timer.Stop();
                data.TimerCount = 20;
            }
        }
        private static IValueConverter colorToColor = new PieceColorToActualColorConverter();
        private void UpdateHighlight()
        {
            if (DataContext is MainPageData data)
            {
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
            bool validMoveMade = true;
            if (DataContext is MainPageData data)
            {
                Point positionOnScreen = GetPosition(rect);
                validMoveMade = data.Game.TakeTurn(positionOnScreen, data.CurrentTurn);
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
                data.Game.ResetGame();
                GameGrid.Children.Clear();
                ResetTimer();
                FillGrid();
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
                        data.Game.SwitchTurn();
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
