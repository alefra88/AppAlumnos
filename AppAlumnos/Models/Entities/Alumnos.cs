using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppAlumnos.Models.Entities
{
    public partial class Alumnos
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaNacimiento { get; set; }
        public string Curp { get; set; } = null!;
        public decimal? SueldoMensual { get; set; }
        public short? IdEstadoOrigen { get; set; }
        public short? IdEstatus { get; set; }
    }
}
