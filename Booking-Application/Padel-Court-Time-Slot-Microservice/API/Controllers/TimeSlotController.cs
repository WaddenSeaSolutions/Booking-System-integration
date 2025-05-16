using Microsoft.AspNetCore.Mvc;
using Padel_Court_Time_Slot_Microservice.Application.Interfaces;
using Padel_Court_Time_Slot_Microservice.Domain.DTO;

namespace Padel_Court_Time_Slot_Microservice.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimeSlotController : ControllerBase
{
    private readonly ITimeSlotService _timeSlotService;

    public TimeSlotController(ITimeSlotService timeSlotService)
    {
        _timeSlotService = timeSlotService;
    }

    [HttpGet("available")]
    public async Task<ActionResult<IEnumerable<AvailableTimeSlotDto>>> GetAvailableTimeSlots()
    {
        var slots = await _timeSlotService.GetAvailableTimeSlotsAsync();
        return Ok(slots);
    }
}