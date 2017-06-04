using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tlpro
{
    public partial class tianjia : Form
    {
        String ty;
        public tianjia(String ty)
        {
            InitializeComponent();
            if (ty == "1")
            {
                label1.Text = "请输入希望添加的科目名字";
                this.ty = ty;
            }
            else
            {
                label1.Text = "你希望添加的章节所属科目为："+ty;
                Graphics g = label1.CreateGraphics();
                SizeF length = g.MeasureString(label1.Text, label1.Font);
                int i=172-(int)length.Width/2;
                label1.Location = new Point(i,25);
                this.ty = ty;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text==String.Empty)
            {
                MessageBox.Show("信息不能为空！");
                return;
            }
            Regex reg = new Regex(@"^\d");
            if (ty=="1"&&reg.IsMatch(textBox1.Text))
            {
                MessageBox.Show("科目名字不能以数字开头！","警告");
                textBox1.SelectAll();
                textBox1.Focus();
                return;
            }
            if (ty == "1" && textBox1.Text.Contains(" "))
            {
                MessageBox.Show("科目名字中不能添加空格！", "警告");
                textBox1.SelectAll();
                textBox1.Focus();
                return;
            }
            string getWeatherUrl = "http://" + Httpadd.Add + ":8080/tlpro/xiugai.jsp?op=kz&ty="
                    + ty + "&name=" + textBox1.Text;
            String s = xiugai(getWeatherUrl);
            if (s.Equals("-1"))
            {
                MessageBox.Show("服务器连接失败！", "警告");
                return;
            }
            else
            {
                AppEntry.TainForm.FillData();
                MessageBox.Show("增加成功！", "恭喜");
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private string xiugai(String getWeatherUrl)
        {

            try
            {

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
    }
}
