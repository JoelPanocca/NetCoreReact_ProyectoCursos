using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio
{
    public class Precio
    {
        public Guid PrecioId { get; set; }
        [Column(TypeName = "decimal(16,2)")]
        public decimal PrecioActual { get; set; }
        [Column(TypeName = "decimal(16,2)")]
        public decimal Promocion { get; set; }
        public Guid CursoId { get; set; }
        public Curso Curso { get; set; }
    }
}