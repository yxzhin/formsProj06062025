using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace theprj2
{
    public static class Data
    {

        public static Dictionary<string, string> users = new Dictionary<string, string>();
        public static string path = "users.txt";

        public static short loadUsers()
        {

            if (string.IsNullOrEmpty(path))
            {

                Error.show(-1);
                return -1;

            }

            if (!File.Exists(path)) return 0;

            string line, userName, password;

            using(StreamReader sr = new StreamReader(path))
            {

                line = sr.ReadLine();

                while(!string.IsNullOrEmpty(line))
                {

                    var splt = line.Split('|');

                    if (splt.Length != 2) continue;

                    (userName, password) = (splt[0], splt[1]);

                    if (users.ContainsKey(userName)) continue;

                    users.Add(userName, password);

                }

            }

            return 1;

        }

        public static void saveUsers()
        {

            using (StreamWriter sw = new StreamWriter(path, false))
            {

                foreach (var user in users) sw.WriteLine($"{user.Key}|{user.Value}");

            }

        }

    }
}
