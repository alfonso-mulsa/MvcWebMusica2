using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(ArtistasMetadata))]
    public partial class Artistas { }
    public class ArtistasMetadata
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public int? GenerosId { get; set; }

        public DateOnly? FechaDeNacimiento { get; set; }

        public int? CiudadesId { get; set; }

        public int? GruposId { get; set; }

        public virtual Ciudades? Ciudades { get; set; }

        public virtual Generos? Generos { get; set; }

        public virtual Grupos? Grupos { get; set; }

    }
}
