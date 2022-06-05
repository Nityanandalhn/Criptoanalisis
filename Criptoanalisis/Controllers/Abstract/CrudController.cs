using Datos.Base;
using Microsoft.AspNetCore.Mvc;

namespace Criptoanalisis.Controllers.Abstract
{
    //WIP - Generificar crud 
    public abstract class CrudController<Entity, Context> : ControllerBase where Entity : class where Context : class
    {
        protected readonly IRepoBase<Entity, Context> _repo;
        protected readonly ILogger<CrudController<Entity, Context>> _logger;
        public CrudController(IRepoBase<Entity, Context> repo, ILogger<CrudController<Entity, Context>> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public virtual IActionResult Get() => Ok(_repo.Get());

        [HttpGet("{pos}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual IActionResult Get(int pos)
        {
            try { return Ok(_repo.GetAtPos(pos)); }
            catch { return NotFound(); }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public virtual IActionResult Post([FromBody] Entity elemento)
        {
            try { return Created(nameof(Get), _repo.Create(elemento)); }
            catch { return BadRequest(); }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual IActionResult Put([FromBody] Entity elemento)
        {
            try { return Accepted(_repo.Update(elemento)); }
            catch { return NotFound(); }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete([FromBody] Entity elemento)
        {
            try { return Ok(_repo.Delete(elemento)); }
            catch { return NotFound(); }
        }
    }
}
