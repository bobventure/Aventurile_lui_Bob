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
        float offSet = 0, vit;
        int acc_secunde, acc_secunde_max = 30;
        bool left = false, right = false;
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
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            boblocation.X = panel1.Width / 2 - bobimage.Width / 2;
            boblocation.Y = 510;
            exitlocation.X = panel1.Width - exit.Width - 10;
            exitlocation.Y = 5;
            
            if (left)
            {
                offSet = offSet + 3 + acc_secunde;
                bobimage = bobleft;
            }
            if(right)
            {
                bobimage = bobright;
                offSet = offSet - 3 - acc_secunde;
            }
            Game();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if(e.KeyCode == Keys.Left)
            {
                acceleratie.Start();
                right = false;
                left = true;
            }
            if(e.KeyCode == Keys.Right)
            {
                acceleratie.Start();
                left = false;
                right = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                acceleratie.Stop();
                if (left == true)
                {
                    
                    deacceleratie.Start();
                }
            }
            if(e.KeyCode == Keys.Right)
            {
                acceleratie.Stop();
                if (right == true)
                {
                    
                    deacceleratie.Start();
                }
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void deacceleratie_Tick(object sender, EventArgs e)
        {
            if(acc_secunde > 0)
                acc_secunde = 3 + acc_secunde - acc_secunde_max / 3;
            if (acc_secunde <= 0)
            {
                deacceleratie.Stop();
                if (right == true)
                    right = false;
                if (left == true)
                    left = false;
                acc_secunde = 0;
            }
        }

        private void acceleratie_Tick(object sender, EventArgs e)
        { 
            if(acc_secunde< acc_secunde_max)
                acc_secunde++;
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
    }
}
