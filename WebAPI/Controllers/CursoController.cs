using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Cursos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/cursos")]
    public class CursoController : ControllerBase
    {
        private readonly IMediator mediator;
        public CursoController(IMediator _mediator)
        {
            mediator = _mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Curso>>> GetCursos()
        {
            return await mediator.Send(new Consulta.ListaCursos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetCurso(int id)
        {
            return await mediator.Send(new ConsultaPorId.ConsultaRq { IdCurso = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> SaveCurso(NuevoCurso.NuevoCursoRq data)
        {
            return await mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> UpdateCurso(int id, EditarCurso.EditarCursoRq data)
        {
            data.CursoId = id;
            return await mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteCurso(int id)
        {
            return await mediator.Send( new EliminarCurso.EliminarCursoRq(){CursoId = id});
        }
    }
}