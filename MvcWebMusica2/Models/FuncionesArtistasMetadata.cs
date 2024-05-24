using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(FuncionesArtistasMetadata))]
    public partial class FuncionesArtistas { }
    public class FuncionesArtistasMetadata
    {
        public int Id { get; set; }

        public int? FuncionesId { get; set; }

        public int? ArtistasId { get; set; }

        public virtual Artistas? Artistas { get; set; }

        public virtual Funciones? Funciones { get; set; }

    }
}
