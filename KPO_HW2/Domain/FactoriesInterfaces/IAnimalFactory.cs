
using KPO_HW2.Domain.Entities;
using KPO_HW2.Domain.Enum;
using KPO_HW2.Domain.ValueObject;
namespace KPO_HW2.Domain.Factories
{
    public interface IAnimalFactory
    {
        public Animal CreateAnimal(
            Species species,
            string name,
            DateTime birthDate,
            Gender gender,
            Food favoriteFood,
            HealthStatus status,
            Guid? enclosureId = null);
    }
}