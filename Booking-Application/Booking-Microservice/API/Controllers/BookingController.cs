using Booking_Microservice.Application.Interfaces;
using Booking_Microservice.Domain.Models;
using Microsoft.AspNetCore.Mvc;


namespace Booking_Microservice.API.Controllers
{
    [Route("/api/booking")]
    [ApiController]
    public class BookingController
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService) 
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public Task<Booking> CreateBooking(Booking booking)
        {
            return _bookingService.CreateBooking(booking);
        }
        [HttpDelete]
        public Task<bool> DeleteBookingAsync(int id)
        {
            return _bookingService.DeleteBookingAsync(id);
        }
        [HttpGet]
        public Task<IEnumerable<Booking[]>> GetAllBookings()
        {
            return _bookingService.GetAllBookings();
        }

        [HttpGet("/{id}")]
        public async Task<IEnumerable<Booking[]>> GetBookingsByUserId(int userId)
        {
            return await _bookingService.GetBookingsByUserId(userId);
        }
    }
}
