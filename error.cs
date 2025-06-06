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

        public static void show(short type)
        {

            switch (type)
            {

                case -1: MessageBox.Show("unesite validne vrednosti", "greska", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
                case -2: MessageBox.Show("fajl nije pronadjen", "greska", MessageBoxButtons.OK, MessageBoxIcon.Error); return;

            }

        }

    }
}
