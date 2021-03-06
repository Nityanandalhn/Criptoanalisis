using Datos;
using Datos.Dtos;
using Datos.Entidades;
using Datos.Repositorios;
using Negocio.Servicios;
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

        [HttpGet("EndpointsPorUsuario/{userId}")]
        public IActionResult GetEndpointPorUsuario(int userId)
        {
            try { return Ok(_service.GetAllEndpointsByUserId(userId)); }
            catch { return NotFound(); }
        }

        [HttpGet("Parametros")]
        public IActionResult GetParametro()
        {
            try { return Ok(_service.GetAllParametersWithEndpointInfo()); }
            catch { return NotFound(); }
        }

        [HttpGet("Intercambios")]
        public IActionResult GetIntercambio()
        {
            try { return Ok(_service.GetAllIntercambiosWithEndpointInfo()); }
            catch { return NotFound(); }
        }

        [HttpPost("Intercambios/{limit}")]
        public IActionResult GetIntercambio(int limit, [FromBody] string url = "")
        {
            try { return Ok(_service.GetLastNIntercambios(limit, url)); }
            catch { return NotFound(); }
        }

        [HttpPost("Intercambios/{limit}/{par}")]
        public IActionResult GetIntercambioPorPar(int limit, string par, [FromBody] string url = "")
        {
            try { return Ok(_service.GetLastNIntercambios(limit, url, par)); }
            catch { return NotFound(); }
        }

        [HttpGet("Usuarios")]
        public IActionResult GetUsuarios()
        {
            try { return Ok(_service.GetAllUsuariosWithIntercambioInfo()); }
            catch { return NotFound(); }
        }
        
        [HttpGet("Monedas")]
        public IActionResult GetMonedas()
        {
            try { return Ok(_service.GetAllMonedas()); }
            catch { return NotFound(); }
        }

        [HttpPost("NuevoEndpoint")]
        public IActionResult PostEndpoint([FromBody] EndpointCreateDto dto)
        {
            try { return Created(nameof(GetEndpoint), _service.CreateEndpoint(dto)); }
            catch (ApplicationException) { return Conflict(); }
            catch { return BadRequest(); }
        }

        [HttpPost("NuevoEndpoint/{userId}")]
        public IActionResult PostEndpoint([FromBody] EndpointCreateDto dto, int userId)
        {
            try { return Created(nameof(GetEndpoint), _service.CreateEndpointPorUsuario(dto, userId)); }
            catch (ApplicationException) { return Conflict(); }
            catch { return BadRequest(); }
        }

        [HttpPut("ActualizarEndpoint/{endpointId}")]
        public IActionResult PutEndpoint([FromBody] EndpointCreateDto dto, int endpointId)
        {
            try { return Accepted(nameof(GetParametro), _service.UpdateEndpoint(dto, endpointId)); }
            catch (ArgumentOutOfRangeException) { return NotFound(); }
            catch { return BadRequest(); }
        }

        [HttpPut("IncluirParametroEnEndpoint/{endpointId}")]
        public IActionResult Incluir([FromBody] ParametroDto parametro, int endpointId)
        {
            try { return Accepted(nameof(GetEndpoint), _service.IncluirParametroEnEndpoint(parametro, endpointId)); }
            catch (ArgumentOutOfRangeException) { return NotFound(); }
            catch { return BadRequest(); }
        }

        [HttpPost("NuevaMoneda")]
        public IActionResult PostMoneda([FromBody] MonedaDto dto)
        {
            try { return Created(nameof(GetEndpoint), _service.CreateMoneda(dto)); }
            catch { return BadRequest(); }
        }

        [HttpDelete("EliminarMoneda")]
        public IActionResult DeleteMoneda([FromBody] string nombre)
        {
            try { return Accepted(nameof(GetEndpoint), _service.DeleteMoneda(new MonedaDto()
            {
                Nombre = nombre
            })); }
            catch { return BadRequest(); }
        }

        [HttpDelete("EliminarEndpointPorUrl")]
        public IActionResult PostMoneda([FromBody] string url)
        {
            try { return Accepted(nameof(GetEndpoint), _service.DeleteEndpoint(url)); }
            catch (ApplicationException) { return NotFound($"No existe endpoint con esta url: {url}"); } 
            catch { return BadRequest(); }
        }
    }
}
