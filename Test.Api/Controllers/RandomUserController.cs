using Microsoft.AspNetCore.Mvc;

using Test.Api.Controllers.Urls;

using TestApp.Domain.Interfaces;
using TestApp.Domain.ViewModels.RandomUser;

namespace Test.Api.Controllers
{
    /// <summary>
    /// Random User Controller
    /// </summary>
    [Route(RandomUserControllerUrls.BaseController)]
    [ApiController]
    public class RandomUserController : BaseController
    {
        /// <summary>
        /// Random User Service
        /// </summary>
        private readonly IRandomUserService _randomUserService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="randomUserService"></param>
        public RandomUserController(IRandomUserService randomUserService)
        {
            _randomUserService = randomUserService;
        }

        /// <summary>
        /// Get Random User
        /// </summary>
        /// <returns>List a Random User</returns>
        [HttpGet(RandomUserControllerUrls.GetRandom)]
        [ProducesResponseType(typeof(RandomUserViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRandomUser()
        {
            return Ok(await _randomUserService.GetRandomUser());
        }

        /// <summary>
        /// Execute Hook
        /// </summary>
        /// <returns>Hook Id</returns>
        [HttpPost(RandomUserControllerUrls.ExecuteHook)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExecuteHook()
        {
            return Ok(await _randomUserService.ExecuteHook());
        }

        public 
    }
}
