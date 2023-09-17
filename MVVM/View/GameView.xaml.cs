using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Threading;
using SnakeGameWPF.MVVM.Model;
using System.Windows.Threading;

namespace SnakeGameWPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        private const int row = 15, col = 15;
        private DispatcherTimer timer = new DispatcherTimer();

        private GameProcessStats Game = new GameProcessStats(row, col);       
        public GameView()
        {
            KeyPressedManager.CanRegister = true;
            InitializeComponent();

            timer.Interval = TimeSpan.FromMilliseconds(150);
            timer.Tick += GameCycle;
            timer.Start();
        }

        #region Logical methods
        
        private void GameCycle(object sender, EventArgs e)
        {
            if(Game.GameOver == true)
            {
                timer.Stop();
                MessageBox.Show("LOSE"); 
                return;
            }
            CheckPressedKey();
            Game.CheckLose();
            Draw();
            Game.TakeFood();
            Game.MoveSnake();
            CheckPressedKey();
        }

        private void Draw()
        {
            bool change_color = false;
            GameDisplay.Children.Clear();
            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    switch (Game.Grid[r, c])
                    {
                        case MapGridValue.Snake:
                            {
                                Rectangle element = new Rectangle();
                                element.Fill = Brushes.Green;
                                GameDisplay.Children.Add(element);
                                Grid.SetColumn(element, c);
                                Grid.SetRow(element,r);
                                break;
                            }
                        case MapGridValue.Food:
                            {
                                Ellipse element = new Ellipse
                                {
                                    Width = 30,
                                    Height = 30,
                                };
                                element.Fill = Brushes.Red;
                                GameDisplay.Children.Add(element);
                                Grid.SetColumn(element, c);
                                Grid.SetRow(element, r);
                                break;
                            }
                        case MapGridValue.Empty:
                            {
                                Brush brush;

                                if (!change_color) brush = Brushes.White;
                                else brush = Brushes.Black;

                                Rectangle element = new Rectangle();
                                element.Fill = brush;
                                GameDisplay.Children.Add(element);
                                Grid.SetColumn(element, c);
                                Grid.SetRow(element, r);
                                break;
                            }
                    }
                    change_color = !change_color;
                }
            }
        }

        private void CheckPressedKey()
        {
            if(KeyPressedManager.PressedKey == Key.Up)
            {
                if (Game.MoveDirection == Direction.Down) return;
                Game.MoveDirection = Direction.Up;
            }
            else if (KeyPressedManager.PressedKey == Key.Down)
            {
                if (Game.MoveDirection == Direction.Up) return;
                Game.MoveDirection = Direction.Down;
            }
            else if (KeyPressedManager.PressedKey == Key.Right)
            {
                if (Game.MoveDirection == Direction.Left) return;
                Game.MoveDirection = Direction.Right;
            }
            else if (KeyPressedManager.PressedKey == Key.Left)
            {
                if (Game.MoveDirection == Direction.Right) return;
                Game.MoveDirection = Direction.Left;
            }
        }
        #endregion
    }
}
