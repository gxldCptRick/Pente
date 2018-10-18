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

		private void StartGame(object sender, RoutedEventArgs e)
		{
			this.PageChangeRequested?.Invoke(PageRequest.Game);

		}

		private void GridSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			Slider slid = (Slider)sender;
			GridSizeNum = (int)slid.Value;
		}
	}
}
