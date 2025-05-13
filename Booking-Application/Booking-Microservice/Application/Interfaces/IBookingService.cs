using Booking_Microservice.Domain.Models;

namespace Booking_Microservice.Application.Interfaces
{
    public interface IBookingService
    {
        Task<Booking> CreateBooking(Booking booking);
        Task<IEnumerable<Booking[]>> GetAllBookings();
        Task<bool> UpdateBookingAsync(Booking booking);
        Task<bool> DeleteBookingAsync(int id);
    }
}
