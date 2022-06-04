using Datos;
using Datos.Base;
using Datos.Repositorios;
using Datos.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Criptoanalisis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EndpointController : ControllerBase
    {
        protected readonly ILogger<EndpointController> _logger;
        protected readonly EndpointService _service;
        public EndpointController(EndpointService service, ILogger<EndpointController> logger)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("Endpoints")]
        public IActionResult Get()
        {
            try { return Ok(_service.GetAllEndpointsWithParameterInfo()); } catch { return NotFound(); }
        }
    }
}
