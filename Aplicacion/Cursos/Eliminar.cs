using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class EliminarCurso
    {
        public class EliminarCursoRq : IRequest
        {
            public int CursoId { get; set; }
        }

        public class Manejador : IRequestHandler<EliminarCursoRq, Unit>
        {
            private readonly CursosOnlineContext _context;
            public Manejador(CursosOnlineContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(EliminarCursoRq request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso.FindAsync(request.CursoId);
                if (curso == null)
                {
                   throw new ManejadorExcepcion(HttpStatusCode.NotFound, new {mensaje = "Curso no encontrado."});
                }
                _context.Curso.Remove(curso);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo actualizar el curso");
            }
        }
    }
}