using Azure.Core;
using Booking_Microservice.Application.Interfaces;
using Booking_Microservice.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
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
        public async Task<IActionResult> CreateBooking([FromBody] Booking booking)
        {
            if (TryGetUserIdFromRequest(out int userId, out IActionResult errorResult) == false)
            {
                return errorResult;
            }

            booking.UserId = userId;

            try
            {
                Booking createdBooking = await _bookingService.CreateBooking(booking);
                return Ok(createdBooking); // Eller CreatedAtAction hvis du vil have den tilbage
            }
            catch (PostgresException ex)
            {
                if (ex.SqlState == "23505" || ex.SqlState == "23P01")
                {
                    return Conflict(new { message = "The requested time slot is already booked for this court." });
                }
                else
                {
                    return StatusCode(500, new { message = "A database error occurred. Please try again later." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected server error occurred. Please try again later." });
            }
        }

        [HttpDelete]
        public Task<bool> DeleteBookingAsync(int id)
        {
            return _bookingService.DeleteBookingAsync(id);
        }
        [HttpGet]
        public async Task<IEnumerable<Booking>> GetAllBookings()
        {
            return await _bookingService.GetAllBookings();
        }

        [HttpGet("/{id}")]
        public async Task<IEnumerable<Booking>> GetBookingsByUserId(int userId)
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


        // Helper method to extract and validate UserId from the request headers
        private bool TryGetUserIdFromRequest(out int userId, out IActionResult errorResult)
        {
            userId = default;
            errorResult = null;

            if (!Request.Headers.TryGetValue("UserId", out var userIdHeaderValues))
            {
                errorResult = Unauthorized("User ID header is missing. Please ensure you are authenticated.");
                return false;
            }

            var userIdString = userIdHeaderValues.FirstOrDefault();

            if (string.IsNullOrEmpty(userIdString))
            {
                errorResult = Unauthorized("User ID header is empty or invalid.");
                return false;
            }

            try
            {
                userId = int.Parse(userIdString);
                return true;
            }
            catch (FormatException)
            {
                errorResult = BadRequest("Invalid User ID format in header. User ID must be a valid integer.");
                return false;
            }
            catch (OverflowException)
            {
                errorResult = BadRequest("Invalid User ID format in header. User ID value is too large or too small.");
                return false;
            }
        }
    }
}
