using Business.Dtos;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

//min goda vän Claude (ai) stod för all api dokumentation


/// <summary>
/// API controller for managing projects
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    /// <summary>
    /// Initializes a new instance of the ProjectsController
    /// </summary>
    /// <param name="projectService">The project service</param>
    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    /// <summary>
    /// Creates a new project
    /// </summary>
    /// <param name="formData">The project information to create</param>
    /// <returns>The newly created project</returns>
    /// <response code="200">Returns the newly created project</response>
    /// <response code="400">If the project data is invalid</response>
    /// <response code="401">If the API key is missing or invalid</response>
    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromForm] AddProjectForm formData)
    {
        if (!ModelState.IsValid)
            return BadRequest(formData);
        var result = await _projectService.CreateProjectAsync(formData);
        return result != null ? Ok(result) : BadRequest();
    }

    /// <summary>
    /// Retrieves all projects
    /// </summary>
    /// <returns>A collection of all projects</returns>
    /// <response code="200">Returns the list of projects</response>
    /// <response code="401">If the API key is missing or invalid</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _projectService.GetProjectsAsync();
        return Ok(result);
    }



    [HttpGet]
    [Route("GetAllWithExpressions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAllWithExpressions([FromQuery] string sortOrder)
    {
        var result = await _projectService.GetProjectsAsync(sortOrder);
        return Ok(result);
    }

    /// <summary>
    /// Retrieves a specific project by ID
    /// </summary>
    /// <param name="id">The ID of the project to retrieve</param>
    /// <returns>The requested project</returns>
    /// <response code="200">Returns the requested project</response>
    /// <response code="404">If the project is not found</response>
    /// <response code="401">If the API key is missing or invalid</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Get(string id)
    {
        var result = await _projectService.GetProjectByIdAsync(id);
        return result != null ? Ok(result) : NotFound();
    }

    /// <summary>
    /// Updates an existing project
    /// </summary>
    /// <param name="formData">The updated project information</param>
    /// <returns>The updated project</returns>
    /// <response code="200">Returns the updated project</response>
    /// <response code="404">If the project is not found</response>
    /// <response code="401">If the API key is missing or invalid</response>
    [HttpPut]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update([FromForm] UpdateProjectForm formData)
    {
        var result = await _projectService.UpdateProjectAsync(formData);
        return result != null ? Ok(result) : NotFound();
    }

    /// <summary>
    /// Deletes a specific project
    /// </summary>
    /// <param name="id">The ID of the project to delete</param>
    /// <returns>No content if successful</returns>
    /// <response code="200">If the project was successfully deleted</response>
    /// <response code="404">If the project is not found</response>
    /// <response code="401">If the API key is missing or invalid</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _projectService.DeleteProjectAsync(id);
        return result ? Ok() : NotFound();
    }
}