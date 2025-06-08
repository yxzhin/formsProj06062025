using Json.Net;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
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
    id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
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

        public long dodajUcenika(Ucenik ucenik)
        {

            try
            {

                string query = "SELECT id FROM ucenici WHERE ime = :ime AND prezime = :prezime";

                using (var command = new SQLiteCommand(query, conn))
                {

                    command.Parameters.AddWithValue(":ime", ucenik.ime);
                    command.Parameters.AddWithValue(":prezime", ucenik.prezime);

                    object result = command.ExecuteScalar();

                    if (result != null) return -4;

                }

                query = @"INSERT INTO ucenici (ime, prezime, odeljenje, uzrast, ocene)
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

        public Ucenik ucitajUcenika(long id)
        {

            string query = "SELECT * FROM ucenici WHERE id = :id";

            using (var command = new SQLiteCommand(query, conn))
            {

                command.Parameters.AddWithValue(":id", id);

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

}
