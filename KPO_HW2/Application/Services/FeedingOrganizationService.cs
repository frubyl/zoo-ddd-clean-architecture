using KPO_HW2.Domain.Events;
using KPO_HW2.Domain.Repositories;
using KPO_HW2.Domain.Service;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KPO_HW2.Application.Services
{
    public class FeedingOrganizationService : BackgroundService, IFeedingOrganizationService
    {
        private readonly IMediator _mediator;
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(1);
        private readonly IFeedingScheduleRepository repository;
        public FeedingOrganizationService(
            IMediator mediator,
            IFeedingScheduleRepository repository)
        {
            _mediator = mediator;
            this.repository = repository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessDueFeedingsAsync(stoppingToken); 
                    await Task.Delay(_checkInterval, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    // Сервис останавливается
                    break;
                }
                catch (Exception ex)
                {
                    await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
                }
            }
        }

        public async Task ProcessDueFeedingsAsync(CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var allSchedules = await repository.GetAllFeedingSchedulesAsync(cancellationToken);
            var dueSchedules = allSchedules
                .Where(fs => fs.FeedingTime <= now && !fs.IsCompleted)
                .ToList();

            foreach (var schedule in dueSchedules)
            {
                var @event = new FeedingTimeEvent(schedule.AnimalId);
                await _mediator.Publish(@event, cancellationToken);
                await repository.MarkFeedingAsCompletedAsync(schedule.FeedingScheduleId, cancellationToken);
            }
        }

    }
}
