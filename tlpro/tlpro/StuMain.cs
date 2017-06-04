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
    public partial class StuMain : Form
    {
        public StuMain()
        {
            InitializeComponent();
        }

        private void StuMain_Load(object sender, EventArgs e)
        {
            FillData();
            Filllist();
            



        }
        private void FillData()
        {
            this.treeView1.Nodes.Clear();
            String s = Loadp().Trim();
            if(s.Equals("-1"))
            {
                MessageBox.Show("服务器连接失败！", "警告");
                return;
            }
            else
            {
              //  MessageBox.Show(s);
                stuXMLs(s);             
            }
        }
        private string Loadp()
        {

            try
            {
                string getWeatherUrl = "http://"+Httpadd.Add+":8080/tlpro/stuse.jsp";
                
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
            XmlNode xxNode = xx.SelectSingleNode("/ke");
            if(!xxNode.HasChildNodes)
            {
                
                return;
            }
            TreeNode Tn=null;
            foreach (XmlNode xxNode2 in xxNode.ChildNodes)
            {
               
                TreeNode TN = new TreeNode(xxNode2.Name);
                TN.Tag = xxNode2.SelectSingleNode("kzhushi").InnerText;
                foreach (XmlNode xxNode3 in xxNode2.ChildNodes)
                {
                    if (xxNode3.Name == "zname")
                    {
                        Tn = new TreeNode(xxNode3.InnerText);
                    }
                    else if(xxNode3.Name == "zhushi")
                    {
                        Tn.Tag = xxNode3.InnerText;
                        TN.Nodes.Add(Tn);
                    }
                  
                    

                }
                treeView1.Nodes.Add(TN);
               

            }
            treeView1.ExpandAll();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
           //   MessageBox.Show(Convert.ToString(treeView1.SelectedNode.Text));
           
            richTextBox1.Text = treeView1.SelectedNode.Text+"\r\n"+ (String)treeView1.SelectedNode.Tag;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            System.Environment.Exit(0);
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text ="目前时间："+ DateTime.Now.ToString();
        }

        private void 软件信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help formh = new Help();
            formh.ShowDialog();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
             String type = String.Empty;
             String name = String.Empty;
             if (treeView1.SelectedNode.Parent==null)
             {

              
                  if(radioButton1.Checked)
                  {
                         type = "1";
                         MessageBox.Show("请先选择对应章节");
                         return;
            
                  }
                  else if(radioButton2.Checked)
                  {
                         type = "2";
                         MessageBox.Show("请先选择对应章节");
                         return;
                }
                  else if (radioButton3.Checked)
                  {
                         type = "3";
                         name = treeView1.SelectedNode.Text;
                  }
            }
            else
            {
                if (radioButton1.Checked)
                {
                    type = "1";
                    name= treeView1.SelectedNode.Text;

                }
                else if (radioButton2.Checked)
                {
                    type = "2";
                    name = treeView1.SelectedNode.Text;
                }
                else if (radioButton3.Checked)
                {
                    type = "3";
                    name = treeView1.SelectedNode.Parent.Text;
                }
            }
            Exam exam = new Exam(type,name);
            exam.ShowDialog();
        }
        public void Filllist()
        {
            listView1.Items.Clear();
            String s=recordl();
            if (s.Equals("-1"))
            {
                
                return;
            }
            else
            {
               XMLre(s);
          //      MessageBox.Show(s);
            }
        }
        public string recordl()
        {

            try
            {
                string getWeatherUrl = "http://" + Httpadd.Add + ":8080/tlpro/record.jsp?ruser="+QuEntry.User;

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
        public void XMLre(String html)              //解析xml
        {
            XmlDocument xx = new XmlDocument();
            xx.LoadXml(html);
            XmlNode xxNode = xx.SelectSingleNode("/re");
            if (!xxNode.HasChildNodes)
            {

                return;
            }
            foreach (XmlNode xxNode2 in xxNode.ChildNodes)
            {

                ListViewItem lv = new ListViewItem(QuEntry.User);
               
               
                foreach (XmlNode xxNode3 in xxNode2.ChildNodes)
                {                      
                      
                       lv.SubItems.Add(xxNode3.InnerText);
                       
                }
                listView1.Items.Add(lv);


            }

        }

        private void 查看账户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zhanghu form = new zhanghu();
            form.ShowDialog();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
