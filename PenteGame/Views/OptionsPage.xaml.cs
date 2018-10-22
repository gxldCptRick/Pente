using PenteGame.Lib.Enums;
using PenteGame.ViewModels;
using PenteGame.Views.Intefaces;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PenteGame.Views
{
    /// <summary>
    /// Interaction logic for OptionsPage.xaml
    /// </summary>
    /// 
    public partial class OptionsPage : Page, IPageChanger
    {
        public static int GridSizeNum;
        public int AlsoGridSize;
        public OptionsPage()
        {
            InitializeComponent();
        }

        public event Action<PageRequest> PageChangeRequested;

        //Goes to the game window
        private void StartGame(object sender, RoutedEventArgs e)
        {
            PageChangeRequested?.Invoke(PageRequest.Game);

        }

        //Gets the size of the game board
        private void GridSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slid = (Slider)sender;
            GridSizeNum = (int)slid.Value;
        }

        //Sets up the game mode
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainPageData data)
            {
                if (SinglePlayer.IsChecked.Value)
                {
                    data.Game.CurrentMode = GameMode.SinglePlayer;
                    data.PlayerTwo.Name = "Boopy";
                }
                else
                {
                    data.Game.CurrentMode = GameMode.MultiPlayer;
                }
                data.ResetGame();
            }
        }
    }
}
