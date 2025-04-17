using Authentication.Dtos;
using Authentication.Services;
using Business.Dtos;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

//min goda vän Claude (ai) stod för all api dokumentation

namespace WebApi.Controllers;

/// <summary>
/// API controller for user authentication operations
/// </summary>
/// 
[Produces("application/json")]
[Consumes("application/json")]
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IAuthService _authService;

    /// <summary>
    /// Initializes a new instance of the UsersController
    /// </summary>
    /// <param name="authService">The authentication service</param>
    public UsersController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Registers a new user
    /// </summary>
    /// <param name="formData">The user registration information</param>
    /// <returns>The newly created user details with authentication token</returns>
    /// <response code="200">Returns the newly registered user information with token</response>
    /// <response code="400">If the registration data is invalid or email already exists</response>
    /// <response code="401">If the API key is missing or invalid</response>
    [HttpPost]
    [Route("signup")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody] SignUpForm formData)
    {
        if (!ModelState.IsValid)
            return BadRequest(formData);
        var result = await _authService.SignUpAsync(formData);
        return result != null ? Ok(result) : BadRequest();
    }

    /// <summary>
    /// Authenticates a user and provides an access token
    /// </summary>
    /// <param name="formData">The user credentials (email and password)</param>
    /// <returns>Authentication result with access token</returns>
    /// <response code="200">Returns authentication token and user information</response>
    /// <response code="400">If the credentials are invalid</response>
    /// <response code="401">If the API key is missing or invalid</response>
    [HttpPost]
    [Route("signin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> SignIn([FromBody] SignInForm formData)
    {
        if (!ModelState.IsValid)
            return BadRequest(formData);

        var result = await _authService.SignInAsync(formData.Email, formData.Password);
        return result != null ? Ok(result) : BadRequest();
    }
}