using Booking_Microservice.Application.Interfaces;
using Booking_Microservice.Domain.Models;

namespace Booking_Microservice.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        public Task<Booking> CreateBooking(Booking booking)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBookingAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Booking[]>> GetAllBookings()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBookingAsync(Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}
