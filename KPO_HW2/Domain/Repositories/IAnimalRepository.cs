using KPO_HW2.Domain.Entities;

namespace KPO_HW2.Domain.Repositories
{
    public interface IAnimalRepository
    {
        public Task AddAnimalAsync(Animal animal, CancellationToken cancellationToken = default);

        public Task DeleteAnimalByIdAsync(Guid animalId, CancellationToken cancellationToken = default);

        public Task ChangeIsHungryAsync(Guid animalId, bool isHungry, CancellationToken cancellationToken = default);

        public Task<int> GetAnimalsCountAsync(CancellationToken cancellationToken = default);

        public Task<Animal> GetAnimalByIdAsync(Guid animalId, CancellationToken cancellationToken = default);

        public Task UpdateAnimalEnclosureAsync(Animal animal, CancellationToken cancellationToken = default);
    }
}
