
using KPO_HW2.Domain.Enum;

namespace KPO_HW2.Domain.ValueObject
{
    public record Species
    {
        public AnimalType Type { get; init; }
        public string Name { get; init; }

        public Species(AnimalType type, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Название вида не может быть пустым");
            }
            Type = type;
            Name = name;
        }
    }
}
