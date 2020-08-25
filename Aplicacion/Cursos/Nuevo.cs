using System;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class NuevoCurso
    {
        public class NuevoCursoRq : IRequest
        {
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime? FechaPublicacion { get; set; }
        }

        public class NuevoCursoRqValidation : AbstractValidator<NuevoCursoRq>
        {
            public NuevoCursoRqValidation()
            {
                RuleFor(t => t.Titulo).NotEmpty().WithMessage("El campo título es obligatorio");
                RuleFor(t => t.Descripcion).NotEmpty().WithMessage("El campo descripción es obligatorio");
                RuleFor(t => t.FechaPublicacion).NotEmpty().WithMessage("El campo fecha publicación es obligatorio");
            }
        }

        public class Manejador : IRequestHandler<NuevoCursoRq>
        {
            private readonly CursosOnlineContext context;
            public Manejador(CursosOnlineContext _context)
            {
                context = _context;
            }

            public async Task<Unit> Handle(NuevoCursoRq request, CancellationToken cancellationToken)
            {
                var curso = new Curso
                {
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion
                };
                context.Curso.Add(curso);
                var valor = await context.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("Error de inserción del curso");
            }
        }
    }
}