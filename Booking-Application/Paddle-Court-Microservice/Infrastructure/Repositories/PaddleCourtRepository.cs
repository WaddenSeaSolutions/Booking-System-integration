using Paddle_Court_Microservice.Application.Interfaces;
using Paddle_Court_Microservice.Domain.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Paddle_Court_Microservice.Infrastructure.Repositories
{
    public class PaddleCourtRepository : IPaddleCourtRepository
    {
        private readonly IMongoCollection<PaddleCourt> _paddleCourts;

        public PaddleCourtRepository(IMongoDatabase database)
        {
            _paddleCourts = database.GetCollection<PaddleCourt>("paddlecourt");
        }

        public async Task<IEnumerable<PaddleCourt>> GetAllPaddleCourts()
        {
            var paddlecourts = await _paddleCourts.Find(_ => true).ToListAsync();
            Console.WriteLine("Paddle courts:");
            
            return paddlecourts;
        }
    }
}
