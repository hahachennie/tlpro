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
    public partial class Load : Form
    {
        public Load()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public static int TryLogin()
        {
            Load form = new Load();
            DialogResult i = form.ShowDialog();
            if (i == DialogResult.OK)
                return 1;
            else if (i == DialogResult.Yes)
                return 2;
            return 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            String user = textBox1.Text;
            String pass = textBox2.Text;
            if (user == String.Empty)
            {
                MessageBox.Show("用户名不能为空！", "提示");
                textBox1.Focus();
                return;
            }
            if (pass == String.Empty)
            {
                MessageBox.Show("密码不能为空！", "提示");
                textBox2.Focus();
                return;
            }
            label4.Text = "         正在登录中，请稍候...";
            String xml = Loadp(user, pass).Trim();
            if (xml.Equals("1"))
            {
                label4.Text = "  欢迎使用课堂检测系统,系统启动中";
                QuEntry.User = textBox1.Text;
                AppEntry.MainForm = new StuMain();
                this.DialogResult = DialogResult.OK;
                this.Close(); //关闭登陆窗体 

            }
            else if (xml.Equals("2"))
            {
                label4.Text = "  欢迎使用课堂检测系统,系统启动中";
                QuEntry.User = textBox1.Text;
                AppEntry.TainForm = new TeaMain();
                this.DialogResult = DialogResult.Yes;
                this.Close(); //关闭登陆窗体 
            }
            else if (xml.Equals("3"))
            {

            }
            else if (xml.Equals("0"))
            {
                label4.Text = "      用户名或密码不正确,请检查！";
                textBox2.SelectAll();
                textBox2.Focus();
                MessageBox.Show("用户名或密码不正确！", "提示");
            }
            else
            {
                label4.Text = "         请检查网络设置！";
                MessageBox.Show("服务器连接失败，请检查网络设置！", "提示");
            }

        }
        private string Loadp(String name, String pass)
        {

            try
            {
                string getWeatherUrl = "http://" + Httpadd.Add + ":8080/tlpro/load.jsp?name=" + name + "&passward=" + pass;
                WebRequest webReq = WebRequest.Create(getWeatherUrl);
                webReq.Timeout = 2000;
                WebResponse webResp = webReq.GetResponse();
                Stream stream = webResp.GetResponseStream();
                StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("GBK"));
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

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "adame" && textBox2.Text.Trim() == "passward")
            {
                IPxiu form1 = new IPxiu();
                form1.ShowDialog();
            }
            else
            {
                zhanghu form = new zhanghu();
                form.ShowDialog();
            }
        }
    }
}
