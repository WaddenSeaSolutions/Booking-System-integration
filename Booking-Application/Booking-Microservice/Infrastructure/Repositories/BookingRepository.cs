using Booking_Microservice.Application.Interfaces;
using Booking_Microservice.Domain.Models;
using Dapper;
using MySqlConnector;

namespace Booking_Microservice.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly MySqlConnection _connection;
        public BookingRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<Booking> CreateBooking(Booking booking)
        {
            var sql = @"INSERT INTO Booking (UserId, StartTime, EndTime, Status)
                VALUES (@UserId, @StartTime, @StartTime, @EndTime
                returning";

            var createdBooking = await _connection.ExecuteScalarAsync<Booking>(sql, booking);

            return createdBooking;
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            var sql = @"DELETE FROM Booking WHERE Id = @Id";
            var result = await _connection.ExecuteAsync(sql, id);

            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<Booking[]>> GetAllBookings()
        {
            var sql = "SELECT * FROM Booking";

            var bookings = await _connection.QueryAsync<Booking[]>(sql);

            return bookings;
        }

        public async Task<IEnumerable<Booking[]>> GetBookingsByUserId(int userId)
        {
            var sql = "SELECT * FROM Booking WHERE UserId = @UserId";
            var bookings = await _connection.QueryAsync<Booking[]>(sql, new { UserId = userId });
            return bookings;
        }


    }
}
