using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Response;
using Application.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CrudApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _userRepository;

    public UserController(ILogger<UserController> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
       
    }
    
    /// <summary>
    /// This endpoint get all users 
    /// </summary>
    /// <returns></returns>
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(List<UserResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting getting user");
      
        var users = await _userRepository.GetAllUsersAsync(cancellationToken);

        if (users.Count == 0)
        {
            _logger.LogInformation("Users not found");
            return NotFound();
        }
        
        _logger.LogInformation("Successfully getting user total {@total}", users.Count);
        
        return Ok(users);
    }

    /// <summary>
    /// This endpoint get a given user by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting getting user");
        
        var user = await _userRepository.GetUserByIdAsync(id, cancellationToken);
    
        if (user == null)
        {
            _logger.LogInformation("User not found");
            return NotFound();
        }
        
        _logger.LogInformation("Successfully getting user {@user}", user);
        
        return Ok(user);
    }
}