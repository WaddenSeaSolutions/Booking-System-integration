using Booking_Microservice.Domain.Responses;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Paddle_Court_Microservice.Application.Interfaces;
using Shared_Contracts.Domain.DTOs;
using Shared_Contracts.Domain.Requests;


namespace Paddle_Court_Microservice.Infrastructure.Messaging
{
    public class PaddleCourtResponder : BackgroundService
    {
        private readonly IBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public PaddleCourtResponder(IBus bus, IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            { 
                _bus.Rpc.RespondAsync<GetPaddleCourtsRequest, GetPaddleCourtsResponse>(async request =>
                {
                    Console.WriteLine("📨 Received request for paddle courts");
                    using var scope = _serviceProvider.CreateScope();
                    var repository = scope.ServiceProvider.GetRequiredService<IPaddleCourtRepository>();

                    var courts = await repository.GetAllPaddleCourts();

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
                Console.WriteLine(typeof(GetPaddleCourtsRequest).AssemblyQualifiedName);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error registering RespondAsync: " + ex);
                return Task.CompletedTask;
            }
        }
    }
}
