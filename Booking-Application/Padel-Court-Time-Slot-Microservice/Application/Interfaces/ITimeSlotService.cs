using Padel_Court_Time_Slot_Microservice.Domain.DTO;
using Padel_Court_Time_Slot_Microservice.Domain.Models;
using Shared_Contracts.Domain.DTOs;

namespace Padel_Court_Time_Slot_Microservice.Application.Interfaces
{
    public interface ITimeSlotService
    {
        Task<IEnumerable<TimeSlot>> GetBookedTimeSlotsAsync();
    }
}