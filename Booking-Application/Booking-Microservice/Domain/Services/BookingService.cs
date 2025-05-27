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
            var createdBooking = await _bookingRepository.CreateBooking(booking);

            if (createdBooking == null)
            {
                // Booking creation failed, do not proceed
                return null;
            }
            var timeSlotResponse = await _timeSlotClient.CreateTimeSlotAsync(
                createdBooking.CourtId,
                createdBooking.StartTime.Date,
                createdBooking.StartTime,
                createdBooking.EndTime
            );
            if (!timeSlotResponse.Success)
            {
                // Roll back booking if timeslot reservation fails
                await _bookingRepository.DeleteBookingAsync(createdBooking.Id);
                return null;
            }

            // Both booking and timeslot reservation succeeded
            return createdBooking;
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
