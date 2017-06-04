using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlpro
{
    class QuEntry
    {
        private static String user = String.Empty;
        private static List<Question> quentry = new List<Question>();
        public static List<Question> RandomSortList<Question>(List<Question> ListT)
        {
            Random random = new Random();
            List<Question> newList = new List<Question>();
            foreach (Question item in ListT)
            {
                newList.Insert(random.Next(newList.Count), item);
            }
            return newList;
        }
        public static List<Question> Quentry
        {
            get { return  quentry; }
            set { quentry = value; }
        }
        public static String User
        {
            get { return user; }
            set { user = value; }
        }

    }
}
