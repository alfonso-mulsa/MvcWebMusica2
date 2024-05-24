using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(GenerosMetadata))]
    public partial class Generos { }
    public class GenerosMetadata
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }
    }
}
