﻿using ClassLibrary;
using System.Windows;
using System.Windows.Controls;

namespace GameFactoryWPF
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : UserControl
    {
        private MainWindow p_MainWindow;

        public GameWindow(Match p_Game, MainWindow p_MainWindow)
        {
            InitializeComponent();
            this.p_MainWindow = p_MainWindow;
            CreatePlayboard(p_Game.p_Rows, p_Game.p_Columns, p_Game.p_WinningLength);
            
        }


        private void CreatePlayboard(int p_Rows, int p_Columns, int p_WinningLength)
        {
            var playboard = new Grid();

            for (int row = 0; row < p_Rows; row++)
            {
                playboard.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }

            for (int col = 0; col < p_Columns; col++)
            {
                playboard.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            for (int row = 0; row < p_Rows; row++)
            {
                for (int col = 0; col < p_Columns; col++)
                {
                    var cellButton = new CellControl();

                    Grid.SetRow(cellButton, row);
                    Grid.SetColumn(cellButton, col);

                    playboard.Children.Add(cellButton);
                }
            }



            p_MainWindow.MainContent.Content = playboard;
        }


    }
}
