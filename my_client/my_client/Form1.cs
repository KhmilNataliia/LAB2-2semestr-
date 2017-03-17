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

namespace my_client
{
    public partial class Form1 : Form
    {
        static private Socket Client;
        private IPAddress ip = null;
        private int port = 0;
        private Thread Th;
        private Thread tH;
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
        string copy = "";
        string cur2 = "";
        bool firstconnect=true;
        public int poscur = 0;
        public string myID = "";
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!save_changes)
            {
                string cur = "";
                cur = richTextBox1.Text;
                if (cur != copy)
                {
                    if (cur != copy && copy != "")
                    {

                        cur2 = changes(cur,copy);
                    }
                    else cur2 = cur;
                    send_text(cur2 + ";;;-5");
                    copy = cur;
                    try
                    {
                        richTextBox1.SelectionStart = poscur - 1;
                        richTextBox1.SelectionLength = 1;
                        richTextBox1.SelectedText = copy[poscur - 1].ToString();
                    }
                    catch (Exception ex) { }
                    flag = false;
                }
            }
        }
        bool flag=true;
        string filename;
        int max_size = 1024;

        

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK && openFileDialog1.FileName.Length > 0)
            {
                filename = openFileDialog1.FileName;
                flag = true; 
                send_text(filename + ";;;f");
                richTextBox1.LoadFile(filename, RichTextBoxStreamType.PlainText);
            }
            button1.Enabled = true;
            label1.Text = filename;
            closeFileToolStripMenuItem.Enabled = true;
            saveFileToolStripMenuItem.Enabled = true;
          }
        
        bool for_file = false;
        bool save_changes = false;

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SaveFile(filename, RichTextBoxStreamType.PlainText);
        }

        private void closeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeFileToolStripMenuItem.Enabled = false;
            saveFileToolStripMenuItem.Enabled = false;
            richTextBox1.Clear();
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
            richTextBox1.Clear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Th.IsAlive) { Th.Abort(); }
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
                case Keys.Right: poscur++; break; 
                case Keys.Left: poscur--; break;
                case Keys.Up: poscur = calc_up(); 
                    richTextBox1.SelectionStart = poscur;
                                        richTextBox1.SelectionLength = 0;
                                        richTextBox1.SelectedText = "";
                                        break;
                case Keys.Down: poscur = calc_down(); 
                    richTextBox1.SelectionStart = poscur;
                                        richTextBox1.SelectionLength = 0;
                                        richTextBox1.SelectedText = "";
                                        break;
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

        struct changed
        {
            public string changed_part = "";
            public int beg = 0;
            public int end = 0;
        }

        changed[] find_all_changes(string cur,string copy)
        {
            changed[] all=new changed[1000];
            int count = 0;
            int ch = 0;

        }


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

        public string changes(string cur, string copy) //finding what was changed and marking it!
        {
            string cur2 = "";
            int change = 0;
            while (change < copy.Length && change < cur.Length && cur[change] == copy[change]) { change++; }
            for (int i = 0; i < change; i++) { cur2 += cur[i]; }
            cur2 += ";;;-3";
            if (copy.Length < cur.Length)
            {
                int dif = change;
                for (int i = change; i < dif + cur.Length - copy.Length; i++) { cur2 += cur[i]; change++; } change--;
                while (copy.Length > change && cur[change + cur.Length - copy.Length] != copy[change]) { cur2 += cur[change + cur.Length - copy.Length]; change++; }
                change += cur.Length - copy.Length;
                if (copy.Length > change)
                {
                    cur2 += ";;;0" + myID + ";;";
                    poscur = change;
                }
                for (int i = change; i < cur.Length; i++) { cur2 += cur[i]; }
                cur2 += ";;;0" + myID + ";;";
            }
            else
            {
                if (copy.Length == cur.Length)
                {
                    while (copy.Length > change && cur[change] != copy[change]) { cur2 += cur[change]; change++; }
                    cur2 += ";;;0" + myID + ";;";
                    poscur = change;
                    for (int i = change; i < cur.Length; i++) { cur2 += cur[i]; }
                }
                else
                {
                    cur2 += ";;;0" + myID + ";;";
                    poscur = change;
                    for (int i = change; i < cur.Length; i++) { cur2 += cur[i]; }
                }
            }
            return cur2;
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
                                int beg = recv_dat_clear.IndexOf(";;;-3");
                                int endc = recv_dat_clear.IndexOf(";;;0") + 7;
                                string changed_part = "";
                                int pos_to_ins_b = 0;
                                int pos_to_ins_e = 0;
                                if (beg != -1)
                                {
                                    string part_before_change = recv_dat_clear.Substring(0, beg);
                                    pos_to_ins_b = 0 + part_before_change.Length;
                                    if (endc < recv_dat_clear.Length)
                                    {
                                        string part_after_change = recv_dat_clear.Substring(endc, recv_dat_clear.Length - endc - 7);
                                        pos_to_ins_e = copy.IndexOf(part_after_change);
                                    }
                                    else pos_to_ins_e = copy.Length;
                                    changed_part = recv_dat_clear.Substring(beg + 5, endc - 12 - beg);
                                }
                                if (endtext > 0) this.Invoke((MethodInvoker)delegate()
                                {
                                    int copy_cur = 0;
                                    if (beg != -1)
                                    {
                                        if (poscur > pos_to_ins_b)
                                        { poscur += changed_part.Length; }
                                        copy_cur = poscur;
                                        save_changes = true;
                                        richTextBox1.SelectionStart = pos_to_ins_b;
                                        richTextBox1.SelectionLength = pos_to_ins_e - pos_to_ins_b;
                                        richTextBox1.SelectedText = changed_part;
                                        richTextBox1.SelectionColor = Color.GreenYellow;
                                        richTextBox1.Select(pos_to_ins_b, changed_part.Length);
                                        richTextBox1.SelectionBackColor = Color.GreenYellow;
                                        copy = richTextBox1.Text;
                                        save_changes = false;
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
                            label1.Text = filename;
                            flag = true;
                            richTextBox1.LoadFile(filename, RichTextBoxStreamType.PlainText);
                            closeFileToolStripMenuItem.Enabled = true;
                            saveFileToolStripMenuItem.Enabled = true;
                            button1.Enabled = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!Client.Connected)
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
