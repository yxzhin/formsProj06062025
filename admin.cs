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
    public partial class Admin : Form
    {

        public string userName;
        public string predmet;

        public Admin(string userName, string predmet)
        {

            this.userName = userName;
            this.predmet = predmet;

            InitializeComponent();

        }

        public void prikaziPodatke(bool filtrirajPoOdeljenju, bool filtrirajPoOcenama)
        {

            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            dataGridView1.Columns.Add("ucenik", "ucenik");
            dataGridView1.Columns.Add("odeljenje", "odeljenje");
            dataGridView1.Columns.Add("ocene", "ocene");

            if (filtrirajPoOdeljenju && filtrirajPoOcenama) return;

            int filterType = filtrirajPoOdeljenju ? 1 : 2;

            List<long> ids = Form1.dbmanagement.ucitajIDSvihUcenika(filterType);

            label1.Text = label1.Text.Replace("{user}", userName);
            label2.Text = label2.Text.Replace("{predmet}", predmet);
            label3.Text = label3.Text.Replace("{broj}", ids.Count.ToString());

            foreach (long id in ids)
            {

                Ucenik ucenik = Form1.dbmanagement.ucitajUcenika(id);

                string user = ucenik.ime + " " + ucenik.prezime;
                string ocene = string.Join(", ", ucenik.ocene[predmet]);

                dataGridView1.Rows.Add(user, ucenik.odeljenje, ocene);

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            prikaziPodatke(checkBox1.Checked, checkBox2.Checked);

        }

        private void Admin_Load(object sender, EventArgs e)
        {

            prikaziPodatke(checkBox1.Checked, checkBox2.Checked);

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count == 0)
            {

                Error.show(-5);
                return;

            }

            DataGridViewRow select = dataGridView1.SelectedRows[0];

            string ocene = select.Cells["ocene"].Value.ToString();

            string ucenik = select.Cells["ucenik"].Value.ToString();
            (string ime, string prezime)
                = (ucenik.Split()[0], ucenik.Split()[1]);

            long id = long.Parse(Form1.dbmanagement.ucitajIDIzImenaIPrezimena(ime, prezime).ToString());

            ChangeGrades changegrades = new ChangeGrades(ocene, id, predmet);
            DialogResult result = changegrades.ShowDialog();

            if (result == DialogResult.OK) prikaziPodatke(checkBox1.Checked, checkBox2.Checked);

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            prikaziPodatke(checkBox1.Checked, checkBox2.Checked);

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

            prikaziPodatke(checkBox1.Checked, checkBox2.Checked);

        }
    }
}
