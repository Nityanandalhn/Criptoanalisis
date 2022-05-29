using Datos;
using Microsoft.AspNetCore.Mvc;

namespace Criptoanalisis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EndpointController : CrudController<Datos.Entidades.Endpoint, EndpointParametrosContext>
    {
        public EndpointController(IRepoBase<Datos.Entidades.Endpoint, EndpointParametrosContext> repo, ILogger<EndpointController> logger) : base(repo, logger)
        {
        }

        [HttpGet("Por id")]
        public IActionResult BuscarGET(int id)
        {
            _logger.LogInformation($"Buscando por id {id}");
            try { return Ok(_repo.GetBy(x => x.Id == id)); } catch { return NotFound(); }
        }
    }
}
