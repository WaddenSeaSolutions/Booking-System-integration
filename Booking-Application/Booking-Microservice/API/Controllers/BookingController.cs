using Azure.Core;
using Booking_Microservice.Application.Interfaces;
using Booking_Microservice.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Paddle_Court_Microservice.Infrastructure.Messaging;
using System.IdentityModel.Tokens.Jwt;


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
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return Unauthorized("Missing or invalid Authorization header");

            var token = authHeader.Substring("Bearer ".Length).Trim();

            var handler = new JwtSecurityTokenHandler();

            JwtSecurityToken jwtToken;
            jwtToken = handler.ReadJwtToken(token);


            var subClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            if (subClaim == null)
                return BadRequest("sub claim not found");

            booking.UserId = int.Parse(subClaim);

            try
            {
                Booking createdBooking = await _bookingService.CreateBooking(booking);
                return Ok(createdBooking);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An unexpected server error occurred. Please try again later.",
                    error = ex.Message,
                    stackTrace = ex.StackTrace
                });
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
            return new OkObjectResult(paddleCourts);
        }
    }
}
