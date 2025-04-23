using KPO_HW2.Domain.Events;
using KPO_HW2.Domain.Repositories;
using MediatR;

namespace KPO_HW2.Application.EventHandlers
{
    public class AnimalMovedEventHandler : INotificationHandler<AnimalMovedEvent>
    {
        private readonly IEnclosureRepository _enclosureRepository;

        public AnimalMovedEventHandler(IEnclosureRepository enclosureRepository)
        {
            _enclosureRepository = enclosureRepository;
        }

        public async Task Handle(AnimalMovedEvent notification, CancellationToken cancellationToken)
        {
            if (notification.FromEnclosureId != null)
            {
                await _enclosureRepository.RemoveAnimalFromEnclosureAsync(notification.AnimalId, (Guid)notification.FromEnclosureId);

            }
            await _enclosureRepository.AddAnimalToEnclosureAsync(notification.AnimalId, notification.ToEnclosureId);
        }
    }
}
