using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace theprj2
{
    public partial class Change : Form
    {

        public long id;

        public Change(long id)
        {

            this.id = id;

            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            string ime = textBox1.Text.Trim();
            string prezime = textBox2.Text.Trim();
            string odeljenje = textBox3.Text.Trim();
            string lozinka = textBox5.Text.Trim();
            bool parseUzrast = int.TryParse(textBox4.Text.Trim(), out int uzrast);

            if (string.IsNullOrEmpty(ime)
                && string.IsNullOrEmpty(prezime)
                && string.IsNullOrEmpty(odeljenje)
                && string.IsNullOrEmpty(lozinka)
                && !parseUzrast)
            {

                Error.show(-1);
                return;

            }

            if(!string.IsNullOrEmpty(textBox4.Text.Trim())
                && !parseUzrast
                || uzrast < 0)
            {

                Error.show(-1);
                return;

            }

            uzrast = parseUzrast ? int.Parse(textBox4.Text.Trim()) : -1;

            Form1.dbmanagement.promeniUcenika(id, ime, prezime, lozinka, odeljenje, uzrast, null);

            MessageBox.Show($"ucenik je promenjen!!", "uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;
            this.Close();

        }
    }
}
