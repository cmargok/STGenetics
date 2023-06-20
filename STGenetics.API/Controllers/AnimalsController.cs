using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STGenetics.Application.Models.Animal;
using STGenetics.Application.Ports;
using STGenetics.Application.Services.Interfaces;
using STGenetics.Domain.Entities;
using STGenetics.Domain.Tools;
using STGenetics.Domain.Tools.ApiResponses;
using STGenetics.Infrastructure.DataAccess;
using System.ComponentModel.DataAnnotations;

namespace STGenetics.API.Controllers
{

    /// <summary>
    /// Animal Class Controller
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalService _animalService;


        /// <summary>
        /// Animal Constructor
        /// </summary>
        /// <param name="animalService"></param>
        public AnimalsController(IAnimalService animalService)
        {
            _animalService = animalService;
        }



        /// <summary>
        /// Add an Animal into the System
        /// </summary>
        /// <param name="newAnimal"></param>
        /// <returns>Confirmation if the animal could be saved</returns>
        [ProducesResponseType(typeof(ApiResponse<int>), 201)]
        [ProducesResponseType(typeof(ApiResponse<string>), 400)]
        [ProducesResponseType(typeof(Problem), 500)]
        [HttpPost]
        public async Task<IActionResult> AddAsync(AnimalDto newAnimal)
        {
            var AnimalId = await _animalService.AddAnimalAsync(newAnimal);

            if (AnimalId == 0) {

                var BadResponse = Tools.CreateResponse("The Animal does not meet the basic validations.", Result.CannotBeCreated, 0);
                return BadRequest(BadResponse);
            }

            var response = Tools.CreateResponse(AnimalId, Result.Success, 1);

            return Created("api/v1/Animals/" + AnimalId, response);
        }




        /// <summary>
        /// Update an animal by the fiven id and its information
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="animal"></param>
        /// <returns>Confirmation</returns>
        [ProducesResponseType(typeof(ApiResponse<int>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 404)]
        [ProducesResponseType(typeof(Problem), 500)]
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAsync(int Id, [FromBody] AnimalUpdateDto animal)
        {
            var isUpdated = await _animalService.UpdateAnimalAsync(Id, animal);

            if (!isUpdated)
            {
                var NotFoundRresponse = Tools.CreateResponse("The updating of the record could not be completed.", Result.BadRequest, 0);
                return NotFound(NotFoundRresponse);
            }

            var response = Tools.CreateResponse(isUpdated, Result.Success, 1);

            return Ok(response);
        }





        /// <summary>
        /// Delete an Animal from the database
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Confirmatio if the animal was deleted</returns>
        [ProducesResponseType(typeof(ApiResponse<int>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 404)]
        [ProducesResponseType(typeof(Problem), 500)]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(int Id)
        {
            var isDeleted = await _animalService.DeleteAnimalAsync(Id);

            if (!isDeleted)
            {
                var NotFoundRresponse = Tools.CreateResponse("The deletion of the record could not be completed. Id doesnt exist", Result.BadRequest, 0);
                return NotFound(NotFoundRresponse);
            }

            var response = Tools.CreateResponse(isDeleted, Result.Success, 1);
            return Ok(response);
        }


        /// <summary>
        /// Gets animals by the given filters
        /// </summary>
        /// <param name="AnimalId"></param>
        /// <param name="Name"></param>
        /// <param name="Sex"></param>
        /// <param name="Status"></param>
        /// <param name="PageSize"></param>
        /// <param name="Page"></param>
        /// <returns>animals by the given filters</returns>
        [ProducesResponseType(typeof(ApiResponse<AnimalFilterDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 400)]
        [ProducesResponseType(typeof(Problem), 500)]
        [HttpGet]
        public async Task<IActionResult> GetFilteredAsync(int? AnimalId, string? Name, string? Sex, bool? Status, int? PageSize, int? Page)
        {            
            var filteredRows = await _animalService.GetAnimalsFilteredAsync(AnimalId, Name, Sex, Status, PageSize, Page);

            if (filteredRows.Quantity == 0)
            {
                var  NoContentResponse= Tools.CreateResponse("The Animal database dont have any animal in or the database has a problem. call an admin.", Result.NoContent, 0);

                return Ok(NoContentResponse);
            }

            if (filteredRows.Animals.Count == 0)
            {
                var NoContentResponse = Tools.CreateResponse("No animal was found with the given parameters.", Result.NoContent, 0);

                return Ok(NoContentResponse);
            }

            var response = Tools.CreateResponse(filteredRows, Result.Success, filteredRows.Animals.Count);
            return Ok(response);
        }
    }

  
}
