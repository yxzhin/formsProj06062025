using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

            if(string.IsNullOrEmpty(ime)
                && string.IsNullOrEmpty(prezime)
                && string.IsNullOrEmpty(odeljenje)
                && string.IsNullOrEmpty(lozinka)
                || !int.TryParse(textBox4.Text.Trim(), out int uzrast))
            {

                Error.show(-1);
                return;

            }

            uzrast = int.Parse(textBox4.Text.Trim());

            Form1.dbmanagement.promeniUcenika(id, ime, prezime, lozinka, odeljenje, uzrast, null);

            MessageBox.Show($"ucenik je promenjen!!", "uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;
            this.Close();

        }
    }
}
