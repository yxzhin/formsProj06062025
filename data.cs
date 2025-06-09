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

        public static List<string> loadAdmins()
        {

            List<string> adminsList = new List<string>();

            if (string.IsNullOrEmpty(path))
            {

                Error.show(-1, "; putanja fajla je prazna");
                return adminsList;

            }

            File.Delete(path);

            using (StreamWriter sw = new StreamWriter(path))
            {

                sw.WriteLine("direktor+admin73|direktor");
                sw.WriteLine("nastavnik1+aa|srpski jezik");

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

                    string name = credentials.Split('+')[0];

                    adminsList.Add($"{name} // {adminType}");

                }

            }

            return adminsList;

        }
    }
}
