using Json.Net;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace theprj2
{
    public class DBManagement
    {

        public SQLiteConnection conn;
        public bool debugMode = true;

        public void connect()
        {

            try
            {

                string connstr = "Data Source=ucenici.db;Version=3;";
                conn = new SQLiteConnection(connstr);
                conn.Open();

                string query;

                query = @"CREATE TABLE IF NOT EXISTS ucenici (
    id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
    ime TEXT NOT NULL,
    prezime TEXT NOT NULL,
    lozinka TEXT NOT NULL,
    odeljenje TEXT NOT NULL,
    uzrast INT NOT NULL,
    ocene TEXT NOT NULL
);";

                using (var command = new SQLiteCommand(query, conn)) command.ExecuteNonQuery();

                if (debugMode)
                {

                    query = "DELETE FROM ucenici";
                    using (var command = new SQLiteCommand(query, conn)) command.ExecuteNonQuery();

                }
                

            } catch (Exception e)
            {

                Error.show(-2, e.Message);
                return;

            }

        }

        public long dodajUcenika(Ucenik ucenik)
        {

            try
            {

                object result = ucitajIDIzImenaIPrezimena(ucenik.ime, ucenik.prezime);

                if (result != null) return -4;

                string query = @"INSERT INTO ucenici (ime, prezime, lozinka, odeljenje, uzrast, ocene)
VALUES (:ime, :prezime, :lozinka, :odeljenje, :uzrast, :ocene);";

                string ocene = JsonNet.Serialize(ucenik.ocene);

                using (var command = new SQLiteCommand(query, conn))
                {

                    command.Parameters.AddWithValue(":ime", ucenik.ime);
                    command.Parameters.AddWithValue(":prezime", ucenik.prezime);
                    command.Parameters.AddWithValue(":lozinka", ucenik.lozinka);
                    command.Parameters.AddWithValue(":odeljenje", ucenik.odeljenje);
                    command.Parameters.AddWithValue(":uzrast", ucenik.uzrast);
                    command.Parameters.AddWithValue(":ocene", ocene);
                    command.ExecuteNonQuery();

                }

                if (Form1.dbmanagement.debugMode)
                {

                    MessageBox.Show($"id: {conn.LastInsertRowId}; ocene: {ocene}");

                }

                return conn.LastInsertRowId;

            } catch (Exception e)
            {

                Error.show(-2, e.Message);
                return -2;

            }

        }

        public List<long> ucitajIDSvihUcenika()
        {

            List<long> ids = new List<long>();

            try
            {

                string query = "SELECT id FROM ucenici";

                using (var command = new SQLiteCommand(query, conn))
                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {

                        ids.Add(long.Parse(reader["id"].ToString()));

                    }

                }

                return ids;

            } catch (Exception e)
            {

                Error.show(-2, e.Message);
                return ids;

            }

        }

        public object ucitajIDIzImenaIPrezimena(string ime, string prezime, bool checkPassword=false, string lozinka=null)
        {

            string query = "SELECT id FROM ucenici WHERE ime = :ime AND prezime = :prezime";
            if (checkPassword) query += " AND lozinka = :lozinka";

            using (var command = new SQLiteCommand(query, conn))
            {

                command.Parameters.AddWithValue(":ime", ime);
                command.Parameters.AddWithValue(":prezime", prezime);
                if(checkPassword) command.Parameters.AddWithValue(":lozinka", lozinka);

                object result = command.ExecuteScalar();

                return result;

            }

        }

        public Ucenik ucitajUcenika(long id)
        {

            string query = "SELECT * FROM ucenici WHERE id = :id";

            using (var command = new SQLiteCommand(query, conn))
            {

                command.Parameters.AddWithValue(":id", id);

                using (var reader = command.ExecuteReader())
                {

                    if (!reader.Read()) return new Ucenik(null, null, null, null, 0, null);

                    string ime = reader["ime"].ToString();
                    string prezime = reader["prezime"].ToString();
                    string lozinka = reader["lozinka"].ToString();
                    string odeljenje = reader["odeljenje"].ToString();
                    int uzrast = int.Parse(reader["uzrast"].ToString());
                    Dictionary<string, List<int>> ocene =
                        JsonNet.Deserialize<Dictionary<string, List<int>>>(reader["ocene"].ToString());

                    Ucenik ucenik = new Ucenik(ime, prezime, lozinka, odeljenje, uzrast, ocene);
                    return ucenik;

                }

            }

        }

        public void izbrisiUcenika(long id)
        {

            string query = "DELETE FROM ucenici WHERE id = :id";

            using (var command = new SQLiteCommand(query, conn))
            {

                command.Parameters.AddWithValue(":id", id);
                command.ExecuteNonQuery();

            }

        }

        public void promeniUcenika(long id, string ime, string prezime, string lozinka,
            string odeljenje, int? uzrast, Dictionary<string, List<int>> ocene)
        {

            string query = @"UPDATE ucenici SET ";

            List<string> attrs = new List<string>();

            if (!string.IsNullOrEmpty(ime)) attrs.Add("ime = :ime");
            if (!string.IsNullOrEmpty(prezime)) attrs.Add("prezime = :prezime");
            if (!string.IsNullOrEmpty(lozinka)) attrs.Add("lozinka = :lozinka");
            if (!string.IsNullOrEmpty(odeljenje)) attrs.Add("odeljenje = :odeljenje");
            if (uzrast != null && uzrast != -1) attrs.Add("uzrast = :uzrast");
            if (ocene != null) attrs.Add("ocene = :ocene");

            query += string.Join(",", attrs);
            query += " WHERE id = :id";

            using ( var command = new SQLiteCommand(query, conn))
            {

                if (!string.IsNullOrEmpty(ime)) command.Parameters.AddWithValue(":ime", ime);
                if (!string.IsNullOrEmpty(prezime)) command.Parameters.AddWithValue(":prezime", prezime);
                if (!string.IsNullOrEmpty(lozinka)) command.Parameters.AddWithValue(":lozinka", lozinka);
                if (!string.IsNullOrEmpty(odeljenje)) command.Parameters.AddWithValue(":odeljenje", odeljenje);
                if (uzrast != null && uzrast != -1) command.Parameters.AddWithValue(":uzrast", uzrast);
                if (ocene != null) command.Parameters.AddWithValue(":ocene", JsonNet.Serialize(ocene));

                command.Parameters.AddWithValue(":id", id);
                command.ExecuteNonQuery();

            }

        }

    }

}
