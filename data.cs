using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace theprj2
{
    public static class Data
    {

        public static Dictionary<string, string> admins = new Dictionary<string, string>();
        public static string path = "admins.txt";

        public static short loadAdmins()
        {

            if (string.IsNullOrEmpty(path))
            {

                Error.show(-1, "; putanja fajla je prazna");
                return -1;

            }

            if (!File.Exists(path))
            {

                using(StreamWriter  sw = new StreamWriter(path))
                {

                    sw.WriteLine("direktor+admin73|direktor");

                }

                return 1;

            }

            string line, credentials, adminType;

            using(StreamReader sr = new StreamReader(path))
            {

                while(!string.IsNullOrEmpty(line = sr.ReadLine()))
                {

                    var splt = line.Split('|');

                    if (splt.Length != 2) continue;

                    (credentials, adminType) = (splt[0], splt[1]);

                    if (admins.ContainsKey(credentials)) continue;

                    admins.Add(credentials, adminType);

                }

            }

            return 1;

        }
    }
}
