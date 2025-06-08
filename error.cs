using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace theprj2
{
    public static class Error
    {

        public static void show(short type, string attrs = null)
        {

            string text = "";

            switch (type)
            {

                case -1: text = "unesite validne vrednosti"; break;
                case -2: text = "doslo je do greske: "; break;
                case -3: text = "imali ste previse neuspelih pokusaja logina"; break;
                case -4: text = "unete vrednosti vec postoje u bazi"; break;

            }

            text = attrs != null ? text+attrs : text;

            MessageBox.Show(text, "greska", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

    }
}
