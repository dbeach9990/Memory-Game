using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Threading;
using System.Windows.Threading;

namespace MemoryGame.Models
{
    public class MemoryGameModel
    {
        #region EVENTS
        //EVENT FOR A MATCH
        #endregion

        #region FIELDS
        private Point _choiceOne;
        private Point _choiceTwo;
        private int[,] _gameBoard;
        private Random _rng = new Random();
        private List<int> _boardFiller;
        private int _size = 4;
        private Button _firstButton;
        private Button _secondButton;
        private int _guessCount = 0;
        private DispatcherTimer _flipTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
        #endregion

        #region CONSTRUCTORS
        public MemoryGameModel()
        {
            _flipTimer.Tick += _flipTimer_Tick;
            _gameBoard = new int[_size, _size];
            _boardFiller = new List<int>();
        }

        private void _flipTimer_Tick(object sender, EventArgs e)
        {
            ResetTiles();
        }
        #endregion

        #region METHODS
        public void SetChoice(Point choice)
        {
            if (_choiceOne == null)
            {
                _choiceOne = choice;
            }
            else {
                _choiceTwo = choice;
               // CheckForMatch();
            }
        }
        public void ResetTiles()
        {
            if(_firstButton != null && _secondButton != null)
            {
                _firstButton.Content = "";
                _secondButton.Content = "";
                _flipTimer.Stop();
            }
        }

        public void React(Button theButton, Point p)
        {
            if(_guessCount == 0)
            {
                ResetTiles();
                _firstButton = theButton;
                _guessCount++;
                _firstButton.Content = GetButtonValue(p);
            }
            else
            {
                _guessCount = 0;
                theButton.Content = GetButtonValue(p);
                _secondButton = theButton;
                if(Convert.ToInt32(_firstButton.Content) == GetButtonValue(p)){
                    Console.WriteLine("You got one!");
                    _firstButton.IsEnabled = false;
                    _secondButton.IsEnabled = false;
                }
                _flipTimer.Start();
                //Clear Button Content after waiting 5 seconds
                
            }
            Console.WriteLine("I am reacting ");
        }

        public int GetButtonValue(Point p)
        {
            return _gameBoard[Convert.ToInt32(p.X), Convert.ToInt32(p.Y)];
        }

        public void PrintValueOfButton(Point p, Button theButton)
        {
            Console.WriteLine("Button Value = " + _gameBoard[Convert.ToInt32(p.X), Convert.ToInt32(p.Y)]);
        }

        /*
        public void CheckForMatch()
        {
            if (_choiceOne != null && _choiceTwo != null){ }
                if (_gameBoard[_choiceOne.X, _choiceOne.Y] == _gameBoard[_choiceTwo.X, _choiceTwo.Y])
                {

                }
            }
        }
        */

        public void RandomizeGameBoard(int size)
        {
            for(int z = 0; z < (size * size) / 2; z++)
            {
                _boardFiller.Add(z + 1);
                _boardFiller.Add(z + 1);
            }
            Console.WriteLine("_boardFiller size after Initializing : " + _boardFiller.Count);

            for(int x = 0;  x < size; x++)
            {
                for(int y = 0; y < size; y++)
                {
                    var rando = _rng.Next(0, _boardFiller.Count);
                    _gameBoard[x, y] = _boardFiller.ElementAt(rando);
                    _boardFiller.RemoveAt(rando);

                    //DEBUGGING
                    Console.WriteLine("_gameBoard[" + x + "," + y + "] = " + _gameBoard[x,y]);
                    Console.WriteLine("_boardFiller size = " + _boardFiller.Count);

                }
            }
        }
        #endregion

    }
}