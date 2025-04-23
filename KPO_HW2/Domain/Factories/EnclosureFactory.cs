using KPO_HW2.Domain.Entities;
using KPO_HW2.Domain.Enum;
using KPO_HW2.Domain.FactoriesInterfaces;
using KPO_HW2.Domain.ValueObject;
namespace KPO_HW2.Domain.Factories
{

    public class EnclosureFactory : IEnclosureFactory
    {
        public Enclosure CreateEnclosure(
               AnimalType type,
                Size size,
                int maxCapacity)
        {
            if (maxCapacity <= 0)
            {
                throw new ArgumentException("Вместимость должна быть положительным числом.", nameof(maxCapacity));
            }
            return new Enclosure(type, size, maxCapacity);
        }
    }
}