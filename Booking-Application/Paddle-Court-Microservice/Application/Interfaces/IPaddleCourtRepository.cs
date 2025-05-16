using Paddle_Court_Microservice.Domain.Models;

namespace Paddle_Court_Microservice.Application.Interfaces
{
    public interface IPaddleCourtRepository
    {
        Task<IEnumerable<PaddleCourt>> GetAllPaddleCourts();
    }
}
