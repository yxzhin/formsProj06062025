using Json.Net;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theprj2
{
    public class DBManagement
    {

        public SQLiteConnection conn;

        public void connect()
        {

            string connstr = "Data Source=ucenici.db;Version=3;";
            conn = new SQLiteConnection(connstr);
            conn.Open();

            string query = @"CREATE TABLE IF NOT EXISTS ucenici (
    id INT PRIMARY KEY NOT NULL AUTOINCREMENT,
    ime TEXT NOT NULL,
    prezime TEXT NOT NULL,
    odeljenje TEXT NOT NULL,
    ocene TEXT NOT NULL
);";

            using (var command = new SQLiteCommand(query, conn)) command.ExecuteNonQuery();

        }

        public void close()
        {

            conn.Close();

        }

        public void dodajUcenika(Ucenik ucenik)
        {

            string query = "INSERT INTO ucenini (ime, prezime, odeljenje, ocene) VALUES (:ime, :prezime, :odeljenje, :ocene);";

            string ocene = JsonNet.Serialize(ucenik.ocene);

            using (var command = new SQLiteCommand(query, conn))
            {

                command.Parameters.AddWithValue(":ime", ucenik.ime);
                command.Parameters.AddWithValue(":prezime", ucenik.prezime);
                command.Parameters.AddWithValue(":odeljenje", ucenik.odeljenje);
                command.Parameters.AddWithValue(":ocene", ocene);
                command.ExecuteNonQuery();

            }

        }

    }

}
