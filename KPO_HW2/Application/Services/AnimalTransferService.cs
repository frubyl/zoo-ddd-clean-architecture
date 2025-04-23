
using KPO_HW2.Domain.Repositories;
using KPO_HW2.Domain.Service;

namespace KPO_HW2.Application.Services
{
    public class AnimalTransferService : IAnimalTransferService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IEnclosureRepository _enclosureRepository;

        public async Task TransferAnimal(Guid animalId, Guid enclosureId)
        {
            var animal = await _animalRepository.GetAnimalByIdAsync(animalId)
                ?? throw new KeyNotFoundException("Животное не найдено");

            var enclosure = await _enclosureRepository.GetEnclosureByIdAsync(enclosureId)
                ?? throw new KeyNotFoundException("Вольер не найден");


            if (enclosure.CurrentAnimalCount >= enclosure.MaxCapacity)
            {
                throw new InvalidOperationException("Вольер заполнен");
            }
            if (animal.EnclosureId == enclosureId)
            {
                throw new InvalidOperationException("Животное уже в вольере");

            }
            if (animal.Species.Type != enclosure.Type)
            {
                throw new InvalidOperationException("Нужен другой тип вольера");

            }

            animal.MoveToEnclosure(enclosureId);
            await _animalRepository.UpdateAnimalEnclosureAsync(animal);

        }
        public AnimalTransferService(IAnimalRepository animalRepository, IEnclosureRepository enclosureRepository)
        {
            _animalRepository = animalRepository;
            _enclosureRepository = enclosureRepository;
        }
    }
}
