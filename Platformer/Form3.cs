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
    public partial class Form3 : Form
    {
        bool goleft = false;
        bool goright = false;
        bool jumping = false;

        int jumpSpeed = 10;
        int force = 8;
        int a = 3;
        int b = 10;
        int c = 10;
        int d = 5;
        int spawn = 200;
        int hp = 10;
        int ok = 0;
        int check = 0;
        int value1, value2,value3;

        Random random1 = new System.Random();
        Random random2 = new System.Random();
        Random random3 = new System.Random();
        public Form3()
        {
            InitializeComponent();
        }
        PictureBox choose(int x)
        {
            if (x == 1)
                return pictureBox1;
            if (x == 2)
                return pictureBox2;
            if (x == 3)
                return pictureBox3;
            if (x == 4)
                return pictureBox4;
            if (x == 5)
                return pictureBox5;
            if (x == 6)
                return pictureBox6;
            if (x == 7)
                return pictureBox7;
            if (x == 9)
                return pictureBox9;
            if (x == 10)
                return pictureBox10;
            if (x == 11)
                return pictureBox11;
            if (x == 12)
                return pictureBox12;
            if (x == 13)
                return pictureBox13;
            if (x == 14)
                return pictureBox14;
            if (x == 15)
                return pictureBox15;
            return pictureBox9;
        }
        private void Form3_KeyDown(object sender, KeyEventArgs e)
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

        private void Form3_KeyUp(object sender, KeyEventArgs e)
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
            if (spawn!=0)
                spawn--;
            else
            {
                boss.Top -= a;
                if (boss.Top < 5)
                {
                    a = 0;
                    label1.Visible = true;
                    label2.Visible = true;
                    this.Text = "Boss";
                }
            }
            if (a == 0)
            {
                if (pictureBox9.Left == 760 && pictureBox10.Left == 760 && pictureBox11.Left == 760 && pictureBox12.Left == 760 && pictureBox13.Left == 760 && pictureBox14.Left == 760 && pictureBox15.Left == 760)
                    ok = 1;
                else
                    ok = 0;
                if (ok == 1)
                {
                    value1 = random1.Next(9, 15);
                    value2 = random2.Next(9, 15);
                    if (value2 == value1 && value2 != 15)
                        value2 = value1 + 1;
                    else if (value2 == value1 && value2 == 15)
                        value2 = 14;
                }
                choose(value1).Left -= b;
                choose(value2).Left -= b;
                if (pictureBox16.Left == 760)
                {
                    value3 = random3.Next(1, 7);
                    pictureBox16.Left = choose(value3).Left+17;
                    pictureBox16.Top = choose(value3).Top-80;
                    pictureBox16.Visible = true;
                }
            }
            if (player.Bounds.IntersectsWith(pictureBox16.Bounds))
                check = 1;
            if(check==1)
            {
                pictureBox16.Left += c;
                if (pictureBox16.Bounds.IntersectsWith(boss.Bounds))
                {
                    hp--;
                    label2.Text = hp.ToString();
                    pictureBox16.Left = 760;
                    pictureBox16.Visible = false;
                    check = 0;
                }
            }
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
                if (x is PictureBox && x.Tag == "danger")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        DrawingControl.ResumeDrawing(this);
                        timer1.Stop();
                        this.Hide();
                        Form6 f = new Form6();
                        f.Show();
                        return;
                    }
                    if(x.Bounds.IntersectsWith(pictureBox8.Bounds))
                        x.Visible = true;
                    if (x.Left < -70)
                    {
                        x.Visible = false;
                        x.Left = 760; 
                    }
                }
            }
            if (hp == 0)
            {
                b = 0;
                c = 0;
                pictureBox9.Left = 800;
                pictureBox10.Left = 800;
                pictureBox11.Left = 800;
                pictureBox12.Left = 800;
                pictureBox13.Left = 800;
                pictureBox14.Left = 800;
                pictureBox15.Left = 800;
                pictureBox16.Left = 800;
                pictureBox9.Visible = false;
                pictureBox10.Visible = false;
                pictureBox11.Visible = false;
                pictureBox12.Visible = false;
                pictureBox13.Visible = false;
                pictureBox14.Visible = false;
                pictureBox15.Visible = false;
                pictureBox16.Visible = false;
                boss.Top += d;
                if (boss.Top >= 700)
                {
                    timer1.Stop();
                    MessageBox.Show("SFÂRȘIT");
                    Application.Exit();
                }
            }
            if (player.Top > 650)
            {
                DrawingControl.ResumeDrawing(this);
                timer1.Stop();
                this.Hide();
                Form6 f = new Form6();
                f.Show();
                return;
            }
            DrawingControl.ResumeDrawing(this);
        }
    }
}
