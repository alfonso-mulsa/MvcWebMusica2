using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(FuncionesMetadata))]
    public partial class Funciones { }
    public class FuncionesMetadata
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }
    }
}
