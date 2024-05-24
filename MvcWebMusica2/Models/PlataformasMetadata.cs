using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(PlataformasMetadata))]
    public partial class Plataformas { }
    public class PlataformasMetadata
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }
    }
}
