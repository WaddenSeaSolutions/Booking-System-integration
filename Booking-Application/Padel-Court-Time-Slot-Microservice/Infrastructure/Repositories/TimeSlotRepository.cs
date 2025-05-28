using MongoDB.Driver;
using Padel_Court_Time_Slot_Microservice.Application.Interfaces;
using Padel_Court_Time_Slot_Microservice.Domain.Models;
using Shared_Contracts.Domain.DTOs;

namespace Padel_Court_Time_Slot_Microservice.Infrastructure.Repositories
{
    public class TimeSlotRepository : ITimeSlotRepository
    {
        private readonly IMongoCollection<TimeSlot> _timeSlots;

        public TimeSlotRepository(IMongoDatabase database)
        {
            _timeSlots = database.GetCollection<TimeSlot>("paddletimedb");
        }

        public async Task<IEnumerable<TimeSlot>> GetAllTimeSlotsAsync()
        {
            return await _timeSlots.Find(_ => true).ToListAsync();
        }

        public async Task AddTimeSlotAsync(TimeSlot timeSlot)
        {
            await _timeSlots.InsertOneAsync(timeSlot);

        }
    }
}