using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseManager
{
    class DBM
    {

        public static void Main()
        {
            ServiceReference1.DatabaseManagerClient proxy = new ServiceReference1.DatabaseManagerClient();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(proxy));
        }
    }
}
