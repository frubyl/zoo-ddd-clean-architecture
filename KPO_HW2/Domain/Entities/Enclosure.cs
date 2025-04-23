using KPO_HW2.Domain.Enum;
using KPO_HW2.Domain.ValueObject;

namespace KPO_HW2.Domain.Entities
{
    public class Enclosure : Entity
    {
        public Guid EnclosureId { get; init; }
        public AnimalType Type { get; private set; }
        public Size Size { get; init; }
        public int CurrentAnimalCount => _animalIds.Count;
        public int MaxCapacity { get; private set; }
        
        public bool IsClear { get; private set; }

        private List<Guid> _animalIds = new List<Guid>();

        public Enclosure(AnimalType type, Size size, int maxCapacity)
        {
            EnclosureId = Guid.NewGuid();
            Type = type;
            Size = size;
            MaxCapacity = maxCapacity;
            IsClear = false;
        }

        public void AddAnimal(Guid animalId)
        {
            _animalIds.Add(animalId);

        }

        public void RemoveAnimal(Guid animalId)
        {
            _animalIds.Remove(animalId);
        }

        public void Clear() { 
            IsClear = true;
        }
    }
}
