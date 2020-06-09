using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Fields
        int score = 0;

        Random rnd;

        PictureBox pb_feed;

        Snake mySnake;

        Direction myDirection;

        bool feedCheck = false;

        PictureBox[] snakeParts;
        #endregion

        //Loads screen
        private void Form1_Load(object sender, EventArgs e)
        {
            mySnake= new Snake(panel1.Width);
            CreateFeed();
            myDirection = new Direction(-10, 0);
            snakeParts = new PictureBox[0];
            for (int i = 0; i < 3; i++)
            {
                Array.Resize(ref snakeParts, snakeParts.Length + 1);
                if (i==0)
                {
                    snakeParts[i] = AddPictureBox(Color.DarkBlue);
                    continue;
                }
                snakeParts[i] = AddPictureBox(Color.Black);
            }
            Update.Start();
        }
        //Adds PictureBox
        PictureBox AddPictureBox(Color renk)
        {
            PictureBox pb = new PictureBox();
            pb.Size = new Size(10, 10);
            pb.BackColor = renk;
            pb.Location = mySnake.GetPosition(snakeParts.Length-1);
            panel1.Controls.Add(pb);
            return pb;
        }

        //Updates Snake
        void UpdateSnakeParts()
        {
            for (int i = 0; i < snakeParts.Length; i++)
            {
                snakeParts[i].Location = mySnake.GetPosition(i);
            }
        }

        //Movement Detection
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.W && myDirection._y!=10)
            {
                myDirection = new Direction(0, -10);
            }
            else if(e.KeyCode==Keys.S && myDirection._y!=-10)
            {
                myDirection = new Direction(0, 10);
            }
            else if(e.KeyCode==Keys.A && myDirection._x!=10)
            {
                myDirection = new Direction(-10, 0);
            }
            else if(e.KeyCode==Keys.D && myDirection._x!=-10)
            {
                myDirection = new Direction(10, 0);
            }
            else if(e.KeyCode==Keys.Escape)
            {
                Application.ExitThread();
            }
        }


        //MOVE !
        private void Update_Tick(object sender, EventArgs e)
        {
            isGameOver();
            mySnake.Move(myDirection);
            UpdateSnakeParts();
            if (!isFeedCheckTrue())
            {
                CreateFeed();
            }
            isNearWall();
        }

        //Creates Feed
         void CreateFeed()
        {
            feedCheck = true;
            rnd = new Random();
            PictureBox feed = new PictureBox();
            feed.BackColor = Color.Red;
            feed.Size = new Size(10, 10);
            feed.Location = new Point(rnd.Next(label2.Location.X+label2.Width+1,(panel1.Width / 10)*10) , rnd.Next(label1.Location.Y+label1.Height+1,(panel1.Height / 10)*10));
            pb_feed = feed;
            panel1.Controls.Add(feed);
        }

        //Checks whether snake ate the feed or not
         bool isFeedCheckTrue()
        {
            if (snakeParts[0].Bounds.IntersectsWith(pb_feed.Bounds))
            {
                score++;
                label2.Text = score.ToString();
                mySnake.Grow();
                Array.Resize(ref snakeParts, snakeParts.Length + 1);
                snakeParts[snakeParts.Length - 1] = AddPictureBox(Color.Black);
                feedCheck = false;
                panel1.Controls.Remove(pb_feed);
            }
            return feedCheck;
        }

        //Checks whether is game over or not
         void isGameOver()
        {
            for (int i = 1; i < mySnake.SnakeSize; i++)
            {
                if (mySnake.GetPosition(0)==mySnake.GetPosition(i))
                {
                    //stop timer
                    Update.Stop();
                    MessageBox.Show("Game Over");
                    Application.Restart();
                }
            }
        }

        //Wall Detections
        void isNearWall()
        {
            if (snakeParts[0].Bounds.X<0)
            {
                mySnake.SetPosition(panel1.Width+10,snakeParts[0].Bounds.Y);
            }
            else if(snakeParts[0].Bounds.X>panel1.Width)
            {
                mySnake.SetPosition(0, snakeParts[0].Bounds.Y);
            }
            else if(snakeParts[0].Bounds.Y<0)
            {
                mySnake.SetPosition(snakeParts[0].Bounds.X, panel1.Height);
            }
            else if(snakeParts[0].Bounds.Y>panel1.Height)
            {
                mySnake.SetPosition(snakeParts[0].Bounds.X, 0);
            }
        }
    }
}
