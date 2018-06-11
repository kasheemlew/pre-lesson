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
    public partial class Form2 : Form
    {
        Label parent;
        public Form2()
        {
            InitializeComponent();
            label2.Text = "请点击日历进行选择！";
        }
        public Form2(Label parent)
        {
            InitializeComponent();
            label2.Text = "请点击日历进行选择！";
            this.parent = parent;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            label2.Text = monthCalendar1.SelectionStart.ToString("yyyy年-MM月-dd号");
            parent.Text = label2.Text;
        }
    }
}
