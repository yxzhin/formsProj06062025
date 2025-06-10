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
        public static List<long> ids;

        private void button1_Click(object sender, EventArgs e)
        {

            string userName = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();
            string credentials = userName + '+' + password;

            if(string.IsNullOrEmpty(userName)
                || string.IsNullOrEmpty(password))
            {

                Error.show(-1);
                return;

            }

            if (failedLoginAttempts == 3)
            {

                Error.show(-3);
                return;

            }

            if (Data.admins.ContainsKey(credentials))
            {

                adminType = Data.admins[credentials];

                if(adminType == "direktor")
                {

                    Direktor direktor = new Direktor();
                    direktor.Show();
                    return;

                }

                Admin admin = new Admin(userName, adminType);
                admin.Show();
                return;

            }

            if (!userName.Contains("_"))
            {

                Error.show(-1);
                return;

            }

            (string ime, string prezime) = (userName.Split('_')[0], userName.Split('_')[1]);

            object result = dbmanagement.ucitajIDIzImenaIPrezimena(ime, prezime, true, password);

            if (result != null)
            {

                User user = new User(long.Parse(result.ToString()));
                user.Show();
                return;

            }

            ++failedLoginAttempts;
            Error.show(-1);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            textBox2.PasswordChar = showPassword ? '*' : '\0';
            showPassword = !showPassword;

        }
    }
}
