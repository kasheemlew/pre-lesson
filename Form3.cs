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
    public partial class Form3 : Form
    {
        Label parentLabel;
        public Form3()
        {
            InitializeComponent();
        }

        public Form3(Label parentLabel)
        {
            InitializeComponent();
            label2.Text = "请点击进行选择！";
            this.parentLabel = parentLabel;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            label2.Text = parentLabel.Text = treeView1.SelectedNode.Text;
        }
    }
}
