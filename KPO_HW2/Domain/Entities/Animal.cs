using KPO_HW2.Domain.Enum;
using KPO_HW2.Domain.Events;
using KPO_HW2.Domain.ValueObject;
using MediatR;

namespace KPO_HW2.Domain.Entities
{
    public class Animal : Entity
    {
        public Guid AnimalId { get; init; }
        public Species Species { get; init; }
        public string Name { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Gender Gender { get; private set; }
        public Food FavoriteFood { get; init; }
        public HealthStatus Status { get; private set; }
        public Guid? EnclosureId { get; set; } // Может быть null, если животное не в вольере

        public bool IsHungry {  get;  set; }

        // Методы 
        public void Feed()
        {
            IsHungry = false;
        }

        public void Treat()
        {
            Status = HealthStatus.Healthy;
        }


        public void MoveToEnclosure(Guid enclosureId)
        {
            var oldEnclosureId = EnclosureId;
            EnclosureId = enclosureId;

            var animalMovedEvent = new AnimalMovedEvent(AnimalId, oldEnclosureId, enclosureId);
            AddDomainEvent(animalMovedEvent);

        }
        public Animal(Species species, string name, DateTime birthDate, Gender gender, Food favoriteFood, HealthStatus status, Guid? enclosureId)
        {
            AnimalId = Guid.NewGuid();
            Species = species;
            Name = name;
            BirthDate = birthDate;
            Gender = gender;
            FavoriteFood = favoriteFood;
            Status = status;
            EnclosureId = enclosureId;
            IsHungry = true;
        }

       
    }
}
