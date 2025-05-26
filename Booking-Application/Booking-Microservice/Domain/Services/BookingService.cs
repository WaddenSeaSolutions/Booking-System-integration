using Booking_Microservice.Application.Interfaces;
using Booking_Microservice.Domain.Models;

namespace Booking_Microservice.Domain.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public Task<Booking> CreateBooking(Booking booking)
        {
            return _bookingRepository.CreateBooking(booking);
        }

        public Task<bool> DeleteBookingAsync(int id)
        {
            return _bookingRepository.DeleteBookingAsync(id);
        }

        public Task<IEnumerable<Booking>> GetAllBookings()
        {
            return _bookingRepository.GetAllBookings();
        }

        public Task<IEnumerable<Booking>> GetBookingsByUserId(int userId)
        {
            return _bookingRepository.GetBookingsByUserId(userId);
        }
    }
}
