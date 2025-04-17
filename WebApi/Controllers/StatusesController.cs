using Business.Dtos;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions.Attributes;

namespace WebApi.Controllers;


//min goda vän Claude (ai) stod för all api dokumentation


/// <summary>
/// API controller for managing statuses
/// </summary>
[UseAdminApiKey]
[Produces("application/json")]
[Consumes("application/json")]
[Route("api/[controller]")]
[ApiController]
public class StatusesController : ControllerBase
{
    private readonly IStatusService _statusService;

    /// <summary>
    /// Initializes a new instance of the StatusesController
    /// </summary>
    /// <param name="statusService">The status service</param>
    public StatusesController(IStatusService statusService)
    {
        _statusService = statusService;
    }

    /// <summary>
    /// Creates a new status
    /// </summary>
    /// <param name="formData">The status information to create</param>
    /// <returns>The newly created status</returns>
    /// <response code="200">Returns the newly created status</response>
    /// <response code="400">If the status data is invalid</response>
    /// <response code="401">If the API key is missing or invalid</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create(AddStatusForm formData)
    {
        if (!ModelState.IsValid)
            return BadRequest(formData);
        var result = await _statusService.CreateStatusAsync(formData);
        return result != null ? Ok(result) : BadRequest();
    }

    /// <summary>
    /// Retrieves all statuses
    /// </summary>
    /// <returns>A collection of all statuses</returns>
    /// <response code="200">Returns the list of statuses</response>
    /// <response code="401">If the API key is missing or invalid</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _statusService.GetStatusesAsync();
        return Ok(result);
    }

    /// <summary>
    /// Retrieves a specific status by ID
    /// </summary>
    /// <param name="id">The ID of the status to retrieve</param>
    /// <returns>The requested status</returns>
    /// <response code="200">Returns the requested status</response>
    /// <response code="404">If the status is not found</response>
    /// <response code="401">If the API key is missing or invalid</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _statusService.GetStatusByIdAsync(id);
        return result != null ? Ok(result) : NotFound();
    }

    /// <summary>
    /// Updates an existing status
    /// </summary>
    /// <param name="formData">The updated status information</param>
    /// <returns>The updated status</returns>
    /// <response code="200">Returns the updated status</response>
    /// <response code="404">If the status is not found</response>
    /// <response code="401">If the API key is missing or invalid</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update(UpdateStatusForm formData)
    {
        var result = await _statusService.UpdateStatusAsync(formData);
        return result != null ? Ok(result) : NotFound();
    }

    /// <summary>
    /// Deletes a specific status
    /// </summary>
    /// <param name="id">The ID of the status to delete</param>
    /// <returns>No content if successful</returns>
    /// <response code="200">If the status was successfully deleted</response>
    /// <response code="404">If the status is not found</response>
    /// <response code="401">If the API key is missing or invalid</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _statusService.DeleteStatusAsync(id);
        return result ? Ok() : NotFound();
    }
}