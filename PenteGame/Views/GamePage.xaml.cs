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
                rect.MouseDown += (s, e) => MessageBox.Show($"Bonjour {e.GetPosition(this.GameGrid)}");
                this.GameGrid.Children.Add(rect);
            }
        }

        public event Action<PageRequest> PageChangeRequested;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FillGrid();
        }

        private void PlayerControl_Loaded(object sender, RoutedEventArgs e)
        {
     
        }
    }
}
