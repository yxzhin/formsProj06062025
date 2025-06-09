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
    public partial class Add : Form
    {
        public Add()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string ime = textBox1.Text.Trim();
            string prezime = textBox2.Text.Trim();
            string odeljenje = textBox3.Text.Trim();
            string lozinka = textBox5.Text.Trim();

            if(string.IsNullOrEmpty(ime)
                || string.IsNullOrEmpty(prezime)
                || string.IsNullOrEmpty(odeljenje)
                || !int.TryParse(textBox4.Text.Trim(), out int uzrast))
            {

                Error.show(-1);
                return;

            }

            Ucenik ucenik = new Ucenik(ime, prezime, lozinka, odeljenje, uzrast, Data.defaultOcene);

            long result = Form1.dbmanagement.dodajUcenika(ucenik);

            if(result == -4)
            {

                Error.show(-4);
                return;

            }

            MessageBox.Show($"ucenik je dodat!! id: {result}", "uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;
            this.Close();

        }
    }
}
