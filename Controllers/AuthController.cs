using System.Security.Claims;
using HolaMundoJwt.Exceptions;
using HolaMundoJwt.Helpers;
using HolaMundoJwt.Models;
using Microsoft.AspNetCore.Mvc;

namespace HolaMundoJwt.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthServices _auth;
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger, IAuthServices auth)
    {
        _logger = logger;
        _auth = auth;
    }    

    [HttpPost]
    public IActionResult Login([FromBody] User user)
    {
        try
        {
            var jwt = _auth.Login(user);
            return Ok(jwt);
        }
        catch (UserNotFoundException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    
    [HttpGet("Admin")]
    [AuthorizeRoles(Rol.Admin)]
    public IActionResult AdminRol()
    {
        return Ok("Welcome Admin!");
    }

    [HttpGet("User")]
    [AuthorizeRoles(Rol.User)]
    public IActionResult UserRol()
    {
        return Ok("Welcome User!");
    }
    
    [HttpGet("Both")]
    [AuthorizeRoles(Rol.User, Rol.Admin)]
    public IActionResult AllRol()
    {
        string rol;

        List<Claim> lista = this.User.Claims.ToList();
        rol = lista.Where(x=> x.Type == "Rol").First().Value;

        return Ok($"Welcome {rol}!");
    }
}