using Padel_Court_Time_Slot_Microservice.Domain.Models;

namespace Padel_Court_Time_Slot_Microservice.Application.Interfaces
{
    public interface ITimeSlotRepository
    {
        Task<IEnumerable<TimeSlot>> GetAvailableTimeSlotsAsync();
    }
}