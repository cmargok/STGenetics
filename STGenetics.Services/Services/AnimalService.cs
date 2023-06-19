using STGenetics.Application.Models.Animal;
using STGenetics.Application.Ports;
using STGenetics.Application.Services.Interfaces;
using STGenetics.Domain.Entities;
using STGenetics.Domain.Tools;
using STGenetics.Domain.Tools.Validations;
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

        public async Task<int> AddAnimalAsync(AnimalDto newAnimal )
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

            var animalId = await _animalRepository.AddAnimalAsync(animal);

            return animalId;                
        }


        public async Task<bool> UpdateAnimalAsync(int Id, AnimalUpdate animal) {

            if (!AnimalValidation(animal) || Id <= 0) return false;

            var update = await _animalRepository.UpdateAnimalAsync(Id, animal);

            return update;

        }


        public async Task<bool> UpdateAnimalsStateAsync(List<int> Ids, bool status) {

            if (Ids.Any(i => i <= 0) ) return false;

            var update = await _animalRepository.UpdateAnimalsStateAsync(Ids, status);

            return update;
        }

        public  async Task<List<int>> CheckAnimalAvalaibilityAsync(List<int> Ids)
        {

            if (Ids.Any(i => i <= 0)) return default!;

            var result = await _animalRepository.CheckAnimalAvalaibilityAsync(Ids);

            return result;


        }


        public async Task<bool> DeleteAnimalAsync(int Id) {

            if (Id <= 0) return false;

            var result = await _animalRepository.DeleteAnimalAsync(Id);

            return result;

        }


        public async Task<AnimalsFilteredDto> GetAnimalsFilteredAsync(int? AnimalId, string? Name, string? Sex, bool? Status, int? PageSize, int? Page) 
        {
            var filter = new AnimalFilterDto
            {
                AnimalId = AnimalId,
                Name = Name,
                Sex = Sex,
                Status = Status,
            };

            if (Page is not null && Page > 0)
                filter.Page = Page;

            if (PageSize is not null && PageSize > 0)
                filter.PageSize = PageSize;


            //get the animal Couunt
            var countTask = _animalRepository.GetAnimalsQuantity();

            //Inits the filtered search
            var filteredAnimalsTask = _animalRepository.FilterAnimalAsync(filter);

            await Task.WhenAll(countTask, filteredAnimalsTask);

            var count = await countTask;
            var animals = await filteredAnimalsTask;

            return new AnimalsFilteredDto { 
                Animals = animals.Select(o => new AnimalDto
                {
                    AnimalId = o.AnimalId,
                    Name = o.Name,
                    Sex = o.Sex,
                    Status = o.Status,
                    BirthDate = o.BirthDate,
                    Breed = o.Breed,
                    Price = o.Price,

                }).ToList(),
                Page = (int)filter.Page!,
                Quantity = count            
            };

        }






        #region
        private static bool AnimalValidation<T>(T Animal) where T : IAnimalValidation 
        {
            Animal.ThrowIfNull();   

            if (Animal.BirthDate.Date.Year < 1990) return false;

            if (Animal.Price < 0) return false;

            return true;
        }



        #endregion
    }
}
