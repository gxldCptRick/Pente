using PenteGame.Views;
using PenteGame.Views.Intefaces;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PenteGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static IDictionary<Type, Page> currentlyLoadedPages;

        static MainWindow()
        {
            currentlyLoadedPages = new Dictionary<Type, Page>();
        }

        public MainWindow()
        {
            InitializeComponent();
            ProccessPageRequest(PageRequest.Main);
        }

        private Page GeneratePage<T>() where T : Page, new()
        {
            Page page = null;
            if (currentlyLoadedPages.ContainsKey(typeof(T)))
            {
                page = currentlyLoadedPages[typeof(T)];
            }
            else
            {
                page = new T
                {
                    DataContext = DataContext
                };

                if (page is IPageChanger pageChanger)
                {
                    pageChanger.PageChangeRequested += ProccessPageRequest;
                }
                currentlyLoadedPages[typeof(T)] = page;
            }

            return page;
        }

        private void ProccessPageRequest(PageRequest pageToGoTo)
        {
            switch (pageToGoTo)
            {
                case PageRequest.Main:
                    MainFrame.Navigate(GeneratePage<MainPage>());
                    break;
                case PageRequest.Help:
                    var window = new HelpfulWindow();
                    window.Show();
                    break;
                case PageRequest.Game:
                    MainFrame.Navigate(GeneratePage<GamePage>());
                    break;
                case PageRequest.GameOver:
                    MainFrame.Navigate(GeneratePage<GameOverPage>());
                    break;
                case PageRequest.Options:
                    MainFrame.Navigate(GeneratePage<OptionsPage>());
                    break;
                default:
                    throw new ArgumentException("You picked an unsuportted PageRequest.", nameof(pageToGoTo));
            }
        }
    }
}
