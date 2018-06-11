using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace 课程大作业
{
    public partial class Form4 : Form
    {
        public SQLiteConnection db = new SQLiteConnection("Data Source=MyDatabase.sqlite; Version=3;");
        public Form4()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            textBox1.Text = richTextBox1.Text = string.Empty;
            var command = new SQLiteCommand("select * from courses where chapter='" + treeView1.SelectedNode.Text.Trim() + "'", db);
            db.Open();
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string courseName;
                courseName = label1.Text = reader["courseName"].ToString();
                label2.Text = reader["teacher"].ToString();
                command = new SQLiteCommand("select * from files where courseName='" + courseName + "'", db);
                reader = command.ExecuteReader();
                while(reader.Read())
                {
                    textBox1.Text += reader["filename"].ToString() + "\n";
                }
                command = new SQLiteCommand("select * from teachingPlans where courseName='" + courseName + "'", db);
                reader = command.ExecuteReader();
                while(reader.Read())
                {
                    renderText(reader["data"].ToString());
                }
            } else
            {
                MessageBox.Show("本章备课还未完成！");
            }
            db.Close();
        }

        private void renderText(string text)
        {
            string[] lines = text.Split('\n');
            foreach (string line in lines)
            {
                var lineTrimed = line.Trim();
                if (lineTrimed.Length > 2)
                {
                    if (lineTrimed.StartsWith("h:"))
                    {
                        var lineBody = lineTrimed.Substring(2, lineTrimed.Length - 2);
                        richTextBox1.SelectionStart = richTextBox1.TextLength;
                        richTextBox1.SelectionLength = 0;

                        richTextBox1.SelectionColor = Color.Black;
                        richTextBox1.SelectionFont = new Font("Courier New", 20);
                        richTextBox1.AppendText(lineBody);
                        richTextBox1.SelectionColor = richTextBox1.ForeColor;
                    }
                    else if (lineTrimed.StartsWith("b:"))
                    {
                        var lineBody = lineTrimed.Substring(2, lineTrimed.Length - 2);
                        richTextBox1.SelectionStart = richTextBox1.TextLength;
                        richTextBox1.SelectionLength = 0;

                        richTextBox1.SelectionColor = Color.Black;
                        richTextBox1.SelectionFont = new Font("Courier New", 10, FontStyle.Bold);
                        richTextBox1.AppendText(lineBody);
                        richTextBox1.SelectionColor = richTextBox1.ForeColor;
                    }
                    else if (lineTrimed.StartsWith("l:"))
                    {
                        var lineBody = lineTrimed.Substring(2, lineTrimed.Length - 2);
                        richTextBox1.SelectionStart = richTextBox1.TextLength;
                        richTextBox1.SelectionLength = 0;

                        richTextBox1.SelectionColor = Color.Black;
                        richTextBox1.SelectionFont = new Font("Courier New", 10, FontStyle.Italic);
                        richTextBox1.AppendText(lineBody);
                        richTextBox1.SelectionColor = richTextBox1.ForeColor;
                    }
                    else
                    {
                        richTextBox1.AppendText(lineTrimed);

                    }
                }
                else
                {
                    richTextBox1.AppendText(lineTrimed);
                }
                richTextBox1.AppendText("\n");
            }
        }
    }
}
