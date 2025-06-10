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
    public partial class ChangeGrades : Form
    {

        public long id;
        public string predmet;

        public ChangeGrades(string ocene, long id, string predmet)
        {

            InitializeComponent();

            this.id = id;
            this.predmet = predmet;

            textBox1.Enabled = false;
            textBox1.Text = ocene;

            label2.Text = label2.Text.Replace("{predmet}", predmet);

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string noveOcene = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(noveOcene))
            {

                Error.show(-1);
                return;

            }

            try
            {

                List<int> noveOceneList = noveOcene.Split(',').Select(int.Parse).ToList();

                foreach(int ocena in noveOceneList)
                {

                    if(ocena < 1
                        || ocena > 5)
                    {

                        Error.show(-1, "; ocene moraju biti u rasponu od 1 do 5");
                        return;

                    }

                }

                Ucenik ucenik = Form1.dbmanagement.ucitajUcenika(id);

                Dictionary<string, List<int>> ocene = ucenik.ocene;

                ocene[predmet] = noveOceneList;

                Form1.dbmanagement.promeniUcenika(id, null, null, null, null, -1, ocene);

                MessageBox.Show("ocene ucenika su promenjene!!", "uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);

            } catch (Exception)
            {

                Error.show(-1);
                return;

            }

            this.DialogResult = DialogResult.OK;
            this.Close();

        }
    }
}
