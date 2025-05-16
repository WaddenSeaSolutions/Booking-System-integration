using Microsoft.AspNetCore.Mvc;
using Paddle_Court_Microservice.Application.Interfaces;
using Paddle_Court_Microservice.Domain.Models;

namespace Paddle_Court_Microservice.API.Controllers
{
    [Route("api/PaddleCourt")]
    [ApiController]
    public class PaddleCourtController
    {
        private readonly IPaddleCourtService _paddleCourtService;
        public PaddleCourtController(IPaddleCourtService paddleCourtService)
        {
            _paddleCourtService = paddleCourtService;
        }
        [HttpGet]
        public Task<IEnumerable<PaddleCourt>> GetAllPaddleCourts()
        {
            return _paddleCourtService.GetAllPaddleCourts();
        }
    }
}
