using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameWPF.MVVM.Model
{
    public class SnakeSegment
    {
        // 0 - up, 1 - right, 2 - down, 3 - left
        public int X;
        public int Y;
        public Direction Direction;

        public SnakeSegment(int X, int Y, Direction Direction = 0)
        {
            this.X = X;
            this.Y = Y;
            this.Direction = Direction;
        }

        public void Move(Direction Direction)
        {
            switch (Direction)
            {
                case Direction.Up:
                    X -= 1;
                    break;
                case Direction.Right:
                    Y += 1;
                    break;
                case Direction.Down:
                    X += 1;
                    break;
                case Direction.Left:
                    Y -= 1;
                    break;
                default:
                    {
                        break;
                    }
            }
            this.Direction = Direction;
        }
    }
}
