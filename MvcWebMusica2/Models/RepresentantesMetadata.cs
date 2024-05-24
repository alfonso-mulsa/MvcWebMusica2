using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(RepresentantesMetadata))]
    public partial class Representantes { }
    public class RepresentantesMetadata
    {
        public int Id { get; set; }

        public string? NombreCompleto { get; set; }

        public DateOnly? FechaNacimiento { get; set; }

        public string? Identificacion { get; set; }

        public string? mail { get; set; }

        public string? Telefono { get; set; }

        public int? CiudadesID { get; set; }

        public virtual Ciudades? Ciudades { get; set; }
    }
}
