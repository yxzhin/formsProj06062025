using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace theprj2
{

    public struct Ucenik
    {

        public string ime;
        public string prezime;
        public string lozinka;
        public string odeljenje;
        public int uzrast;
        public Dictionary<string, List<int>> ocene;

        public Ucenik(string ime, string prezime, string lozinka, string odeljenje,
            int uzrast, Dictionary<string, List<int>> ocene)
        {

            this.ime = ime;
            this.prezime = prezime;
            this.lozinka = lozinka;
            this.odeljenje = odeljenje;
            this.uzrast = uzrast;
            this.ocene = ocene;

        }

    }

}
