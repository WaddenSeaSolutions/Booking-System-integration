using Paddle_Court_Microservice.Application.Interfaces;
using Paddle_Court_Microservice.Domain.Models;

namespace Paddle_Court_Microservice.Domain.Services
{
    public class PaddleCourtService : IPaddleCourtService
    {
        private readonly IPaddleCourtRepository _paddleCourtRepository;
        public PaddleCourtService(IPaddleCourtRepository paddleCourtRepository)
        {
            _paddleCourtRepository = paddleCourtRepository;
        }
        public Task<IEnumerable<PaddleCourt>> GetAllPaddleCourts()
        {
            return _paddleCourtRepository.GetAllPaddleCourts();
        }
    }
}
