﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_application
{
    public partial class Form1 : Form
    {
        private static Socket client_kzq, client_zkp;
        private static IPEndPoint point;
        byte[] kzq_sendBuffer = new byte[14];
        byte[] kzq_recBuffer = new byte[14];
        byte[] zkp_sendBuffer = new byte[14];
        byte[] zkp_recBuffer = new byte[14];
        private bool zkp_isConn, kzq_isConn;
        private Thread ConnectionState;

        public Form1()
        {
            InitializeComponent();
    }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            if (kzq_isConn == false)
            {
                try
                {
                    client_kzq = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    EndPoint serverPoint = new IPEndPoint(IPAddress.Parse("172.16.16.168"), 8089);
                    client_kzq.Connect(serverPoint);
                   
                }
                catch (Exception)
                {
                    client_kzq.Close();
                    throw;
                }

                //button11.Text = "控制器状态：连接";
                //button11.BackColor = Color.Red;
                kzq_isConn = true;
            }
            else
            {
                //button11.Text = "控制器状态：断开";
                //button11.BackColor = System.Drawing.Color.LightGreen;
                kzq_isConn = false;

                if (client_kzq!=null)
                {
                    client_kzq.Close();
                }

            }
            ConnectionState = new Thread(updateConnState);
            ConnectionState.IsBackground = true;
            ConnectionState.Start();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (zkp_isConn == false)
            {
                try
                {
                    client_zkp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    EndPoint serverPoint = new IPEndPoint(IPAddress.Parse("172.16.16.68"), 7080);
                    client_zkp.Connect(serverPoint);

                }
                catch (Exception)
                {
                    client_zkp.Close();
                    throw;
                }

                //button12.Text = "中控屏状态：连接";
                //button12.BackColor = Color.Red;
                zkp_isConn = true;
            }
            else
            {
                //button12.Text = "中控屏状态：断开";
                //button12.BackColor = System.Drawing.Color.LightGreen;
                zkp_isConn = false;

                if (client_zkp != null)
                {
                    client_zkp.Close();
                }

            }

                ConnectionState = new Thread(updateConnState);
                ConnectionState.IsBackground = true;
                ConnectionState.Start(); 

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (!kzq_isConn)
            {
                MessageBox.Show("请先连接控制器", "提示");
            }
            else
            {

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if(!kzq_isConn) {
                MessageBox.Show("请先连接控制器","提示");
            } else
            {
                kzq_sendBuffer[4] = 0x1;
                kzq_buffer_head();
                if (client_kzq!=null)
                {
                    client_kzq.Send(kzq_sendBuffer);
                }
            }
        }

        private void kzq_buffer_head()
        {
            kzq_sendBuffer[0] = 0xaa;
            kzq_sendBuffer[1] = 0x75;
            kzq_sendBuffer[2] = 0x7;
            kzq_sendBuffer[7] = 0;
            int i;
            for (i = 0; i < 7; i++)
            {
                kzq_sendBuffer[7] = (byte)(kzq_sendBuffer[7] ^ kzq_sendBuffer[i]);
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                kzq_sendBuffer[5] = (byte)(kzq_sendBuffer[5] | (0x1 << 0));
            }
            else
            {
                kzq_sendBuffer[5] = (byte)(kzq_sendBuffer[5] &  ~(0x1 << 0));
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                kzq_sendBuffer[5] = (byte)(kzq_sendBuffer[5] | (0x1 << 1));
            }
            else
            {
                kzq_sendBuffer[5] = (byte)(kzq_sendBuffer[5] & ~(0x1 << 1));
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                kzq_sendBuffer[5] = (byte)(kzq_sendBuffer[5] | (0x1 << 2));
            }
            else
            {
                kzq_sendBuffer[5] = (byte)(kzq_sendBuffer[5] & ~(0x1 << 2));
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                kzq_sendBuffer[5] = (byte)(kzq_sendBuffer[5] | (0x1 << 3));
            }
            else
            {
                kzq_sendBuffer[5] = (byte)(kzq_sendBuffer[5] & ~(0x1 << 3));
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                kzq_sendBuffer[5] = (byte)(kzq_sendBuffer[5] | (0x1 << 4));
            }
            else
            {
                kzq_sendBuffer[5] = (byte)(kzq_sendBuffer[5] & ~(0x1 << 4));
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                kzq_sendBuffer[5] = (byte)(kzq_sendBuffer[5] | (0x1 << 5));
            }
            else
            {
                kzq_sendBuffer[5] = (byte)(kzq_sendBuffer[5] & ~(0x1 << 5));
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
            {
                kzq_sendBuffer[5] = (byte)(kzq_sendBuffer[5] | (0x1 << 6));
            }
            else
            {
                kzq_sendBuffer[5] = (byte)(kzq_sendBuffer[5] & ~(0x1 << 6));
            }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked)
            {
                kzq_sendBuffer[5] = (byte)(kzq_sendBuffer[5] | (0x1 << 7));
            }
            else
            {
                kzq_sendBuffer[5] = (byte)(kzq_sendBuffer[5] & ~(0x1 << 7));
            }
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked)
            {
                kzq_sendBuffer[6] = (byte)(kzq_sendBuffer[6] | (0x1 << 0));
            }
            else
            {
                kzq_sendBuffer[6] = (byte)(kzq_sendBuffer[6] & ~(0x1 << 0));
            }
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked)
            {
                kzq_sendBuffer[6] = (byte)(kzq_sendBuffer[6] | (0x1 << 1));
            }
            else
            {
                kzq_sendBuffer[6] = (byte)(kzq_sendBuffer[6] & ~(0x1 << 1));
            }
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox11.Checked)
            {
                kzq_sendBuffer[6] = (byte)(kzq_sendBuffer[6] | (0x1 << 2));
            }
            else
            {
                kzq_sendBuffer[6] = (byte)(kzq_sendBuffer[6] & ~(0x1 << 2));
            }
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox12.Checked)
            {
                kzq_sendBuffer[6] = (byte)(kzq_sendBuffer[6] | (0x1 << 3));
            }
            else
            {
                kzq_sendBuffer[6] = (byte)(kzq_sendBuffer[6] & ~(0x1 << 3));
            }
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox13.Checked)
            {
                kzq_sendBuffer[6] = (byte)(kzq_sendBuffer[6] | (0x1 << 4));
            }
            else
            {
                kzq_sendBuffer[6] = (byte)(kzq_sendBuffer[6] & ~(0x1 << 4));
            }
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox14.Checked)
            {
                kzq_sendBuffer[6] = (byte)(kzq_sendBuffer[6] | (0x1 << 5));
            }
            else
            {
                kzq_sendBuffer[6] = (byte)(kzq_sendBuffer[6] & ~(0x1 << 5));
            }
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox15.Checked)
            {
                kzq_sendBuffer[6] = (byte)(kzq_sendBuffer[6] | (0x1 << 6));
            }
            else
            {
                kzq_sendBuffer[6] = (byte)(kzq_sendBuffer[6] & ~(0x1 << 6));
            }
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox16.Checked)
            {
                kzq_sendBuffer[6] = (byte)(kzq_sendBuffer[6] | (0x1 << 7));
            }
            else
            {
                kzq_sendBuffer[6] = (byte)(kzq_sendBuffer[6] & ~(0x1 << 7));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!zkp_isConn)
            {
                MessageBox.Show("请先连接中控屏", "提示");
            }
            else
            {
                zkp_sendBuffer[4] = 0x20;
                zkp_buffer_head();
                if (client_zkp != null)
                {
                    client_zkp.Send(zkp_sendBuffer);
                }
            }
        }

        private void zkp_buffer_head()
        {
            zkp_sendBuffer[0] = 0xaa;
            zkp_sendBuffer[1] = 0x75;
            zkp_sendBuffer[2] = 14;
            zkp_sendBuffer[13] = 0;
            int i;
            for (i = 0; i < 13; i++)
            {
                zkp_sendBuffer[13] = (byte)(zkp_sendBuffer[13] ^ zkp_sendBuffer[i]);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (!zkp_isConn)
            {
                MessageBox.Show("请先连接中控屏", "提示");
            }
            else
            {

            }
        }

        private void updateConnState()
        {
            {
                this.Invoke(new Action(() =>
                {
                    if (kzq_isConn)
                    {
                        button11.Text = "控制器状态：连接";
                        button11.BackColor = System.Drawing.Color.LightGreen;
                    }
                    else
                    {
                        button11.Text = "控制器状态：断开";
                        button11.BackColor = Color.Red;
                    }

                    if (zkp_isConn)
                    {
                        button12.Text = "中控屏状态：连接";
                        button12.BackColor = System.Drawing.Color.LightGreen;
                    }
                    else
                    {
                        button12.Text = "中控屏状态：断开";
                        button12.BackColor = Color.Red;
                    }
                }));
            }
            if (client_kzq != null && client_zkp != null)
            {
                kzq_isConn = client_kzq.Connected;
                zkp_isConn = client_zkp.Connected;
            }


        }
        private void checkBox33_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
