using Microsoft.AspNetCore.Mvc;

namespace FaultHandling.ResponseService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResponseController : ControllerBase
{
    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        var randomNumber = new Random().Next(1, 101);

        if (randomNumber >= id)
        {
            Console.WriteLine("--> Failure - Generate a HTTP 500");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        Console.WriteLine("--> Success - Generate a HTTP 200");

        return Ok();
    }
}