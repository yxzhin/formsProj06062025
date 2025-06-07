using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theprj2
{
    public struct Ucenik
    {

        public string ime;
        public string prezime;
        public string odeljenje;
        public int uzrast;
        public List<Dictionary<string, List<int>>> ocene;

        public Ucenik(string ime, string prezime, string odeljenje, int uzrast, List<Dictionary<string, List<int>>> ocene)
        {

            this.ime = ime;
            this.prezime = prezime;
            this.odeljenje = odeljenje;
            this.uzrast = uzrast;
            this.ocene = ocene;

        }

    }

}
