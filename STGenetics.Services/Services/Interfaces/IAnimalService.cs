using STGenetics.Application.Models.Animal;
using STGenetics.Domain.Entities;

namespace STGenetics.Application.Services.Interfaces
{
    public interface IAnimalService
    {
        /// <summary>
        /// Adds a new animal to the database asynchronously and returns the ID of the newly added animal
        /// </summary>
        /// <param name="newAnimal"></param>
        /// <returns>ID of the newly added animal</returns>
        public Task<int> AddAnimalAsync(AnimalDto newAnimal);

        /// <summary>
        /// Updates an existing animal with the provided ID using the information from the AnimalUpdateDto object
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="animal"></param>
        /// <returns>Returns a boolean indicating whether the update was successful or not</returns>
        public Task<bool> UpdateAnimalAsync(int Id, AnimalUpdateDto animal);

        /// <summary>
        /// Updates the status (active or inactive) of multiple animals specified by their IDs.
        /// </summary>
        /// <param name="Ids"></param>
        /// <param name="status"></param>
        /// <returns>Returns a boolean indicating whether the update was successful or not</returns>
        public Task<bool> UpdateAnimalsStateAsync(List<int> Ids, bool status);

        /// <summary>
        ///  Retrieves information about multiple animals specified by their IDs
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns>Returns a list of Animal objects containing the requested animal information.</returns>
        public Task<List<Animal>> GetAnimalsInfoAsync(List<int> Ids);

        /// <summary>
        /// Deletes an animal with the specified ID from the database. 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Returns a boolean indicating whether the deletion was successful or not.</returns>
        public Task<bool> DeleteAnimalAsync(int Id);

        /// <summary>
        /// Retrieves a filtered list of animals based on the provided filters such as AnimalId, Name, Sex, Status, PageSize, and Page. Returns a AnimalsFilteredDto object containing the filtered animal data.
        /// </summary>
        /// <param name="AnimalId"></param>
        /// <param name="Name"></param>
        /// <param name="Sex"></param>
        /// <param name="Status"></param>
        /// <param name="PageSize"></param>
        /// <param name="Page"></param>
        /// <returns>List of animals filtered</returns>
        public Task<AnimalsFilteredDto> GetAnimalsFilteredAsync(int? AnimalId, string? Name, string? Sex, bool? Status, int? PageSize, int? Page);

    }
}
