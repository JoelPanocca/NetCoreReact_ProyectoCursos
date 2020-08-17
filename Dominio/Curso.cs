using System.Collections.Generic; 
namespace Dominio
{
    public class Curso
    {
        public int CursoId { get; set; }    
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime FechaPublicacion { get; set; }
        public byte[] FotoPortada { get; set; }   
        public Precio Precio { get; set; }
        public ICollection<Comentario> Comentarios { get; set; }
        public ICollection<CursoInstructor> InstructoresLink { get; set; }
    }
}