using PenteGame.ViewModels;
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
            PageChangeRequested.Invoke(PageRequest.Main);
        }

        private void RestartButtonClicked(object sender, RoutedEventArgs e)
        {
            if(this.DataContext is MainPageData data)
            {
                data.Game.ResetGame();
            }
            PageChangeRequested.Invoke(PageRequest.Game);
        }
    }
}
