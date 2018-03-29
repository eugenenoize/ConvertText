using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvertText
{
    public partial class Form1 : Form
    {
        private string finaltext;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {      
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt";
            saveFileDialog1.DefaultExt = "txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel) { return; }
            string save = saveFileDialog1.FileName;
            File.WriteAllText(save, finaltext);
            MessageBox.Show("Файл сохранен");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text files(*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel) { return; }

                StreamReader fs = new StreamReader(openFileDialog1.FileName);
                string text = fs.ReadToEnd();
                TextCount(text);
            
        }

        private void TextCount(string text)
        {
            Regex reg = new Regex(@"[a-zA-Zа-яА-Я]+");
            var collection = reg.Matches(text).Cast<Match>().GroupBy(w => w.Value).Select(w => new Word { Name = w.Key, Count = w.Count() }).OrderByDescending(w => w.Count).ToList();

            StringBuilder final = new StringBuilder();
            foreach(var item in collection)
            {
                final.AppendLine(item.Name + "\t" + ":" + item.Count);
            }
            finaltext = final.ToString();
            MessageBox.Show("Файл обработан");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    public class Word
    {
        public int Count { get; set; }
        public string Name { get; set; }
    }
}