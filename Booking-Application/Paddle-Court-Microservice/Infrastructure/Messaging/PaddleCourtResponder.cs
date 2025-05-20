using EasyNetQ;
using Paddle_Court_Microservice.Application.Interfaces;
using Paddle_Court_Microservice.Domain.DTOs;
using Paddle_Court_Microservice.Domain.Requests;
using Paddle_Court_Microservice.Domain.Responses;

namespace Paddle_Court_Microservice.Infrastructure.Messaging
{
    public class PaddleCourtResponder : BackgroundService
    {
        private readonly IBus _bus;
        private readonly IPaddleCourtRepository _repository;

        public PaddleCourtResponder(IBus bus, IPaddleCourtRepository repository)
        {
            _bus = bus;
            _repository = repository;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus.Rpc.RespondAsync<GetPaddleCourtsRequest, GetPaddleCourtsResponse>(async request =>
            {
                var courts = await _repository.GetAllPaddleCourts();

                return new GetPaddleCourtsResponse
                {
                    RequestId = request.RequestId,
                    Courts = courts.Select(c => new PaddleCourtDto
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).ToList()
                };
            });

            return Task.CompletedTask;
        }
    }
}
