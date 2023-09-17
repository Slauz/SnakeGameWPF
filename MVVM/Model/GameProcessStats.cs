using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace SnakeGameWPF.MVVM.Model
{
    public class GameProcessStats
    {
        public int Rows { get; }
        public int Cols { get; }
        public bool GameOver { get; private set; } = false;
        public MapGridValue[,] Grid { get; }
        public Direction MoveDirection { get; set; }

        private int FoodRow;
        private int FoodCol;

        private readonly List<SnakeSegment> Snake = new List<SnakeSegment>();
        private readonly Random random = new Random();

        public GameProcessStats(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Grid = new MapGridValue[Rows, Cols];
            MoveDirection = Direction.Up;

            CreateEmpty();
            CreateSnake();
            CreateFood();
        }
        private void CreateSnake()
        {
            int cols = Rows / 2;

            for (int rows = 6; rows <= 9; rows++)
            {
                Grid[rows, cols] = MapGridValue.Snake;
                Snake.Add(new SnakeSegment(rows, cols, MoveDirection));
            }
        }

        private void CreateEmpty()
        {
            for (int rows = 0; rows < Rows; rows++)
            {
                for (int cols = 0; cols < Cols; cols++)
                {
                    Grid[rows, cols] = MapGridValue.Empty;
                }
            }
        }
        private void CreateFood()
        {
            int row = 0, col = 0;

            bool Accesible = false;
            while (!Accesible)
            {
                row = random.Next(0, Rows);
                col = random.Next(0, Cols);

                if (!Snake.Any(segment => segment.X == row && segment.Y == col))
                {
                    Accesible = true;
                }
            }
            Grid[row, col] = MapGridValue.Food;
            FoodRow = row;
            FoodCol = col;  
        }
        public void AddSnakeSegment()
        {
            int NewSegmentRow = Snake.Last().X;
            int NewSegmentCol = Snake.Last().Y;

            switch (Snake.Last().Direction)
            {
                case Direction.Up:
                    NewSegmentRow += 1;
                    break;
                case Direction.Right:
                    NewSegmentCol -= 1;
                    break;
                case Direction.Down:
                    NewSegmentRow -= 1;
                    break;
                case Direction.Left:
                    NewSegmentCol += 1;
                    break;
                default:
                    {
                        break;
                    }
            }

            Snake.Add(new SnakeSegment(NewSegmentRow, NewSegmentCol, MoveDirection));
        }

        public void TakeFood()
        {
            SnakeSegment Head = Snake[0];
            if (Grid[Head.X, Head.Y] == MapGridValue.Food)
            {
                AddSnakeSegment();
                CreateFood();
            }
        }

        private bool CheckLoseOutside()
        {
            return Snake.First().X > Rows || Snake.First().Y > Cols || Snake.First().X < 0 || Snake.First().Y < 0;
        }

        private bool CheckLoseSnake()
        {
            return Snake.Skip(1).Any(segment => Snake[0].X == segment.X && Snake[0].Y == segment.Y);
        }

        public void CheckLose()
        {
            if (CheckLoseOutside() | CheckLoseSnake()) { GameOver = true; }
        }

        public void MoveSnake()
        {
            try
            { 
                CreateEmpty();
                for (int i = Snake.Count - 1; i > 0; i--)
                {
                    Snake[i].Move(Snake[i - 1].Direction);
                    Grid[Snake[i - 1].X, Snake[i - 1].Y] = MapGridValue.Snake;
                }
                Snake[0].Move(MoveDirection);
                Grid[Snake[0].X, Snake[0].Y] = MapGridValue.Snake;
                Grid[FoodRow, FoodCol] = MapGridValue.Food;
            }
            catch
            {
                GameOver = true;
            }
             
        }

       
    }
}
