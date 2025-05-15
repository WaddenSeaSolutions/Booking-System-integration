using Booking_Microservice.Domain.Models;

namespace Booking_Microservice.Application.Interfaces
{
    public interface IBookingService
    {
        Task<Booking> CreateBooking(Booking booking);
        Task<IEnumerable<Booking[]>> GetAllBookings();
        Task<bool> DeleteBookingAsync(int id);
        Task<IEnumerable<Booking[]>> GetBookingsByUserId(int userId);
    }
}
