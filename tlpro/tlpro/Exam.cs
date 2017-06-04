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
    public partial class Exam : Form
    {
        int second5 = 0;
        int minute5 = 0;
        int yida = 0;
        int ryida = 0;
        int ticount;
        bool ticun=true;
        String type = String.Empty;
        String name = String.Empty;
        List<Question> Qrand = null;
        public Exam(String type,String name)
        {
            InitializeComponent();
            this.ControlBox = false;
            this.type = type;
            this.name = name;
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            String minute = null;
            String second = null;
           if (DateTime.Now.Minute<10)
            {
                minute = "0" + DateTime.Now.Minute.ToString();
            }
            else
            {
                minute = DateTime.Now.Minute.ToString();
            }
            if(DateTime.Now.Second < 10)
            {
                second = "0" + DateTime.Now.Second.ToString();
            }
            else
            {
                second = DateTime.Now.Second.ToString();
            }
            label3.Text ="当前时间 "+ DateTime.Now.Hour.ToString() + ":"+ minute + ":" + second;
            
            ++second5;
            if (second5 == 60)
            {
                second5 = 0;
                ++minute5;
            }
            label5.Text = "用时："+minute5.ToString()+"分"+second5.ToString()+"秒";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            exit();
            AppEntry.MainForm.Filllist();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Qrand[yida].Type == 1)
            {
                if(radioButton1.Checked)
                {
                    Qrand[yida].Answer = 1;
                    Qrand[yida].Temp = radioButton1.Text;
                }
                else  if (radioButton2.Checked)
                {
                    Qrand[yida].Answer = 2;
                    Qrand[yida].Temp = radioButton2.Text;
                }
                else if (radioButton3.Checked)
                {
                    Qrand[yida].Answer = 3;
                    Qrand[yida].Temp = radioButton3.Text;
                }
                else if (radioButton4.Checked)
                {
                    Qrand[yida].Answer = 4;
                    Qrand[yida].Temp = radioButton4.Text;
                }
                if(Qrand[yida].Answer!=Qrand[yida].Rightan)
                {
                    String ttemp = "回答错误！\r\n" + Qrand[yida].Timu + "\r\n正确答案："+Qrand[yida].Rightan;
                    MessageBox.Show(ttemp);
                }
                else
                {
                    ++ryida;
                }
            }
            else if (Qrand[yida].Type == 2)
            {
                if (radioButton1.Checked)
                {
                    Qrand[yida].Answer = 1;
                    Qrand[yida].Temp = radioButton1.Text;
                }
                else if (radioButton2.Checked)
                {
                    Qrand[yida].Answer = 2;
                    Qrand[yida].Temp = radioButton2.Text;
                }
                if (Qrand[yida].Answer != Qrand[yida].Rightan)
                {
                    String ttemp = "回答错误！\r\n" + Qrand[yida].Timu + "\r\n正确答案：" + Qrand[yida].Rightan;
                    MessageBox.Show(ttemp);
                }
                else
                {
                    ++ryida;
                }

            }
            else if (Qrand[yida].Type == 3)
            {
                int shu = Qrand[yida].rejianda.Count;
                int dui = 0;
                String daa = String.Empty;
                for (int i=0;i<shu;++i)
                {
                    int inde = richTextBox3.Text.IndexOf(Qrand[yida].rejianda[i]);
                    if (inde != -1)
                        ++dui;
                    daa += Qrand[yida].rejianda[i]+" ";
                }
                daa += shu.ToString() + "方面内容";
                if (dui < shu)
                {
                    String ttemp = String.Empty;
                    if (richTextBox3.Text.Trim()==""|| richTextBox3.Text.Trim()==String.Empty)
                    {
                        ttemp = "不回答代表你是在藐视我吗？！";
                    }
                    else
                    {
                        
                        if (dui == 0)
                            ttemp = "回答错误！牛头不对马嘴，你是来消遣我的吗？！";
                        else
                            ttemp = "回答片面，还缺少" + (shu - dui).ToString() + "个方面";
                    }
                    ttemp += "\r\n" + Qrand[yida].Timu + "\r\n正确答案：必须包括" + daa;
                    MessageBox.Show(ttemp);
                }
                else
                    ++ryida;

            }
            if (yida == ticount - 1)
            {
               
                label1.Text = "已答" + (yida+1).ToString() + "题";
                label2.Text = "正确" + ryida.ToString() + "题";
                MessageBox.Show("没有下一题了！");

                return;
            }
            else
            {
                ++yida;
                label1.Text = "已答" + yida.ToString() + "题";
                label2.Text = "正确" + ryida.ToString() + "题";
                radioButton1.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = false;
                radioButton4.Visible = false;
                label4.Visible = false;
                richTextBox3.Visible = false;
                jiazai();
            }

        }

        private void Exam_Load(object sender, EventArgs e)
        {
            Filltimu();
            richTextBox1.TabStop = false;
            richTextBox2.TabStop = false;
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
                string getWeatherUrl = "http://" + Httpadd.Add + ":8080/tlpro/chuti.jsp?type="+type+"&name="+name;
                
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
                        qu.Ka ="A:"+ xxNode3.InnerText;
                    }
                    else if (xxNode3.Name == "kb")
                    {
                        qu.Kb ="B:"+ xxNode3.InnerText;
                    }
                    else if (xxNode3.Name == "kc")
                    {
                        qu.Kc ="C:"+ xxNode3.InnerText;
                    }
                    else if (xxNode3.Name == "kd")
                    {
                        qu.Kd = "D:"+xxNode3.InnerText;
                    }
                    else if (xxNode3.Name == "kranswer")
                    {
                        if(qu.Type!=3)
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
            if(type=="2")
            {
                Qrand = QuEntry.Quentry;
            }
            else
            {
                Qrand = QuEntry.RandomSortList(QuEntry.Quentry);
            }
            ticount = Qrand.Count;
            
        }
        private void jiazai()
        {
            richTextBox2.Text = Qrand[yida].Timu;
            if (Qrand[yida].Type == 1)
            {
                label4.Visible = false;
                radioButton1.Visible = true;
                radioButton2.Visible = true;
                radioButton3.Visible = true;
                radioButton4.Visible = true;
                radioButton1.Text = Qrand[yida].Ka;
                radioButton2.Text = Qrand[yida].Kb;
                radioButton3.Text = Qrand[yida].Kc;
                radioButton4.Text = Qrand[yida].Kd;

            }
            else if (Qrand[yida].Type == 2)
            {
                label4.Visible = false;
                radioButton1.Visible = true;
                radioButton2.Visible = true;
                radioButton1.Text = "√";
                radioButton2.Text = "×";

            }
            else if (Qrand[yida].Type == 3)
            {
                label4.Visible = true;
                label4.Text = "简答题作答：";
                richTextBox3.Visible = true;

            }
        }
        private void exit()
        {
            String time = DateTime.Now.ToString("yyyy-MM-dd")+"_"+DateTime.Now.ToString("HH:mm:ss");
            try
            {
                String getWeatherUrl = "http://" + Httpadd.Add + ":8080/tlpro/xiugai.jsp?op=record&user=" + QuEntry.User 
                    + "&rightshu=" + ryida.ToString() + "&zongshu=" + yida.ToString()+"&time="+time;

                WebRequest webReq = WebRequest.Create(getWeatherUrl);
                WebResponse webResp = webReq.GetResponse();
                Stream stream = webResp.GetResponseStream();
                StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("UTF-8"));
                string html = sr.ReadToEnd();
                sr.Close();
                stream.Close();
              
            }
            catch
            {
                return ;
            }

        }

    }
}
