using Microsoft.AspNetCore.Mvc;

using Test.Api.Controllers.Urls;

using TestApp.Domain.Interfaces;
using TestApp.Domain.Requests;
using TestApp.Domain.ViewModels;

namespace Test.Api.Controllers
{
    /// <summary>
    /// Object Controller
    /// </summary>
    [Route(ObjectControllerUrls.BaseController)]
    [ApiController]
    public class ObjectController : BaseController
    {
        /// <summary>
        /// Object Service
        /// </summary>
        private readonly IObjectService _objectService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="objectService"></param>
        public ObjectController(IObjectService objectService)
        {
            _objectService = objectService;
        }

        /// <summary>
        /// Get All
        /// </summary>
        /// <returns>List of All Objects</returns>
        [HttpGet(ObjectControllerUrls.GetAll)]
        [ProducesResponseType(typeof(List<ObjectViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _objectService.GetAll());
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id">Id to Filter</param>
        /// <returns>Object filter by Id</returns>
        [HttpGet(ObjectControllerUrls.Get)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Invalid Id");
            }

            return Ok(await _objectService.GetById(id));
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Id of Object Created</returns>
        [HttpPost(ObjectControllerUrls.Create)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(CreateOrUpdateObjectRequest request)
        {
            return Ok(await _objectService.Create(request));
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        [HttpPut(ObjectControllerUrls.Update)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(string id, CreateOrUpdateObjectRequest request)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Invalid Id");
            }

            await _objectService.Update(id, request);

            return NoContent();
        }
    }
}
