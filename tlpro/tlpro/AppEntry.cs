using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlpro
{
    class AppEntry
    {
        public static int Login()
        {
            return Load.TryLogin();
        }
        private static StuMain _stuMain = null;
        public static StuMain MainForm
        {
            get { return _stuMain; }
            set { _stuMain = value; }
        }
        private static TeaMain _teaMain = null;
        public static TeaMain TainForm
        {
            get { return _teaMain; }
            set { _teaMain = value; }
        }
    }
}

