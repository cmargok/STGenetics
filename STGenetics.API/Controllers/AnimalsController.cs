using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STGenetics.Application.Models.Animal;
using STGenetics.Application.Ports;
using STGenetics.Application.Services.Interfaces;
using STGenetics.Domain.Tools;

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
        /// <param name="cancellationToken"></param>
        /// <returns>Confirmation if the animal could be saved</returns>
        [ProducesResponseType(typeof(ApiResponse<int>), 201)]
        [ProducesResponseType(typeof(ApiResponse<int>), 400)]
        [ProducesResponseType(typeof(Problem), 500)]
        [HttpPost()]
        public async Task<IActionResult> AddAsync(AnimalDto newAnimal, CancellationToken cancellationToken)
        {
            var AnimalId = await _animalService.AddAnimalAsync(newAnimal, cancellationToken);

            var BadRresponse = Tools.CreateResponse("The Animal does not meet the basic validations.", Result.CannotBeCreated, 0);

             if (AnimalId == 0) return BadRequest(BadRresponse);

            var response = Tools.CreateResponse(AnimalId, Result.Success, 1);

            return Created("api/v1/Animals/"+ AnimalId,response);
        }
    }
}
