using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(GirasMetadata))]
    public partial class Giras { }
    public class GirasMetadata
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public int? GruposId { get; set; }

        public DateOnly? FechaInicio { get; set; }

        public DateOnly? FechaFin { get; set; }

        public virtual Grupos? Grupos { get; set; }
    }
}
