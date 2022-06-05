﻿using Datos.Dtos;
using Datos.Entidades;
using Datos.Interfaces;
using Datos.Mappers;
using Datos.Repositorios;
using System.Linq.Expressions;

namespace Negocio.Servicios
{
    public class ApiConfigurationService
    {
        protected readonly ILogger<ApiConfigurationService> _logger;
        protected readonly IEndpointsRepository _endpointRepo;
        protected readonly IParametroRepository _parametrosRepo;
        protected readonly IIntercambioRepository _intercambiosRepo;
        protected readonly IUsuarioRepository _usuarioRepo;
        protected readonly IMonedaRepository _monedaRepo;

        public ApiConfigurationService(ILogger<ApiConfigurationService> logger, 
            EndpointsRepository endpointRepo,
            ParametrosRepository parametrosRepo,
            IntercambioRepository monedasRepo,
            UsuarioRepository usuarioRepo,
            MonedaRepository monedaRepo)
        {
            _logger = logger;
            _endpointRepo = endpointRepo;
            _parametrosRepo = parametrosRepo;
            _intercambiosRepo = monedasRepo;
            _usuarioRepo = usuarioRepo;
            _monedaRepo = monedaRepo;
        }

        public IEnumerable<EndpointDto> GetAllEndpointsWithParameterInfo()
        {
            _logger.LogInformation("Buscando todos los endpoint");
            return _endpointRepo.Get().Select(x => EndpointMapper.FromEntity(x));
        }

        public List<ParametroDto> GetAllParametersWithEndpointInfo() =>
            _parametrosRepo.Get().Select(x => ParametroMapper.FromEntity(x)).ToList();

        public List<IntercambioDto> GetAllIntercambiosWithEndpointInfo() =>
            _intercambiosRepo.Get().Select(x => IntercambioMapper.FromEntity(x)).ToList();

        public List<UsuarioDto> GetAllUsuariosWithIntercambioInfo() =>
            _usuarioRepo.Get().Select(x => UsuarioMapper.FromEntity(x)).ToList();

        public List<Moneda> GetAllMonedas() =>
            _monedaRepo.Get().ToList();

        public List<Endpoints> GetEndpointBy(Expression<Func<Endpoints, bool>> predicado) => 
            _endpointRepo.GetBy(predicado).ToList();

        public Endpoints CreateEndpoint(EndpointCreateDto dto) =>
            _endpointRepo.Create(EndpointMapper.FromCreateDto(dto));

        public Parametro CreateParametro(ParametroCreateDto dto) =>
            _parametrosRepo.Create(ParametroMapper.FromCreateDto(dto));

        public Endpoints UpdateEndpoint(EndpointCreateDto dto, int id)
        {
            Endpoints edp = BuscarEndpointPorId(id);
            edp.Url = dto.Url ?? edp.Url;
            edp.Tipo = dto.Tipo ?? edp.Tipo;
            return _endpointRepo.Update(edp);
        }

        public Endpoints UpdateEndpoint(Endpoints edp)
        {
            return _endpointRepo.Update(edp);
        }

        public Endpoints DeleteEndpoint(EndpointDto dto) =>
            _endpointRepo.Delete(EndpointMapper.FromDto(dto));

        public EndpointDto IncluirParametroEnEndpoint(ParametroDto dto, int id)
        {
            Parametro param;
            try
            {
                param = BuscarParametroPorId(dto.Id);
            } 
            catch
            {
                param = CreateParametro(ParametroMapper.CreateDtoFromDto(dto));
            }
            _endpointRepo.IncluirParametro(BuscarEndpointPorId(id), param);
            return EndpointMapper.FromEntity(BuscarEndpointPorId(id));
        }

        public Endpoints BuscarEndpointPorId(int id) 
            => _endpointRepo.GetBy(x => x.Id == id).ToList()[0];

        public Parametro BuscarParametroPorId(int id)
            => _parametrosRepo.GetBy(x => x.Id == id).ToList()[0];

        public List<Endpoints> EndpointsConUsuariosActivos() => 
            _endpointRepo.GetAllEndpointInfo().Where(x => x.UsuariosActivos!.Count > 0).ToList();

        public Intercambio CreateIntercambio(IntercambioCreateDto intercambio) 
            => _intercambiosRepo.Create(IntercambioMapper.FromCreateDto(intercambio));

        public Intercambio CreateIntercambioDesdeProceso(Intercambio intercambio) => 
            _intercambiosRepo.CrearDesdeProceso(intercambio);

        public void Guardar() => _endpointRepo.GuardarCambios();
    }
}
