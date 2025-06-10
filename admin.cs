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

        public void prikaziPodatke()
        {

            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            dataGridView1.Columns.Add("ucenik", "ucenik");
            dataGridView1.Columns.Add("odeljenje", "odeljenje");
            dataGridView1.Columns.Add("ocene", "ocene");

            List<long> ids = Form1.dbmanagement.ucitajIDSvihUcenika();

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

            prikaziPodatke();

        }

        private void Admin_Load(object sender, EventArgs e)
        {

            prikaziPodatke();

        }

        private void button2_Click(object sender, EventArgs e)
        {



        }
    }
}
