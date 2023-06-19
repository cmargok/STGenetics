using STGenetics.Application.Models.Animal;
using STGenetics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGenetics.Application.Ports
{
    public interface IAnimalRepository
    {
        /// <summary>
        /// Add an animal Into the Database 
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Id of the animal if animal was createdd successfully</returns>
        public Task<int> AddAnimalAsync(Animal animal, CancellationToken cancellationToken);

        /// <summary>
        /// Update an animal from the database
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="animal"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>True if the animal was updated successfully</returns>
        public Task<bool> UpdateAnimalAsync(int Id, Animal animal, CancellationToken cancellationToken);


        /// <summary>
        /// Change the state to active or to inactive when the animal is selected in the purchase
        /// </summary>
        /// <param name="Ids"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>true if all the animals have changed their state</returns>
        public Task<bool> UpdateAnimalsStateAsync(List<int> Ids, CancellationToken cancellationToken);


        /// <summary>
        /// Checks if the animals are free to get sell
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="animal"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A empty list if all the animals are active, or the ids for the animal who are not</returns>
        public Task<List<int>> CheckAnimalAvalaibilityAsync(List<int> Ids, CancellationToken cancellationToken);


        /// <summary>
        /// Delete an animal from the database
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>True if the animal was deleted successfully</returns>
        public Task<bool> DeleteAnimalAsync(int Id, CancellationToken cancellationToken);


        /// <summary>
        /// Filters the animal database by applying the filter chosen parameters
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A list with the animals filtered</returns>
        public Task<List<Animal>> FilterAnimalAsync(AnimalFilterDto filter, CancellationToken cancellationToken);



        public Task<List<Animal>> Getand();

        public Task<int> GetAnimalsQuantity(CancellationToken cancellationToken);


    }
}
