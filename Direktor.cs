﻿using System;
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

            listBox2.Items.Clear();

            Form1.ids = Form1.dbmanagement.ucitajIDSvihUcenika();

            foreach (long id in Form1.ids)
            {

                string ime, prezime, odeljenje, uzrast, lozinka;

                Ucenik ucenik = Form1.dbmanagement.ucitajUcenika(id);

                (ime, prezime, odeljenje, uzrast, lozinka) =
                    (ucenik.ime, ucenik.prezime, ucenik.odeljenje, ucenik.uzrast.ToString(), ucenik.lozinka);

                listBox2.Items.Add($"{ime} {prezime} | {odeljenje} | {uzrast} god. | lozinka: {lozinka}");

            }

        }

        private void prikaziNastavnike()
        {

            listBox1.Items.Clear();

            foreach(string admin in Form1.adminsList)
            {

                listBox1.Items.Add(admin);

            }

        }

        private long ucitajIDIzSelektovanogPolja()
        {

            string[] splt = listBox2.SelectedItem.ToString().Split('|')[0].Split();
            (string ime, string prezime) = (splt[0], splt[1]);

            object id = Form1.dbmanagement.ucitajIDIzImenaIPrezimena(ime, prezime);

            return long.Parse(id.ToString());

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Add add = new Add();

            DialogResult result = add.ShowDialog();

            if (result == DialogResult.OK) prikaziUcenike();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (listBox2.SelectedItem == null)
            {

                Error.show(-5);
                return;

            }

            long id = ucitajIDIzSelektovanogPolja();

            Form1.dbmanagement.izbrisiUcenika(id);

            prikaziUcenike();

            MessageBox.Show($"ucenik je uspesno izbrisan!!", "uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (listBox2.SelectedItem == null)
            {

                Error.show(-5);
                return;

            }

            long id = ucitajIDIzSelektovanogPolja();

            Change change = new Change(id);

            DialogResult result = change.ShowDialog();

            prikaziUcenike();

        }
    }
}
