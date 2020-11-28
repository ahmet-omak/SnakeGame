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
        #region PROPS
        public int SnakeSize { get; set; }
        #endregion

        #region GLOBAL VARIABLES
        int panelWidth;
        SnakeParts[] snakeBody;
        Direction myDir;
        #endregion

        //Constructor 
        public Snake(int _panelWidth)
        {
            this.SnakeSize = 4;
            this.panelWidth = _panelWidth;
            snakeBody = new SnakeParts[4];
            snakeBody[0] = new SnakeParts(panelWidth,50);
            snakeBody[1] = new SnakeParts(panelWidth+15, 50);
            snakeBody[2] = new SnakeParts(panelWidth+25, 50);
            snakeBody[3] = new SnakeParts(panelWidth + 35, 50);
        }

        //Move snake
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

        //Grow snake
        public void Grow()
        {
            Array.Resize(ref snakeBody, snakeBody.Length+1);
            snakeBody[snakeBody.Length - 1] = new SnakeParts(snakeBody[snakeBody.Length - 2]._x - myDir._x, snakeBody[snakeBody.Length - 2]._y - myDir._y);
            SnakeSize++;
        }

        //Get Position
        public Point GetPosition(int number)
        {
            return new Point(snakeBody[number]._x, snakeBody[number]._y);
        }

        //Set Position
        public void SetPosition(int x,int y)
        {
            snakeBody[0]._x = x;
            snakeBody[0]._y = y;
        }
    }

    class SnakeParts
    {
        #region PROPS   
        public int _x { get; set; }
        public int _y { get; set; }
        #endregion

        #region GLOBAL VARIABLES
        public readonly int sizeX;
        public readonly int sizeY;
        #endregion

        //Constructor 
        public SnakeParts(int x,int y)
        {
            this._x = x;
            this._y = y;
            this.sizeX =10;
            this.sizeY = 10;
        }
    }

    class Direction
    {
        #region GLOBAL VARIABLES
        public readonly int _x;
        public readonly int _y;
        #endregion

        //Constructor 
        public Direction(int x,int y)
        {
            this._x = x;
            this._y = y;
        }
    }
}
