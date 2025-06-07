using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace theprj2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dbmanagement.connect();
            adminsList = Data.loadAdmins();
            textBox2.PasswordChar = '*';
        }

        public static DBManagement dbmanagement = new DBManagement();
        public static List<string> adminsList;
        private static short failedLoginAttempts;
        public static string adminType;
        private static bool showPassword = false;

        private void button1_Click(object sender, EventArgs e)
        {

            string userName = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();
            string credentials = userName + '+' + password;

            if (failedLoginAttempts == 3)
            {

                Error.show(-3);
                return;

            }

            if (!Data.admins.ContainsKey(credentials))
            {

                ++failedLoginAttempts;
                Error.show(-1);
                return;

            }

            adminType = Data.admins[credentials];

            if (adminType == "direktor")
            {

                Direktor direktor = new Direktor();
                direktor.Show();

            } else
            {

                Admin admin = new Admin();
                admin.Show();

            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            textBox2.PasswordChar = showPassword ? '*' : '\0';
            showPassword = !showPassword;

        }
    }
}
