using Microsoft.AspNetCore.Mvc;

namespace FaultHandling.RequestService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RequestController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public RequestController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    public async Task<ActionResult> Make()
    {
        const string RequestUri = "https://localhost:7042/api/response/25";

        var client = _httpClientFactory.CreateClient("Test");

        var response = await client.GetAsync(RequestUri);

        ////config this policy as global in DI Container with Test name HttpClient////
        //var response =
        //    await _clientPolicy.ImmediateHttpRetry.ExecuteAsync(() =>
        //        client.GetAsync(RequestUri));

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("--> ResponseService returned SUCCESS");
            return Ok();
        }

        Console.WriteLine("--> ResponseService returned FAILURE");

        return StatusCode(StatusCodes.Status500InternalServerError);
    }
}