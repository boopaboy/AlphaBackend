using Business.Dtos;
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using WebApi.Extensions.Attributes;

namespace WebApi.Controllers
{
    /// <summary>
    /// API controller for managing members
    /// </summary>
    /// 
    //min goda vän Claude (ai) stod för all api dokumentation
    [UseAdminApiKey]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMembersService _membersService;

        /// <summary>
        /// Initializes a new instance of the MembersController
        /// </summary>
        /// <param name="membersService">The members service</param>
        public MembersController(IMembersService membersService)
        {
            _membersService = membersService;
        }

        /// <summary>
        /// Creates a new member
        /// </summary>
        /// <param name="formData">The member information to create</param>
        /// <returns>The newly created member</returns>
        /// <response code="200">Returns the newly created member</response>
        /// <response code="400">If the member data is invalid or email already exists</response>
        /// <response code="401">If the API key is missing or invalid</response>
        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromForm] AddMemberForm formData)
        {
            if (!ModelState.IsValid)
                return BadRequest(formData);
            var result = await _membersService.CreateMemberAsync(formData);
            return result != null ? Ok(result) : BadRequest(new { message = "Email already exists" });
        }

        /// <summary>
        /// Retrieves all members
        /// </summary>
        /// <returns>A collection of all members</returns>
        /// <response code="200">Returns the list of members</response>
        /// <response code="401">If the API key is missing or invalid</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _membersService.GetAllMembersAsync();
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a specific member by ID
        /// </summary>
        /// <param name="id">The ID of the member to retrieve</param>
        /// <returns>The requested member</returns>
        /// <response code="200">Returns the requested member</response>
        /// <response code="404">If the member is not found</response>
        /// <response code="401">If the API key is missing or invalid</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _membersService.GetMemberByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        /// <summary>
        /// Updates an existing member
        /// </summary>
        /// <param name="formData">The updated member information</param>
        /// <returns>The updated member</returns>
        /// <response code="200">Returns the updated member</response>
        /// <response code="404">If the member is not found</response>
        /// <response code="401">If the API key is missing or invalid</response>
        [HttpPut]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromForm] UpdateMemberForm formData)
        {
            var result = await _membersService.UpdateMemberAsync(formData);
            return result != null ? Ok(result) : NotFound();
        }

        /// <summary>
        /// Deletes a specific member
        /// </summary>
        /// <param name="id">The ID of the member to delete</param>
        /// <returns>No content if successful</returns>
        /// <response code="200">If the member was successfully deleted</response>
        /// <response code="404">If the member is not found</response>
        /// <response code="401">If the API key is missing or invalid</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _membersService.DeleteMemberAsync(id);
            return result ? Ok() : NotFound();
        }
    }
}