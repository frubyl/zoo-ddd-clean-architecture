
using KPO_HW2.Domain.Entities;
using KPO_HW2.Domain.Repositories;
using MediatR;

namespace KPO_HW2.Infrastructure.Repositories
{
    public class InMemoryAnimalRepository : IAnimalRepository
    {
        private readonly List<Animal> _animals = new List<Animal>();
        private readonly IServiceScopeFactory _scopeFactory;
        public async Task AddAnimalAsync(Animal animal, CancellationToken cancellationToken = default)
        {
            _animals.Add(animal);
        }

        public async Task DeleteAnimalByIdAsync(Guid animalId, CancellationToken cancellationToken = default)
        {
            var removedCount = _animals.RemoveAll(a => a.AnimalId == animalId);
            if (removedCount == 0)
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task ChangeIsHungryAsync(Guid animalId, bool isHungry, CancellationToken cancellationToken = default)
        {
            var animal = _animals.FirstOrDefault(a => a.AnimalId == animalId);
            animal.IsHungry = isHungry;
        }

        public async Task<int> GetAnimalsCountAsync(CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(_animals.Count);
        }

        public async Task<Animal> GetAnimalByIdAsync(Guid animalId, CancellationToken cancellationToken = default)
        {
            var animal = _animals.FirstOrDefault(a => a.AnimalId == animalId);
            if (animal == null)
            {
                throw new KeyNotFoundException();
            }
            return await Task.FromResult(_animals.FirstOrDefault(a => a.AnimalId == animalId));
        }

        public async Task UpdateAnimalEnclosureAsync(Animal animal, CancellationToken cancellationToken = default)
        {
            var existingAnimal = _animals.FirstOrDefault(a => a.AnimalId == animal.AnimalId);
            existingAnimal.EnclosureId = animal.EnclosureId;
            await DispatchEvents(existingAnimal);
        }

        private async Task DispatchEvents(Animal animal)
        {
            var events = animal.DomainEvents.ToList();
            animal.CLearDomainEvent();

            foreach (var @event in events)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    await mediator.Publish(@event);
                }
            }
        }

        public InMemoryAnimalRepository(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
    }
}
