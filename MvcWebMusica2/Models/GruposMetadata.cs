using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(GruposMetadata))]
    public partial class Grupos { }
    public class GruposMetadata
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public bool grupo { get; set; }

        public DateOnly? FechaCreacion { get; set; }

        public int? CiudadesId { get; set; }

        public int? RepresentantesId { get; set; }

        public int? GenerosId { get; set; }

        public virtual Ciudades? Ciudades { get; set; }

        public virtual Generos? Generos { get; set; }

        public virtual Representantes? Representantes { get; set; }
    }
}
