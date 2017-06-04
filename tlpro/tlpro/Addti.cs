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
using System.Xml;

namespace tlpro
{
    public partial class Addti : Form
    {
        String type = String.Empty;
        String pare = String.Empty;
        String chil = String.Empty;
        bool ticun = true;
        int yida = 0;
        int ticount;
        List<Question> Qrand = null;
        public Addti(String type, String pare, String chil)
        {
            InitializeComponent();
            this.type = type;
            this.pare = pare;
            this.chil = chil;
            if (type == "1")
            {
                radioButton1.Visible = true;
                radioButton2.Visible = true;
                radioButton3.Visible = true;
                radioButton4.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
            }
            else if (type == "2")
            {
                radioButton1.Visible = true;
                radioButton2.Visible = true;
                radioButton1.Text = "√";
                radioButton2.Text = "×";
                radioButton1.Location = new Point(56, 217);
                radioButton2.Location = new Point(56, 258);
            }
            else if (type == "3")
            {
                label1.Visible = true;
                richTextBox2.Visible = true;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Addti_Load(object sender, EventArgs e)
        {
            if (type == "-1")
            {
                button1.Text = "确认修改";
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                Filltimu();
            }
        }
        private void Filltimu()
        {
            String ti = Loade();
            if (ti.Equals("-1"))
            {
                MessageBox.Show("服务器连接失败！", "警告");
                return;
            }
            else
            {
                stuXMLs(ti);

            }

            if (ticun == false)
            {
                ticun = true;
                this.Close();
            }
            else
            {
                jiazai();
            }
        }
        private string Loade()
        {

            try
            {
                string getWeatherUrl = "http://" + Httpadd.Add + ":8080/tlpro/chuti.jsp?type=1&name=" + chil;

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
        private void stuXMLs(String html)
        {
            XmlDocument xx = new XmlDocument();
            xx.LoadXml(html);
            XmlNode xxNode = xx.SelectSingleNode("/ti");
            if (!xxNode.HasChildNodes)
            {
                MessageBox.Show("此章节尚无题库！");
                ticun = false;
                return;

            }

            QuEntry.Quentry.Clear();
            foreach (XmlNode xxNode2 in xxNode.ChildNodes)
            {
                Question qu = new Question();
                qu.rejianda = new List<string>();
                foreach (XmlNode xxNode3 in xxNode2.ChildNodes)
                {
                    if (xxNode3.Name == "ktimu")
                    {
                        qu.Timu = xxNode3.InnerText;
                    }
                    else if (xxNode3.Name == "id")
                    {
                        qu.ID = int.Parse(xxNode3.InnerText);
                    }
                    else if (xxNode3.Name == "ktype")
                    {
                        qu.Type = int.Parse(xxNode3.InnerText);
                    }
                    else if (xxNode3.Name == "ka")
                    {
                        qu.Ka = xxNode3.InnerText;
                    }
                    else if (xxNode3.Name == "kb")
                    {
                        qu.Kb = xxNode3.InnerText;
                    }
                    else if (xxNode3.Name == "kc")
                    {
                        qu.Kc = xxNode3.InnerText;
                    }
                    else if (xxNode3.Name == "kd")
                    {
                        qu.Kd = xxNode3.InnerText;
                    }
                    else if (xxNode3.Name == "kranswer")
                    {
                        if (qu.Type != 3)
                            qu.Rightan = int.Parse(xxNode3.InnerText);
                    }
                    else if (xxNode3.Name == "kjianda")
                    {
                        String jianda = xxNode3.InnerText;
                        qu.rejianda.Add(jianda);
                    }

                }
                QuEntry.Quentry.Add(qu);
            }

            Qrand = QuEntry.Quentry;
            ticount = Qrand.Count;
        }
        private void jiazai()
        {
            richTextBox1.Text = Qrand[yida].Timu;
            if (Qrand[yida].Type == 1)
            {
                label1.Visible = false;
                radioButton1.Visible = true;
                radioButton2.Visible = true;
                radioButton3.Visible = true;
                radioButton4.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox1.Text = Qrand[yida].Ka;
                textBox2.Text = Qrand[yida].Kb;
                textBox3.Text = Qrand[yida].Kc;
                textBox4.Text = Qrand[yida].Kd;

            }
            else if (Qrand[yida].Type == 2)
            {
                label1.Visible = false;
                radioButton1.Visible = true;
                radioButton2.Visible = true;
                radioButton1.Text = "√";
                radioButton2.Text = "×";

            }
            else if (Qrand[yida].Type == 3)
            {
                label1.Visible = true;

                richTextBox2.Visible = true;
                richTextBox2.Text = "";
                foreach (String iii in Qrand[yida].rejianda)
                {
                    richTextBox2.Text += iii + " ";
                }
                String temp = richTextBox2.Text.Trim();
                richTextBox2.Text = temp.Replace(" ", "++");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == String.Empty)
            {
                MessageBox.Show("题目不能为空！", "警告");
                return;
            }
            if (type == "1")
            {
                if (textBox1.Text == String.Empty || textBox2.Text == String.Empty ||
                    textBox3.Text == String.Empty || textBox4.Text == String.Empty)
                {
                    MessageBox.Show("选项答案不能为空！", "警告");
                    return;
                }
            }
            else if (type == "3")
            {
                if (richTextBox2.Text == String.Empty)
                {
                    MessageBox.Show("简答题答案不能为空！", "警告");
                    return;
                }
                if (richTextBox2.Text.Contains(" "))
                {
                    MessageBox.Show("简答题答案不能包含空格，必须以“++”作为分隔！", "警告");
                    return;
                }
            }
            Question qu = new Question();
            qu.Timu = richTextBox1.Text.Replace(" ", "**");
            qu.Type = int.Parse(type);
            qu.Ka = textBox1.Text;
            qu.Kb = textBox2.Text;
            qu.Kc = textBox3.Text;
            qu.Kd = textBox4.Text;
            if (radioButton1.Checked)
                qu.Rightan = 1;
            else if (radioButton2.Checked)
                qu.Rightan = 2;
            else if (radioButton3.Checked)
                qu.Rightan = 3;
            else if (radioButton4.Checked)
                qu.Rightan = 4;
            String jianda = richTextBox2.Text.Replace("++", "</kjianda><kjianda>");
            if (type != "-1")
            {

                string getWeatherUrl = "http://" + Httpadd.Add + ":8080/tlpro/xiugai.jsp?op=addti&ty="
                   + type + "&timu=" + qu.Timu + "&ka=" + qu.Ka + "&kb=" + qu.Kb + "&kc=" + qu.Kc + "&kd=" + qu.Kd
                   + "&ranswer=" + qu.Rightan.ToString() + "&kechen=" + pare + "&zhangjie=" + chil.Replace(" ", "**") + "&jianda=" + jianda;
                String s = xiugai(getWeatherUrl);
                if (s.Equals("-1"))
                {
                    MessageBox.Show("服务器连接失败！", "警告");
                    return;
                }
                else
                {
                    MessageBox.Show("添加成功！", "恭喜");
                    this.Close();
                }
            }
            else
            {
                string getWeatherUrl = "http://" + Httpadd.Add + ":8080/tlpro/xiugai.jsp?op=gait"
                 + "&timu=" + qu.Timu + "&ka=" + qu.Ka + "&kb=" + qu.Kb + "&kc=" + qu.Kc + "&kd=" + qu.Kd
                  + "&ranswer=" + qu.Rightan.ToString() + "&id=" + Qrand[yida].ID + "&jianda=" + jianda;
                String s = xiugai(getWeatherUrl);
                if (s.Equals("-1"))
                {
                    MessageBox.Show("服务器连接失败！", "警告");
                    return;
                }
                else
                {
                    MessageBox.Show("修改成功！", "恭喜");

                }
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (yida == ticount - 1)
            {
                MessageBox.Show("没有下一题了！");

                return;
            }
            ++yida;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            radioButton3.Visible = false;
            radioButton4.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            label1.Visible = false;
            richTextBox2.Visible = false;
            jiazai();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string getWeatherUrl = "http://" + Httpadd.Add + ":8080/tlpro/xiugai.jsp?op=shant&id=" + Qrand[yida].ID;
            String s = xiugai(getWeatherUrl);
            if (s.Equals("-1"))
            {
                MessageBox.Show("服务器连接失败！", "警告");
                return;
            }
            else
            {
                MessageBox.Show("删除成功！", "恭喜");
                if (yida == ticount - 1)
                {
                    this.Close();
                }
                else
                {

                    ++yida;

                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (yida == 0)
            {
                MessageBox.Show("没有上一题了！");
                return;
            }
            else
            {
                --yida;
                radioButton1.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = false;
                radioButton4.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
                textBox3.Visible = false;
                textBox4.Visible = false;
                label1.Visible = false;
                richTextBox2.Visible = false;
                jiazai();
            }
        }
    }
}
