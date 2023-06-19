using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STGenetics.Application.Models.Animal;
using STGenetics.Application.Ports;
using STGenetics.Application.Services.Interfaces;
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
   // [Authorize]
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
        [ProducesResponseType(typeof(ApiResponse<int>), 400)]
        [ProducesResponseType(typeof(Problem), 500)]
        [HttpPost]
        public async Task<IActionResult> AddAsync(AnimalDto newAnimal)
        {
             var AnimalId = await _animalService.AddAnimalAsync(newAnimal);

            var BadRresponse = Tools.CreateResponse("The Animal does not meet the basic validations.", Result.CannotBeCreated, 0);

             if (AnimalId == 0) return BadRequest(BadRresponse);

            var response = Tools.CreateResponse(AnimalId, Result.Success, 1);

            return Created("api/v1/Animals/"+ AnimalId,response);
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="AnimalId"></param>
        /// <param name="Name"></param>
        /// <param name="Sex"></param>
        /// <param name="Status"></param>
        /// <param name="PageSize"></param>
        /// <param name="Page"></param>
        /// <returns></returns>
        [HttpGet("aaa")]
        public async Task<IActionResult> AddAsync(int? AnimalId, string? Name, string? Sex, bool? Status, int? PageSize, int? Page)
        {
        

            return Ok();
        }
    }

  
}
