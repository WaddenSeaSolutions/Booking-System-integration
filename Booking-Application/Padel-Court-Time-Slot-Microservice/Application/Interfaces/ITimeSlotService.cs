using Padel_Court_Time_Slot_Microservice.Domain.DTO;

namespace Padel_Court_Time_Slot_Microservice.Application.Interfaces
{
    public interface ITimeSlotService
    {
        Task<IEnumerable<AvailableTimeSlotDto>> GetAvailableTimeSlotsAsync();
    }
}