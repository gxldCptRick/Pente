using PenteGame.Converters;
using PenteGame.Lib.Enums;
using PenteGame.ViewModels;
using PenteGame.Views.Intefaces;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PenteGame.Views
{
    /// <summary>
    /// Interaction logic for GameOverPage.xaml
    /// </summary>
    public partial class GameOverPage : Page, IPageChanger
    {
        public GameOverPage()
        {
            InitializeComponent();
        }

        public event Action<PageRequest> PageChangeRequested;

        private void ExitButtonClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void NewGameButtonClicked(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainPageData data)
            {
                data.ResetGame();
            }

            PageChangeRequested.Invoke(PageRequest.Main);
        }

        private void RestartButtonClicked(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainPageData data)
            {
                data.ResetGame();
            }
            PageChangeRequested.Invoke(PageRequest.Game);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainPageData data)
            {
                var fightMEDean = new PieceColorToActualColorConverter();
                switch (data.Game.CurrentTurn)
                {
                    case Lib.Enums.PieceColor.Black:
                        winnerLabel.Content = $"{data.PlayerOne.Name} Wins!";
                        winnerLabel.Foreground = fightMEDean.Convert(PieceColor.Black, null, null, null) as Brush;
                        winnerLabel.Background = fightMEDean.Convert(PieceColor.White, null, null, null) as Brush;
                        break;
                    case Lib.Enums.PieceColor.White:
                        winnerLabel.Content = $"{data.PlayerTwo.Name} Wins!";
                        winnerLabel.Foreground = fightMEDean.Convert(PieceColor.White, null, null, null) as Brush;
                        winnerLabel.Background = fightMEDean.Convert(PieceColor.Black, null, null, null) as Brush;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
