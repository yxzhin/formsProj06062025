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
    public partial class User : Form
    {
        public User(long id)
        {

            this.id = id;

            InitializeComponent();

        }

        public long id;

        public void prikaziPodatke()
        {

            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            dataGridView1.Columns.Add("predmet", "predmet");
            dataGridView1.Columns.Add("ocene", "ocene");

            Ucenik ucenik = Form1.dbmanagement.ucitajUcenika(id);

            string user = ucenik.ime + " " + ucenik.prezime;

            label1.Text = label1.Text.Replace("{user}", user);
            label2.Text = label2.Text.Replace("{odeljenje}", ucenik.odeljenje);
            label3.Text = label3.Text.Replace("{uzrast}", ucenik.uzrast + " ");

            foreach (string predmet in Data.defaultOcene.Keys)
            {

                string ocene = string.Join(", ", ucenik.ocene[predmet]);

                dataGridView1.Rows.Add(predmet, ocene);

            }

        }

        private void User_Load(object sender, EventArgs e)
        {

            prikaziPodatke();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            prikaziPodatke();

        }
    }
}
