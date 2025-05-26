using MongoDB.Driver;
using Padel_Court_Time_Slot_Microservice.Application.Interfaces;
using Padel_Court_Time_Slot_Microservice.Domain.Models;
using Shared_Contracts.Domain.DTOs;

namespace Padel_Court_Time_Slot_Microservice.Infrastructure.Repositories
{
    public class TimeSlotRepository : ITimeSlotRepository
    {
        private readonly IMongoCollection<TimeSlot> _timeSlots;

        public TimeSlotRepository(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MongoDb"));
            var database = client.GetDatabase("PadelCourttimeslotDb");
            _timeSlots = database.GetCollection<TimeSlot>("TimeSlots");
        }

        public async Task<IEnumerable<TimeSlot>> GetAvailableTimeSlotsAsync()
        {
            var filter = Builders<TimeSlot>.Filter.Eq(t => t.IsAvailable, true);
            return await _timeSlots.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<TimeSlot>> GetBookedTimeSlotsAsync()
        {
            var filter = Builders<TimeSlot>.Filter.Eq(t => t.IsAvailable, false);
            return await _timeSlots.Find(filter).ToListAsync();
        }
    }
}