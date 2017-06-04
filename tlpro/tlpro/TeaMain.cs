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
    public partial class TeaMain : Form
    {
        public TeaMain()
        {
            InitializeComponent();
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zhanghu form = new zhanghu();
            form.Show();
        }

        private void 信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help formh = new Help();
            formh.Show();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = "目前时间：" + DateTime.Now.ToString();
        }

        private void TeaMain_Load(object sender, EventArgs e)
        {
            FillData();
        }
        public void FillData()
        {
            this.treeView1.Nodes.Clear();
            String s = Loadp().Trim();
            if (s.Equals("-1"))
            {
                MessageBox.Show("服务器连接失败！", "警告");
                return;
            }
            else
            {
                stuXMLs(s);
            }
        }
        public string Loadp()
        {

            try
            {
                string getWeatherUrl = "http://" + Httpadd.Add + ":8080/tlpro/stuse.jsp";

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
        public void stuXMLs(String html)
        {
            XmlDocument xx = new XmlDocument();
            xx.LoadXml(html);
            XmlNode xxNode = xx.SelectSingleNode("/ke");
            if (!xxNode.HasChildNodes)
            {

                return;
            }
            TreeNode Tn = null;
            foreach (XmlNode xxNode2 in xxNode.ChildNodes)
            {

                TreeNode TN = new TreeNode(xxNode2.Name);
                TN.Name = xxNode2.SelectSingleNode("kid").InnerText;
                TN.Tag = xxNode2.SelectSingleNode("kzhushi").InnerText;
                foreach (XmlNode xxNode3 in xxNode2.ChildNodes)
                {

                    if (xxNode3.Name == "zname")
                    {
                        Tn = new TreeNode(xxNode3.InnerText);
                    }
                    else if (xxNode3.Name == "zid")
                    {
                        Tn.Name = xxNode3.InnerText;
                    }
                    else if (xxNode3.Name == "zhushi")
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
            richTextBox1.Text = (String)treeView1.SelectedNode.Tag;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String treeype = String.Empty;
            if (treeView1.SelectedNode.Parent == null)
            {
                treeype = "1";
            }
            else
            {
                treeype = "2";
            }
            string getWeatherUrl = "http://" + Httpadd.Add + ":8080/tlpro/xiugai.jsp?op=zhushi&ty="
                    + treeype + "&name=" + treeView1.SelectedNode.Text + "&nei=" + richTextBox1.Text;
            String s = xiugai(getWeatherUrl);
            if (s.Equals("-1"))
            {
                MessageBox.Show("服务器连接失败！", "警告");
                return;
            }
            else
            {
                FillData();
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

        private void button4_Click(object sender, EventArgs e)
        {
            tianjia form = new tianjia("1");
            form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            String ty = String.Empty;

            if (treeView1.SelectedNode == null)
            {
                MessageBox.Show("请先选择目标科目或章节！", "警告");
                return;
            }
            if (treeView1.SelectedNode.Parent == null)
                ty = treeView1.SelectedNode.Text;
            else
                ty = treeView1.SelectedNode.Parent.Text;
            tianjia form = new tianjia(ty);
            form.ShowDialog();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            String pare = String.Empty;
            String chil = String.Empty;
            if (treeView1.SelectedNode == null)
            {
                MessageBox.Show("请先选择目标科目或章节！", "警告");
                return;
            }
            String id = treeView1.SelectedNode.Name;
            int i;
            if (treeView1.SelectedNode.Nodes.Count > 0)
            {
                i = 1;
            }
            else
            {
                i = -1;
            }
            if (treeView1.SelectedNode.Parent == null)
            {
                pare = treeView1.SelectedNode.Text;
                chil = "-1";
            }
            else
            {

                pare = treeView1.SelectedNode.Parent.Text;
                chil = treeView1.SelectedNode.Text;
            }
            Shangai form = new Shangai(pare, chil, i, id);
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String pare = String.Empty;
            String chil = String.Empty;
            if (treeView1.SelectedNode == null)
            {
                MessageBox.Show("请先选择目标章节再增加题目！", "警告");
                return;
            }
            if (treeView1.SelectedNode.Parent == null)
            {
                MessageBox.Show("请先选择目标章节，而不是科目，再增加题目！", "警告");
                return;
            }
            else
            {

                pare = treeView1.SelectedNode.Parent.Text;
                chil = treeView1.SelectedNode.Text;
            }
            String type = String.Empty;
            if (radioButton1.Checked)
            {
                type = "1";
            }
            else if (radioButton2.Checked)
            {
                type = "2";
            }
            else if (radioButton3.Checked)
            {
                type = "3";
            }
            Addti form = new Addti(type, pare, chil);
            form.ShowDialog();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
            {
                MessageBox.Show("请先选择目标章节再管理题目！", "警告");
                return;
            }
            if (treeView1.SelectedNode.Parent == null)
            {
                MessageBox.Show("请先选择目标章节，而不是科目，再管理题目！", "警告");
                return;
            }
            Addti form = new Addti("-1", "", treeView1.SelectedNode.Text);
            form.ShowDialog();
        }
    }
}
