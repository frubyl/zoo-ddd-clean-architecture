using KPO_HW2.Domain.Entities;
using KPO_HW2.Domain.Enum;
using KPO_HW2.Domain.ValueObject;
namespace KPO_HW2.Domain.FactoriesInterfaces
{
    public interface IEnclosureFactory
    {
        public Enclosure CreateEnclosure(
            AnimalType type, 
            Size size, 
            int maxCapacity);
    }
}