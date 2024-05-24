using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(PaisesMetadata))]
    public partial class Paises { }
    public class PaisesMetadata
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }
    }
}
