using Booking_Microservice.Domain.Models;

namespace Booking_Microservice.API.Controllers
{
    public class BookingController
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
