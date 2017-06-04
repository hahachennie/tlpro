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
    public partial class Shangai : Form
    {
        String pare = String.Empty;
        String chil = String.Empty;
        int zi;
        String id;
        public Shangai(String pare,String chil,int zi,String id)
        {
            InitializeComponent();
            this.pare = pare;
            this.chil = chil;
            this.zi = zi;
            this.id = id;
            if(chil=="-1")
            {
                label1.Text = "你想要修改的是：科目 "+pare;
                button2.Text = "删除科目";
            }
            else
            {
                label1.Text = "你想要修改的是：" + pare+" 下的章节 "+chil;
                button2.Text = "删除章节";
            }
            Graphics g = label1.CreateGraphics();
            SizeF length = g.MeasureString(label1.Text, label1.Font);
            int i = 190 - (int)length.Width / 2;
            label1.Location = new Point(i, 25);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == String.Empty)
            {
                MessageBox.Show("新名字不能为空！");
                return;
            }
            Regex reg = new Regex(@"^\d");
            if (chil=="-1"&&reg.IsMatch(textBox1.Text))
            {
                MessageBox.Show("科目名字不能以数字开头！", "警告");
                textBox1.SelectAll();
                textBox1.Focus();
                return;
            }
            String name = textBox1.Text;
            name.Replace(" ","**");
            string getWeatherUrl = "http://" + Httpadd.Add + ":8080/tlpro/xiugai.jsp?op=gai&ty="
                    + chil + "&name=" + textBox1.Text+"&id="+id;
            String s = xiugai(getWeatherUrl);
            if (s.Equals("-1"))
            {
                MessageBox.Show("服务器连接失败！", "警告");
                return;
            }
            else
            {
                AppEntry.TainForm.FillData();
                MessageBox.Show("修改成功！", "恭喜");
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (zi == 1)
            {
                MessageBox.Show("删除科目时不允许科目下存在章节！","警告");
                return;
            }
            string getWeatherUrl = "http://" + Httpadd.Add + ":8080/tlpro/xiugai.jsp?op=shan&ty="
                  + chil + "&id="+id;
            String s = xiugai(getWeatherUrl);
            if (s.Equals("-1"))
            {
                MessageBox.Show("服务器连接失败！", "警告");
                return;
            }
            else
            {
                MessageBox.Show("删除成功！", "恭喜");
                AppEntry.TainForm.FillData();
               this.Close();
            }
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
