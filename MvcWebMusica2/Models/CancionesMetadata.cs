using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(CancionesMetadata))]
    public partial class Canciones { }
    public class CancionesMetadata
    {
        public int Id { get; set; }

        public string? Titulo { get; set; }

        public TimeOnly? Duracion { get; set; }

        public int? AlbumesId { get; set; }

        public bool Single { get; set; }

        public virtual Albumes? Albumes { get; set; }
    }
}
