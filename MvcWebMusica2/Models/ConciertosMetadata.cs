using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(ConciertosMetadata))]
    public partial class Conciertos { }
    public class ConciertosMetadata
    {
        public int Id { get; set; }

        public int? GirasId { get; set; }

        public DateOnly? Fecha { get; set; }

        public int? CiudadesId { get; set; }

        public string? Direccion { get; set; }

        public virtual Ciudades? Ciudades { get; set; }

        public virtual Giras? Giras { get; set; }
    }
}
