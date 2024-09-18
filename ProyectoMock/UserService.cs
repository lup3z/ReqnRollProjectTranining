using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoMock
{
    public class UserService
    {
        private readonly IDbConnection _dbConnection;

        public UserService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void Create(User user)
        {
            var sql = "INSERT INTO Customers (Id, Name, Email) VALUES (@Id, @Name, @Email)";
            _dbConnection.Execute(sql, new { user.Id, user.Name, user.Email });
        }

        public IEnumerable<User> GetCustomers()
        {
            var sql = "SELECT Id, Name, Email FROM Customers";
            return _dbConnection.Query<User>(sql).ToList();
        }


        // Método para ejecutar comandos SQL como CREATE TABLE
        public void ExecuteNonQuery(string sql)
        {
            _dbConnection.Execute(sql);
        }

    }
}
