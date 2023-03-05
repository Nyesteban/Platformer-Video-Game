using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Platformer
{
    public partial class Form1 : Form
    {
        bool goleft = false;
        bool goright = false;
        bool jumping = false;

        int jumpSpeed = 10;
        int force = 8;
        int score = 0;
        int b = 5;
        int c = 10;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
                player.Image = Image.FromFile("playerleft.png");
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = true;
                player.Image = Image.FromFile("playerright.png");
            }
            if (e.KeyCode == Keys.Space && !jumping)
            {
                jumping = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }
            if (jumping)
            {
                jumping = false;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawingControl.SuspendDrawing(this);
            player.Top += jumpSpeed;
            if (jumping && force < 0)
            {
                jumping = false;
            }

            if (goleft)
            {
                player.Left -= 5;
            }

            if (goright)
            {
                player.Left += 5;
            }

            if (jumping)
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else
            {
                jumpSpeed = 12;
            }

            enemy1.Left += b;
            if (enemy1.Bounds.IntersectsWith(pictureBox2.Bounds))
                b = -b;
            if (enemy1.Bounds.IntersectsWith(pictureBox11.Bounds))
                b = -b;

            enemy2.Top += c;
            if (enemy2.Bounds.IntersectsWith(pictureBox5.Bounds))
                c = -c;
            if (enemy2.Bounds.IntersectsWith(pictureBox2.Bounds))
                c = -c;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "platform")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds) && !jumping)
                    {
                        force = 8;
                        player.Top = x.Top - player.Height;
                    }
                }
                if (x is PictureBox && x.Tag == "coin")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds) /*&& !jumping*/)
                    {
                        this.Controls.Remove(x);
                        score++;
                        label2.Text = score.ToString();
                    }
                }
                if (x is PictureBox && x.Tag == "danger")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        DrawingControl.ResumeDrawing(this);
                        timer1.Stop();
                        this.Hide();
                        Form4 f = new Form4();
                        f.Show();
                        return;
                    }
                }
            }
            if (player.Bounds.IntersectsWith(door.Bounds))
            {
                DrawingControl.ResumeDrawing(this);
                timer1.Stop();
                this.Hide();
                Form2 f = new Form2();
                f.Show();
                return;
            }
            if (player.Top > 650)
            {
                DrawingControl.ResumeDrawing(this);
                timer1.Stop();;
                this.Hide();
                Form4 f = new Form4();
                f.Show();
                return;
            }
            DrawingControl.ResumeDrawing(this);
        }
        }
}