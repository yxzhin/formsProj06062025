using Json.Net;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace theprj2
{
    public class DBManagement
    {

        public SQLiteConnection conn;

        public void connect()
        {

            try
            {

                string connstr = "Data Source=ucenici.db;Version=3;";
                conn = new SQLiteConnection(connstr);
                conn.Open();

                string query = @"CREATE TABLE IF NOT EXISTS ucenici (
    id INT PRIMARY KEY NOT NULL AUTOINCREMENT,
    ime TEXT NOT NULL,
    prezime TEXT NOT NULL,
    odeljenje TEXT NOT NULL,
    uzrast INT NOT NULL,
    ocene TEXT NOT NULL
);";

                using (var command = new SQLiteCommand(query, conn)) command.ExecuteNonQuery();

            } catch (Exception e)
            {

                Error.show(-2, e.Message);
                return;

            }

        }

        public void close()
        {

            conn.Close();

        }

        public long dodajUcenika(Ucenik ucenik)
        {

            try
            {

                string query = @"INSERT INTO ucenini (ime, prezime, odeljenje, uzrast, ocene)
VALUES (:ime, :prezime, :odeljenje, :uzrast, :ocene);";

                string ocene = JsonNet.Serialize(ucenik.ocene);

                using (var command = new SQLiteCommand(query, conn))
                {

                    command.Parameters.AddWithValue(":ime", ucenik.ime);
                    command.Parameters.AddWithValue(":prezime", ucenik.prezime);
                    command.Parameters.AddWithValue(":odeljenje", ucenik.odeljenje);
                    command.Parameters.AddWithValue(":uzrast", ucenik.uzrast);
                    command.Parameters.AddWithValue(":ocene", ocene);
                    command.ExecuteNonQuery();

                }

                return conn.LastInsertRowId;

            } catch (Exception e)
            {

                Error.show(-2, e.Message);
                return -2;

            }

        }

        public Ucenik ucitajUcenika(long id)
        {

            string query = "SELECT * FROM ucenici WHERE id = :id";

            using (var command = new SQLiteCommand(query, conn))
            using (var reader = command.ExecuteReader())
            {

                if (!reader.Read()) return new Ucenik(null, null, null, 0, new List<Dictionary<string, List<int>>>());

                string ime = reader["ime"].ToString();
                string prezime = reader["prezime"].ToString();
                string odeljenje = reader["odeljenje"].ToString();
                int uzrast = int.Parse(reader["uzrast"].ToString());
                List<Dictionary<string, List<int>>> ocene =
                    JsonNet.Deserialize<List<Dictionary<string, List<int>>>>(reader["ocene"].ToString());

                Ucenik ucenik = new Ucenik(ime, prezime, odeljenje, uzrast, ocene);
                return ucenik;

            }

        }

    }

}
