using Booking_Microservice.Application.Interfaces;
using Booking_Microservice.Domain.Models;
using Dapper;
using MySqlConnector;
using Npgsql;
using System.Data;

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
            if (_connection.State != ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }

            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    var conflictSql = @"
                        SELECT 1
                        FROM bookings
                        WHERE court_id = @CourtId
                        AND (
                        (@StartTime < end_time) AND
                        (@EndTime > start_time)
                        ) LIMIT 1;";

                    var conflict = await _connection.QueryFirstOrDefaultAsync<int?>(
                        conflictSql,
                        new { booking.CourtId, booking.StartTime, booking.EndTime },
                        transaction
                    );

                    if (conflict != null)
                    {
                        transaction.Rollback();
                        throw new InvalidOperationException("A booking already exists for this court.");
                    }

                    var insertSql = @"
                INSERT INTO bookings (
                    user_id,
                    court_id,
                    total_price,
                    start_time,
                    end_time,
                    status,
                    created_at
                )
                VALUES (
                    @UserId,
                    @CourtId,
                    @TotalPrice,
                    @StartTime,
                    @EndTime,
                    @Status,
                    @CreatedAt
                ) RETURNING *;";

                    var createdBooking = await _connection.QuerySingleOrDefaultAsync<Booking>(
                        insertSql,
                        booking,
                        transaction
                    );

                    if (createdBooking == null)
                    {
                        transaction.Rollback();
                        throw new InvalidOperationException("Booking creation failed.");
                    }

                    transaction.Commit();
                    return createdBooking;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }


        public async Task<bool> DeleteBookingAsync(int id)
        {
            var sql = "DELETE FROM bookings WHERE id = @Id";

            try
            {
                var rowsAffected = await _connection.ExecuteAsync(sql, new { Id = id });
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Booking>> GetAllBookings()
        {
            var sql = @"
                SELECT
                    id,
                    user_id AS UserId,
                    court_id AS CourtId,
                    total_price AS TotalPrice,
                    start_time AS StartTime,
                    end_time AS EndTime,
                    created_at AS CreatedAt,
                    status
                FROM bookings;";

            try
            {
                var bookings = await _connection.QueryAsync<Booking>(sql);
                return bookings;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUserId(int userId)
        {
            var sql = @"
                SELECT
                    id,
                    user_id AS UserId,
                    court_id AS CourtId,
                    total_price AS TotalPrice,
                    start_time AS StartTime,
                    end_time AS EndTime,
                    created_at AS CreatedAt,
                    status
                FROM bookings
                WHERE user_id = @UserId;";

            try
            {
                var bookings = await _connection.QueryAsync<Booking>(sql, new { UserId = userId });
                return bookings;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
