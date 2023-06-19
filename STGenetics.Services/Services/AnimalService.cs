using STGenetics.Application.Models.Animal;
using STGenetics.Application.Ports;
using STGenetics.Application.Services.Interfaces;
using STGenetics.Domain.Entities;
using STGenetics.Domain.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGenetics.Application.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _animalRepository;
        public AnimalService(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<int> AddAnimalAsync(AnimalDto newAnimal, CancellationToken cancellationToken)
        {
           if(!AnimalValidation(newAnimal)) return 0;

            var animal = new Animal() {
                BirthDate = newAnimal.BirthDate,
                Name = newAnimal.Name,
                Breed = newAnimal.Breed,
                Price = newAnimal.Price,
                Sex = newAnimal.Sex,
                Status = newAnimal.Status,
            };

            var animalId = await _animalRepository.AddAnimalAsync(animal, cancellationToken);

            return animalId;                
        }



        private static bool AnimalValidation(AnimalDto Animal)
        {
            Animal.ThrowIfNull();   

            if (Animal.BirthDate.Date.Year < 1990) return false;

            if (Animal.Price < 0) return false;

            return true;
        }
    }
}
