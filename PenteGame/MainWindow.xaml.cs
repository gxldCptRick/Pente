﻿using PenteGame.Views;
using PenteGame.Views.Intefaces;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PenteGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ProccessPageRequest(PageRequest.Main);
        }

        private T GeneratePage<T>() where T : Page, IPageChanger, new()
        {
            var page = new T
            {
                DataContext = this.DataContext
            };
            page.PageChangeRequested += ProccessPageRequest;
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
                default:
                    throw new ArgumentException("You picked an unsuportted PageRequest.", nameof(pageToGoTo));
            }
        }
    }
}
