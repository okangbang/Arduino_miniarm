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
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace MouseControl
{
    public partial class Form1 : Form
    {
        bool IsReceving = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void SendAsHex(string str)
        {
            int Len = str.Length;
            byte[] send = new byte[Len / 2];
            int j = 0;
            for (int i = 0; i < Len; i = i + 2, j++)
                send[j] = Convert.ToByte(str.Substring(i, 2), 16);
            serialPort1.Write(send, 0, send.Length);
        }

        private string DelSpace(string str)
        {
            string TempStr = string.Empty;
            int Len = str.Length;
            for (int i = 0; i < Len; i++)
            {
                if (str[i] != ' ')
                    TempStr += str[i];
            }
            Len = TempStr.Length;
            if (Len % 2 != 0)
                TempStr = '0' + TempStr;
            return TempStr;
        }

        private void ReOpenPort()
        {
            try
            {
                btClose_Click(null, null);
                if (!serialPort1.IsOpen)
                    btOpen_Click(null, null);
            }
            catch (Exception Err)
            {
                controlPanel.Close();
                MessageBox.Show(Err.Message, "MouseControl");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (string com in System.IO.Ports.SerialPort.GetPortNames())
                    this.cmPort.Items.Add(com);
                cmPort.SelectedIndex = 0;
            }
            catch
            {
                controlPanel.Close();
                MessageBox.Show("Not found serial port!", "MouseControl");
            }
        }

        private void OpenPort()
        {
            //***避免串口死锁***
            serialPort1.WriteTimeout = 1000;  //写超时，如果底层串口驱动效率问题，能有效的避免死锁。
            serialPort1.ReadTimeout = 1000;   //读超时，同上。
            serialPort1.NewLine = "\r\n";     //回车换行。
            serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.sComm_DataReceived);   //注册事件。
            //***避免串口死锁***

            serialPort1.PortName = cmPort.Text;
            serialPort1.BaudRate = int.Parse(cmBaudRate.Text);
            serialPort1.DataBits = 8;
            serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), "None");
            serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "1");
            serialPort1.Open();
        }

        private void ClosePort()
        {
            //安全关闭当前串口。
            //***避免串口死锁***
            serialPort1.DataReceived -= this.sComm_DataReceived;   //注销串口中断接收事件，避免下次再执行进来，造成死锁。
            while (IsReceving)
                Application.DoEvents();     //处理串口接收事件及其它系统消息。
            serialPort1.Close();            //现在没有死锁，可以关闭串口。
            //***避免串口死锁***
        }
        public static Form2 controlPanel = null;
        private void btOpen_Click(object sender, EventArgs e)
        {
            try
            {
                OpenPort();
                if (serialPort1.IsOpen)
                {
                    btClose.Enabled = true;
                    btOpen.Enabled = false;
                    cmPort.Enabled = false;
                    cmBaudRate.Enabled = false;
                    
                    if (controlPanel == null)
                    {
                        controlPanel = new Form2(this);
                        controlPanel.Show();
                    }
                    else
                    {
                        controlPanel.Activate();
                    }
                }
            }
            catch (Exception er)
            {
                ClosePort();
                MessageBox.Show("Open port failed!" + er.Message, "MouseControl");
            }
        }

        private void cmPort_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                cmPort.Items.Clear();
                foreach (string com in System.IO.Ports.SerialPort.GetPortNames())
                    this.cmPort.Items.Add(com);
            }
            catch
            {
                controlPanel.Form2Closing();
                MessageBox.Show("Not found serial port!", "MouseControl");
            }
        }

        int stateMachine = 0;
        int t = 0;
        public static byte rxBuf;
        public static int rxData;
        public static byte rxAdd;
        byte[] dataBuf = new byte[3];
        private void sComm_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                rxBuf = (byte)serialPort1.ReadByte();
                if (stateMachine == 0)
                {
                    if (rxBuf == 0xff) stateMachine = 1;
                    else stateMachine = 0;
                }
                else if (stateMachine == 1)
                {
                    if (rxBuf == 0xaa) stateMachine = 2;
                    else stateMachine = 0;
                }
                else if (stateMachine == 2)
                {
                    dataBuf[t++] = rxBuf;
                    if (t > 2)  //receive 3 byte data
                    {
                        rxAdd = dataBuf[0];
                        rxData = (dataBuf[2] + (dataBuf[1] << 8)) - 32768; //
                        controlPanel.set_data();
                        stateMachine = 0;
                        t = 0;
                    }
                }
            }
            catch (Exception Err)
            {
                controlPanel.Form2Closing();
                MessageBox.Show(Err.Message, "MouseControl");
            }
        }

        public void sendData(string input)
        {
            try
            {
                if (!serialPort1.IsOpen)
                    btOpen_Click(null, null);

                string TempStr = string.Empty;
                TempStr = input;
                TempStr = DelSpace(TempStr);
                SendAsHex(TempStr);
            }
            catch (Exception err)
            {
                controlPanel.Form2Closing();
                MessageBox.Show(err.Message, "MouseControl");
            }
        }

        public void btClose_Click(object sender, EventArgs e)
        {
            ClosePort();
            if (!serialPort1.IsOpen)
            {
                btOpen.Enabled = true;
                btClose.Enabled = false;
                cmPort.Enabled = true;
                cmBaudRate.Enabled = true;
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            btClose_Click(null, null);
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.ufactory.cc/");
        }

        private void logo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.ufactory.cc/");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Mouse Control V1.0 Instructions：" + Environment.NewLine + 
                " " + Environment.NewLine +
                "1. " + "Stretch: W, Up & S, Down" + Environment.NewLine +
                "2. " + "Height: E, PageUp & Q, PageDown" + Environment.NewLine + 
                "3. " + "Rotation: D, Right & A, Left" + Environment.NewLine +
                "4. " + "Hand Rotation: Z & X, Click Right Button and scroll up or down " + Environment.NewLine +
                "5. " + "Catch: O or Click Left Button, Release: P or Double Clicks Left Button" + Environment.NewLine + 
               
                "     --------------------------------------------------------------- " + Environment.NewLine + 
                " " + Environment.NewLine +
                "         www.ufactory.cc       E-mail：info@ufactory.cc", "Mouse Control - Help");
        }
        protected override bool ProcessCmdKey(ref   Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                button1_Click(null, null);
            }
            if (keyData == Keys.O)
            {
                btOpen_Click(null, null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}