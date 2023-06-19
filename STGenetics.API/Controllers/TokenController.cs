using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STGenetics.Application.Security;
using STGenetics.Domain.Tools.ApiResponses;

namespace STGenetics.API.Controllers
{
    /// <summary>
    /// Token Controller Class
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]

    [AllowAnonymous]
    public class TokenController : ControllerBase
    {

        private readonly ITokenService _tokenService;

        /// <summary>
        /// Token Controller Constructor
        /// </summary>
        /// <param name="tokenService"></param>
        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }


        /// <summary>
        /// Method to obtain the token for use in the API
        /// </summary>
        /// <returns>Token</returns>
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(Problem), 500)]
        [HttpPost]
        public async Task<IActionResult> GenerateToken(User user)
        {
            if(_tokenService.ValidateUser(user)) return Unauthorized();

            var token = await _tokenService.GetToken();

            return new JsonResult(token);

        }
    }
}
