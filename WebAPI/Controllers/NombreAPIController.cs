using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NombreAPIController : ControllerBase
{
    private readonly ILogger<NombreAPIController> _logger;

    public NombreAPIController (ILogger<NombreAPIController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<string> ObtenerNombreAPI ()
    {
        return Ok("Sistema Gestión DS");
    }
}
