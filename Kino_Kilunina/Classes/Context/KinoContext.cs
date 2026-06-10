using Kino_Kilunina.Classes.Model;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Kino_Kilunina.Classes.Context
{
    public class KinoContext
    {
        public List<Kino> GetAll()
        {
            var list = new List<Kino>();
            var connection = Connection.OpenConnection();
            var reader = Connection.Query("SELECT Id, Name, CountZal, Count FROM Kino", connection);
            while (reader.Read())
            {
                list.Add(new Kino(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetInt32(2),
                    reader.GetInt32(3)
                ));
            }
            reader.Close();
            Connection.CloseConnection(connection);
            return list;
        }

        public Kino GetById(int id)
        {
            Kino kino = null;
            var connection = Connection.OpenConnection();
            var reader = Connection.Query($"SELECT Id, Name, CountZal, Count FROM Kino WHERE Id = {id}", connection);
            if (reader.Read())
            {
                kino = new Kino(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetInt32(2),
                    reader.GetInt32(3)
                );
            }
            reader.Close();
            Connection.CloseConnection(connection);
            return kino;
        }

        public void Add(Kino kino)
        {
            var connection = Connection.OpenConnection();
            string sql = $"INSERT INTO Kino (Name, CountZal, Count) VALUES ('{kino.Name}', {kino.CountZal}, {kino.Count})";
            new MySqlCommand(sql, connection).ExecuteNonQuery();
            Connection.CloseConnection(connection);
        }

        public void Update(Kino kino)
        {
            var connection = Connection.OpenConnection();
            string sql = $"UPDATE Kino SET Name = '{kino.Name}', CountZal = {kino.CountZal}, Count = {kino.Count} WHERE Id = {kino.Id}";
            new MySqlCommand(sql, connection).ExecuteNonQuery();
            Connection.CloseConnection(connection);
        }

        public void Delete(int id)
        {
            var connection = Connection.OpenConnection();
            new MySqlCommand($"DELETE FROM Kino WHERE Id = {id}", connection).ExecuteNonQuery();
            Connection.CloseConnection(connection);
        }
    }
}
