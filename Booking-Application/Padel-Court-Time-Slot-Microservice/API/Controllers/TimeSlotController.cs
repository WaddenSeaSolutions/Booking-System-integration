using Microsoft.AspNetCore.Mvc;
using Padel_Court_Time_Slot_Microservice.Application.Interfaces;
using Padel_Court_Time_Slot_Microservice.Domain.DTO;
using Padel_Court_Time_Slot_Microservice.Domain.Models;
using Shared_Contracts.Domain.DTOs;

namespace Padel_Court_Time_Slot_Microservice.API.Controllers;

[ApiController]
[Route("api/TimeSlotController")]
public class TimeSlotController : ControllerBase
{
    private readonly ITimeSlotService _timeSlotService;

    public TimeSlotController(ITimeSlotService timeSlotService)
    {
        _timeSlotService = timeSlotService;
    }

    [HttpGet()]
    public async Task<ActionResult<IEnumerable<TimeSlot>>> GetBookedTimeSlots()
    {
        var slots = await _timeSlotService.GetBookedTimeSlotsAsync();
        return Ok(slots);
    }
}