using Azure.Core;
using Booking_Microservice.Application.Interfaces;
using Booking_Microservice.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Paddle_Court_Microservice.Infrastructure.Messaging;


namespace Booking_Microservice.API.Controllers
{
    [Route("/api/booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly PaddleCourtClient _paddleCourtClient;
        public BookingController(IBookingService bookingService, PaddleCourtClient paddleCourtClient)
        {
            _bookingService = bookingService;
            _paddleCourtClient = paddleCourtClient;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(Booking booking)
        {

            if (!Request.Headers.TryGetValue("UserId", out var userIdHeaderValues))
            {
                return Unauthorized("User ID header is missing. Please ensure you are authenticated.");
            }

            // Gets the first value from the UserId header
            var userIdString = userIdHeaderValues.FirstOrDefault();

            // Check if the UserId header is present and not empty
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("User ID header is empty.");
            }
            booking.UserId = int.Parse(userIdString);

            return Ok(_bookingService.CreateBooking(booking));
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

        [HttpGet("GetPaddleCourts")]
        public async Task<IActionResult> GetPaddleCourts()
        {
            var paddleCourts = await _paddleCourtClient.GetPaddleCourtsAsync();
            Console.WriteLine("Paddle courts retrieved successfully.");
            return new OkObjectResult(paddleCourts);
        }
    }
}
