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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page, IPageChanger
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public event Action<PageRequest> PageChangeRequested;

        //Exits the application
        private void ExitButtonClicked(object sender, RoutedEventArgs e)
        {

            Application.Current.Shutdown();
        }

        //Goes to the options page 
        private void StartButtonClicked(object sender, RoutedEventArgs e)
        {
            this.PageChangeRequested?.Invoke(PageRequest.Options);
        }

        //Opens up the help window
        private void HelpButtonClicked(object sender, RoutedEventArgs e)
        {
            this.PageChangeRequested?.Invoke(PageRequest.Help);
        }
    }
}
