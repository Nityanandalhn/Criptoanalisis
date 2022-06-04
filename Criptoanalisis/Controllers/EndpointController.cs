using Datos;
using Datos.Base;
using Datos.Dtos;
using Datos.Repositorios;
using Datos.Servicios;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

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
            try { return Ok(_service.GetAllEndpointsWithParameterInfo()); } 
            catch { return NotFound(); }
        }

        [HttpPost("NuevoEndpoint")]
        public IActionResult Post([FromBody] EndpointCreateDto dto)
        {
            try { return Created(nameof(Get), _service.Create(dto)); }
            catch { return BadRequest(); }
        }

        [HttpPut("ActualizarEndpoint{id}")]
        public IActionResult Put([FromBody] EndpointCreateDto dto, int id)
        {
            try { return Accepted(nameof(Get), _service.Update(dto, id)); }
            catch (ArgumentOutOfRangeException) { return NotFound(); }
            catch { return BadRequest(); }
        }
    }
}
