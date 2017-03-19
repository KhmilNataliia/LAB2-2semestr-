using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using text_work;

namespace my_client
{
    public partial class Form1 : Form
    {
        static private Socket Client;
        private IPAddress ip = null;
        private int port = 0;
        private Thread Th;
        public Form1()
        {
            InitializeComponent();
            try
            {
                var sr = new StreamReader(@"client_info/connect_info.txt");
                string buffer = sr.ReadToEnd();
                sr.Close();
                string[] connect_inf = buffer.Split(':');
                ip = IPAddress.Parse(connect_inf[0]);
                port = int.Parse(connect_inf[1]);
                label2.ForeColor = Color.Green;
                label2.Text = "you are sucsesfully connected";
                Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                if (ip != null)
                {
                    Client.Connect(ip, port);
                    Th = new Thread(delegate() { recv_data(); });
                    Th.Start();
                }
            }
            catch (Exception ex)
            {
                label2.ForeColor = Color.Red;
                label2.Text = "Settings not found!";
                Form2 form = new Form2();
                form.Show();
            }
        }
        text work_with_text = new text();
        string copy = "";
        string cur2 = "";
        string copy_of_changes = "";
        bool firstconnect=true;
        public int poscur = 0;
        public string myID = "";
        bool opening_file = false;
        bool exit = false;
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!save_changes && !opening_file)
            {
                string cur = "";
                cur = richTextBox1.Text;
                if (cur != copy)
                {
                    if (cur != copy && copy != "")
                    {

                        cur2 = work_with_text.changes(cur, copy, ref poscur);
                       copy_of_changes= work_with_text.adding_to_other_changings(cur2, copy_of_changes);
                    }
                    else cur2 = cur;
                    send_text(copy_of_changes + ";;;-5");
                    copy = cur;
                    try
                    {
                        richTextBox1.SelectionStart = poscur - 1;
                        richTextBox1.SelectionLength = 1;
                        richTextBox1.SelectedText = copy[poscur - 1].ToString();
                    }
                    catch (Exception ex) { }
                }
            }
        }
        string filename;
        int max_size = 100000;

        

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            opening_file = true;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK && openFileDialog1.FileName.Length > 0)
            {
                filename = openFileDialog1.FileName;
                send_text(filename + ";;;f");
                richTextBox1.LoadFile(filename, RichTextBoxStreamType.PlainText);
            }
            button1.Enabled = true;
            label1.Text = filename;
            copy_of_changes = richTextBox1.Text;
            copy = richTextBox1.Text;
            closeFileToolStripMenuItem.Enabled = true;
            saveFileToolStripMenuItem.Enabled = true;
            opening_file = false;
          }
        
        bool save_changes = false;

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SaveFile(filename, RichTextBoxStreamType.PlainText);
        }

        private void closeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeFileToolStripMenuItem.Enabled = false;
            saveFileToolStripMenuItem.Enabled = false;
            save_changes = true;
            richTextBox1.Clear();
            copy = "";
            copy_of_changes = ";;;-3;;;-4";
            send_text(copy_of_changes + ";;;-5");
            save_changes = false;
            label1.Text = "File";
        }

        private void saveHowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
                File.WriteAllText(sfd.FileName, richTextBox1.Text);
            filename = sfd.FileName;
            saveFileToolStripMenuItem.Enabled = true;
            closeFileToolStripMenuItem.Enabled = true;
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save_changes = true;
            richTextBox1.Clear();
            copy = "";
            copy_of_changes = ";;;-3;;;-4";
            send_text(copy_of_changes+";;;-5");
            save_changes=false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exit = true;
            try
            {
                if (Th.IsAlive)
                { Th.Abort(); }

                send_text(";;;;-2;");
                if (Client.Connected)
                {
                    send_text(";;;;-2;");
                    Client.Disconnect(false);
                }
            }
            catch (Exception ex) { };
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Enabled = true;
            richTextBox1.SelectionStart = 0;
            richTextBox1.SelectionLength = 0;
            richTextBox1.SelectedText = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right: if (poscur <= richTextBox1.Text.Length) { poscur++; } break;
                case Keys.Left: if (poscur > 0) { poscur--; } break;
                case Keys.Up: poscur = calc_up();
                                        break;
                case Keys.Down: poscur = calc_down(); 
                                        break;
                case Keys.Back: if (poscur > 0) { poscur--; } break;
                default: poscur++; break;
            }
        }
        private void richTextBox1_MouseClick(object sender, MouseEventArgs e)
        {

            MessageBox.Show("please, use only keys: 'right', 'left', 'up' and 'down' for moving in the text!");
            if (poscur > 0)
            {
                richTextBox1.SelectionStart = poscur - 1;
                richTextBox1.SelectionLength = 1;
                richTextBox1.SelectedText = copy[poscur - 1].ToString();
            }
            else
            {
                richTextBox1.SelectionStart = 0;
                richTextBox1.SelectionLength = 0;
                richTextBox1.SelectedText = "";
            }
        }



        //functions for working with data!



        int calc_up()
        {
            int cur_line = richTextBox1.GetLineFromCharIndex(poscur);
            if (cur_line == 0) return poscur;
            int distanse = 0; int i = 0;
            for(i=poscur;  i!=0 && richTextBox1.Text[i-1]!='\n';i--) distanse++;
                if (richTextBox1.Lines[cur_line - 1].Length < distanse)
                { return (poscur - distanse - 1); }
                else return (poscur - distanse - (richTextBox1.Lines[cur_line - 1].Length - distanse)-1);
        }

        int calc_down()
        {
            int cur_line = richTextBox1.GetLineFromCharIndex(poscur);
            int distanse = 0; int i = 0;
            for (i = poscur; i != 0 && richTextBox1.Text[i - 1] != '\n'; i--) distanse++;
            if (distanse < richTextBox1.Lines[cur_line + 1].Length)
            {
                return (poscur + richTextBox1.Lines[cur_line].Length + 1);
            }
            else return (poscur + (richTextBox1.Lines[cur_line].Length - distanse) + richTextBox1.Lines[cur_line + 1].Length+1);
        }

        void send_text(string text_dat) //preparing to sending and sending
        {
            if (text_dat != "" && text_dat != " ")
            {
                byte[] buffer = new byte[max_size];
                buffer = Encoding.UTF8.GetBytes(text_dat);
                Client.Send(buffer);
            }
        }

       
        void recv_data() //working with received data
        {
            byte[] buffer = new byte[max_size];
            for (int i = 0; i < buffer.Length; i++) { buffer[i] = 0; }
            for (; ; )
            {
                try
                {
                    Client.Receive(buffer);
                    {
                         text.changed[] mas_of_change=new text.changed[1000];
                        int num=0;
                        string recv_dat = Encoding.UTF8.GetString(buffer);
                        for (int i = 0; i < buffer.Length; i++) { buffer[i] = 0; }
                        int isfile = recv_dat.IndexOf(";;;f");
                        if (isfile == -1)
                        {
                            int endtext = recv_dat.IndexOf(";;;-5");
                            if (endtext == -1) { continue; }
                            string recv_dat_clear = "";
                            for (int i = 0; i < endtext; i++)
                            {
                                recv_dat_clear += recv_dat[i];
                            }
                            if (firstconnect)
                            {
                                myID = recv_dat_clear;
                                firstconnect = false;
                            }
                            else
                            {
                               
                               num=work_with_text.find_all_changes(copy,recv_dat_clear,ref mas_of_change);
                               copy_of_changes = recv_dat_clear;
                                if (num > 0) this.Invoke((MethodInvoker)delegate()
                                {
                                    int copy_cur = 0;
                                    for (int i = 0; i < num;i++ )
                                    {
                                        if (poscur > mas_of_change[i].beg)
                                        { poscur +=mas_of_change[i].changed_part.Length-(mas_of_change[i].end-mas_of_change[i].beg); }
                                        copy_cur = poscur;
                                        save_changes = true;
                                        richTextBox1.SelectionStart =mas_of_change[i].beg;
                                        richTextBox1.SelectionLength = mas_of_change[i].end-mas_of_change[i].beg;
                                        richTextBox1.SelectedText =mas_of_change[i].changed_part;
                                        richTextBox1.SelectionColor = Color.GreenYellow;
                                        richTextBox1.Select(mas_of_change[i].beg,mas_of_change[i].changed_part.Length);
                                        richTextBox1.SelectionBackColor = Color.GreenYellow;
                                        copy = richTextBox1.Text;
                                        save_changes = false;
                                        poscur = copy_cur;
                                    }
                                    poscur = copy_cur;
                                    if (poscur != 0)
                                    {
                                        richTextBox1.SelectionStart = poscur;
                                        richTextBox1.SelectionLength = 0;
                                        richTextBox1.SelectedText = "";
                                        
                                        poscur = copy_cur;
                                    }
                                    else
                                    {
                                        richTextBox1.SelectionStart =0;
                                        richTextBox1.SelectionLength = 0;
                                        richTextBox1.SelectedText = "";
                                    }
                                });
                            }
                        }
                        else
                        {
                            filename = "";
                            for (int i = 0; i < isfile; i++)
                            {
                                filename += recv_dat[i];

                            }
                            this.Invoke((MethodInvoker)delegate() { label1.Text = filename; });
                            opening_file = true;
                            this.Invoke((MethodInvoker)delegate()
                            {
                                richTextBox1.LoadFile(filename, RichTextBoxStreamType.PlainText);
                                copy_of_changes = richTextBox1.Text;
                                copy = richTextBox1.Text;
                                closeFileToolStripMenuItem.Enabled = true;
                                saveFileToolStripMenuItem.Enabled = true;
                                button1.Enabled = true;
                            });
                            opening_file = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!Client.Connected && !exit)
                    {
                        MessageBox.Show("the connection was lost, the program will be closed!");
                        Application.Exit();
                        Th.Abort();
                    }
                }
            }
        }
    }
}
