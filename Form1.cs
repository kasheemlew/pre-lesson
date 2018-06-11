using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;

namespace 课程大作业
{
    public partial class Form1 : Form
    {
        public SQLiteConnection db = new SQLiteConnection("Data Source=MyDatabase.sqlite; Version=3;");
        public string teachingPlan;
        public Form1()
        {
            InitializeComponent();
            dbAddTables();
        }

        private bool tableExists(string tableName)
        {
            bool exists;
            db.Open();

            try
            {
                var cmd = new SQLiteCommand(
                  "select case when exists((select * from information_schema.tables where table_name = '" + tableName + "')) then 1 else 0 end", db);

                exists = (int)cmd.ExecuteScalar() == 1;
            }
            catch
            {
                try
                {
                    exists = true;
                    var cmdOthers = new SQLiteCommand("select 1 from " + tableName + " where 1 = 0", db);
                    cmdOthers.ExecuteNonQuery();
                }
                catch
                {
                    exists = false;
                }
            }
            db.Close();
            return exists;
        }

        private void dbAddTables()
        {
            if (!tableExists("files"))
            {
                db.Open();
                var cmd = new SQLiteCommand("create table files(courseName varchar(255), filename varchar(255), data blob);", db);
                cmd.ExecuteNonQuery();
                db.Close();
            }
            if (!tableExists("courses"))
            {
                db.Open();
                var cmd = new SQLiteCommand("create table courses(courseName varchar(255), teacher varchar(255), date varchar(255), chapter varchar(255));", db);
                cmd.ExecuteNonQuery();
                db.Close();
            }
            if (!tableExists("teachingPlans"))
            { 
                db.Open();
                var cmd = new SQLiteCommand("create table teachingPlans(courseName varchar(255), data text);", db);
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form5 f = new Form5(this);
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请填写课程主题！");
                return;
            }

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "所有文件(*.*) | *.*";
            dialog.Multiselect = false; 
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                String path = dialog.FileName;
                string[] pathSplit = path.Split('/');
                string filename = pathSplit[pathSplit.Length - 1];
                byte[] file;
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new BinaryReader(stream))
                    {
                        file = reader.ReadBytes((int)stream.Length);
                    }
                }
                using (SQLiteCommand sqlWrite = new SQLiteCommand("INSERT INTO  files(courseName, filename, data) Values('"+ textBox1.Text.Trim()+"', '" +filename+ "', @File)", db))
                {
                    sqlWrite.Parameters.Add("@File", DbType.Binary, file.Length).Value = file;
                    db.Open();
                    sqlWrite.ExecuteNonQuery();
                    db.Close();
                }
                textBox3.Text += filename + "\n";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            f.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3(label5);
            f.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2(label6);
            f.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请填写课程主题！");
                return;
            }
            if (textBox2.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请填写教师名称！");
                return;
            }
            if (label5.Text.Trim() == "点击进行选择")
            {
                MessageBox.Show("请选择章节！");
                return;
            }
            if (label6.Text.Trim() == "点击进行选择")
            {
                MessageBox.Show("请选择日期！");
                return;
            }
            try
            {
                db.Open();
                var cmd = new SQLiteCommand("insert into courses(courseName, teacher, date, chapter) values ('" + textBox1.Text.Trim() + "', '" + textBox2.Text.Trim() + "', '" + label6.Text.Trim() + "', '" + label5.Text.Trim() + "');", db);
                cmd.ExecuteNonQuery();
                cmd = new SQLiteCommand("insert into teachingPlans(courseName, data) values ('" + textBox1.Text.Trim() + "', '" + this.teachingPlan + "');", db);
                cmd.ExecuteNonQuery();
                MessageBox.Show("已提交！");
                db.Close();
                this.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
