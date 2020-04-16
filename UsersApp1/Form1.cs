using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.Protocols;
using System.Collections;
using System.DirectoryServices.AccountManagement;

namespace UsersApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void Log(string strLOG)
        {
            if (strLOG.Length == 0) return;
            string[] lines = { strLOG };
            System.IO.File.AppendAllLines(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserApp1LOG.txt"), lines, System.Text.Encoding.Default);
        }

        private void Test1()
        {
            string st = "";
            long n = 0;
            try
            {
                using (var context = new PrincipalContext(ContextType.Domain, "mhp.com.ua.inc"))
                {
                    using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                    {
                        foreach (var result in searcher.FindAll())
                        {
                            DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
                            n++;
                            st = n.ToString("00000") + " Користувач: " + de.Properties["sn"].Value + " " + de.Properties["givenName"].Value + "(" + de.Properties["samAccountName"].Value + ") " + de.Properties["userPrincipalName"].Value;
                            lbl.Text = n.ToString(); ;
                            Log(st);
                            Application.DoEvents();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            lblDone.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            lbl.Text = "0";
            lblDone.Visible = false;
            Test1();
            button1.Enabled = true;
        }
    }
}
