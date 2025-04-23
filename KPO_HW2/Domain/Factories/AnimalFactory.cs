using KPO_HW2.Domain.Entities;
using KPO_HW2.Domain.Enum;
using KPO_HW2.Domain.FactoriesInterfaces;
using KPO_HW2.Domain.ValueObject;
namespace KPO_HW2.Domain.Factories {

    public class AnimalFactory : IAnimalFactory
    {
        public Animal CreateAnimal(
            Species species,
            string name,
            DateTime birthDate,
            Gender gender,
            Food favoriteFood,
            HealthStatus status,
            Guid? enclosureId = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Имя животного не может быть пустым или содержать только пробелы.", nameof(name));
            }
            if (birthDate > DateTime.UtcNow)
            {
                throw new ArgumentException("Дата рождения не может быть в будущем.", nameof(birthDate));
            }
            return new Animal(
                              species,
                              name,
                              birthDate,
                              gender,
                              favoriteFood,
                              status,
                              enclosureId
                          );
        }
    } 
}