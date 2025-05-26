using MongoDB.Driver;
using Padel_Court_Time_Slot_Microservice.Application.Interfaces;
using Padel_Court_Time_Slot_Microservice.Domain.DTO;
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

        public Task<IEnumerable<TimeSlot>> GetBookedTimeSlotsAsync()
        {

            var timeSlots = await _timeSlotRepository.GetOccupiedTimeSlots(courtId, start, end);

            // Mapping fra TimeSlot (domain model) til TimeSlotDto
            return timeSlots.Select(ts => new TimeSlotDto
            {
                Id = ts.Id,
                CourtId = ts.CourtId,
                Date = ts.Date,
                StartTime = ts.StartTime,
                EndTime = ts.EndTime,
                Status = ts.Status
                // Tilføj andre properties her, som er defineret i din TimeSlotDto
                // f.eks., BookingId = ts.BookingId, UserId = ts.UserId
            }).ToList(); // .ToList() for at eksekvere forespørgslen og returnere en konkret liste
        }
    }
}