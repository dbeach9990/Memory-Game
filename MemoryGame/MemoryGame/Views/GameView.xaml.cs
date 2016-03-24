using MemoryGame.Models;
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

namespace MemoryGame.Views
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        #region FIELDS
        private MemoryGameModel _gameModel;
        #endregion

        public GameView()
        {
            InitializeComponent();
            _gameModel = new MemoryGameModel();
            _gameModel.RandomizeGameBoard(4);
            LoadGame();
        }

        public void LoadGame()
        {

            for (int x = 0; x < 4; x++)
            {
                var colDef = new ColumnDefinition();
                GameGrid.ColumnDefinitions.Add(colDef);

            }
            for (int y = 0; y < 4; y++)
            {
                var rowDef = new RowDefinition();
                GameGrid.RowDefinitions.Add(rowDef);

            }

            for (int a = 0; a < 4; a++)
            {
                for (int b = 0; b < 4; b++)
                {
                    var button = new Button();
                    Grid.SetColumn(button, a);
                    Grid.SetRow(button, b);
                    GameGrid.Children.Add(button);
                    button.Click += ButtonOnClick;
                }
            }
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            var theButton = sender as Button;
            var shotPosition = new Point(Grid.GetColumn(theButton), Grid.GetRow(theButton));
            //theButton.Content = _gameModel.GetButtonValue(shotPosition);

            _gameModel.PrintValueOfButton(shotPosition, theButton);
            Console.WriteLine("Button Clicked at (" + Grid.GetRow(theButton) + "," + Grid.GetColumn(theButton) + ")");
            _gameModel.React(theButton, shotPosition);
        }
    }
}
