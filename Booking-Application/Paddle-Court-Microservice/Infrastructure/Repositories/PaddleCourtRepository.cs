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
            _paddleCourts = database.GetCollection<PaddleCourt>("PaddleCourt");
        }

        public async Task<IEnumerable<PaddleCourt>> GetAllPaddleCourts()
        {
            return await _paddleCourts.Find(_ => true).ToListAsync();
        }
    }
}
