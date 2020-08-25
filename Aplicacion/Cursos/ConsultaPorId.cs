using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class ConsultaPorId
    {
        public class ConsultaRq : IRequest<Curso>
        {
            public int IdCurso { get; set; }            
        }

        public class Manejador : IRequestHandler<ConsultaRq, Curso>
        {
            private readonly CursosOnlineContext _context;
            public Manejador(CursosOnlineContext context)
            {
                _context = context;
            }
            public async Task<Curso> Handle(ConsultaRq request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso.FindAsync(request.IdCurso);
                if (curso == null)
                {
                   throw new ManejadorExcepcion(HttpStatusCode.NotFound, new {mensaje = "Curso no encontrado."});
                }
                return curso;
            }
        }

    }
}