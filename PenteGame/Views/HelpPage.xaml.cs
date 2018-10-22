using PenteGame.Views.Intefaces;
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

namespace PenteGame.Views
{
    /// <summary>
    /// Interaction logic for Help.xaml
    /// </summary>
    public partial class HelpPage : Page, IPageChanger
    {
        private static readonly Uri linkToAwesomeHTMLPageThatWeTotallyDidntStealThisNameWillBeChangedBeforeProbablyUmmYeah;

        //Totally not stealing an html for the help
        static HelpPage()
        {
            linkToAwesomeHTMLPageThatWeTotallyDidntStealThisNameWillBeChangedBeforeProbablyUmmYeah = new Uri(Directory.GetCurrentDirectory() + "/Resources/HowToPlay.html");
        }

        public HelpPage()
        {
            InitializeComponent();
            this.thing.Navigate(linkToAwesomeHTMLPageThatWeTotallyDidntStealThisNameWillBeChangedBeforeProbablyUmmYeah);
        }

        public event Action<PageRequest> PageChangeRequested;
    }
}
