using System;
using System.Collections.Generic;
using System.IO;
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

namespace PenteGame.Views.UserControls
{
    /// <summary>
    /// Interaction logic for PlayerControl.xaml
    /// </summary>
    public partial class PlayerControl : UserControl
    {
        public PlayerControl()
        {
            InitializeComponent();
        }

        private  static BitmapImage yes;

        static PlayerControl()
        {
            yes = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "/Resources/PurplePiece.png"));
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.otherThing.Source = yes;
        }
    }
}
