using KPO_HW2.Domain.Enum;

namespace KPO_HW2.Domain.ValueObject
{
    public record Food
    {
        public FoodType Type { get; }
        public string Name { get; }

        public Food(FoodType type, string name)
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
