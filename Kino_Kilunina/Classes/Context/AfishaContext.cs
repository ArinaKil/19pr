using Kino_Kilunina.Classes.Model;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Kino_Kilunina.Classes.Context
{
    public class AfishaContext
    {
        public List<Afisha> GetAll()
        {
            var list = new List<Afisha>();
            var connection = Connection.OpenConnection();
            var reader = Connection.Query("SELECT Id, IdKinoteatr, Name, Time, Price FROM Afisha", connection);
            while (reader.Read())
            {
                list.Add(new Afisha(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetString(2),
                    reader.GetDateTime(3),
                    reader.GetInt32(4)
                ));
            }
            reader.Close();
            Connection.CloseConnection(connection);
            return list;
        }

        public Afisha GetById(int id)
        {
            Afisha afisha = null;
            var connection = Connection.OpenConnection();
            var reader = Connection.Query($"SELECT Id, IdKinoteatr, Name, Time, Price FROM Afisha WHERE Id = {id}", connection);
            if (reader.Read())
            {
                afisha = new Afisha(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetString(2),
                    reader.GetDateTime(3),
                    reader.GetInt32(4)
                );
            }
            reader.Close();
            Connection.CloseConnection(connection);
            return afisha;
        }

        public void Add(Afisha afisha)
        {
            var connection = Connection.OpenConnection();
            string sql = $"INSERT INTO Afisha (IdKinoteatr, Name, Time, Price) VALUES ({afisha.IdKinoteatr}, '{afisha.Name}', '{afisha.Time:yyyy-MM-dd HH:mm:ss}', {afisha.Price})";
            new MySqlCommand(sql, connection).ExecuteNonQuery();
            Connection.CloseConnection(connection);
        }

        public void Update(Afisha afisha)
        {
            var connection = Connection.OpenConnection();
            string sql = $"UPDATE Afisha SET IdKinoteatr = {afisha.IdKinoteatr}, Name = '{afisha.Name}', Time = '{afisha.Time:yyyy-MM-dd HH:mm:ss}', Price = {afisha.Price} WHERE Id = {afisha.Id}";
            new MySqlCommand(sql, connection).ExecuteNonQuery();
            Connection.CloseConnection(connection);
        }

        public bool HasByKinoteatr(int idKinoteatr)
        {
            var connection = Connection.OpenConnection();
            var reader = Connection.Query($"SELECT COUNT(*) FROM Afisha WHERE IdKinoteatr = {idKinoteatr}", connection);
            reader.Read();
            int count = reader.GetInt32(0);
            reader.Close();
            Connection.CloseConnection(connection);
            return count > 0;
        }

        public void Delete(int id)
        {
            var connection = Connection.OpenConnection();
            new MySqlCommand($"DELETE FROM Afisha WHERE Id = {id}", connection).ExecuteNonQuery();
            Connection.CloseConnection(connection);
        }
    }
}
