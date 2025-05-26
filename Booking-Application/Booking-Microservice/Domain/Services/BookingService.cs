using Booking_Microservice.Application.Interfaces;
using Booking_Microservice.Domain.Models;
using Booking_Microservice.Infrastructure.Messaging;

namespace Booking_Microservice.Domain.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        
        private readonly TimeSlotClient _timeSlotClient;

        public BookingService(IBookingRepository bookingRepository, TimeSlotClient timeSlotClient)
        {
            _bookingRepository = bookingRepository;
            _timeSlotClient = timeSlotClient;
        }
        public async Task<Booking> CreateBooking(Booking booking)
        {
            var timeSlotResponse = await _timeSlotClient.CreateTimeSlotAsync(
                int.Parse(booking.CourtId),
                booking.StartTime.Date,
                booking.StartTime,
                booking.EndTime
            );

            if (!timeSlotResponse.Success)
            {
                // Optionally, you can log or return null or throw an exception
                // depending on your error handling strategy
                return null;
            }

            // Only create the booking if the time slot was successfully created
            return await _bookingRepository.CreateBooking(booking);
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
