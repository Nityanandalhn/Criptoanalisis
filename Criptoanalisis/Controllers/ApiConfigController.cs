using Datos;
using Datos.Base;
using Datos.Dtos;
using Datos.Entidades;
using Datos.Repositorios;
using Datos.Servicios;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Criptoanalisis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiConfigController : ControllerBase
    {
        protected readonly ILogger<ApiConfigController> _logger;
        protected readonly ApiConfigurationService _service;
        public ApiConfigController(ApiConfigurationService service, ILogger<ApiConfigController> logger)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("Endpoints")]
        public IActionResult GetEndpoint()
        {
            try { return Ok(_service.GetAllEndpointsWithParameterInfo()); } 
            catch { return NotFound(); }
        }

        [HttpPost("NuevoEndpoint")]
        public IActionResult PostEndpoint([FromBody] EndpointCreateDto dto)
        {
            try { return Created(nameof(GetEndpoint), _service.CreateEndpoint(dto)); }
            catch { return BadRequest(); }
        }

        [HttpPut("ActualizarEndpoint{id}")]
        public IActionResult PutEndpoint([FromBody] EndpointCreateDto dto, int id)
        {
            try { return Accepted(nameof(GetEndpoint), _service.UpdateEndpoint(dto, id)); }
            catch (ArgumentOutOfRangeException) { return NotFound(); }
            catch { return BadRequest(); }
        }

        [HttpPut("IncluirParametroEnEndpoint/{id}")]
        public IActionResult Incluir([FromBody] Parametros parametro, int id)
        {
            try { return Accepted(nameof(GetEndpoint), _service.IncluirParametroEnEndpoint(parametro, id)); }
            catch (ArgumentOutOfRangeException) { return NotFound(); }
            catch { return BadRequest(); }
        }
    }
}
