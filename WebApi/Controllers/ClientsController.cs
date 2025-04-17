using Business.Dtos;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Extensions.Attributes;

namespace WebApi.Controllers;

/// <summary>
/// API controller for managing clients
/// </summary>
/// 
//min goda vän Claude (ai) stod för all api dokumentation
[UseAdminApiKey]

[Produces("application/json")]
[Consumes("application/json")]
[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    /// <summary>
    /// Initializes a new instance of the ClientsController
    /// </summary>
    /// <param name="clientService">The client service</param>
    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    /// <summary>
    /// Creates a new client
    /// </summary>
    /// <param name="formData">The client information to create</param>
    /// <returns>The newly created client</returns>
    /// <response code="200">Returns the newly created client</response>
    /// <response code="400">If the client data is invalid</response>
    /// <response code="401">If the API key is missing or invalid</response>
    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Client>> Create(AddClientForm formData)
    {
        if (!ModelState.IsValid)
            return BadRequest(formData);
        var result = await _clientService.CreateClientAsync(formData);
        return result != null ? Ok(result) : BadRequest();
    }

    /// <summary>
    /// Retrieves all clients
    /// </summary>
    /// <returns>A collection of all clients</returns>
    /// <response code="200">Returns the list of clients</response>
    /// <response code="401">If the API key is missing or invalid</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Client>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<Client>>> GetAll()
    {
        var result = await _clientService.GetClientsAsync();
        return Ok(result);
    }

    /// <summary>
    /// Retrieves a specific client by ID
    /// </summary>
    /// <param name="id">The ID of the client to retrieve</param>
    /// <returns>The requested client</returns>
    /// <response code="200">Returns the requested client</response>
    /// <response code="404">If the client is not found</response>
    /// <response code="401">If the API key is missing or invalid</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Client>> Get(int id)
    {
        var result = await _clientService.GetClientByIdAsync(id);
        return result != null ? Ok(result) : NotFound();
    }

    /// <summary>
    /// Updates an existing client
    /// </summary>
    /// <param name="formData">The updated client information</param>
    /// <returns>The updated client</returns>
    /// <response code="200">Returns the updated client</response>
    /// <response code="404">If the client is not found</response>
    /// <response code="401">If the API key is missing or invalid</response>
    [HttpPut]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Client>> Update(UpdateClientForm formData)
    {
        var result = await _clientService.UpdateClientAsync(formData);
        return result != null ? Ok(result) : NotFound();
    }

    /// <summary>
    /// Deletes a specific client
    /// </summary>
    /// <param name="id">The ID of the client to delete</param>
    /// <returns>No content if successful</returns>
    /// <response code="200">If the client was successfully deleted</response>
    /// <response code="404">If the client is not found</response>
    /// <response code="401">If the API key is missing or invalid</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _clientService.DeleteClientAsync(id);
        return result ? Ok() : NotFound();
    }
}