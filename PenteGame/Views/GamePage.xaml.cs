﻿using PenteGame.Lib.Enums;
using PenteGame.ViewModels;
using PenteGame.Views.Intefaces;
using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
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
            FillGrid();
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Start();
            Dispatcher.Invoke(() =>
            {

                PlayerOneDisply.Highlight.Background = Brushes.Black;
                PlayerTwoDisplay.Highlight.Background = Brushes.Transparent;

            });
            }

        private PieceColor PlayerColor = PieceColor.Black;
        private void AddPiece(object sender, MouseButtonEventArgs e)
        {
            //On click, replace the fill with the right color 
            Rectangle rect = (Rectangle)sender;
            bool validMoveMade = true;
            if (DataContext is MainPageData data)
            {
                Point positionOnScreen = GetPosition(rect);
                validMoveMade = data.Game.TakeTurn(positionOnScreen, PlayerColor);
                Console.WriteLine($"column {positionOnScreen.x}  row {positionOnScreen.y} color");
                if (validMoveMade && (rect.Fill != gray && rect.Fill != purple))
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
            return coord;
        }

        private void ResetButton(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainPageData data)
            {
                data.Game.ResetGame();
                GameGrid.Children.Clear();
                FillGrid();
            }
        }

        int time = 20;
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            time -= 1;
            Dispatcher.Invoke(() =>
            {
                timerLabel.Content = time.ToString();
            });
            if (time == 0)
            {
                Dispatcher.Invoke(() =>
                {
                    if (PlayerColor == PieceColor.Black)
                    {
                        PlayerOneDisply.Highlight.Background = Brushes.Transparent;
                        PlayerTwoDisplay.Highlight.Background = Brushes.Black;
                        
                    }
                    else
                    {

                        PlayerOneDisply.Highlight.Background = Brushes.Black;
                        PlayerTwoDisplay.Highlight.Background = Brushes.Transparent;
                    }
                if (DataContext is MainPageData data)
                {
                    data.Game.SwitchTurn();
                        PlayerColor = data.Game.CurrentTurn;
                }
                });
                //toggle current turn
                time = 20;
            }
            //set timer label to timer.tostring
        }
    }
}
