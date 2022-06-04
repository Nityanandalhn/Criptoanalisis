using Datos;
using Datos.Base;
using Microsoft.AspNetCore.Mvc;

namespace Criptoanalisis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametrosController : CrudController<Datos.Entidades.Parametros, CriptoAnalisisContext>
    {
        public ParametrosController(IRepoBase<Datos.Entidades.Parametros, CriptoAnalisisContext> repo, ILogger<ParametrosController> logger) : base(repo, logger)
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
