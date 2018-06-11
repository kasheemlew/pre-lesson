using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 课程大作业
{
    public partial class Form5 : Form
    {
        Form1 parent;
        public Form5()
        {
            InitializeComponent();
        }
        public Form5(Form1 parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            renderText();
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parent.teachingPlan = richTextBox1.Text;
            MessageBox.Show("保存成功！");
            this.Close();
        }

        private void renderText()
        {
            richTextBox2.Text = string.Empty;
            string[] lines = richTextBox1.Text.Split('\n');
            foreach (string line in lines)
            {
                var  lineTrimed = line.Trim();
                if (lineTrimed.Length > 2)
                {
                    if (lineTrimed.StartsWith("h:"))
                    {
                        var lineBody = lineTrimed.Substring(2, lineTrimed.Length-2);
                        richTextBox2.SelectionStart = richTextBox2.TextLength;
                        richTextBox2.SelectionLength = 0;

                        richTextBox2.SelectionColor = Color.Black;
                        richTextBox2.SelectionFont = new Font("Courier New", 20);
                        richTextBox2.AppendText(lineBody);
                        richTextBox2.SelectionColor = richTextBox2.ForeColor;
                    }
                    else if (lineTrimed.StartsWith("b:"))
                    {
                        var lineBody = lineTrimed.Substring(2, lineTrimed.Length - 2);
                        richTextBox2.SelectionStart = richTextBox2.TextLength;
                        richTextBox2.SelectionLength = 0;

                        richTextBox2.SelectionColor = Color.Black;
                        richTextBox2.SelectionFont = new Font("Courier New", 10, FontStyle.Bold);
                        richTextBox2.AppendText(lineBody);
                        richTextBox2.SelectionColor = richTextBox2.ForeColor;
                    }
                    else if (lineTrimed.StartsWith("l:"))
                    {
                        var lineBody = lineTrimed.Substring(2, lineTrimed.Length - 2);
                        richTextBox2.SelectionStart = richTextBox2.TextLength;
                        richTextBox2.SelectionLength = 0;

                        richTextBox2.SelectionColor = Color.Black;
                        richTextBox2.SelectionFont = new Font("Courier New", 10, FontStyle.Italic);
                        richTextBox2.AppendText(lineBody);
                        richTextBox2.SelectionColor = richTextBox2.ForeColor;
                    }
                    else
                    {
                        richTextBox2.AppendText(lineTrimed);

                    }
                }
                else
                {
                    richTextBox2.AppendText(lineTrimed);
                }
                richTextBox2.AppendText("\n");
            }
        }
    }
}
