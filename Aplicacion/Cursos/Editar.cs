using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class EditarCurso
    {
        public class EditarCursoRq : IRequest
        {
            public int CursoId { get; set; }
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime? FechaPublicacion { get; set; }
        }

         public class EditarCursoRqValidation : AbstractValidator<EditarCursoRq>
        {
            public EditarCursoRqValidation()
            {
                RuleFor(t => t.Titulo).NotEmpty().WithMessage("El campo título es obligatorio");
                RuleFor(t => t.Descripcion).NotEmpty().WithMessage("El campo descripción es obligatorio");
                RuleFor(t => t.FechaPublicacion).NotEmpty().WithMessage("El campo fecha publicación es obligatorio");
            }
        }

        public class Manejador : IRequestHandler<EditarCursoRq>
        {
            private readonly CursosOnlineContext context;
            public Manejador(CursosOnlineContext _context)
            {
                context = _context;
            }
            public async Task<Unit> Handle(EditarCursoRq request, CancellationToken cancellationToken)
            {
                var curso = await context.Curso.FindAsync(request.CursoId);
                if (curso == null)
                {
                   throw new ManejadorExcepcion(HttpStatusCode.NotFound, new {mensaje = "Curso no encontrado."});
                }
                curso.Titulo = request.Titulo ?? curso.Titulo;
                curso.Descripcion = request.Descripcion ?? curso.Descripcion;
                curso.FechaPublicacion = request.FechaPublicacion ?? curso.FechaPublicacion;
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo actualizar el curso");
            }
        }
    }
}