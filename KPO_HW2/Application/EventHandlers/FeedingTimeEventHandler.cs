using KPO_HW2.Domain.Entities;
using KPO_HW2.Domain.Events;
using KPO_HW2.Domain.Repositories;
using MediatR;

namespace KPO_HW2.Application.EventHandlers
{
    public class FeedingTimeEventHandler : INotificationHandler<FeedingTimeEvent>
    {
        private readonly IAnimalRepository _animalRepository;

        public FeedingTimeEventHandler(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task Handle(FeedingTimeEvent notification, CancellationToken cancellationToken)
        {
            await _animalRepository.ChangeIsHungryAsync(notification.AnimalId, false);
        }
    }
}
