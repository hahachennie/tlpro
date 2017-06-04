using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlpro
{
    class Question
    {
        private int id = 0;
        private String timu = String.Empty;//题目
        private int type = 0;//题目类型，1为选择题，2为判断题，3为简答题
        private int answer = 0;//作答答案：type==1时，1A 2B 3C 4D；type==2时，1对2错；type==3时无意义 
        private int rightan = 0;//正确答案
        private List<String> jianda = null;//type==3时有效,储存关键字
        private String ka, kb, kc, kd;
        private String temp = String.Empty;
        public List<String> rejianda
        {
            get { return jianda; }
            set { jianda = value; }
        }
        public int Type
        {
            get { return type; }
            set { type = value; }
        }
        public String Timu
        {
            get { return timu; }
            set { timu = value; }
        }
        public String Ka
        {
            get { return ka; }
            set { ka = value; }
        }
        public String Kb
        {
            get { return kb; }
            set { kb = value; }
        }
        public String Kc
        {
            get { return kc; }
            set { kc = value; }
        }
        public String Kd
        {
            get { return kd; }
            set { kd = value; }
        }
        public int Answer
        {
            get { return answer; }
            set { answer = value; }
        }
        public int Rightan
        {
            get { return rightan; }
            set { rightan = value; }
        }
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public String Temp
        {
            get { return temp; }
            set { temp = value; }
        }
    }
}
