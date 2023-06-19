using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STGenetics.Application.Models.Animal;
using STGenetics.Application.Ports;
using STGenetics.Application.Services.Interfaces;
using STGenetics.Domain.Tools;
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
        private IAnimalRepository y;


        /// <summary>
        /// Animal Constructor
        /// </summary>
        /// <param name="animalService"></param>
        public AnimalsController(IAnimalService animalService, IAnimalRepository animalRepository)
        {
            _animalService = animalService;
            y = animalRepository;
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
        [HttpPost]
        public async Task<IActionResult> AddAsync(AnimalDto newAnimal, CancellationToken cancellationToken)
        {
             var AnimalId = await _animalService.AddAnimalAsync(newAnimal, cancellationToken);

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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("aaa")]
        public async Task<IActionResult> AddAsync(int? AnimalId, string? Name, string? Sex, bool? Status, int? PageSize, int? Page, CancellationToken cancellationToken)
        {
            var filter = new AnimalFilterDto
            {
                AnimalId = AnimalId,
                Name = Name,
                Sex = Sex,
                Status = Status,
            };

            if (Page is not null)
                filter.Page = Page;

            if (PageSize is not null)
                filter.PageSize = PageSize;

            var countTask = y.GetAnimalsQuantity(cancellationToken);
            var filteredAnimalsTask = y.FilterAnimalAsync(filter, cancellationToken);

            await Task.WhenAll(countTask, filteredAnimalsTask);

            var count = await countTask;
            var animals = await filteredAnimalsTask;

            return Ok(new { Quantity = count, list = animals, page = filter.Page });
        }
    }
}
