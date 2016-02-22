using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Aventurile_lui_Bob
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Bitmap back;
        Bitmap bobright;
        Bitmap bobleft;
        Bitmap bobimage;
        Bitmap exit;
        Point boblocation = Point.Empty;
        Point exitlocation = Point.Empty;
        Point Boborigin = Point.Empty;
        float offSet = 0, vitX=0,vitY=0;
        int G=10;
        bool left = false, right = false, jump=false;        
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public void Game()
        {
            int imageNumber = panel1.Width / back.Width + 3;
            using (Bitmap frame = new Bitmap(panel1.Width, panel1.Height))
            {
                using (Graphics graph = Graphics.FromImage(frame))
                {
                    for (int i = 0; i < imageNumber; i++)
                    {
                        graph.DrawImage(back, offSet/2 % back.Width + i * back.Width - back.Width, 0);
                    }
                    graph.DrawImage(bobimage, boblocation);
                    graph.DrawImage(exit,exitlocation);

                }
                using (Graphics graph = panel1.CreateGraphics())
                {
                    graph.DrawImage(frame, Point.Empty);
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {


            back = Properties.Resources.back;
            bobleft = Properties.Resources.bobleft;
            bobright = Properties.Resources.bobright;
            bobimage = bobright;
            exit = Properties.Resources.Exit;
            timer1.Start();

         }
        public void corectare()
        {
            if (vitX > 0)
                vitX--;
            if (vitX < 0)
                vitX++;
            if(vitY>G)
                vitY-=G;
            if (vitY > Boborigin.Y)
                vitY--;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            boblocation.X = panel1.Width / 2 - bobimage.Width / 2;
            boblocation.Y = 510;
            exitlocation.X = panel1.Width - exit.Width - 10;
            exitlocation.Y = 5;
            Boborigin.X = 0;
            Boborigin.Y = 510;
            boblocation.Y = boblocation.Y + Convert.ToInt32(vitY);
            offSet = offSet + vitX;
            corectare();
            if(left)
            {
                bobimage = bobleft;
                vitX+=2;
            }
            if(right)
            {
                bobimage = bobright;
                vitX -= 2;
            }
            if(jump)
            {
                vitY = -100;
            }
            Game();
            
        }
        public int grounded()
        {
            if (boblocation.Y == Boborigin.Y)
                return 1;
            return 0;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (jump != true && grounded()==1)
            {
                if (e.KeyCode == Keys.Space)
                {
                    jump = true;
                }

            }
            if(e.KeyCode == Keys.Left)
            {
                right = false;
                left = true;
            }
            if(e.KeyCode == Keys.Right)
            {
                left = false;
                right = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
                if (e.KeyCode == Keys.Space)
                {
                    if(jump==true)
                        jump = false;

                }
            if(e.KeyCode == Keys.Left)
            {
                if (left == true)
                {
                    left = false;
                }
            }
            if(e.KeyCode == Keys.Right)
            {
                if (right == true)
                {
                    right = false;

                }
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            Point mousePt = new Point(e.X, e.Y);
            if (mousePt.X > panel1.Width - exit.Width - 10 && mousePt.X < panel1.Width - 10 && mousePt.Y > panel1.Top + 10 && mousePt.Y < panel1.Top + 10 + exit.Height)
                Application.Exit();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
