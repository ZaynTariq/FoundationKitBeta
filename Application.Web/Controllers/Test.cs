namespace FoundationKit.Web.Controllers;

using FoundationKit.Authentication.Persistence.DataAccess.ReadRepositories;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("/[controller]/[action]")]
public class TestController : ControllerBase
{
    private readonly IReadRepository _readRepository;

    public TestController(IReadRepository readRepository)
    {
        _readRepository = readRepository;
    }
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}
