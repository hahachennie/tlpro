using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tlpro
{
    public partial class IPxiu : Form
    {
        public IPxiu()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == String.Empty)
            {
                MessageBox.Show("IP地址不能为空！");
                return;
            }
            if (!textBox1.Text.Contains(".") && textBox1.Text != "localhost")
            {
                MessageBox.Show("IP地址格式为XXX.XXX.XXX.XXX类型！\r\n如果在本机请填写localhost");
                return;
            }
            Httpadd.Add = textBox1.Text.Trim();
            this.Close();
        }
    }
}
