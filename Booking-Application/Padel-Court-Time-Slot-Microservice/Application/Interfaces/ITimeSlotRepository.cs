using Padel_Court_Time_Slot_Microservice.Domain.Models;
using Shared_Contracts.Domain.DTOs;

namespace Padel_Court_Time_Slot_Microservice.Application.Interfaces
{
    public interface ITimeSlotRepository
    {
        Task<IEnumerable<TimeSlot>> GetBookedTimeSlotsAsync();
    }
}