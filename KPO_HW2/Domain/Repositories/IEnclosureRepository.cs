
using KPO_HW2.Domain.Entities;

namespace KPO_HW2.Domain.Repositories
{
    public interface IEnclosureRepository
    {
        public Task AddEnclosureAsync(Enclosure enclosure, CancellationToken cancellationToken = default);

        public Task DeleteEnclosureByIdAsync(Guid enclosureId, CancellationToken cancellationToken = default);

        public Task<int> GetFreeEnclosureCountAsync(CancellationToken cancellationToken = default);

        public Task<Enclosure> GetEnclosureByIdAsync(Guid enclosureId, CancellationToken cancellationToken = default);

        public Task RemoveAnimalFromEnclosureAsync(Guid animalId, Guid enclosureId, CancellationToken cancellationToken = default);

        public Task AddAnimalToEnclosureAsync(Guid animalId, Guid enclosureId, CancellationToken cancellationToken = default);

        public Task<List<Enclosure>> GetAllEnclosuresAsync(CancellationToken cancellationToken = default);
    }
}
