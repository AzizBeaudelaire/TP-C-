using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<AddPost> Get()
    {
       
    }
}
