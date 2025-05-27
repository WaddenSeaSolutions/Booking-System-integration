using Dapper;
using MySqlConnector;
using System.Data;
using User_Microservice.Domain.Models;

namespace User_Microservice.Infrastructure.Repositories;

public class UserRepository
{
    private readonly MySqlConnection _connection;

    public UserRepository(MySqlConnection connection)
    {
        _connection = connection;
    }

    public User RegisterUser(User user)
    {
        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }
        using (var transaction = _connection.BeginTransaction())
        {
            var existingUser = _connection.QueryFirstOrDefault<User>(
                @"SELECT * FROM Users WHERE Username = @Username OR Email = @Email;",
                new { user.Username, user.Email },
                transaction: transaction
            );

            if (existingUser != null)
            {
                transaction.Rollback();
                throw new InvalidOperationException("User already exists");
            }

            var sql = @"INSERT INTO Users (Username, Email, Password) 
                    VALUES (@Username, @Email, @Password);
                    SELECT LAST_INSERT_ID();";

            int userId = _connection.ExecuteScalar<int>(sql, new
            {
                user.Username,
                user.Email,
                user.Password
            }, transaction: transaction);

            user.Id = userId;

            transaction.Commit();

            return user;
        }
    }

    public User GetUserByUsername(string username)
    {
        var sql = "SELECT Id, Username, Email, Password FROM Users WHERE Username = @Username";
        return _connection.QueryFirstOrDefault<User>(sql, new { Username = username });
    }
}
