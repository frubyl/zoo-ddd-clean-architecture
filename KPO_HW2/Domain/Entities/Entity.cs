
using MediatR;

namespace KPO_HW2.Domain.Entities
{
    public abstract class Entity
    {
        protected List<INotification> _domainEvents = new List<INotification>();

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents.Remove(eventItem);
        }

        public void CLearDomainEvent()
        {
            _domainEvents.Clear();
        }

    }
}
