using MediatR;

namespace KPO_HW2.Domain.Events
{
    public class FeedingTimeEvent : INotification
    {
        public Guid AnimalId { get; init; }
        public FeedingTimeEvent(Guid animalId)
        {
            AnimalId = animalId;
        }
    }
}
