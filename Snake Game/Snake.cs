using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Game
{
    class Snake
    {
        int panelWidth;

        SnakeParts[] snakeBody;
        int snakeSize;
        Direction myDir;

        public int SnakeSize { get => snakeSize; set => snakeSize = value; }

        public Snake(int _panelWidth)
        {
            this.SnakeSize = 3;
            this.panelWidth = _panelWidth;
            snakeBody = new SnakeParts[3];
            snakeBody[0] = new SnakeParts(panelWidth, 50);
            snakeBody[1] = new SnakeParts(panelWidth+10, 50);
            snakeBody[2] = new SnakeParts(panelWidth+20, 50);
        }

        public void Move(Direction dir)
        {
            myDir = dir;
            if (dir!=null)
            {
                for (int i = snakeBody.Length-1; i >0 ; i--)
                {
                    snakeBody[i] = new SnakeParts(snakeBody[i - 1]._x, snakeBody[i - 1]._y);
                }
                snakeBody[0] = new SnakeParts(snakeBody[0]._x + dir._x, snakeBody[0]._y + dir._y);
            }
        }
        public void Grow()
        {
            Array.Resize(ref snakeBody, snakeBody.Length+1);
            snakeBody[snakeBody.Length - 1] = new SnakeParts(snakeBody[snakeBody.Length - 2]._x - myDir._x, snakeBody[snakeBody.Length - 2]._y - myDir._y);
            SnakeSize++;
        }
        public Point GetPosition(int number)
        {
            return new Point(snakeBody[number]._x, snakeBody[number]._y);
        }
        public void SetPosition(int x,int y)
        {
            snakeBody[0]._x = x;
            snakeBody[0]._y = y;
        }
    }
    class SnakeParts
    {
        public  int _x;
        public  int _y;
        public readonly int sizeX;
        public readonly int sizeY;
        public SnakeParts(int x,int y)
        {
            this._x = x;
            this._y = y;
            this.sizeX = 10;
            this.sizeY = 10;
        }
    }
    class Direction
    {
        public readonly int _x;
        public readonly int _y;
        public Direction(int x,int y)
        {
            this._x = x;
            this._y = y;
        }
    }
}
