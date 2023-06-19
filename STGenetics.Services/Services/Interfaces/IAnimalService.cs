using STGenetics.Application.Models.Animal;

namespace STGenetics.Application.Services.Interfaces
{
    public interface IAnimalService
    {
        public Task<int> AddAnimalAsync(AnimalDto newAnimal, CancellationToken cancellationToken);

    }
}
