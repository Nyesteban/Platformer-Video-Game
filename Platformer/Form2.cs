using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platformer
{
    public partial class Form2 : Form
    {
        bool goleft = false;
        bool goright = false;
        bool jumping = false;

        int jumpSpeed = 10;
        int force = 8;
        int score = 0;
        int b = 7;
        int c = 7;
        int d = 3;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
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

        private void Form2_KeyUp(object sender, KeyEventArgs e)
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

            pictureBox2.Top += b;
            if (pictureBox2.Top > 405)
                b = -b;
            if (pictureBox2.Top < 60)
                b = -b;

            pictureBox3.Left += c;
            if (pictureBox3.Left > 409)
                c = -c;
            if (pictureBox3.Left < 271)
                c = -c;

            pictureBox6.Left -= d;
            pictureBox6.Top += d;
            if (pictureBox6.Left > 740)
                d = -d;
            if (pictureBox6.Left < 530)
                d = -d;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "platform")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds) && !jumping)
                    {
                        force = 8;
                        player.Top = x.Top - player.Height;
                        if ((x.Name == "pictureBox3" && goleft == false) || (x.Name == "pictureBox3" && goright == false))
                            player.Left += c;
                        if ((x.Name == "pictureBox6" && goleft == false) || (x.Name == "pictureBox6" && goright == false))
                            player.Left -= d;
                        if (x.Name == "pictureBox5")
                            pictureBox5.Image = Image.FromFile("gold.png");
                        if (x.Name == "pictureBox9")
                        {
                            pictureBox9.BackgroundImage = Image.FromFile("wormhole.png");
                            player.Left = 36;
                            player.Top = 290;
                        }
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
            }
            if (player.Bounds.IntersectsWith(door.Bounds))
            {
                DrawingControl.ResumeDrawing(this);
                timer1.Stop();
                this.Hide();
                Form3 f = new Form3();
                f.Show();
                return;
            }
            if (player.Top > 420)
            {
                DrawingControl.ResumeDrawing(this);
                timer1.Stop();
                this.Hide();
                Form5 f = new Form5();
                f.Show();
                return;
            }
            DrawingControl.ResumeDrawing(this);
        }
    }
}
