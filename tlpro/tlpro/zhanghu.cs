using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tlpro
{
    public partial class zhanghu : Form
    {
        public zhanghu()
        {
            InitializeComponent();
            if(QuEntry.User!=String.Empty)
            {
                textBox1.ReadOnly = true;
                textBox1.Text = QuEntry.User;
                textBox1.TabStop = false;
                textBox1.Enabled = false;
                ty = 1;//1表示已登录
            }
            else
            {
                this.Text = "注册";
            }
        }
        private int ty = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if(ty==1)
            {
                if (textBox1.Text == String.Empty)
                {
                    MessageBox.Show("用户名不能为空！", "提示");
                    textBox1.Focus();
                    return;
                }
               
            }
            if (textBox2.Text == String.Empty)
            {
                MessageBox.Show("密码不能为空！", "提示");
                textBox2.Focus();
                return;
            }
            if (textBox3.Text == String.Empty)
            {
                MessageBox.Show("确认密码不能为空！", "提示");
                textBox3.Focus();
                return;
            }
            if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("两次密码不一致！", "提示");
                textBox3.Focus();
                return;
            }
            String s=zh();
            if(s=="-1")
            {
                MessageBox.Show("用户名已存在！","警告");
                return;
            }
            if(ty==1)
                MessageBox.Show("修改成功！");
            else
                MessageBox.Show("注册成功！");
            this.Close();
        }
        private string zh()
        {

            try
            {
                string getWeatherUrl = "http://" + Httpadd.Add + ":8080/tlpro/xiugai.jsp?op=zhanghu&ty="+ty.ToString()
                    +"&name="+ textBox1.Text+"&pass="+textBox2.Text;
                
                WebRequest webReq = WebRequest.Create(getWeatherUrl);
                WebResponse webResp = webReq.GetResponse();
                Stream stream = webResp.GetResponseStream();
                StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("UTF-8"));
                string html = sr.ReadToEnd();
                sr.Close();
                stream.Close();
                return html;
            }
            catch
            {
                return "-1";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            this.Close();
        }
    }
}
