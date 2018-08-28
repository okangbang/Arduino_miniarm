/*
 Mouse control
 created 10 Jan. 2014
 modified 16 Jan 2014
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MouseControl
{
    public partial class Form2 : Form
    {
        int xCount = 0;
        int yCount = 0;
        int zCount = 0;
        int rCount = 0;
        int xCount_lst = 0;
        int yCount_lst = 0;
        int zCount_lst = 0;
        int rCount_lst = 0;
        int speed = 50;
        int center = 100;
        byte buttonData = 0x00; // H->L  4:Xbutton2(+) 3:Xbutton1(-) 2:Right 1:Middle 0:Left
        byte keyData = 0x00;  // H->L  5:- 4:+ 3:D 2:A 1:S 0:W
        byte buttonData_lst = 0x00;
        bool release_flag = true;
        
        int xCountMax = 90;
        int xCountMin = -90;
        int yCountMax = 210;
        int yCountMin = 0;
        int zCountMax = 150;
        int zCountMin = -180; 
        int rCountMax = 90;
        int rCountMin = -90;
        public Form1 parent;
        public Form2(Form1 parent)
        {
            InitializeComponent();
            this.parent = parent; 
            this.listView1.MouseWheel += new System.Windows.Forms.MouseEventHandler(listView1_MouseWheel);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(Form2_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(Form2_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(Form2_MouseUp);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(Form2_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(Form2_KeyUp);
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            int min = center - (51-speed);
            int max = center + (51-speed);
            int x = Cursor.Position.X - this.Left - 3;
            int y = Cursor.Position.Y - this.Top - 23;
            if (x < min || x > max || y < min || y > max)
            {
                Cursor.Position = new System.Drawing.Point(this.Left + center + 3, this.Top + center + 23);
            }
            
            if (x < min) xCount--;
            if (x > max) xCount++;
            if (y < min) yCount++;
            if (y > max) yCount--;

        }

        private void listView1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                buttonData &= 0x7f;
                buttonData |= 0x40;
                if((buttonData & 0x10) == 0x10)
                    rCount--;
                else
                    zCount--;
                
                //wheel reset
                buttonData &= 0x3f;
            }
            else if (e.Delta < 0)
            {
                buttonData &= 0xbf;
                buttonData |= 0x80;
                if ((buttonData & 0x10) == 0x10)
                    rCount++;
                else
                    zCount++;

                //wheel reset
                buttonData &= 0x3f;
            }
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                buttonData &= 0xfd;
                
                buttonData |= 0x01;
                l_label.Text = "Grip"; 
            }
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                buttonData &= 0xfe;
                buttonData |= 0x02;
                l_label.Text = "Release";
            }
            if (e.Button == MouseButtons.Middle)
            {
                buttonData |= 0x04;
            //    m_label.Text = "T";
            }
            if (e.Button == MouseButtons.Middle && e.Clicks == 2)
            {
                buttonData |= 0x08;
            //    m2_label.Text = "T";
            }
            if(e.Button == MouseButtons.Right) 
            {
                buttonData |= 0x10;
                r_label.Text = "*"; 
            }
            if (e.Button == MouseButtons.Right && e.Clicks == 2)
            {
                buttonData |= 0x20;
            //    r2_label.Text = "T";
            }
            if (e.Button == MouseButtons.XButton1)
            {
                speed = --speed < 1 ? 1 : speed;
                label11.Text = Convert.ToString(speed);
            }
            if (e.Button == MouseButtons.XButton2)
            {
                speed = ++speed > 50 ? 50 : speed;
                label11.Text = Convert.ToString(speed);
            }
        }

        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                buttonData &= 0xfc;
            }
            if (e.Button == MouseButtons.Middle)
            {
                buttonData &= 0xf3;
            //    m_label.Text = "F";
            //    m2_label.Text = "F";
            }
            if (e.Button == MouseButtons.Right)
            {
                buttonData &= 0xcf;
                r_label.Text = " ";
            //    r2_label.Text = "F";
            }
        }
       
        private void Form2_Load(object sender, EventArgs e)
        {
            Cursor.Clip = new Rectangle(this.Location.X + 33, this.Location.Y + 53, 130, 130);
        }

        private void exit_bt_Click(object sender, EventArgs e)
        {
            Form1.controlPanel = null;
            Cursor.Clip = Rectangle.Empty;
            this.parent.btClose_Click(null, null);
            this.Close();
        }
       
        public void set_data()
        {
            switch (Form1.rxAdd)
            {
                case 0x01: xCountMax = Form1.rxData; break;
                case 0x02: xCountMin = Form1.rxData; break;
                case 0x03: yCountMax = Form1.rxData; break;
                case 0x04: yCountMin = Form1.rxData; break;
                case 0x11: xCount = Form1.rxData; break;
                case 0x12: yCount = Form1.rxData; break;
                default: break;
            }
        }

        private void sendData()
        {
            this.parent.sendData("ff");
            this.parent.sendData("aa");
            this.parent.sendData(((xCount >> 8) & 0x00FF).ToString("x"));
            this.parent.sendData((xCount & 0x00FF).ToString("x"));
            this.parent.sendData(((yCount >> 8) & 0x00FF).ToString("x"));
            this.parent.sendData((yCount & 0x00FF).ToString("x"));
            this.parent.sendData(((zCount >> 8) & 0x00FF).ToString("x"));
            this.parent.sendData((zCount & 0x00FF).ToString("x"));
            this.parent.sendData(((rCount >> 8) & 0x00FF).ToString("x"));
            this.parent.sendData((rCount & 0x00FF).ToString("x"));
            this.parent.sendData(buttonData.ToString("x"));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (xCount_lst != xCount || yCount_lst != yCount || zCount_lst != zCount || rCount_lst != rCount || ( buttonData & 0xff) != 0x00)
                {
                    xCount = xCount < xCountMin ? xCountMin : xCount;
                    xCount = xCount > xCountMax ? xCountMax : xCount;
                    yCount = yCount < yCountMin ? yCountMin : yCount;
                    yCount = yCount > yCountMax ? yCountMax : yCount;
                    zCount = zCount < zCountMin ? zCountMin : zCount;
                    zCount = zCount > zCountMax ? zCountMax : zCount;
                    rCount = rCount < rCountMin ? rCountMin : rCount;
                    rCount = rCount > rCountMax ? rCountMax : rCount;

                    label4.Text = Convert.ToString(xCount);
                    label5.Text = Convert.ToString(yCount);
                    label1.Text = Convert.ToString(zCount);
                    label20.Text = Convert.ToString(rCount);
                    sendData();

                    //wheel reset
               //     buttonData &= 0x3f;
               //     label1.Text = "None";

                    xCount_lst = xCount;
                    yCount_lst = yCount;
                    zCount_lst = zCount;
                    rCount_lst = rCount;
                    buttonData_lst = buttonData;
                }
            }
            catch (Exception Err)
            {
                MessageBox.Show("Timing errors!" + Err.Message, "MouseControl");
                timer1.Enabled = false;
                Form2Closing();
            }
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Oemplus:
                                    speed = ++speed > 50 ? 50 : speed;
                                    label11.Text = Convert.ToString(speed);
                                    break;
                case Keys.OemMinus:
                                    speed = --speed < 1 ? 1 : speed;
                                    label11.Text = Convert.ToString(speed);
                                    break;
                case Keys.W:
                case Keys.Up:
                                    keyData |= 0x01;
                                    yCount += 1;
                                    break;
                case Keys.S:
                case Keys.Down:
                                    keyData |= 0x02;
                                    yCount -= 1;
                                    break;
                case Keys.D:
                case Keys.Right:
                                    keyData |= 0x08;
                                    xCount += 1;
                                    break;
                case Keys.A:
                case Keys.Left:
                                    keyData |= 0x04;
                                    xCount -= 1;
                                    break;
                case Keys.Q:
                case Keys.PageDown:
                                    buttonData &= 0x7f;
                                    buttonData |= 0x40;
                                    zCount -= 1;
                                    break;
                case Keys.E:
                case Keys.PageUp:
                                    buttonData &= 0xbf;
                                    buttonData |= 0x80;
                                    zCount += 1;
                                    break;
                case Keys.O:
                                    buttonData |= 0x01;
                                    l_label.Text = "Grip";
                                    break;
                case Keys.P:
                                    buttonData |= 0x02;
                                    l_label.Text = "Release";
                                    //l2_label.Text = "T";
                                    break;
                case Keys.Space:
                                    buttonData |= 0x04;
                                  //  m_label.Text = "T";
                                    break;
                case Keys.U:
                                    buttonData |= 0x10;
                            //        r_label.Text = "T"; 
                                    break;
                case Keys.I:
                                    buttonData |= 0x20;
                                 //   r2_label.Text = "T";
                                    break;

                case Keys.Z:
                                    buttonData &= 0x7f;
                                    buttonData |= 0x50;
                                    rCount -= 1;
                                    break;
                case Keys.X:
                                    buttonData &= 0xbf;
                                    buttonData |= 0x90;
                                    rCount += 1;
                                    break;

                case Keys.R:
                                    if (release_flag)
                                    {
                                        Cursor.Clip = Rectangle.Empty;
                                        release_flag = false;
                                    }
                                    else
                                    {
                                        Cursor.Clip = new Rectangle(this.Location.X + 33, this.Location.Y + 53, 130, 130);
                                        release_flag = true;
                                    }
                                    break;
            }
        }
        
        private void Form2_KeyUp(object sender, KeyEventArgs e)
        {
 
            if (e.KeyData == Keys.O)
            {
                buttonData &= 0xfe;
             //   l_label.Text = "F";
            }
            if (e.KeyData == Keys.P)
            {
                buttonData &= 0xfd;
            //    l2_label.Text = "F";
            }
            if (e.KeyData == Keys.Space)
            {
                buttonData &= 0xfb;
            //    m_label.Text = "F";
            }
            if (e.KeyData == Keys.U)
            {
                buttonData &= 0xef;
            //    r_label.Text = "F";
            }
            if (e.KeyData == Keys.I)
            {
                buttonData &= 0xdf;
            //    r2_label.Text = "F";
            }
            if (e.KeyData == Keys.Z || e.KeyData == Keys.X || e.KeyData == Keys.PageDown || e.KeyData == Keys.Up)
            {
                buttonData &= 0x3f;
             //   label1.Text = "None";
            }
            if (e.KeyData == Keys.Q || e.KeyData == Keys.E)
            {
                buttonData &= 0x2f;
            //    label1.Text = "None";
            //    r_label.Text = "F";
            }

            if (e.KeyData == Keys.W || e.KeyData == Keys.Up)
            {
                keyData &= 0xfe;
            }
            if (e.KeyData == Keys.S || e.KeyData == Keys.Down)
            {
                keyData &= 0xfd;
            }
            if (e.KeyData == Keys.D || e.KeyData == Keys.Right)
            {
                keyData &= 0xf7;
            }
            if (e.KeyData == Keys.A || e.KeyData == Keys.Left)
            {
                keyData &= 0xfb;
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)    
        {
            timer1.Enabled = false;
            Form1.controlPanel = null;
            Cursor.Clip = Rectangle.Empty;
            this.parent.btClose_Click(null, null);
        }

        public void Form2Closing()
        {
            timer1.Enabled = false;
            Form1.controlPanel = null;
            Cursor.Clip = Rectangle.Empty;
            this.parent.btClose_Click(null, null);
            this.Close();
        }
    }
}
