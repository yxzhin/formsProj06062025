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

namespace theprj2
{
    public partial class Direktor : Form
    {
        public Direktor()
        {
            InitializeComponent();
            prikaziUcenike();
            prikaziNastavnike();
        }

        private void prikaziUcenike()
        {

            List<long> ids = Form1.dbmanagement.ucitajIDSvihUcenika();
                 
            foreach (long id in ids)
            {

                string ime, prezime, odeljenje;

                Ucenik ucenik = Form1.dbmanagement.ucitajUcenika(id);
                (ime, prezime, odeljenje) = (ucenik.ime, ucenik.prezime, ucenik.odeljenje);

                listBox2.Items.Add($"{ime} {prezime} {odeljenje}");

            }

        }

        private void prikaziNastavnike()
        {

            foreach(string admin in Form1.adminsList)
            {

                listBox1.Items.Add(admin);

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {



        }
    }
}
