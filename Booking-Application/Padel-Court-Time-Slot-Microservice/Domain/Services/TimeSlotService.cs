using Padel_Court_Time_Slot_Microservice.Application.Interfaces;
using Padel_Court_Time_Slot_Microservice.Domain.DTO;
using Padel_Court_Time_Slot_Microservice.Domain.Models;

namespace Padel_Court_Time_Slot_Microservice.Application.Services
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly ITimeSlotRepository _timeSlotRepository;

        public TimeSlotService(ITimeSlotRepository timeSlotRepository)
        {
            _timeSlotRepository = timeSlotRepository;
        }

        public async Task<IEnumerable<AvailableTimeSlotDto>> GetAvailableTimeSlotsAsync()
        {
            var timeSlots = await _timeSlotRepository.GetAvailableTimeSlotsAsync();

            return timeSlots.Select(slot => new AvailableTimeSlotDto
            {
                Id = slot.Id,
                StartTime = slot.StartTime,
                EndTime = slot.EndTime,
                IsAvailable = slot.IsAvailable
            });
        }
    }
}