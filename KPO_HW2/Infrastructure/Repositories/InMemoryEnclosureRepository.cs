
using KPO_HW2.Domain.Entities;
using KPO_HW2.Domain.Repositories;

namespace KPO_HW2.Infrastructure.Repositories
{
    public class InMemoryEnclosureRepository : IEnclosureRepository
    {
        private readonly List<Enclosure> _enclosures = new List<Enclosure>();
        public async Task AddEnclosureAsync(Enclosure enclosure, CancellationToken cancellationToken = default)
        {
            _enclosures.Add(enclosure);
        }

        public async Task DeleteEnclosureByIdAsync(Guid enclosureId, CancellationToken cancellationToken = default)
        {
            var removedCount = _enclosures.RemoveAll(e => e.EnclosureId == enclosureId);
            if (removedCount == 0)
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<int> GetFreeEnclosureCountAsync(CancellationToken cancellationToken = default)
        {
            return _enclosures.Count(e => e.CurrentAnimalCount == 0);
        }

        public async Task<Enclosure> GetEnclosureByIdAsync(Guid enclosureId, CancellationToken cancellationToken = default)
        {
            var enclosure = _enclosures.FirstOrDefault(e => e.EnclosureId == enclosureId);
            if (enclosure == null) {
                throw new KeyNotFoundException();
            }
            return enclosure;
        }

        public async Task RemoveAnimalFromEnclosureAsync(Guid animalId, Guid enclosureId, CancellationToken cancellationToken = default)
        {
            var enclosure = _enclosures.FirstOrDefault(e => e.EnclosureId == enclosureId);
            if (enclosure == null)
            {
                throw new KeyNotFoundException();
            }
            enclosure.RemoveAnimal(animalId);
        }

        public async Task AddAnimalToEnclosureAsync(Guid animalId, Guid enclosureId, CancellationToken cancellationToken = default)
        {
            var enclosure = _enclosures.FirstOrDefault(e => e.EnclosureId == enclosureId);
            if (enclosure == null)
            {
                throw new KeyNotFoundException();
            }
            enclosure.AddAnimal(animalId);
        }

        public async Task<List<Enclosure>> GetAllEnclosuresAsync(CancellationToken cancellationToken = default)
        {
            return _enclosures;
        }

        public InMemoryEnclosureRepository() { }
    }
}
