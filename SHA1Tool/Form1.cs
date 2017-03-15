using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
//Author:mahdi abasi
//15-March-2017
namespace SHA1Tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string GetSha1Hash(string filePath)
        {
            using (FileStream fs = File.OpenRead(filePath))
            {
                SHA1 sha = new SHA1Managed();
                return BitConverter.ToString(sha.ComputeHash(fs));
            }
        }
        public static string TextToHash(string text)
        {
            var sh = SHA1.Create();
            var hash = new StringBuilder();
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            byte[] b = sh.ComputeHash(bytes);
            foreach (byte a in b)
            {
                var h = a.ToString("x2");
                hash.Append(h);
            }
            return hash.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog2.FileName = "";

            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!="")
            textBox2.Text=GetSha1Hash(textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
          textBox4.Text =TextToHash(textBox3.Text);
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        string path = "";
        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog2.Filter = "Text files|*.txt";
            openFileDialog2.FileName = "";
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                textBox6.Text = openFileDialog2.SafeFileName;
                path = openFileDialog2.FileName;
            }
            
        }
       
        private void button5_Click(object sender, EventArgs e)
        {
            if (BF.IsBusy==false)
            {
                textBox7.BackColor = Color.Yellow;
                BF.RunWorkerAsync();
            }
          
          
        }

        private void BF_DoWork(object sender, DoWorkEventArgs e)
        {
            
           string line;
           bool find = false;
           int c=0, max = File.ReadLines(path).Count(); 
           StreamReader file = new StreamReader(path);

            while ((line = file.ReadLine()) != null)
            {
               textBox7.Invoke(new Action(delegate (){textBox7.Text = c+" of "+max+"   "+ "\r\n" + line;}));
                
                if (TextToHash(line).Trim() == textBox5.Text.Trim().ToLower())
                {
                    textBox7.Invoke(new Action(delegate () { textBox7.Text = "Found: " + line; }));
                    textBox7.BackColor = Color.Green;
                    file.Close();
                    find = true;
                    break;
                }
                c++;
                
            }
            if (find == false)
            {
                textBox7.Invoke(new Action(delegate () { textBox7.Text = "Not Found!"; }));
                textBox7.BackColor = Color.Red;
            }
           
            file.Close();
            
        }
    }
}
