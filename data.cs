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

        public static Dictionary<string, List<int>> defaultOcene = new Dictionary<string, List<int>>
        {

            { "srpski jezik", new List<int>() },
            { "matematika", new List<int>() },
            { "programiranje", new List<int>() },

        };

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
                sw.WriteLine("nastavnik2+bb|matematika");
                sw.WriteLine("nastavnik3+cc|programiranje");

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

                    (string name, string password) 
                        = (credentials.Split('+')[0], credentials.Split('+')[1]);

                    adminsList.Add($"{name} | {adminType} | lozinka: {password}");

                }

            }

            return adminsList;

        }
    }
}
