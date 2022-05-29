using Datos;
using Microsoft.AspNetCore.Mvc;

namespace Criptoanalisis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigApisController : CrudController<Apis, ApiConfigsContext>
    {
        public ConfigApisController(IRepoBase<Apis, ApiConfigsContext> repo, ILogger<ConfigApisController> logger) : base(repo, logger)
        {
        }

        [HttpGet("EndpointsConocidosTipoGET")]
        public IActionResult BuscarGET()
        {
            _logger.LogInformation("Buscando métodos GET");
            try { return Ok(_repo.GetBy(x => x.Metodo == Metodos.GET)); } catch { return NotFound(); }
        }
    }
}
