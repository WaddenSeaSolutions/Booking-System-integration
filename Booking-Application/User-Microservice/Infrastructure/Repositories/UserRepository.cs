using Dapper;
using MySqlConnector;
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
        var sql = @"INSERT INTO Users (Username, Email, Password) 
                            VALUES (@Username, @Email, @Password);
                            SELECT LAST_INSERT_ID();";

        int userId = _connection.ExecuteScalar<int>(sql, new
        {
            user.Username,
            user.Email,
            user.Password
        });

        user.Id = userId;
        return user;
    }

    public User GetUserByUsername(string username)
    {
        var sql = "SELECT Id, Username, Email, Password FROM Users WHERE Username = @Username";
        return _connection.QueryFirstOrDefault<User>(sql, new { Username = username });
    }
}
