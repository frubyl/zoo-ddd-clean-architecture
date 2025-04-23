
using MediatR;

namespace KPO_HW2.Domain.Events
{
    public class AnimalMovedEvent : INotification
    {
        public Guid AnimalId { get; init; }
        public Guid? FromEnclosureId { get; init; }
        public Guid ToEnclosureId { get; init; }
        public AnimalMovedEvent(Guid animalId, Guid? fromEnclosureId, Guid toEnclosureId)
        {
            AnimalId = animalId;
            FromEnclosureId = fromEnclosureId;
            ToEnclosureId = toEnclosureId;
        }
    }
}
