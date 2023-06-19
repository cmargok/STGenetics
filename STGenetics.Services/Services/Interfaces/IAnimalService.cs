using STGenetics.Application.Models.Animal;
using STGenetics.Domain.Entities;

namespace STGenetics.Application.Services.Interfaces
{
    public interface IAnimalService
    {
        public Task<int> AddAnimalAsync(AnimalDto newAnimal);
        public Task<bool> UpdateAnimalAsync(int Id, AnimalUpdate animal);
        public Task<bool> UpdateAnimalsStateAsync(List<int> Ids, bool status);
        public Task<List<int>> CheckAnimalAvalaibilityAsync(List<int> Ids);
        public Task<bool> DeleteAnimalAsync(int Id);
        public Task<AnimalsFilteredDto> GetAnimalsFilteredAsync(int? AnimalId, string? Name, string? Sex, bool? Status, int? PageSize, int? Page);

    }
}
